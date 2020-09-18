using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Handler : MonoBehaviour
{
    public static AI_Handler Instance;
    private int aiTurn=1;
    private CountryHandler ctr;
    public AILand[] aiLand;
    public int[] aiArmy;

    public GameObject particle;

    private int attackPhaseIndex = 1;

    [System.Serializable]
    public struct AILand
    {
        public List<CountryHandler> aiLandList;
    }

    void Awake()
    {
        Instance = this;
    }

    public void AITurn(int phase)
    {
        if (aiTurn >= 4)
        {
            aiTurn = 1;
            GameplayManager.Instance.isPlayerInput = true;
            GameplayManager.Instance.UpdateMessage($" Your turn");
            return;
        }
        else
            aiTurn++;

        if(GameplayManager.Instance.phase == 1)
        {
            ctr = Map.Instance.MapPick();

            ctr.AIInput(aiTurn, this);
            aiLand[aiTurn - 2].aiLandList.Add(ctr);

            if (particle)
            {
                particle.transform.localPosition = ctr.transform.localPosition;
                particle.SetActive(true);
                SoundManagerScript.SoundInstance.PlayBoom();
            }
        }

        if(GameplayManager.Instance.phase == 2)
        {
            if(IsArmyDeployed() == true)
            {
                GameplayManager.Instance.AIArmyDeployed();
                aiTurn = 0;
                return;
            }

            CountryHandler country = aiLand[aiTurn - 2].aiLandList[Random.Range(0, aiLand[aiTurn - 2].aiLandList.Count - 1)];
            country.AIInput(aiTurn, this);
            aiArmy[aiTurn - 2]--;

            if (particle)
            {
                particle.transform.localPosition = country.transform.localPosition;
                particle.SetActive(true);
                SoundManagerScript.SoundInstance.PlaySword();
            }
        }

        if(GameplayManager.Instance.phase == 3 && GameplayManager.Instance.isPlayerInput == false)
        {
            StartCoroutine(AttackPhaseRoutine());
        }
    }

    private IEnumerator AttackPhaseRoutine()
    {
        bool win = false;

        CountryHandler attackerCountry = GetRandomAICountry();
        attackerCountry.AIInput(aiTurn, this, 1);

        yield return new WaitForSeconds(3f);

        CountryHandler victimCountry = GetAjacentCountry();
        victimCountry.AIInput(aiTurn, this, 2);

        yield return new WaitForSeconds(3f);

        if (attackerCountry.country.army > victimCountry.country.army)
        {
 //           AnyPlayerLoseTerritory(attackerCountry, victimCountry);
            victimCountry.CurrentCountryCaptured(aiTurn);
            attackerCountry.CaptureCountrySuccessfull(attackerCountry.country.army);
            win = true;
        }
        else
        {
            if (victimCountry.country.army >= attackerCountry.country.army)
            {
  //              AnyPlayerLoseTerritory(victimCountry, attackerCountry);
                attackerCountry.CurrentCountryCaptured(victimCountry.country.playerID);
                victimCountry.CaptureCountrySuccessfull(victimCountry.country.army);
                win = false;
            }

        }

        yield return new WaitForSeconds(3f);

        if(win == true)
            GameplayManager.Instance.OnMoveArmy(true);

        yield return new WaitForSeconds(1f);

        AITurn(3);
    }

    private bool IsArmyDeployed()
    {
        for(int i=0;i<aiArmy.Length;i++)
        {
            if (aiArmy[i] > 0)
                return false;
        }

        return true;
    }

    public void AILoseTerritory(CountryHandler cr)
    {
        foreach(CountryHandler c in aiLand[cr.country.playerID - 2].aiLandList)
        {
            if (c.gameObject.name == cr.gameObject.name)
                aiLand[cr.country.playerID - 2].aiLandList.Remove(c);
        }
    }

    public void AnyPlayerLoseTerritory(CountryHandler gainer, CountryHandler loser)
    {
        CountryHandler c = aiLand[loser.country.playerID - 2].aiLandList.Find(obj => obj.name == loser.name);
        aiLand[loser.country.playerID - 2].aiLandList.Remove(c);
    }

    public CountryHandler GetRandomAICountry()
    {
        bool hasArmy = false;
        CountryHandler ctr = null;

        do
        {
            ctr = aiLand[aiTurn - 2].aiLandList[Random.Range(0, aiLand[aiTurn - 2].aiLandList.Count - 1)];

            if (ctr.country.army > 0)
                hasArmy = true;

        } while (hasArmy == false);

        print("Choosen Atlast " + ctr.name);

        return ctr;
    }

    public CountryHandler GetAjacentCountry()
    {
        CountryHandler attacker = GameplayManager.Instance.GetAttackerCountry();
        CountryHandler victim = null;

        if(attacker.country.Hamsay.Count > 0)
        {
            foreach(CountryHandler ctr in attacker.country.Hamsay)
            {
                if (attacker.country.playerID != ctr.country.playerID)
                {
                    victim = attacker.country.Hamsay[Random.Range(0, attacker.country.Hamsay.Count - 1)];
                }
            }
        }

        return victim;
    }
}
