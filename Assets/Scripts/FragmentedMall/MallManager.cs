using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                if (choiceData.isRight)
                {
                    GoodPartsCount++;
                }

                StartCoroutine(AnimationCoroutine(choiceData, item));
                
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

    private IEnumerator AnimationCoroutine(Choice choiceData, MallItem item)
    {
        item.ParticleSystem.Play();

        yield return new WaitForSeconds(item.ParticleSystem.main.duration);

        item.SpriteRenderer.sprite = choiceData.result;

        if (choiceData.sound != null)
        {
            GameManager.Instance.GetComponent<AudioSource>().PlayOneShot(choiceData.sound);
        }

        item.SpriteRenderer.transform.DOScale(modulesAnimationScale, modulesAnimationTime).SetEase(Ease.InExpo).OnComplete(() => resetOnComplete(item));
    }
}
