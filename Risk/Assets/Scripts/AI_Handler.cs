using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Handler : MonoBehaviour
{
    private int aiTurn=1;
    private CountryHandler ctr;
    void Start()
    {

    }

    public void AITurn()
    {
        if (aiTurn >= 4)
        {
            aiTurn = 1;
            return;
        }
        else
            aiTurn++;
        ctr=Map.Instance.MapPick();
        ctr.AIInput(aiTurn);
    }
}
