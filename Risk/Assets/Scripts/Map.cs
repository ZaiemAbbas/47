using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public static Map Instance;

    private int[][] themaps;
    public List<CountryHandler> handler_pack;

    //---
    public CountryHandler MapPick()
    {
        int r;
        CountryHandler ctr;
        r = Random.Range(0, handler_pack.Count-1);
        ctr=handler_pack[r];
        handler_pack.RemoveAt(r);
        return ctr;
    }
    public void PlayerMapPick(CountryHandler plr)
    {
        handler_pack.Remove(plr);
    }
    void Awake()
    {
        Instance = this;
    }

    //private void AddHandlers()
    //{
    //    foreach(CountryHandler c in )
    //    {
    //        handler_pack.Add(c);
    //    }
    //}
    private void ZeroInStart()
    {   
        // initailizing 2D Array with 0 in the start
        themaps = new int[42][];
        for(int j=0;j<42;j++)
        {
            for(int i=0;i<42;i++)
            {
                themaps[j][i] = 0;
            }
        }
    }

    public void AssignMapValue(int a)
    {

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
