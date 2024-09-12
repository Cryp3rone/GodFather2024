using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.U2D;

public class MallManager : MonoBehaviour
{
    public static MallManager Instance { get; private set; }

    public event Action<Choice, int> OnQuestionAnswer;


    [SerializeField] private List<MallItem> _mallParts = new List<MallItem>();
    
    public int GoodPartsCount { get; private set; }
    public int GoodPartsToWin { get; private set; }

    private void Awake() => Instance = this;

    private void OnEnable()
    {
        OnQuestionAnswer += ShowPart;
    }

    private void OnDisable()
    {
        OnQuestionAnswer -= ShowPart;
    }

    private void ShowPart(Choice choiceData, int index)
    {
        foreach (MallItem item in _mallParts)
        {
            if(item.Index == index)
            {
                item.SpriteRenderer.sprite = choiceData.result;

                if(choiceData.isRight)
                {
                    GoodPartsCount++;
                    Debug.Log(GoodPartsCount);
                }
                return;
            }
        }
    }

    public void InvokeOnQuestionAnswer(Choice choiceData, int index)
        => OnQuestionAnswer?.Invoke(choiceData, index);
}
