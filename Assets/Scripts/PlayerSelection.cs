using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerSelection : MonoBehaviour
{
    public Text selectedPlayersText;
    public Slider playerSlider;
    public GameObject menu;
    public GameObject help;
    public static int p = 1;

    public void startGame() {
        // DontDestroyOnLoad(this.gameObject);
        SceneManager.LoadScene("GameScene");
    }

    void Update() {
        playerSlider.value = p;
        selectedPlayersText.text = p + " players selected!";
    }

    public void ValueChangeCheck() {
        p = (int)playerSlider.value;
    }

    public void helpMenu() {
        menu.SetActive(false);
        help.SetActive(true);
    }

    public void quit() {
        Application.Quit();
    }
}
