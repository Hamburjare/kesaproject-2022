using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d;

    private float moveSpeed;

    private float jumpForce;

    public KeyCode left;
    public KeyCode right;
    public KeyCode jump;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        jumpForce = 10f;
        moveSpeed = 10f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (Input.GetKey(left))
        {
            rb2d.velocity = new Vector2(-moveSpeed, rb2d.velocity.y);
            // spriteRenderer.flipX = true;
        }
        else if (Input.GetKey(right))
        {
            rb2d.velocity = new Vector2(moveSpeed, rb2d.velocity.y);
            // spriteRenderer.flipX = false;
        }
        else
        {
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        }

        if (Input.GetKey(jump))
        {


            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
        }
    }
}
