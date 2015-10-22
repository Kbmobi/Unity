using UnityEngine;
using System.Collections;

public class DebugDealer : MonoBehaviour 
{
    public CardStack dealer;
    public CardStack player;

    //int count = 0;
    //int[] cards = new int[] {9, 6, 12};

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 256, 28), "Hit Me!"))
        {
            player.Push(dealer.Pop());
        }

        //Test for known cards
        //if (GUI.Button(new Rect(10, 10, 256, 28), "Hit Me!"))
        //{
        //    player.Push(cards[count++]);
        //}
    }


}
