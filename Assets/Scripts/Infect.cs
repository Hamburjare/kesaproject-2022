using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infect : MonoBehaviour
{
    SpriteRenderer m_SpriteRenderer;
    Color m_NewColor;

    // Start is called before the first frame update
    void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            m_NewColor = new Color(0, 1, 0, 1);
            m_SpriteRenderer.color = m_NewColor;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
