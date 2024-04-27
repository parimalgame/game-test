using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class GameManager : MonoBehaviour
{
    public GridLayoutGroup glp_game;


    [Header("Card Comparison List")]
    public List<CardScript> cardComparison;
    [Header("Card Type List")]
    public List<CardScript.CardPattern> cardsToBePutIn;
    public int matchedCardsCount = 0;

    public int PairCount;

    private void Awake()
    {
        OnGridManage();
    }

    void OnGridManage()
    {
        if(PlayerPrefs.GetInt("layout") == 0)
        {
            glp_game.cellSize = new Vector2(210, 315);
            glp_game.constraintCount = 2;
            PairCount = 3;
        }
        else if (PlayerPrefs.GetInt("layout") == 1)
        {
            glp_game.cellSize = new Vector2(210, 315);
            glp_game.constraintCount = 3;
            PairCount = 6;
        }
        else if (PlayerPrefs.GetInt("layout") == 2)
        {
            glp_game.cellSize = new Vector2(210, 315);
            glp_game.constraintCount = 4;
            PairCount = 10;
        }
        else if (PlayerPrefs.GetInt("layout") == 3)
        {
            glp_game.cellSize = new Vector2(175, 262);
            glp_game.constraintCount = 5;
            PairCount = 15;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        GenerateRandomCards();
    }

    void SetUpCardsToBePutIn()
    {
        Array array = Enum.GetValues(typeof(CardScript.CardPattern));
        List<CardScript.CardPattern> paire = new List<CardScript.CardPattern>();
        foreach (var item in array)
        {
            paire.Add((CardScript.CardPattern)item);
        }

        for(int i = 0; i<= PairCount; i++)
        {
            cardsToBePutIn.Add(paire[i]);
        }

        cardsToBePutIn.RemoveAt(0);
    }

    void GenerateRandomCards()
    {
        int positionIndex = 0;

        for (int i = 0; i < 2; i++)
        {
            SetUpCardsToBePutIn();
            int maxRandomNumber = cardsToBePutIn.Count;
            for (int j = 0; j < maxRandomNumber; maxRandomNumber--)
            {
                int randomNumber = UnityEngine.Random.Range(0, maxRandomNumber);
                AddNewCard(cardsToBePutIn[randomNumber]);
                cardsToBePutIn.RemoveAt(randomNumber);
                positionIndex++;
            }
        }
    }


    void AddNewCard(CardScript.CardPattern cardPattern)
    {
        GameObject card = Instantiate(Resources.Load<GameObject>("Card"));
        card.GetComponent<CardScript>().cardPattern = cardPattern;
        card.name = "Card_" + cardPattern.ToString();
        card.transform.SetParent(glp_game.transform);
      

    }


    public void AddCardInCardComparison(CardScript card)
    {
        SoundManager.instance.OnPlaySound(SoundManager.instance.flip);
        cardComparison.Add(card);
    }

    public bool ReadyToCompareCards
    {
        get
        {
            return cardComparison.Count == 2;
        }
    }

    public void CompareCardsInList()
    {
        if (ReadyToCompareCards)
        {
            Debug.Log("Two cards are ready for comparison.");
            if (cardComparison[0].cardPattern == cardComparison[1].cardPattern)
            {
                Debug.Log("Cards matched.");
                SoundManager.instance.OnPlaySound(SoundManager.instance.match);
                foreach (var card in cardComparison)
                {
                    card.cardState = CardScript.CardState.Matched;
                }
                ClearCardComparison();
                matchedCardsCount += 2;
                if (matchedCardsCount >= glp_game.transform.childCount)
                {
                    StartCoroutine(ReloadScene());
                }
            }
            else
            {
                Debug.Log("Cards do not match.");
                StartCoroutine(MissMatchCards());
            }
        }
    }

    void ClearCardComparison()
    {
        cardComparison.Clear();
    }

    void TurnBackCards()
    {
        SoundManager.instance.OnPlaySound(SoundManager.instance.fail);
        foreach (var card in cardComparison)
        {
            card.UnFlipCard();
            card.cardState = CardScript.CardState.Unflipped;
        }
    }

    IEnumerator MissMatchCards()
    {
       
        yield return new WaitForSeconds(1.5f);
        TurnBackCards();
        ClearCardComparison();
    }

    IEnumerator ReloadScene()
    {
        SoundManager.instance.OnPlaySound(SoundManager.instance.win);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(0);
    }










    // Update is called once per frame
    void Update()
    {
        
    }
}
