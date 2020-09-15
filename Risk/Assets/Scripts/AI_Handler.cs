using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Handler : MonoBehaviour
{
    private int aiTurn=1;
    private CountryHandler ctr;
    public AILand[] aiLand;
    public int[] aiArmy;

    public GameObject particle;

    [System.Serializable]
    public struct AILand
    {
        public List<CountryHandler> aiLandList;
    }

    public void AITurn(int phase)
    {
        if (aiTurn >= 4)
        {
            aiTurn = 1;
            GameplayManager.Instance.isPlayerInput = true;
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
}
