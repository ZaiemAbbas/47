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

    private int playerTurn = 0;
    private int remainingLands = 5;

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
    }

    public int PlayerTurn()
    {
        if (playerTurn >= 4)
            playerTurn = 1;
        else
            playerTurn++;

        playerTurnText.text = playerTurn.ToString();

        if (phase == 1)
        {
            if (remainingLands <= 0)
                phase = 2;
            else
                remainingLands--;
        }

        return playerTurn;
    }

    public int GetCurrentPlayer()
    {
        return playerTurn;
    }

    private void SetPlayerArmy()
    {
        for(int i=0;i<player.Length;i++)
        {
            player[i].armyCount = 13;
            player[i].armyText.text = player[i].armyCount.ToString();
        }
    }

    public int GetPlayerArmy()
    {
        return player[playerTurn - 1].armyCount;
    }
}
