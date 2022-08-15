using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    private Rigidbody2D rb2d;

    public float moveSpeed;

    public float jumpForce;

    public KeyCode jump;

    public Transform groundCheckPoint;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    private bool isGround;

    public TextMeshProUGUI scoreText;

    [SerializeField]
    TextMeshProUGUI _moneyText;

    [SerializeField]
    TextMeshProUGUI _diamondText;


    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        rb2d = GetComponent<Rigidbody2D>();

    }

    void Start()
    {
        _moneyText.text = string.Format("{0, -15:N0}", GameManager.Instance.money);
        _diamondText.text = string.Format("{0, -15:N0}", GameManager.Instance.diamonds);
    }

    void Update()
    {
        isGround = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, whatIsGround);
        _moneyText.text = string.Format("{0, -15:N0}", GameManager.Instance.money);
        _diamondText.text = string.Format("{0, -15:N0}", GameManager.Instance.diamonds);
    }

    void FixedUpdate()
    {

        if (Input.GetKey(jump) && isGround)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce * Convert.ToSingle(GameManager.Instance.jumpForce));
        }

        /* Making sure that the player does not go above the y position of 20. */
        if (transform.position.y >= 20)
        {
            transform.position = new Vector3(transform.position.x, 20, transform.position.z);
        }


    }

}
