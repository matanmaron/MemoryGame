using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text TimerText;
    [SerializeField] private Text InfoText;
    [SerializeField] private Text FeedbackText;

    private const string _infoText = "Press 'ESC' For Menu";
    private const string _winText = "You WIN";
    private const string _loseText = "Time's Up !";
    private string LastText;
    private string LastTimer;

    internal void ResetUI()
    {
        LastText = string.Empty;
        LastTimer = string.Empty;
        InfoText.text = string.Empty;
        FeedbackText.text = string.Empty;
        TimerText.text = string.Empty;
    }

    internal void PauseUI()
    {
        InfoText.text = string.Empty;
        LastText = FeedbackText.text;
        LastTimer = TimerText.text;
        TimerText.text = string.Empty;
        FeedbackText.text = string.Empty;
    }

    internal void UnPauseUI()
    {
        InfoText.text = _infoText;
        TimerText.text = LastTimer;
        FeedbackText.text = LastText;
    }

    internal void SetTimerUI(int seconds)
    {
        TimerText.text = seconds.ToString();
    }

    internal void WinUI()
    {
        FeedbackText.text = _winText;
        TimerText.text = string.Empty;
    }

    internal void LoseUI()
    {
        FeedbackText.text = _loseText;
        TimerText.text = string.Empty;
    }

    internal void NewGameUI()
    {
        ResetUI();
        InfoText.text = _infoText;
    }
}
