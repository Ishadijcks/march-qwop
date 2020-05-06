using TMPro;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    public static float highScore = 0;

    public Transform target;
    public TextMeshProUGUI scoreDisplay;
    public TextMeshProUGUI highscoreDisplay;
    public TextMeshProUGUI timeDisplay;

    public Game game;

    private float highestDistance = 0.0f;
    private float startTime;

    public bool isStarted = false;

    private void Start()
    {
        highscoreDisplay.text = "Best: " + highScore.ToString("0.0") + "m";

        game.OnGameOver += OnGameOver;
    }

    public void Go()
    {
        startTime = Time.time;
        isStarted = true;
    }

    void Update()
    {
        if (game.isOver || !isStarted)
        {
            return;
        }

        highestDistance = Mathf.Max(highestDistance, target.position.x);

        highScore = Mathf.Max(highScore, highestDistance);

        scoreDisplay.text = highestDistance.ToString("0.0") + "m";
        timeDisplay.text = (Time.time - startTime).ToString("0.0") + "s";
    }

    public void OnGameOver()
    {
        highscoreDisplay.text = "Best: " + highScore.ToString("0.0") + "m";
    }
}