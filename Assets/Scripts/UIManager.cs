using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text TimerText;
    [SerializeField] private GameObject ESCButton;
    [SerializeField] private Button WinPanel;
    [SerializeField] private Button LosePanel;

    private const string _infoText = "Press 'ESC' For Menu";
    private string LastTimer;

    internal void ResetUI()
    {
        LastTimer = "00:00";
        WinPanel.gameObject.SetActive(false);
        LosePanel.gameObject.SetActive(false);
        ESCButton.SetActive(false);
        TimerText.text = string.Empty;
    }

    internal void PauseUI()
    {
        ESCButton.SetActive(false);
        LastTimer = TimerText.text;
        TimerText.text = string.Empty;
    }

    internal void UnPauseUI()
    {
        ESCButton.SetActive(true);
        TimerText.text = LastTimer;
    }

    internal void SetTimerUI(int seconds)
    {
        TimerText.text = "00:" + seconds.ToString("D2");
    }

    internal void WinUI()
    {
        WinPanel.gameObject.SetActive(true);
        TimerText.text = string.Empty;
    }

    internal void LoseUI()
    {
        LosePanel.gameObject.SetActive(true);
        TimerText.text = string.Empty;
    }

    internal void NewGameUI()
    {
        ResetUI();
        ESCButton.SetActive(true);
    }
}
