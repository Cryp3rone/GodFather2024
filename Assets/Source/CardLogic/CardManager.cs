using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance { get; private set; }

    public List<CardData> cardDatas;

    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private GameObject questionBox;
    [SerializeField] private GameObject propAlienBox;
    [SerializeField] private GameObject propRobotBox;
    [SerializeField] private TextMeshProUGUI questionText;
    private bool isCardsDisplayed;

    private void Awake() => Instance = this;

    private void Start()
    {
        propAlienBox.SetActive(false);
        propRobotBox.SetActive(false);
        questionBox.SetActive(false);
        isCardsDisplayed = false;

        cardDatas = Resources.LoadAll<CardData>("Cards").ToList();

        foreach (CardData card in cardDatas)
        {
            Debug.Log(card.cardName);
        }
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            ShowPropositions(cardDatas[0]);
        }
    }

    public void ShowPropositions(CardData data)
    {
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
}
