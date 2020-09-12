using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Handler : MonoBehaviour
{
    private int aiTurn=1;
    private CountryHandler ctr;
    public AILand[] aiLand;

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

        if(phase == 1)
        {
            ctr = Map.Instance.MapPick();
            ctr.AIInput(aiTurn, this);
            aiLand[aiTurn - 2].aiLandList.Add(ctr);

            if (particle)
            {
                particle.transform.localPosition = ctr.transform.localPosition;
                particle.SetActive(true);
            }
        }

        if(phase == 2)
        {
            CountryHandler country = aiLand[aiTurn - 2].aiLandList[Random.Range(0, aiLand[aiTurn - 2].aiLandList.Count - 1)];
            country.AIInput(aiTurn, this);

            if (particle)
            {
                particle.transform.localPosition = country.transform.localPosition;
                particle.SetActive(true);
            }
        }
    }
}
