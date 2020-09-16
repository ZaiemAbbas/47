using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

[RequireComponent(typeof(PolygonCollider2D))]
public class CountryHandler : MonoBehaviour
{
    public Country country;
    private SpriteRenderer sprite;
    public Color32 oldColor, startColor, hoverColor;
    private bool selected = false;
    private bool isHighlited = false;
    private Color32 highlightColor;
    private int attackStance = 0;
    private GameplayManager gameplayInstance;
    //-----------------------------------------------------------------------//
    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    //    sprite.color = startColor;
    }

    void Start()
    {
        gameplayInstance = GameplayManager.Instance;
    }

    public void AIInput(int AIIndex, AI_Handler ai)
    {
        if (gameplayInstance.phase == 1 && selected == false)
        {
            /////------AI Handling Phase 1 (Distribution of Lands)
            Country countryObj = OnGetTribe(AIIndex);
            countryObj.playerID = AIIndex;
            AssignColor(countryObj);
            selected = true;
            gameplayInstance.RemoveLands();
            gameplayInstance.Invoke("AITurn", Random.Range(0.01f, 0.02f));
        }
        else if(gameplayInstance.phase==2 )
        {
            /////------AI Handling Phase 2 (Distribution of army)
            if (gameplayInstance.phase == 2)
            {
                country.army++;
                //-----------------army--()

                if (GetComponentInChildren<TextMeshPro>())
                    GetComponentInChildren<TextMeshPro>().text = country.army.ToString();

                gameplayInstance.Invoke("AITurn", Random.Range(0.01f, 0.02f));
            }
        }

    }

    private void OnMouseDown()
    {
        //if (selected == true)
        //    return;

        if(gameplayInstance.phase == 1 && selected == false && gameplayInstance.isPlayerInput == true)
        {
            gameplayInstance.isPlayerInput = false;
            Country countryObj = OnGetTribe(1);
            countryObj.playerID = 1;
            AssignColor(countryObj);
            selected = true;
            Map.Instance.PlayerMapPick(this);
            gameplayInstance.Invoke("AITurn", Random.Range(0.2f, 0.5f));
            gameplayInstance.RemoveLands();
            SoundManagerScript.SoundInstance.PlayBoom();
        }

        if (gameplayInstance.phase == 2 && gameplayInstance.isPlayerInput == true)
        {
            if (gameplayInstance.GetCurrentPlayer() != country.playerID)
                return;

            country.army++;
            gameplayInstance.ReduceArmy();

            if (GetComponentInChildren<TextMeshPro>())
                GetComponentInChildren<TextMeshPro>().text = country.army.ToString();

            gameplayInstance.Invoke("AITurn", Random.Range(0.5f, 1.0f));
            SoundManagerScript.SoundInstance.PlaySword();
        }

        if(gameplayInstance.phase == 3 && gameplayInstance.isPlayerInput == true && !isHighlited)
        {
            if (gameplayInstance.GetCurrentPlayer() == country.playerID)
            {
                if (gameplayInstance.GetAttackerCountry() != null)
                    return;

                SelectHighlightCountry();
                isHighlited = true;
                gameplayInstance.SetAttackingCountryPair(this, null);
            }
            else
            {
                if(CheckAdjacent() == true && gameplayInstance.GetAttackerCountry().country.army > country.army)
                {
                    SelectHighlightCountry();
                    isHighlited = true;
                    gameplayInstance.SetAttackingCountryPair(gameplayInstance.GetAttackerCountry(), this);
                    gameplayInstance.OnReadyForAttack();
                }
            }

        }
    }

    private Country OnGetTribe(int turn)
    {
        country.playerID = turn;

        switch (turn)
        {
            case 1:
                country.tribe = Country.theTribes.HariSingh;
                break;
            case 2:
                country.tribe = Country.theTribes.Cleopatra;
                break;
            case 3:
                country.tribe = Country.theTribes.LionHeart;
                break;
            case 4:
                country.tribe = Country.theTribes.SherKhan;
                break;
        }

        return country;
    }

    private void AssignColor(Country countryObj)
    {
        switch (countryObj.tribe)
        {
            case Country.theTribes.HariSingh:
                sprite.color = Color.red;
                highlightColor = new Color32(255, 136, 122, 255);
                break;
            case Country.theTribes.Cleopatra:
                sprite.color = Color.yellow;
                highlightColor = new Color32(255, 253, 191, 255);
                break;
            case Country.theTribes.LionHeart:
                sprite.color = Color.blue;
                highlightColor = new Color32(162, 235, 250, 255);
                break;
            case Country.theTribes.SherKhan:
                sprite.color = Color.green;
                highlightColor = new Color32(181, 247, 161, 255);
                break;
        }
    }

    private void SelectHighlightCountry()
    {
        sprite.color = highlightColor;
    }

    private bool CheckAdjacent()
    {
        CountryHandler attacker = gameplayInstance.GetAttackerCountry();

        foreach(CountryHandler ctr in country.Hamsay)
        {
            if(attacker.name == ctr.name)
            {
                return true;
            }
        }

        return false;
    }

    public void CaptureCountrySuccessfull(int attackerArmy)
    {
        country.army -= attackerArmy;

        if (GetComponentInChildren<TextMeshPro>())
            GetComponentInChildren<TextMeshPro>().text = country.army.ToString();

        Country countryObj = OnGetTribe(country.playerID);
        AssignColor(countryObj);
    }

    public void CurrentCountryCaptured(int id)
    {
        country.playerID = id;
        Country countryObj = OnGetTribe(id);
        AssignColor(countryObj);

        country.army = 0;

        if (GetComponentInChildren<TextMeshPro>())
            GetComponentInChildren<TextMeshPro>().text = country.army.ToString();
    }

    //void OnDrawGizmos()
    //{
    //    country.name = this.name;
    //    this.tag = "Country";
    //}
    public void TintColor(Color32 c)
    {
        sprite.color = c;
    }

}
