using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Instructions : MonoBehaviour
{
    public GameObject menu;
    public GameObject help;
    public Dropdown player;
    public Text left;
    public Text right;
    public Text launch;
    public Text scoreboard;
    public Text pause;

    public void showInstructions(int p) {
        switch (p) {
            case 1:
                left.text = "A";
                right.text = "D";
                launch.text = "X";
                scoreboard.text = "T";
                pause.text = "Escape / ESC";
                break;
            case 2:
                left.text = "Joystick 1";
                right.text = "Joystick 3";
                launch.text = "Joystick 4";
                scoreboard.text = "Joystick 2";
                pause.text = "R1";
                break;
            case 3:
                left.text = "Joystick 3";
                right.text = "Joystick 1";
                launch.text = "Joystick 4";
                scoreboard.text = "Joystick 2";
                pause.text = "R1";
                break;
            default:
                left.text = "Left arrow";
                right.text = "Right arrow";
                launch.text = "Space";
                scoreboard.text = "T";
                pause.text = "Escape / ESC";
                break;
        }
    }

    public void playerPicked(Dropdown change) {
        showInstructions(change.value);
    }

    public void close() {
        help.SetActive(false);
        menu.SetActive(true);
    }
}
