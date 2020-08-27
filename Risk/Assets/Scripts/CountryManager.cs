using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountryManager : MonoBehaviour
{
    public static CountryManager instance;
    public List<GameObject> countrylist = new List<GameObject>();
    void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        AddCountryData();
    }
    void AddCountryData()
    {
        GameObject[] theArray = GameObject.FindGameObjectsWithTag("Country") as GameObject[];
        foreach(GameObject country in theArray)
        {
            countrylist.Add(country);
        }
        TintCountries();
    }

    void TintCountries()
    {
        for(int i=0;i<countrylist.Count;i++)
        {
            //if(countrylist[i].GetComponent<CountryHandler>())
            //{
            //    print(countrylist[i].name);
            //}

            CountryHandler countHandler = countrylist[i].GetComponent<CountryHandler>();
            if(countHandler.country.tribe == Country.theTribes.HariSingh)
            {
                countHandler.TintColor(new Color32(255, 0, 0, 128));
            }
            if (countHandler.country.tribe == Country.theTribes.SherKhan)
            {
                countHandler.TintColor(new Color32(0, 255, 0, 128));
            }
            if (countHandler.country.tribe == Country.theTribes.Cleopatra)
            {
                countHandler.TintColor(new Color32(0, 255, 255, 128));
            }
            if (countHandler.country.tribe == Country.theTribes.LionHeart)
            {
                countHandler.TintColor(new Color32(0, 0, 255, 128));
            }
            if (countHandler.country.tribe == Country.theTribes.none)
            {
                countHandler.TintColor(new Color32(255, 255, 255, 255));
            }
        }
    }
}