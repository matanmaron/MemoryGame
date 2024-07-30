using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Sprite[] CardTypes;
    [SerializeField] private Card OriginCard;
    [SerializeField] private GameObject Grid;
    [SerializeField] private AudioSource MatchAudio;
    [SerializeField] private AudioSource NoMatchAudio;
    [SerializeField] private AudioSource WinAudio;
    [SerializeField] private AudioSource LoseAudio;
    [SerializeField] private AudioSource Music;
    [SerializeField] private GameObject TimerIcon;
    [SerializeField] private UIManager _UIManager;
    [SerializeField] private MenuManager _MenuManager;

    private GameStats gameStats;
    private const int _rows = 4;
    private const int _cols = 4;
    private const float CardDelay = 1f;
    private Card _first;
    private Card _second;

    internal bool gameOver;
    internal bool paused;

    internal static GameManager GameManagerInstance { get; private set; } //singelton

    private void Awake()
    {
        if (GameManagerInstance != null && GameManagerInstance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            GameManagerInstance = this;
        }
    }

    private void Start()
    {
        gameStats = new GameStats();
        _UIManager.ResetUI();
        _MenuManager.ToggleResume(false);
        paused = true;
        ToggleMenu();
        if (CardTypes.Length == 0)
        {
            Debug.Log("no cards...");
            Application.Quit();
        }
    }

    private void Update()
    {
        if (!paused && Input.GetKeyDown(KeyCode.Escape))
        {
            OnESC();
        }
        if (!paused && !gameOver)
        {
            UpdateTimer();
        }
    }

    public void OnESC()
    {
        _UIManager.ResetUI();
        if (!Music.isPlaying)
        {
            Music.UnPause();
        }
        if (!gameOver)
        {
            _MenuManager.ToggleResume(true);
        }
        paused = !paused;
        ToggleMenu();
    }

    private void ToggleMenu()
    {
        Grid.SetActive(!paused);
        TimerIcon.SetActive(!paused);
        _MenuManager.ToggleMenu(paused);

        if (paused)
        {
            _UIManager.PauseUI();
        }
        else
        {
            _UIManager.UnPauseUI();
        }
    }

    private void AddAllCards()
    {
        for (int i = 0; i < _rows; i++)
        {
            for (int j = 0; j < _cols; j++)
            {
                Card newcard;
                newcard = Instantiate(OriginCard, Grid.transform) as Card;

                int index = i * _cols + j;

                int place = gameStats.AllCards[index];
                if (place >= 0)
                {
                    newcard.CardFace.sprite = CardTypes[place];
                    newcard.CardID = place;
                }
                else
                {
                    newcard.SetInvisiable();
                }
            }
        }
    }

    private void UpdateTimer()
    {
        if (gameStats.timer == -99)
        {
            return;
        }
        gameStats.timer -= Time.deltaTime;
        int seconds = Convert.ToInt32(gameStats.timer % 60);
        _UIManager.SetTimerUI(seconds);
        if (seconds < 0)
        {
            GameOverLose();
        }
    }

    private void HideCards(int val)
    {
        for (int i = 0; i < gameStats.AllCards.Length; i++)
        {
            if (gameStats.AllCards[i] == val)
            {
                gameStats.AllCards[i] = -1;
            }
        }
    }

    private IEnumerator Match(bool match)
    {
        paused = true;
        yield return new WaitForSeconds(CardDelay);
        if (match)
        {
            _first.SetInvisiable();
            _second.SetInvisiable();
            HideCards(_first.CardID);
        }
        else
        {
            _first.UnReaveal();
            _second.UnReaveal();
        }
        _first = null;
        _second = null;

        paused = false;
    }

    private void CleanCards()
    {
        foreach (Transform child in Grid.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void GetAllCards()
    {
        gameStats.AllCards = new int[CardTypes.Length * 2];
        for (int i = 0; i < CardTypes.Length; i++)
        {
            gameStats.AllCards[i] = i;
            gameStats.AllCards[i + CardTypes.Length] = i;
        }
        gameStats.AllCards = gameStats.AllCards.Shuffle();
    }

    private void GameOverWin()
    {
        gameOver = true;
        Music.Pause();
        _MenuManager.ToggleResume(false);
        _UIManager.WinUI();
        WinAudio.Play();
    }

    private void GameOverLose()
    {
        gameOver = true;
        Music.Pause();
        _MenuManager.ToggleResume(false);
        _UIManager.LoseUI();
        LoseAudio.Play();
    }

    internal void Game(Card card)
    {
        if (!paused)
        {
            if (_first == null)
            {
                _first = card;
            }
            else if (_first != null && _second == null && _first != card)
            {
                _second = card;
                if (_first.CardFace.sprite == _second.CardFace.sprite)
                {
                    StartCoroutine(Match(true));
                    MatchAudio.PlayDelayed(CardDelay);
                    gameStats.CardsLeft -= 2;
                }
                else
                {
                    StartCoroutine(Match(false));
                    NoMatchAudio.PlayDelayed(CardDelay);
                }
            }
        }
        if (gameStats.CardsLeft <= 0)
        {
            GameOverWin();
        }
    }

    #region Buttons
    public void QuitBtn()
    {
        Application.Quit();
    }
    public void ResumeBtn()
    {
        _MenuManager.ToggleResume(paused);
        paused = !paused;
        ToggleMenu();
    }
    public void NewGameBtn(bool timer)
    {
        gameOver = false;
        CleanCards();
        _MenuManager.ToggleResume(false);
        _UIManager.NewGameUI();
        paused = true;
        ToggleMenu();
        GetAllCards();
        gameStats.CardsLeft = CardTypes.Length * 2;
        if (timer)
        {
            gameStats.timer = 3;
        }
        else
        {
            gameStats.timer = -99;
        }
        AddAllCards();
        ResumeBtn();
    }
    public void SaveBtn()
    {
        try
        {
            DataStorage.SaveNonPersisted<GameStats>(gameStats, 0);
            Debug.Log("save ok");
            paused = false;
            ToggleMenu();
        }
        catch (Exception ex)
        {
            Debug.Log("fail to save: " + ex);

        }
    }
    public void LoadBtn()
    {
        try
        {
            gameStats = DataStorage.LoadNonPersisted<GameStats>(0);
            if (gameStats.timer > 0)
            {
                _UIManager.NewGameUI();
                gameOver = false;
            }
            CleanCards();
            AddAllCards();
            paused = false;
            ToggleMenu();
        }
        catch (Exception ex)
        {
            Debug.Log("fail to load: " + ex);
        }
    }
    #endregion Buttons
}