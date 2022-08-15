using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MoveLeft : MonoBehaviour
{

    float speed;

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.D))
        {
            speed = Time.deltaTime * PlayerController.Instance.moveSpeed * Convert.ToSingle(GameManager.Instance.playerSpeed);
            transform.Translate(Vector3.left * speed);

        }
    }
    void LateUpdate()
    {
        if (Input.GetKey(KeyCode.D))
        {
            GameManager.Instance.Score();

        }
    }

}
