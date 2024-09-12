using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance { get; private set; }

    public List<CardData> cardDatas;

    public event Action<CardData> OnPropositionFinished;

    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private GameObject questionBox;
    [SerializeField] private GameObject propAlienBox;
    [SerializeField] private GameObject propRobotBox;
    [SerializeField] private TextMeshProUGUI questionText;
    private bool isCardsDisplayed;

    private int currentCardID;

    private void Awake() => Instance = this;

    private void Start()
    {
        propAlienBox.SetActive(false);
        propRobotBox.SetActive(false);
        questionBox.SetActive(false);
        isCardsDisplayed = false;

        cardDatas = Resources.LoadAll<CardData>("Cards").ToList();
        currentCardID = 0;

        foreach (CardData card in cardDatas)
        {
            Debug.Log(card.cardName);
        }
        
        ShowPropositions(cardDatas[currentCardID]);
    }

    private void OnEnable()
    {
        OnPropositionFinished += ShowPropositions;
    }

    private void OnDisable()
    {
        OnPropositionFinished -= ShowPropositions;
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            ShowPropositions(cardDatas[currentCardID]);
        }
    }

    public void ShowPropositions(CardData data)
    {
        Debug.Log("show proposition");

        propAlienBox.SetActive(true);
        propRobotBox.SetActive(true);
        questionBox.SetActive(true);
        isCardsDisplayed = true;

        questionText.text = data.cardQuestion;

        Choice robotChoice = data.goodChoice;
        Choice alienChoice = data.badChoice;

        if (data.isRobotGood)
        {
            robotChoice = data.goodChoice;
            alienChoice = data.badChoice;
        }
        else
        {
            robotChoice = data.badChoice;
            alienChoice = data.goodChoice;
        }

        propAlienBox.GetComponent<Card>().SetCard(data.baseId, alienChoice);
        propRobotBox.GetComponent<Card>().SetCard(data.baseId, robotChoice);
    }

    public void HidePropsoitions()
    {
        propAlienBox.SetActive(false);
        propRobotBox.SetActive(false);
        questionBox.SetActive(false);
        isCardsDisplayed = false;
    }

    public void InvokeOnFinishedProposition()
    {
        cardDatas.RemoveAt(currentCardID);

        if (cardDatas.Count > 0)
        {
            OnPropositionFinished?.Invoke(cardDatas[currentCardID]);
        }
        else
        {
            GameManager.Instance.CheckWinLose();
        }
    }
}
