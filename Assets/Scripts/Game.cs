using System;
using System.Collections;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static bool firstTime = true;

    public PlayerController player;
    public CameraController camera;
    public ScoreKeeper score;


    public bool isOver;
    
    public event Action OnGameOver;
    
    public GameObject gameOverScreen;
    public GameObject gameWonScreen;

    private void Start()
    {
        if (!firstTime)
        {
            player.Go();
            score.Go();
            camera.followTarget = true;
        }
        else
        {
            StartCoroutine(StartUpRoutine());
        }

        firstTime = false;
    }

    public void Lose()
    {
        if (isOver)
        {
            return;
        }
        isOver = true;
        gameOverScreen.SetActive(true);
        OnGameOver?.Invoke();
    }
    
    public void Win()
    {
        if (isOver)
        {
            return;
        }
        isOver = true;
        gameWonScreen.SetActive(true);
        OnGameOver?.Invoke();
    }

    public IEnumerator StartUpRoutine()
    {
        yield return StartCoroutine(camera.MoveCameraFromFinishToPlayer());
        player.Go();
        score.Go();
    }


    
}