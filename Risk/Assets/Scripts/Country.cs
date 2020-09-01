using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]   //  to show on inspector
public class Country 
{
    public int M_ID;
    public string name;
    public List<CountryHandler> Hamsay;
    //public void AddButton()
    //{
    //    Hamsay.Add();
    //}
    public enum thecontinent
    {
        NorthAmerica,
        SouthAmerica,
        Asia,
        Europe,
        Africa,
        Australia
    }

    public enum theTribes
    {
        none,
        Cleopatra,
        SherKhan,
        HariSingh,
        LionHeart
    }


    public thecontinent continent;
    public theTribes tribe;
    public int moneyreward;
    public int expreward;
    public int army = 0;
    public int playerID = 0;
}
