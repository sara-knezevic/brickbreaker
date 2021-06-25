using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int lives = 3;
    public int score = 0;
    public Text livesText;
    public Text scoresText;
    public Text longerPadText;
    public Text slowerBallText;
    public GameManager GM;
    public bool death;
    private int playerTag;
    public KeyCode toggleScores;

    // Start is called before the first frame update
    void Start()
    {
        livesText.text = "LIVES: " + lives;
        scoresText.text = "SCORE: " + score;
        playerTag = (int)(gameObject.tag[gameObject.tag.Length - 1] - '0');
        GM.scores.Add("Player " + playerTag.ToString(), score);
    }

    public void updateLives(int changeInLives) {
        lives += changeInLives;

        if (lives <= 0) {
            death = true;
            GM.numOfDeaths++;
            GM.gameOver();
        }

        livesText.text = "LIVES: " + lives;
    }

    public void updateScore(int points) {
        score += points;
        scoresText.text = "SCORE: " + score;

        GM.scores["Player " + playerTag.ToString()] = score;
        GM.updateScores();
    }
}
