using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public int score = 0;
    public bool isGameOver = false;
    
    // gameobject for the ship
    [SerializeField]
    public GameObject shipModel;
    // button to start the game
    [SerializeField]
    public GameObject startGameButton;
    // text that appears when you game over
    [SerializeField]
    public Text gameOverText;
    // text to display current score
    [SerializeField]
    public Text scoreText;
    [SerializeField]
    public Text titleText;
    [SerializeField] 
    public Spawner spawner;

    private static Game instance;

    private void Start()
    {
        instance = this;
        titleText.enabled = true;
        gameOverText.enabled = false;
        scoreText.enabled = false;
        startGameButton.SetActive(true);
    }

    public static void GameOver()
    {
        instance.titleText.enabled = true;
        instance.startGameButton.SetActive(true);
        instance.isGameOver = true;
        instance.spawner.StopSpawning();
        instance.shipModel.GetComponent<Player>().Die();
        instance.gameOverText.enabled = true;
    }

    public void NewGame()
    {
        isGameOver = false;
        titleText.enabled = false;
        startGameButton.SetActive(false);
        shipModel.transform.position = new Vector3(0, -3.22f, 0);
        score = 0;
        scoreText.text = "Score: " + score;
        scoreText.enabled = true;
        spawner.BeginSpawning();
        shipModel.GetComponent<Player>().Respawn();
        spawner.ClearAsteroids();
        gameOverText.enabled = false;
    }

    public static void AsteroidDestroyed()
    {
        instance.score++;
        instance.scoreText.text = "Score: " + instance.score;
    }

    public Player GetShip()
    {
        return shipModel.GetComponent<Player>();
    }

    public Spawner GetSpawner()
    {
        return spawner.GetComponent<Spawner>();
    }
}
