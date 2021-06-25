using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public GameObject player;
    public GameManager GM;

    public void createPlayer(int num) {
        GameObject p;
        Vector3 rot = new Vector3(0, 0, 0);

        switch (num) {
            case 1:
                p = Instantiate(player, new Vector2(0, -11), Quaternion.Euler(0, 0, 0));
                p.tag = "Player1";
                rot.z = 0;
                foreach (Transform t in p.transform)
                {
                    t.gameObject.tag = "Player1";
                }

                p.transform.GetChild(0).GetComponent<PaddleMovement>().left = KeyCode.LeftArrow;
                p.transform.GetChild(0).GetComponent<PaddleMovement>().right = KeyCode.RightArrow;
                p.transform.GetChild(1).GetComponent<BallScript>().launch = KeyCode.Space;
                p.transform.GetComponent<Player>().toggleScores = KeyCode.T;
                break;
            case 2:
                p = Instantiate(player, new Vector2(0, 11), Quaternion.Euler(0, 0, 180));
                p.tag = "Player2";
                rot.z = 0;
                foreach (Transform t in p.transform)
                {
                    t.gameObject.tag = "Player2";
                }
                
                p.transform.GetChild(0).GetComponent<PaddleMovement>().left = KeyCode.D;
                p.transform.GetChild(0).GetComponent<PaddleMovement>().right = KeyCode.A;
                p.transform.GetChild(1).GetComponent<BallScript>().launch = KeyCode.X;
                p.transform.GetComponent<Player>().toggleScores = KeyCode.T;
                break;
            case 3:
                p = Instantiate(player, new Vector2(-11, 0), Quaternion.Euler(0, 0, -90));
                p.tag = "Player3";
                rot.z = 90;
                foreach (Transform t in p.transform)
                {
                    t.gameObject.tag = "Player3";
                }
                
                p.transform.GetChild(0).GetComponent<PaddleMovement>().left = KeyCode.Joystick2Button0;
                p.transform.GetChild(0).GetComponent<PaddleMovement>().right = KeyCode.Joystick2Button2;
                p.transform.GetChild(1).GetComponent<BallScript>().launch = KeyCode.Joystick2Button3;
                p.transform.GetComponent<Player>().toggleScores = KeyCode.Joystick2Button1;
                break;
            case 4:
                p = Instantiate(player, new Vector2(11, 0), Quaternion.Euler(0, 0, 90));
                p.tag = "Player4";
                rot.z = 90;
                foreach (Transform t in p.transform)
                {
                    t.gameObject.tag = "Player4";
                }

                p.transform.GetChild(0).GetComponent<PaddleMovement>().left = KeyCode.Joystick1Button2;
                p.transform.GetChild(0).GetComponent<PaddleMovement>().right = KeyCode.Joystick1Button0;
                p.transform.GetChild(1).GetComponent<BallScript>().launch = KeyCode.Joystick1Button3;
                p.transform.GetComponent<Player>().toggleScores = KeyCode.Joystick1Button1;
                break;
            default:
                return;
        }

        p.transform.GetChild(0).GetChild(2).GetChild(0).rotation = Quaternion.Euler(rot);

        p.transform.GetChild(2).GetChild(0).rotation = Quaternion.Euler(rot);
        p.transform.GetChild(2).GetChild(1).rotation = Quaternion.Euler(rot);

        p.GetComponent<Player>().GM = GM;
        p.transform.GetChild(0).GetComponent<PaddleMovement>().GM = GM;
        p.transform.GetChild(1).GetComponent<BallScript>().GM = GM;
        
        p.layer = 9;
        p.transform.GetChild(1).gameObject.layer = 9;
    }
}
