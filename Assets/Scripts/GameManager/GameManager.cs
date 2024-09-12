using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private GameObject _winScreen;
    [SerializeField] private GameObject _loseScreen;
    [SerializeField] private GameObject boxResult;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Transform resultHidePose, resultShowPose;
    [SerializeField] private Button resultButton;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        boxResult.SetActive(false);
        boxResult.transform.position = resultHidePose.position;
        resultButton.interactable = false;
    }

    public void Start()
    {
        CardManager.Instance.ShowPropositions(CardManager.Instance.cardDatas.First());
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (!pauseMenu.activeSelf)
            {
                pauseMenu.SetActive(true);
                return;
            }

            pauseMenu.SetActive(false);
        }
    }

    public void ShowResult()
    {
        boxResult.SetActive(true);
        boxResult.transform.DOMoveY(resultShowPose.position.y, 2f).SetEase(Ease.OutBack).OnComplete(() => resultButton.interactable = true);
    }

    public void HideResult(TweenCallback onComplete)
    {
        resultButton.interactable = false;
        boxResult.transform.DOMoveY(resultHidePose.position.y, 0.5f).SetEase(Ease.InBack).OnComplete(onComplete);
    }

    public void OnClickResult()
    {
        HideResult(OnCompleteHide);
    }

    public void OnCompleteHide()
    {
        boxResult.SetActive(false);
        if (CardManager.Instance.cardDatas.Count > 0)
        {
            CardManager.Instance.ShowPropositions(CardManager.Instance.cardDatas.First());
        }
        else
        {
            CheckWinLose();
        }
    }

    public void CheckWinLose()
    {
        if(MallManager.Instance.GoodPartsCount >= MallManager.Instance.GoodPartsToWin)
        {
            _winScreen.SetActive(true);
        }
        else
        {
            _loseScreen.SetActive(false);
        }
    }
}
