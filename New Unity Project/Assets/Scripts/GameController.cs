using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour 
{
    int dealersFirstCard = -1;
	int dealersFirstCardValue = -1;
	bool dealerHasFaceDown;
    public CardStack player;
    public CardStack dealer;
    public CardStack deck;
	public Text playerTotal;
	public Text dealerTotal;
    public Button hitButton;
    public Button stayButton;
	private SpriteRenderer sp;
    /* Cards delt to each player
     * first player hits/stays/doubles/splits
     * dealer must have min of hard 17 before they can stay
     * dealer cards, first card is hidden, second card shown (OR first card show, 2nd card now drawn till after)
     */
    #region public methods
    public void Hit()
    {
        player.Push(deck.Pop());
		SetTotals();
        if (player.HandValue() > 21)
        {
            //TODO: Bust Player
            hitButton.interactable = false;
            stayButton.interactable = false;
        }
    }
    public void Stay()
    {
	   sp = new SpriteRenderer();
       hitButton.interactable = false;
       stayButton.interactable = false;
       
       CardStackView view = dealer.GetComponent<CardStackView>();
	   view.Toggle(dealersFirstCard, true);
	   view.ShowFirstCard();
	
       StartCoroutine(DealersTurn());
    }
    #endregion

    #region Unity messages
    void Start()
    {
        StartGame();
    }
    #endregion

    void StartGame()
    {
        hitButton.interactable = true;
        stayButton.interactable = true;

        for(int i = 0; i < 2; i++)
        {
            player.Push(deck.Pop());
			SetTotals();
            HitDealer();
        }
    }

	void SetTotals()
	{
		playerTotal.text = player.HandValue().ToString();
		if (dealerHasFaceDown) {
			dealerTotal.text = (dealer.HandValue() - dealersFirstCardValue).ToString();
		} else {
			dealerTotal.text = dealer.HandValue().ToString();
		}
	}

    void HitDealer()
    {
        int card = deck.Pop();

        if (dealersFirstCard < 0)
        {
            dealersFirstCard = card;
			dealerHasFaceDown = true;
        }

        dealer.Push(card);

		if (dealersFirstCardValue < 0) {
			dealersFirstCardValue = dealer.HandValue();
		}

		SetTotals();

        if (dealer.CardCount > 1)
        {
            CardStackView view = dealer.GetComponent<CardStackView>();
            view.Toggle(card, true);
        }
    }

    IEnumerator DealersTurn()
    {
		dealerHasFaceDown = false;
        while (dealer.HandValue() < 40)
        {
			HitDealer();
			SetTotals();
            yield return new WaitForSeconds(1f);
        }
    }
}
