using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

#if UNITY_EDITOR
using UnityEditor;
#endif



public class UIManager : MonoBehaviour
{
    [SerializeField]
    // Starts skin menu open
    GameObject lastPressed;

    [SerializeField]
    GameObject skinList;

    [SerializeField]
    GameObject themeList;

    void Start()
    {
        lastPressed.GetComponent<Animator>().Play("ButtonUp");
    }

    void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void ButtonPress(int index)
    {
        EventSystem currentEvent = EventSystem.current;
        GameObject selectedBtn = currentEvent.currentSelectedGameObject;

        switch (index)
        {

            // Start Button
            case 0:
                StartGame();
                break;

            // Perks Button
            case 1:
                CheckLastPress(selectedBtn);

                Debug.Log($"{index} {selectedBtn.transform.position}");

                break;

            // Skin Button
            case 2:
                skinList.SetActive(true);
                themeList.SetActive(false);
                CheckLastPress(selectedBtn);
                Debug.Log($"{index} {selectedBtn.transform.position}");
                break;

            // Theme Button
            case 3:
                skinList.SetActive(false);
                themeList.SetActive(true);
                CheckLastPress(selectedBtn);
                Debug.Log($"{index} {selectedBtn.transform.position}");
                break;

            // Shop Button
            case 4:
                CheckLastPress(selectedBtn);
                Debug.Log($"{index} {selectedBtn.transform.position}");
                break;

        }
    }

    void CheckLastPress(GameObject selectedBtn)
    {
        Animator _animatorActive = selectedBtn.GetComponent<Animator>();
        Animator _animatorPressed = lastPressed.GetComponent<Animator>();
        /* Checking if the last button pressed is the same as the current button pressed. If it is not
        the same, it will move the last button pressed down 15 units and move the current button
        pressed up 15 units. */
        if (lastPressed != selectedBtn)
        {
            _animatorActive.Play("ButtonUp");
            _animatorPressed.Play("ButtonDown");
            // lastPressed.transform.position = new Vector3(lastPressed.transform.position.x, lastPressed.transform.position.y - 15);
            // selectedBtn.transform.position = new Vector3(selectedBtn.transform.position.x, selectedBtn.transform.position.y + 15);
        }
        lastPressed = selectedBtn;
    }
}
