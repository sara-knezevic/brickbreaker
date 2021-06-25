using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public bool inPlay;
    public Transform paddle;
    public float speed;
    public Transform explosion;
    public GameManager GM;

    public Transform extraLife;
    public Transform longerPad;
    public Transform slowerBall;
    public KeyCode launch;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D> ();
    }

    // Update is called once per frame
    void Update()
    {
        if (GM.gameOverFlag) return;
        else if (transform.parent.GetComponent<Player>().death) return;

        if (!inPlay) {
            transform.position = paddle.position;
        }

        if (Input.GetKeyDown(launch) && !inPlay) {
            inPlay = true;

            if (gameObject.tag == "Player1") {
                rb.AddForce(Vector2.up * speed);
            } else if (gameObject.tag == "Player2") {
                rb.AddForce(Vector2.down * speed);
            } else if (gameObject.tag == "Player3") {
                rb.AddForce(Vector2.right * speed);
            } else if (gameObject.tag == "Player4") {
                rb.AddForce(Vector2.left * speed);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag(gameObject.tag)) {
            rb.velocity = Vector2.zero;
            inPlay = false;

            transform.parent.GetComponent<Player>().updateLives(-1);
        } 
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.transform.CompareTag("Brick")) {

            int powerChance = Random.Range(1, 101);

            if (powerChance < 50) {
                int powerPick = Random.Range(1, 4);
                switch (powerPick) {
                    case 1:
                        Instantiate(extraLife, other.transform.position, paddle.transform.rotation);
                        break;
                    case 2:
                        Instantiate(longerPad, other.transform.position, paddle.transform.rotation);
                        break;
                    case 3:
                        Instantiate(slowerBall, other.transform.position, paddle.transform.rotation);
                        break;
                }
            }

            Transform newExplosion = Instantiate(explosion, other.transform.position, other.transform.rotation);
            Destroy(newExplosion.gameObject, 2);
            Destroy(other.gameObject);

            transform.parent.GetComponent<Player>().updateScore(other.gameObject.GetComponent<BrickScript>().points);
            GM.updateNumberOfBricks();
        }
    }
}
