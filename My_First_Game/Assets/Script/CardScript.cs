using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardScript : MonoBehaviour
{
    public CardState cardState;
    public CardPattern cardPattern;
    public GameManager gameManager;

    private bool coroutineAllowed, facedUp;

    Image Card;
    // Cards start in the unflipped state
    void Start()
    {
        Card = GetComponent<Image>();
        this.GetComponent<Button>().onClick.AddListener( () => { OnFlipCard(); });

        cardState = CardState.Unflipped;
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        coroutineAllowed = true;
        facedUp = false;
    }

    // When a card is clicked
    // If the card is already flipped or if two cards are already being compared, do nothing
    // Otherwise, flip the card and add it to the comparison list
    // Then, start comparing cards
    public void OnFlipCard()
    {
        if (cardState.Equals(CardState.Flipped))
        {
            return;
        }
        if (gameManager.ReadyToCompareCards)
        {
            return;
        }

        FlipCard();
        gameManager.AddCardInCardComparison(this);
        gameManager.CompareCardsInList();
    }

    void FlipCard()
    {
       cardState = CardState.Flipped;
       StartCoroutine(RotateCard());
    }

    public void UnFlipCard()
    {
        StartCoroutine(RotateCard());
    }

    private IEnumerator RotateCard()
    {
        coroutineAllowed = false;

        if (!facedUp)
        {
            for (float i = 0f; i <= 180f; i += 10f)
            {
                transform.rotation = Quaternion.Euler(0f, i, 0f);
                if (i == 90f)
                {
                    Debug.Log(" >> " + cardPattern);
                    Card.sprite = Resources.Load<Sprite>(cardPattern.ToString());
                }
                yield return new WaitForSeconds(0.01f);
            }
        }

        else if (facedUp)
        {
            for (float i = 180f; i >= 0f; i -= 10f)
            {
                transform.rotation = Quaternion.Euler(0f, i, 0f);
                if (i == 90f)
                {
                    Card.sprite = Resources.Load<Sprite>("Back");
                }
                yield return new WaitForSeconds(0.01f);
            }
        }

        coroutineAllowed = true;
        transform.rotation = Quaternion.Euler(0f, 0, 0f);
        facedUp = !facedUp;
    }










    public enum CardState { Unflipped, Flipped, Matched };
    public enum CardPattern
    {
        None, A, B, C, D, E, F, G, H, I, J, K, L, M, N, O
    }
}
