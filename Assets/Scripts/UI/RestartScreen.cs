using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class RestartScreen : Screen
{
    [SerializeField] private TMP_Text header;
    [SerializeField] private List<RestartTextMap> texts;
    
    public void SetHeader(RestartScreenType type)
    {
        header.text = texts.FirstOrDefault(t => t.type == type).text;
    }

    public void RestartClicked()
    {
        Close();
        GameController.Instance.Restart();
    }
}

[Serializable]
public struct RestartTextMap
{
    public RestartScreenType type;
    public string text;
}

public enum RestartScreenType
{
    None,
    
    Win,
    Lose,
}
