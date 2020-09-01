using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    private int[][] themaps;

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
