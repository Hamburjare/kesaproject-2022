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

    public static UIManager Instance;

    [SerializeField]
    // Starts skin menu open
    GameObject lastPressed;

    [SerializeField]
    GameObject skinList;

    [SerializeField]
    GameObject themeList;

    void Start()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        lastPressed.GetComponent<Animator>().Play("ButtonUp");
    }

    void Update()
    {

    }

    void StartGame()
    {
        GameManager.Instance.gameStarted = true;
        SceneManager.LoadScene("MainScene");
    }

    public void ButtonPress(int index)
    {
        EventSystem currentEvent = EventSystem.current;
        GameObject justPressed = currentEvent.currentSelectedGameObject;

        switch (index)
        {

            // Start Button
            case 0:
                StartGame();
                break;

            // Perks Button
            case 1:
                CheckLastPress(justPressed);

                Debug.Log($"{index} {justPressed.transform.position}");

                break;

            // Skin Button
            case 2:
                skinList.SetActive(true);
                themeList.SetActive(false);
                CheckLastPress(justPressed);
                Debug.Log($"{index} {justPressed.transform.position}");
                break;

            // Theme Button
            case 3:
                skinList.SetActive(false);
                themeList.SetActive(true);
                CheckLastPress(justPressed);
                Debug.Log($"{index} {justPressed.transform.position}");
                break;

            // Shop Button
            case 4:
                CheckLastPress(justPressed);
                Debug.Log($"{index} {justPressed.transform.position}");
                break;

        }
    }

    void CheckLastPress(GameObject justPressed)
    {
        Animator _animatorActive = justPressed.GetComponent<Animator>();
        Animator _animatorPressed = lastPressed.GetComponent<Animator>();
        /* Checking if the last button pressed is the same as the current button pressed. If it is not
        the same, it will move the last button pressed down 15 units and move the current button
        pressed up 15 units. */
        if (lastPressed != justPressed)
        {
            _animatorActive.Play("ButtonUp");
            _animatorPressed.Play("ButtonDown");
            // lastPressed.transform.position = new Vector3(lastPressed.transform.position.x, lastPressed.transform.position.y - 15);
            // justPressed.transform.position = new Vector3(justPressed.transform.position.x, justPressed.transform.position.y + 15);
        }
        lastPressed = justPressed;
    }

    public void SkinButton(int i)
    {
        SkinScroller.Skins[] skins = SkinScroller.Instance.skins;
        // Debug.Log($"Index : {i}, Price : {skins[i].price}, Name : {skins[i].name}, Owned : {skins[i].owned}");

        if (GameManager.Instance.money >= skins[i].price && !skins[i].owned)
        {
            GameManager.Instance.SetMoney(skins[i].price);
            skins[i].owned = true;
            SkinScroller.Instance.SaveSkinData();
            SkinScroller.Instance.LoadSkinData();
            SkinScroller.Instance.ReloadCell();
            Debug.Log($"Ostit skinin hintaan {skins[i].price}, sinulla on rahaa vielä {GameManager.Instance.money}");

        } 
        else if(!skins[i].owned)
            Debug.Log($"Sinulta puuttuu vielä {skins[i].price - GameManager.Instance.money}");

    }
    public void ThemeButton(int i)
    {
        ThemeScroller.Themes[] themes = ThemeScroller.Instance.themes;
        // Debug.Log($"Index : {i}, Price : {skins[i].price}, Name : {skins[i].name}, Owned : {skins[i].owned}");

        if (GameManager.Instance.money >= themes[i].price && !themes[i].owned)
        {
            GameManager.Instance.SetMoney(themes[i].price);
            themes[i].owned = true;
            ThemeScroller.Instance.SaveThemeData();
            ThemeScroller.Instance.LoadThemeData();
            ThemeScroller.Instance.ReloadCell();
            Debug.Log($"Ostit teeman hintaan {themes[i].price}, sinulla on rahaa vielä {GameManager.Instance.money}");

        } 
        else if(!themes[i].owned)
            Debug.Log($"Sinulta puuttuu vielä {themes[i].price - GameManager.Instance.money}");

    }

    public void CloseApplication()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
                Application.Quit();
#endif
    }


}
