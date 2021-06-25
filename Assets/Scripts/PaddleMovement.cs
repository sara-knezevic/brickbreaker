using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleMovement : MonoBehaviour
{
    public float speed;
    public float rightScreenEdge;
    public float leftScreenEdge;
    public float topScreenEdge;
    public float bottomScreenEdge;
    public GameManager GM;
    public GameObject ball;
    private bool padFlag, slowFlag;
    private float padTimer, slowTimer;
    private int padded = 0;
    private Vector2 defaultPad;
    private float defaultSpeed;
    private string playerNum;
    
    public KeyCode left;
    public KeyCode right;

    void Start()
    {
        defaultPad = transform.GetChild(0).localScale;
        defaultSpeed = ball.gameObject.GetComponent<BallScript>().speed;

        playerNum = gameObject.tag;
    }

    void Update()
    {
        if (GM.gameOverFlag) return;
        else if (transform.parent.GetComponent<Player>().death) return;

        Vector2 moveInput = Vector2.zero;
        
        if (Input.GetKey(right)) {
            moveInput = Vector2.right;
        } else if (Input.GetKey(left)) {
            moveInput = Vector2.left;
        }

        transform.Translate(moveInput * speed * Time.deltaTime);

        if (playerNum == "Player1" || playerNum == "Player2") {
            if (transform.GetChild(0).position.x < leftScreenEdge + 2 * padded) {
                transform.position = new Vector2(leftScreenEdge + 2 * padded, transform.position.y);
            }

            if (transform.GetChild(0).position.x > rightScreenEdge - 2 * padded) {
                transform.position = new Vector2(rightScreenEdge - 2 * padded, transform.position.y);
            }
        } else {
            if (transform.GetChild(0).position.y > topScreenEdge - 2 * padded) {
            transform.position = new Vector2(transform.position.x, topScreenEdge - 2 * padded);
            }

            if (transform.GetChild(0).position.y < bottomScreenEdge + 2 * padded) {
                transform.position = new Vector2(transform.position.x, bottomScreenEdge + 2 * padded);
            }
        }

        if (padFlag) {
            padTimer += Time.deltaTime;
            transform.parent.GetComponent<Player>().longerPadText.text = "Longer pad!\n" + (5.0f - padTimer).ToString("0.00") + " seconds left!";
            if (padTimer > 5.0f) {
                padded = 0;
                transform.GetChild(0).localScale = defaultPad;
                padFlag = false;
                transform.parent.GetComponent<Player>().longerPadText.text = "";
            }
        }

        if (slowFlag) {
            slowTimer += Time.deltaTime;
            transform.parent.GetComponent<Player>().slowerBallText.text = "Slower ball!\n" + (5.0f - slowTimer).ToString("0.00") + " seconds left!";
            if (slowTimer > 5.0f) {
                ball.gameObject.GetComponent<BallScript>().speed = defaultSpeed;
                slowFlag = false;
                transform.parent.GetComponent<Player>().slowerBallText.text = "";
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("ExtraLife")) {
            transform.parent.GetComponent<Player>().updateLives(1);
            Destroy(other.gameObject);
        } else if (other.CompareTag("LongerPad")) {
            padFlag = true;
            padTimer = 0.0f;
            if (transform.GetChild(0).localScale.x < 4) {
                padded++;
                transform.GetChild(0).localScale = new Vector2(transform.GetChild(0).localScale.x + 1, transform.GetChild(0).localScale.y);
            }
            Destroy(other.gameObject);
        } else if (other.CompareTag("SlowerBall")) {
            slowFlag = true;
            slowTimer = 0.0f;
            if (ball.gameObject.GetComponent<BallScript>().speed >= 500) {
                ball.gameObject.GetComponent<BallScript>().speed -= 100;
            }
            Destroy(other.gameObject);
        }
    }
}
