using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("UI Panels")]
    public GameObject startMenu;
    public GameObject hudPanel;
    public GameObject restartMenu;

    [Header("UI Text Elements")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;

    [Header("Settings")]
    public float gameDuration = 60f;
    
    private int score = 0;
    private float timeLeft;
    private bool isGameActive = false;

    void Awake() 
    { 
        // Singleton pattern instance assignment
        instance = this; 
    }

    void Start()
    {
        // Initial interface state with only start menu active
        startMenu.SetActive(true);
        hudPanel.SetActive(false);
        restartMenu.SetActive(false);
    }

    void Update()
    {
        // Timer update during active game sessions
        if (isGameActive) UpdateTimer();
    }

    public void StartGame()
    {
        // State and value reset at session start
        score = 0;
        timeLeft = gameDuration;
        isGameActive = true;
        
        scoreText.text = "Score: 0";
        timerText.text = "Time: " + gameDuration.ToString();

        // UI transition to gameplay view
        startMenu.SetActive(false);
        hudPanel.SetActive(true);
        restartMenu.SetActive(false);
    }

    void UpdateTimer()
    {
        // Countdown logic and display update
        timeLeft -= Time.deltaTime;
        timerText.text = "Time: " + Mathf.Max(0, timeLeft).ToString("F0");

        if (timeLeft <= 0) EndGame();
    }

    void EndGame()
    {
        isGameActive = false;
        
        // Final UI state with results and restart option
        hudPanel.SetActive(true);
        restartMenu.SetActive(true);
        
        // Removal of remaining targets in the scene
        TargetBehaviour[] targets = FindObjectsOfType<TargetBehaviour>();
        foreach (TargetBehaviour t in targets) Destroy(t.gameObject);
    }

    public void AddScore(int value)
    {
        if (!isGameActive) return;
        score += value;
        // Real-time score update in HUD
        scoreText.text = "Score: " + score;
    }

    public bool IsGameActive() { return isGameActive; }
}
