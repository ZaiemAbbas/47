using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public static Map Instance;

    //private int[][] themaps;
    public List<CountryHandler> handler_pack;
    private List<CountryHandler> storedCountries;


    //---
    public CountryHandler MapPick()
    {
        int r;
        CountryHandler ctr;
        r = Random.Range(0, handler_pack.Count-1);
        ctr=handler_pack[r];
        handler_pack.RemoveAt(r);
       

 //       GameplayManager.Instance.RemoveLands();
        //if (remainingLands <= 0)
        //    GameplayManager.Instance.phase = 2;
        return ctr;
    }
    public void PlayerMapPick(CountryHandler plr)
    {
        handler_pack.Remove(plr);
  //      GameplayManager.Instance.RemoveLands();
        //remainingLands--;
        //if (remainingLands == 0)
        //    GameplayManager.Instance.phase = 2;

    }
    void Awake()
    {
        Instance = this;
        storedCountries = new List<CountryHandler>();

        foreach (CountryHandler ch in handler_pack)
            storedCountries.Add(ch);
    }

    void Start()
    {
        DisableArmyCount();
    }

    //private void AddHandlers()
    //{
    //    foreach(CountryHandler c in )
    //    {
    //        handler_pack.Add(c);
    //    }
    //}
    //private void ZeroInStart()
    //{   
    //    // initailizing 2D Array with 0 in the start
    //    themaps = new int[42][];
    //    for(int j=0;j<42;j++)
    //    {
    //        for(int i=0;i<42;i++)
    //        {
    //            themaps[j][i] = 0;
    //        }
    //    }
    //}

    public void AssignMapValue(int a)
    {

    }

    public void DisableArmyCount()
    {
        foreach (CountryHandler obj in storedCountries)
        {
            obj.GetComponentInChildren<TMPro.TextMeshPro>().enabled = false;
        }
    }

    public void EnableArmyCount()
    {
        foreach (CountryHandler obj in storedCountries)
        {
            obj.GetComponentInChildren<TMPro.TextMeshPro>().enabled = true;
        }
    }
}
