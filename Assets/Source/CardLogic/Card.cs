using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private TextMeshProUGUI tmp;
    [SerializeField] private Button button;
    [SerializeField] private Slider buttonFill;
    [SerializeField] private float timeToFillButton;
    [SerializeField] private GameObject buttonBorder;
    [SerializeField] private Ease buttonAnimationEasing;
    [SerializeField] private float buttonAnimationTime;
    [SerializeField] private float buttonAnimationScale;
    [SerializeField] private Color buttonAnimationEnterColor;
    private Color buttonAnimationExitColor;

    private int baseId;
    private Choice data;

    private bool fillCoroutineStarted = false;
    private Coroutine fillCoroutine;

    public void Awake()
    {
        button = GetComponentInChildren<Button>();

        button.onClick.AddListener(SendChoice);

        buttonAnimationExitColor = button.image.color;

        buttonFill.maxValue = timeToFillButton;
    }

    public void SetCard(int sendBaseId, Choice sendData)
    {
        baseId = sendBaseId;
        data = sendData;

        tmp.text = data.proposition;
    }

    public void SendChoice()
    {
        //if (buttonFill.value >= buttonFill.maxValue)
        //{
            CardManager.Instance.UpdateChoice(data, baseId);
        //}
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(buttonAnimationScale, buttonAnimationTime).SetEase(buttonAnimationEasing);
        button.image.DOColor(buttonAnimationEnterColor, buttonAnimationTime);
        //buttonBorder.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(1, buttonAnimationTime).SetEase(buttonAnimationEasing);
        button.image.DOColor(buttonAnimationExitColor, buttonAnimationTime);
        //buttonBorder.gameObject.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!fillCoroutineStarted)
        {
            fillCoroutine = StartCoroutine(ButtonFillCoroutine());
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        StopCoroutine(fillCoroutine);
        fillCoroutineStarted = false;
        Debug.Log("stop coroutine");
        buttonFill.value = 0;
    }

    private IEnumerator ButtonFillCoroutine()
    {
        fillCoroutineStarted = true;
        Debug.Log("coroutine started");
        while (true)
        {
            buttonFill.value += Time.deltaTime;
            yield return null;
        }
    }
}
