using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("UI")]
    [SerializeField] private Text scoreText;
    [SerializeField] private Text instructionsText;
    [SerializeField] private Button restartButton;

    [Header("Gameplay")]
    [SerializeField] private int totalPickups = 8;
    [SerializeField] private string startInstructions = "Use Arrow Keys";
    [SerializeField] private string winMessage = "You Win!";
    [SerializeField] private string loseMessage = "Game Over";
    [SerializeField] private float fallYThreshold = -10f;

    public bool IsGameOver { get; private set; }
    public float FallYThreshold => fallYThreshold;

    private int currentScore;
    private PlayerController player;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        currentScore = 0;
        IsGameOver = false;

        if (scoreText != null)
        {
            scoreText.text = $"Score: 00/{totalPickups:00}";
        }

        if (instructionsText != null)
        {
            instructionsText.text = startInstructions;
        }

        if (restartButton != null)
        {
            restartButton.onClick.AddListener(RestartGame);
        }
    }

    public void RegisterPickup()
    {
        if (IsGameOver)
            return;

        currentScore++;

        if (scoreText != null)
        {
            scoreText.text = $"Score: {currentScore:00}/{totalPickups:00}";
        }

        if (currentScore == 1 && instructionsText != null)
        {
            instructionsText.text = string.Empty;
        }

        if (currentScore >= totalPickups)
        {
            WinGame();
        }
    }

    private void WinGame()
    {
        IsGameOver = true;

        if (instructionsText != null)
        {
            instructionsText.text = winMessage;
        }

        if (player != null)
        {
            player.FreezeOnWin();
        }
    }

    public void PlayerDied()
    {
        if (IsGameOver)
            return;

        IsGameOver = true;

        if (instructionsText != null)
        {
            instructionsText.text = loseMessage;
        }

        Invoke(nameof(RestartGame), 1.2f);
    }

    public void RestartGame()
    {
        Scene active = SceneManager.GetActiveScene();
        SceneManager.LoadScene(active.buildIndex);
    }
}
