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
    [HideInInspector]
    public bool isInAttackingState = false;
    [HideInInspector]
    public bool isInMovingState = false;
    private int moveArmyCount = 0;

    private CountryHandler attackingCountry;
    private CountryHandler victimCountry;

    private AI_Handler aiHandler;


    [System.Serializable]
    public struct Player
    {
        public int armyCount;
        public Text armyText;
    }

    [Header("UI")]
    public Text messageText;
    public Text playerTurnText;
    public DiceUI diceUI;
    public GameObject movearmypannel;
    public TextMeshProUGUI moveArmyText;
    public GameObject cancelBtn;
    public GameObject stopBtn;

    [System.Serializable]
    public struct DiceUI
    {
        public GameObject dicePanel;
        public GameObject[] diceButtons;
    }


    void Awake()
    {
        Instance = this;
        aiHandler = GetComponent<AI_Handler>();
        phase = 1;
        playerTurnText.text = "1";
        isPlayerInput = true;
        SoundManagerScript.SoundInstance.BackMusic();
    }

    void Start()
    {
        UpdateMessage($" Your turn to choose a country");
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

        aiHandler.AITurn(phase);
    }

    public void AttackPhaseInitialize()
    {
        if(playerArmy < 1 && aiArmyDeployed == true)
        {
            print("AttackPhaseInitialize");
            phase = 3;
            UpdateMessage($"Attacking Phase Started");
        }
    }

    public void AIArmyDeployed()
    {
        aiArmyDeployed = true;
    }

    public void UpdateMessage(string msg)
    {
        messageText.text = msg;
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

        UpdateMessage($" Your turn to start attack");
    }

    private void StartAIAttack()
    {
        cancelBtn.SetActive(false);
        stopBtn.SetActive(false);
        isPlayerInput = false;

        print("Start AI Attack");
        Invoke("AITurn", 2f);
    }

    public void SetAttackingCountryPair(CountryHandler attacker, CountryHandler victim, bool isAI = false)
    {
        if(victim == null)
        {
            UpdateMessage($"{attacker.country.tribe.ToString()} chose a territory to launch attack");
        }
        else
        {
            if (isAI == false)
                isInAttackingState = true;

            UpdateMessage($"{attacker.country.tribe.ToString()} is going to attack {victim.country.tribe.ToString()}");
        }

        if(isAI == false)
        {
            cancelBtn.SetActive(true);
            stopBtn.SetActive(true);
        }

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
        if(attackingCountry.country.army > victimCountry.country.army)
        {
            UpdateMessage($" Successfully captured {victimCountry.country.tribe.ToString()} territory");
            aiHandler.AILoseTerritory(victimCountry);
            victimCountry.CurrentCountryCaptured(1);
            attackingCountry.CaptureCountrySuccessfull(army);
            AllowMoveArmy();
            isInAttackingState = false;
        }
        else
        {
            if(victimCountry.country.army >= attackingCountry.country.army)
            {
                UpdateMessage($" {victimCountry.country.tribe.ToString()} has captured your territory");
                attackingCountry.CurrentCountryCaptured(victimCountry.country.playerID);
                victimCountry.CaptureCountrySuccessfull(army);
                isInAttackingState = false;

                attackingCountry = null;
                victimCountry = null;
                ResetDice();

                StartAIAttack();
            }

        }
    }

    public void OnCancelAttack()
    {
        if(attackingCountry)
            attackingCountry.CancelAttackHighlight();

        if(victimCountry)
            victimCountry.CancelAttackHighlight();

        attackingCountry = null;
        victimCountry = null;
        isInAttackingState = false;
        ResetDice();
        cancelBtn.SetActive(false);
    }

    public void OnStopAttack()
    {
        OnCancelAttack();
        StartAIAttack();
    }

    private void ResetDice()
    {
        diceUI.dicePanel.SetActive(false);

        foreach (GameObject btn in diceUI.diceButtons)
            btn.SetActive(false);
    }

    #endregion Attack Phase

    #region Move Army Phase

    public void AddArmy()
    {
        if (moveArmyCount >= attackingCountry.country.army)
            return;

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

    private void AllowMoveArmy()
    {
        isInMovingState = true;
        movearmypannel.SetActive(true);
        diceUI.dicePanel.SetActive(false);
        cancelBtn.SetActive(false);
        stopBtn.SetActive(false);
        UpdateMessage($" Ready to move army");
    }

    public void OnMoveArmy()
    {
        attackingCountry.SetArmy(attackingCountry.country.army - moveArmyCount);
        victimCountry.SetArmy(moveArmyCount);
        OnArmyMoved();
    }

    public void OnMoveArmy(bool isAI)
    {
        attackingCountry.SetArmy(attackingCountry.country.army - 1);
        victimCountry.SetArmy(1);
        OnArmyMoved(isAI);
    }

    private void OnArmyMoved(bool isAI = false)
    {
        if(isAI == false)
        {
            UpdateMessage($" {moveArmyCount} army moved to newly captured territory");
            movearmypannel.SetActive(false);
            attackingCountry = null;
            victimCountry = null;
            isInMovingState = false;
            moveArmyCount = 0;
            moveArmyText.text = moveArmyCount.ToString();

            ResetDice();
            cancelBtn.SetActive(false);
            stopBtn.SetActive(true);
        }
        else
        {
            attackingCountry = null;
            victimCountry = null;
        }
    }

    #endregion Move Army Phase

    //-------ZOOM In Function-------
    public void ZoomIn(float inc)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - inc, 0, 1);

    }
}
