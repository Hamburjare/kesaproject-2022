using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d;

    [SerializeField] private float moveSpeed = 10f;

    [SerializeField] private float jumpForce = 10f;

    public KeyCode left;
    public KeyCode right;
    public KeyCode jump;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        
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

        if (Input.GetKeyDown(jump))
        {

            // Kyll�, joten pelihahmo hypp��
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
        }
    }
}
