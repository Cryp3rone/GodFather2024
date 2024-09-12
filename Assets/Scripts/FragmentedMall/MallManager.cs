using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.U2D;

public class MallManager : MonoBehaviour
{
    public static MallManager Instance { get; private set; }

    public event Action<Sprite, int> OnQuestionAnswer;


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

    private void ShowPart(Sprite sprite, int index)
    {
        foreach (MallItem item in _mallParts)
        {
            if(item.Index == index)
            {
                item.SpriteRenderer.sprite = sprite;

                if(item.IsGood)
                {
                    GoodPartsCount++;
                }
                return;
            }
        }
    }

    public void InvokeOnQuestionAnswer(Sprite sprite, int index)
        => OnQuestionAnswer?.Invoke(sprite, index);
}
