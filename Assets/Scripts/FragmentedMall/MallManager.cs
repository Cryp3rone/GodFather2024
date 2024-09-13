using DG.Tweening;
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

    [SerializeField] private float modulesAnimationScale;
    [SerializeField] private float modulesAnimationTime;
    
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

                if(choiceData.sound != null) 
                { 
                    GameManager.Instance.GetComponent<AudioSource>().PlayOneShot(choiceData.sound);
                }

                if(choiceData.particle != null)
                {

                }

                Debug.Log(choiceData.result);

                if(choiceData.isRight)
                {
                    GoodPartsCount++;
                    Debug.Log(GoodPartsCount);
                }

                item.SpriteRenderer.transform.DOScale(modulesAnimationScale, modulesAnimationTime).SetEase(Ease.InExpo).OnComplete(() => resetOnComplete(item));
                
                return;
            }
        }
    }

    public void InvokeOnQuestionAnswer(Choice choiceData, int index)
        => OnQuestionAnswer?.Invoke(choiceData, index);

    private void resetOnComplete(MallItem item)
    {
        item.SpriteRenderer.transform.DOScale(1, modulesAnimationTime / 4).SetEase(Ease.OutBounce);
    }
}
