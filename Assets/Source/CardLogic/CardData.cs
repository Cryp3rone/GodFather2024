using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Cards/CardData")]
public class CardData : ScriptableObject
{
    public string cardName;
    public string cardQuestion;
    public int baseId;
    public bool isRobotGood;
    public Choice goodChoice;
    public Choice badChoice;
}

[Serializable]
public class Choice
{
    public bool isRight = false;
    public string proposition;
    public Sprite result;
    public AudioClip sound;
    public string resultText;
}
