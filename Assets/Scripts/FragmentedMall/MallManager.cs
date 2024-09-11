using System;
using System.Collections.Generic;
using UnityEngine;

public class MallManager : MonoBehaviour
{
    public static MallManager Instance;

    public event Action<Sprite, int> OnQuestionAnswer;

    [SerializeField] private List<MallItem> _mallParts = new List<MallItem>();

    [SerializeField] private int _score;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

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

                _score += item.ScoreValue;
                return;
            }
        }
    }
}
