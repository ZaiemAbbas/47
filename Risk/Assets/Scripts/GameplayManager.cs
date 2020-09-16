using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager Instance;
    public Player[] player;
    //public float zoomin, zoomout;

    [HideInInspector]
    public int phase = 1;
    public int playerArmy = 13;
    public bool isPlayerInput = false;

    private int playerTurn = 0;

    private int remainingLands = 42;

    private bool aiArmyDeployed = false;
    private int moveArmyCount = 0;

    private CountryHandler attackingCountry;
    private CountryHandler victimCountry;


    [System.Serializable]
    public struct Player
    {
        public int armyCount;
        public Text armyText;
    }

    [Header("UI")]
    public Text playerTurnText;
    public DiceUI diceUI;
    public GameObject movearmypannel;
    public TextMeshProUGUI moveArmyText;

    [System.Serializable]
    public struct DiceUI
    {
        public GameObject dicePanel;
        public GameObject[] diceButtons;
    }


    void Awake()
    {
        Instance = this;
        phase = 1;
        playerTurnText.text = "1";
        isPlayerInput = true;
        SoundManagerScript.SoundInstance.BackMusic();
    }
    void Update()
    {
        if(Input.touchCount==2)
        {
            Touch touchzero = Input.GetTouch(0);
            Touch touchone = Input.GetTouch(1);
            Vector2 touchzeroprev = touchzero.position - touchzero.deltaPosition;
            Vector2 touchoneprev = touchone.position - touchone.deltaPosition;
            float prevmag = (touchzeroprev - touchoneprev).magnitude;
            float curvmag = (touchzero.position - touchone.position).magnitude;
            ZoomIn((curvmag - prevmag) * 0.01f);

        }

    }

    public void Phase2Turn()
    {
        playerTurn = 1;
    }

    public int GetCurrentPlayer()
    {
        return 1;
    }

    public void RemoveLands()
    {
        if (remainingLands <= 0)
        {
            phase = 2;
            Map.Instance.EnableArmyCount();
            isPlayerInput = true;
        }
        else
            remainingLands--;
    }

    public void ReduceArmy()
    {
        playerArmy--;
        AttackPhaseInitialize();
    }

    //public int GetPlayerArmy()
    //{
    //    return player[playerTurn - 1].armyCount;
    //}

    public void AITurn()
    {
        if (remainingLands < 1 && phase == 1)
        {
            phase = 2;
            Map.Instance.EnableArmyCount();
            isPlayerInput = false;
        }

        GetComponent<AI_Handler>().AITurn(phase);
    }

    public void AttackPhaseInitialize()
    {
        if(playerArmy < 1 && aiArmyDeployed == true)
        {
            print("AttackPhaseInitialize");
            phase = 3;
        }
    }

    public void AIArmyDeployed()
    {
        aiArmyDeployed = true;
    }

    #region Attack Phase

    public void OnReadyForAttack()
    {
        diceUI.dicePanel.SetActive(true);

        for(int i=0; i < attackingCountry.country.army + 1; i++)
        {
            if (i > 3)
                break;

            diceUI.diceButtons[i].SetActive(true);
        }
    }

    public void SetAttackingCountryPair(CountryHandler attacker, CountryHandler victim)
    {
        attackingCountry = attacker;
        victimCountry = victim;
    }

    public CountryHandler GetAttackerCountry()
    {
        return attackingCountry;
    }

    public CountryHandler GetVictimCountry()
    {
        return victimCountry;
    }

    public void ClearAttackers()
    {
        attackingCountry = null;
        victimCountry = null;
    }

    public void OnSelectDice(int army)
    {
        if(army > victimCountry.country.army)
        {
            victimCountry.CurrentCountryCaptured(1);
            attackingCountry.CaptureCountrySuccessfull(army);
        }
        else
        {
            attackingCountry.CurrentCountryCaptured(victimCountry.country.playerID);
            victimCountry.CaptureCountrySuccessfull(army);
        }
    }

    #endregion Attack Phase

    #region Move Army Phase

    public void AddArmy()
    {
        moveArmyCount++;
        moveArmyText.text = moveArmyCount.ToString();
    }

    public void SubtractArmy()
    {
        if (moveArmyCount < 1)
            return;

        moveArmyCount--;
        moveArmyText.text = moveArmyCount.ToString();
    }

    #endregion Move Army Phase

    //-------ZOOM In Function-------
    public void ZoomIn(float inc)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - inc, 0, 1);

    }
}
