using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LuotiController : MonoBehaviour
{

    [SerializeField]
    private float moveSpeed = 7f;

    [SerializeField]
    private Rigidbody2D myBody;

    private void OnDisable()
    {
        myBody.velocity = Vector2.zero;
    }

    public void ShootBullet(Vector3 direction)
    {
        myBody.velocity = direction * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground") || collision.CompareTag("Player") || collision.CompareTag("Deletor"))
        {
            gameObject.SetActive(false);
        }
    }
}