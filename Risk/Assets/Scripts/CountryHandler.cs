using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(PolygonCollider2D))]
public class CountryHandler : MonoBehaviour
{
    public Country country;
    private SpriteRenderer sprite;
    public Color32 oldColor, startColor, hoverColor;
    private bool selected = false;
    //-----------------------------------------------------------------------//
    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    //    sprite.color = startColor;
    }

    public void AIInput(int AIIndex, AI_Handler ai)
    {
        if (GameplayManager.Instance.phase == 1 && selected == false)
        {
            /////------AI Handling Phase 1 (Distribution of Lands)
            Country countryObj = OnGetTribe(AIIndex);
            countryObj.playerID = AIIndex;
            AssignColor(countryObj);
            selected = true;
            GameplayManager.Instance.Invoke("AITurn", Random.Range(0.5f, 1f));
            GameplayManager.Instance.RemoveLands();
        }
        else if(GameplayManager.Instance.phase==2 )
        {
            /////------AI Handling Phase 2 (Distribution of army)
            if (GameplayManager.Instance.phase == 2)
            {
                country.army++;
                //-----------------army--()

                if (GetComponentInChildren<TextMeshPro>())
                    GetComponentInChildren<TextMeshPro>().text = country.army.ToString();

                GameplayManager.Instance.Invoke("AITurn", Random.Range(0.2f, 0.5f));
            }
        }

    }

    private void OnMouseDown()
    {
        //if (selected == true)
        //    return;

        if(GameplayManager.Instance.phase == 1 && selected == false && GameplayManager.Instance.isPlayerInput == true)
        {
            GameplayManager.Instance.isPlayerInput = false;
            Country countryObj = OnGetTribe(1);
            countryObj.playerID = 1;
            AssignColor(countryObj);
            selected = true;
            Map.Instance.PlayerMapPick(this);
            GameplayManager.Instance.Invoke("AITurn", Random.Range(0.2f, 0.5f));
            GameplayManager.Instance.RemoveLands();
            SoundManagerScript.SoundInstance.PlayBoom();
        }

        if (GameplayManager.Instance.phase == 2 && GameplayManager.Instance.isPlayerInput == true)
        {
            if (GameplayManager.Instance.GetCurrentPlayer() != country.playerID)
                return;

            country.army++;

     //       GameplayManager.Instance.Phase2Turn();

            if (GetComponentInChildren<TextMeshPro>())
                GetComponentInChildren<TextMeshPro>().text = country.army.ToString();

            GameplayManager.Instance.Invoke("AITurn", Random.Range(0.5f, 1.0f));
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
                break;
            case Country.theTribes.Cleopatra:
                sprite.color = Color.yellow;
                break;
            case Country.theTribes.LionHeart:
                sprite.color = Color.blue;
                break;
            case Country.theTribes.SherKhan:
                sprite.color = Color.green;
                break;
        }
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
