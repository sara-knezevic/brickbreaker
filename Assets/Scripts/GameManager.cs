using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool gameOverFlag;
    public GameObject gameOverPanel;
    public GameObject scoreboardPanel;
    public GameObject wallGenerator;
    public GameObject playerScript;
    public int brickCount;
    public static int numOfPlayers;
    public int numOfDeaths = 0;
    private bool paused, scoreboard;
    public Dictionary<string, int> scores = new Dictionary<string, int>();
    private string winnerKey;
    private string winnerText;

    // Start is called before the first frame update
    void Start()
    {
        wallGenerator.gameObject.GetComponent<WallGenerator>().generateWall();
        brickCount = GameObject.FindGameObjectsWithTag("Brick").Length;

        numOfPlayers = PlayerSelection.p;

        for (int i = 1; i <= numOfPlayers; i++) {
            playerScript.gameObject.GetComponent<PlayerScript>().createPlayer(i);
        }

        Time.timeScale = 1;
    }
    
    void Update() {
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton5)) && !paused && !gameOverFlag) {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
            gameOverPanel.transform.GetChild(0).GetComponent<Text>().text = "GAME PAUSED";
            paused = true;
        } else if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton5)) && paused && !gameOverFlag) {
            Time.timeScale = 1;
            gameOverPanel.SetActive(false);
            paused = false;
        }

        if (Input.GetKeyDown(KeyCode.T) || Input.GetKeyDown(KeyCode.JoystickButton1)) {
            toggleScoreboard();
        }
    }

    public void toggleScoreboard() {
        if (!scoreboard) {
            scoreboardPanel.SetActive(true);
            scoreboard = true;
            updateScores();
        } else {
            scoreboardPanel.SetActive(false);
            scoreboard = false;
        }
    }

    public void updateScores() {
        scoreboardPanel.transform.GetChild(0).GetComponent<Text>().text = "";
        foreach (KeyValuePair<string, int> s in scores.OrderByDescending(key=> key.Value)) {
            scoreboardPanel.transform.GetChild(0).GetComponent<Text>().text += s.Key + ": " + s.Value + "\n";
        }
    }

    public void updateNumberOfBricks() {
        brickCount--;

        if (brickCount <= 0) {
            gameOverFlag = true;
            winnerKey = scores.OrderByDescending(key => key.Value).First().Key;
            winnerText = "";
            winnerText += winnerKey;
            foreach (KeyValuePair<string, int> s in scores.OrderByDescending(key => key.Value)) {
                if (s.Key != winnerKey && s.Value == scores[winnerKey]) {
                    winnerText += " & " + s.Key;
                }
            }
            gameOverPanel.transform.GetChild(0).GetComponent<Text>().text = winnerText + " won!\nVictory!";
            gameOverPanel.SetActive(true);
        }
    }

    public void gameOver() {
        if (numOfPlayers == numOfDeaths) {
            gameOverFlag = true;
            gameOverPanel.transform.GetChild(0).GetComponent<Text>().text = "GAME OVER";
            gameOverPanel.SetActive(true);
        }
    }

    public void playAgain() {
        SceneManager.LoadScene("GameScene");
    }

    public void mainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void quit() {
        Application.Quit();
    }
}
