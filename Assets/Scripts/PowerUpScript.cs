using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScript : MonoBehaviour
{
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(0f, -1f) * Time.deltaTime * speed);

        if (transform.position.y < -11f || transform.position.x > 18f
            || transform.position.y > 11f || transform.position.x < -18f) {
            Destroy(gameObject);
        }
    }
}
