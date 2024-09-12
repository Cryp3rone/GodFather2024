using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tmp;
    [SerializeField] private Button button;

    private int baseId;
    private Choice data;

    public void Awake()
    {
        button = GetComponentInChildren<Button>();

        button.onClick.AddListener(SendChoice);
    }

    public void SetCard(int sendBaseId, Choice sendData)
    {
        baseId = sendBaseId;
        data = sendData;

        tmp.text = data.proposition;
    }

    public void SendChoice()
        => CardManager.Instance.UpdateChoice(data, baseId);
}
