using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusArmy : MonoBehaviour
{
    [HideInInspector]
    public int reward_army = 0;
    public AI_Handler aihandler;

    public GameplayManager game_manager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int ContinentArmy(GameplayManager.Player current_player)
    {
        //game_manager.GetComponent<AI_Handler>().aiLand[current_player];
        return reward_army;
    }
}
