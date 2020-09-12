using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager Instance;
    public Player[] player;

    [HideInInspector]
    public int phase = 1;

    [HideInInspector]
    public bool isPlayerInput = false;

    private int playerTurn = 0;

    private int remainingLands = 40;

    [System.Serializable]
    public struct Player
    {
        public int armyCount;
        public Text armyText;
    }

    [Header("UI")]
    public Text playerTurnText;


    void Awake()
    {
        Instance = this;
        phase = 1;
        playerTurnText.text = "1";
        isPlayerInput = true;
    }

    //public int Phase1Turn()
    //{
    //    playerTurn = 1;

    //    //if (playerTurn >= 4)
    //    //    playerTurn = 1;
    //    //else
    //    //    playerTurn++;

    //    playerTurnText.text = (playerTurn+1).ToString();

    //    if (phase == 1)
    //    {
    //        // Entring to phase 2
    //        if (remainingLands <= 0)
    //        {
    //            phase = 2;
    //            isPlayerInput = true;
    //            //  Function to assign total army to all players
    //            SetPlayerArmy();
    //        }

    //    }

    //    return playerTurn;
    //}

    public void Phase2Turn()
    {
        playerTurn = 1;
    }

    public int GetCurrentPlayer()
    {
        return playerTurn;
    }

    //  assign army
    private void SetPlayerArmy()
    {
        for(int i=0;i<player.Length;i++)
        {
            player[i].armyCount = 5;
            player[i].armyText.text = player[i].armyCount.ToString();
            
        }
    }

    public void RemoveLands()
    {
        if (remainingLands <= 0)
        {
            phase = 2;
            isPlayerInput = true;
        }
        else
            remainingLands--;
    }

    public int GetPlayerArmy()
    {
        return player[playerTurn - 1].armyCount;
    }

    public void AITurn()
    {
        GetComponent<AI_Handler>().AITurn(phase);
    }
}
