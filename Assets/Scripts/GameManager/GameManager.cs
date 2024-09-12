using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private GameObject _winScreen;
    [SerializeField] private GameObject _loseScreen;

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
