using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private GameObject _winScreen;
    [SerializeField] private GameObject _loseScreen;
    [SerializeField] private GameObject boxResult;
    [SerializeField] private GameObject pauseMenu;

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
    => boxResult.SetActive(true);

    public void OnClickResult()
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
