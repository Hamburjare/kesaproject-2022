using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    private Rigidbody2D rb2d;

    public float moveSpeed = 10f;

    public float jumpForce = 10f;

    public KeyCode left;
    public KeyCode right;
    public KeyCode jump;

    // public bool automaticRunning = true; //PROTOTYPING ONLY

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

    }

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // if (!automaticRunning)
        // {
        //     if (Input.GetKey(left))
        //     {
        //         rb2d.velocity = new Vector2(-moveSpeed, rb2d.velocity.y);
        //     }
        //     else if (Input.GetKey(right))
        //     {
        //         rb2d.velocity = new Vector2(moveSpeed, rb2d.velocity.y);
        //     }
        //     else
        //     {
        //         rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        //     }
        // }
        // else
        // {
        //     rb2d.velocity = new Vector2(moveSpeed, rb2d.velocity.y);
        // }


        if (Input.GetKey(jump))
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
        }

        /* Making sure that the player does not go above the y position of 20. */
        if (transform.position.y >= 20)
        {
            transform.position = new Vector3(transform.position.x, 20, transform.position.z);
        }
    }
}
