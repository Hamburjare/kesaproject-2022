using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDeletor : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Destroy(other.gameObject);
        }

    }

}
