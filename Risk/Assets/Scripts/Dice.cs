using System.Collections;
using UnityEngine;

public class Dice : MonoBehaviour {

    private Sprite[] diceSides;
    private SpriteRenderer rend;
    private string dice_color;
    public int n;
    

    public IEnumerator RollTheDice()
    {
        int randomDiceSide = 0;
        for (int i = 0; i <= 20; i++)
        {
            randomDiceSide = Random.Range(0, 6);
            rend.sprite = diceSides[randomDiceSide];
            yield return new WaitForSeconds(0.05f);
            n=randomDiceSide+1;
        }
        print(n+"");
     }

    public int RoleDice(int a)
    {
        if (a == 1)
            dice_color = "DiceBlack/";
        else
            dice_color = "DiceWhite/";
        rend = GetComponent<SpriteRenderer>();
        diceSides = Resources.LoadAll<Sprite>(dice_color);
        rend.sprite = diceSides[5];
        StartCoroutine("RollTheDice");
        return n;
    }
    private void OnMouseDown()
    {
        RoleDice(0);
    }
}
