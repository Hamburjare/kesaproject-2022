using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;


#if UNITY_EDITOR
using UnityEditor;
#endif



public class UIManager : MonoBehaviour
{

    public static UIManager Instance;

    [SerializeField]
    // Starts skin menu open
    GameObject _lastPressed;

    [SerializeField]
    GameObject _skinList;

    [SerializeField]
    GameObject _themeList;

    [SerializeField]
    GameObject _shopPanel;

    [SerializeField]
    GameObject _abilitiesPanel;

    [SerializeField]
    TextMeshProUGUI _infectionValueText;

    [SerializeField]
    TextMeshProUGUI _infectionButtonText;
    [SerializeField]
    GameObject _infectionButtonMoneyIcon;

    [SerializeField]
    TextMeshProUGUI _speedValueText;

    [SerializeField]
    TextMeshProUGUI _speedButtonText;
    [SerializeField]
    GameObject _speedButtonMoneyIcon;

    [SerializeField]
    TextMeshProUGUI _jumpValueText;

    [SerializeField]
    TextMeshProUGUI _jumpButtonText;
    [SerializeField]
    GameObject _jumpButtonMoneyIcon;

    [SerializeField]
    GameObject _buttons;

    [SerializeField]
    TextMeshProUGUI _skinPriceButtonText;

    [SerializeField]
    TextMeshProUGUI _themePriceButtonText;

    [SerializeField]
    TextMeshProUGUI _moneyPriceButtonText;

    [SerializeField]
    GameObject _moneyBuyConfirmObject;

    [SerializeField]
    TextMeshProUGUI _diamondsPriceButtonText;

    [SerializeField]
    GameObject _diamondsBuyConfirmObject;

    [SerializeField]
    TextMeshProUGUI _moneyText;

    [SerializeField]
    TextMeshProUGUI _diamondText;

    public GameObject skinBuyConfirmObject;

    public GameObject themeBuyConfirmObject;

    public int skinIndex;

    public int themeIndex;

    ulong _cost;
    ulong _amount;

    void Start()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        _lastPressed.GetComponent<Animator>().Play("ButtonUp");
        _moneyText.text = string.Format("{0, -15:N0}", GameManager.Instance.money);
        _diamondText.text = string.Format("{0, -15:N0}", GameManager.Instance.diamonds);
        _infectionValueText.text = $"{GameManager.Instance.infectionSpeed} x";
        AbilityPrice(GameManager.Instance.infectionSpeed, "infection");
        AbilityPrice(GameManager.Instance.jumpForce, "jump");
        AbilityPrice(GameManager.Instance.playerSpeed, "speed");


    }

    void Update()
    {
        _moneyText.text = string.Format("{0, -15:N0}", GameManager.Instance.money);
        _diamondText.text = string.Format("{0, -15:N0}", GameManager.Instance.diamonds);
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

            // Abilities Button
            case 1:
                _abilitiesPanel.SetActive(true);
                _skinList.SetActive(false);
                _themeList.SetActive(false);
                _shopPanel.SetActive(false);

                CheckLastPress(justPressed);

                break;

            // Skin Button
            case 2:
                _skinList.SetActive(true);
                _themeList.SetActive(false);
                _shopPanel.SetActive(false);
                _abilitiesPanel.SetActive(false);

                CheckLastPress(justPressed);
                break;

            // Theme Button
            case 3:
                _themeList.SetActive(true);
                _skinList.SetActive(false);
                _shopPanel.SetActive(false);
                _abilitiesPanel.SetActive(false);

                CheckLastPress(justPressed);
                break;

            // Shop Button
            case 4:
                _shopPanel.SetActive(true);
                _skinList.SetActive(false);
                _themeList.SetActive(false);
                _abilitiesPanel.SetActive(false);

                CheckLastPress(justPressed);
                break;

        }
    }

    void CheckLastPress(GameObject justPressed)
    {
        Animator _animatorActive = justPressed.GetComponent<Animator>();
        Animator _animatorPressed = _lastPressed.GetComponent<Animator>();
        Animator _animatorShop = _buttons.GetComponent<Animator>();

        if (justPressed.name == "ShopButton" && justPressed != _lastPressed)
        {
            _animatorShop.Play("ButtonsDown");
        }
        else if (_lastPressed.name == "ShopButton" && justPressed != _lastPressed)
        {
            _animatorShop.Play("ButtonsUp");
        }

        /* Checking if the last button pressed is the same as the current button pressed. If it is not
        the same, it will move the last button pressed down 15 units and move the current button
        pressed up 15 units. */
        if (_lastPressed != justPressed)
        {
            _animatorActive.Play("ButtonUp");
            _animatorPressed.Play("ButtonDown");
            // lastPressed.transform.position = new Vector3(lastPressed.transform.position.x, lastPressed.transform.position.y - 15);
            // justPressed.transform.position = new Vector3(justPressed.transform.position.x, justPressed.transform.position.y + 15);
        }
        _lastPressed = justPressed;
    }


    public void SkinButton()
    {
        SkinScroller.Skins[] skins = SkinScroller.Instance.skins;

        int i = skinIndex;

        if (!skins[i].owned)
        {
            _skinPriceButtonText.text = string.Format("{0, -15:N0}", skins[i].price);
            skinBuyConfirmObject.SetActive(true);
            return;
        }

        SelectSkin(skins, i);

    }
    public void ThemeButton()
    {
        ThemeScroller.Themes[] themes = ThemeScroller.Instance.themes;

        int i = themeIndex;

        if (!themes[i].owned)
        {
            _themePriceButtonText.text = string.Format("{0, -15:N0}", themes[i].price);
            themeBuyConfirmObject.SetActive(true);
            return;
        }

        SelectTheme(themes, i);
    }

    void SelectSkin(SkinScroller.Skins[] skins, int i)
    {
        for (int j = 0; j < skins.Length; j++)
        {
            skins[j].selected = false;
        }

        skins[i].selected = true;
        SkinScroller.Instance.ReloadCell();
        SkinScroller.Instance.SaveSkinData();
        AbilityPrice(GameManager.Instance.infectionSpeed, "infection");
        AbilityPrice(GameManager.Instance.jumpForce, "jump");
        AbilityPrice(GameManager.Instance.playerSpeed, "speed");


    }

    void SelectTheme(ThemeScroller.Themes[] themes, int i)
    {
        for (int j = 0; j < themes.Length; j++)
        {
            themes[j].selected = false;
        }

        themes[i].selected = true;
        ThemeScroller.Instance.ReloadCell();
        ThemeScroller.Instance.SaveThemeData();
    }

    public void BuyTheme()
    {
        themeBuyConfirmObject.SetActive(false);
        int i = themeIndex;
        ThemeScroller.Themes[] themes = ThemeScroller.Instance.themes;
        if (GameManager.Instance.money >= themes[i].price && !themes[i].owned)
        {
            GameManager.Instance.SetMoney('-', themes[i].price);
            themes[i].owned = true;
            ThemeScroller.Instance.SaveThemeData();
            ThemeScroller.Instance.ReloadCell();
            Debug.Log($"Ostit teeman hintaan {themes[i].price}, sinulla on rahaa vielä {GameManager.Instance.money}");

        }
        else if (!themes[i].owned)
            Debug.Log($"Sinulta puuttuu vielä {themes[i].price - GameManager.Instance.money}");

        SelectTheme(themes, i);
    }

    public void BuySkin()
    {
        skinBuyConfirmObject.SetActive(false);
        int i = skinIndex;
        SkinScroller.Skins[] skins = SkinScroller.Instance.skins;
        if (GameManager.Instance.money >= skins[i].price && !skins[i].owned)
        {
            GameManager.Instance.SetMoney('-', skins[i].price);
            skins[i].owned = true;
            SkinScroller.Instance.SaveSkinData();
            SkinScroller.Instance.ReloadCell();
            Debug.Log($"Ostit skinin hintaan {skins[i].price}, sinulla on rahaa vielä {GameManager.Instance.money}");

        }
        else if (!skins[i].owned)
            Debug.Log($"Sinulta puuttuu vielä {skins[i].price - GameManager.Instance.money}");

        SelectSkin(skins, i);
    }

    public void BuyMoneyConfirm(int i)
    {

        switch (i)
        {
            case 0:
                _cost = 100;
                if (_cost <= GameManager.Instance.diamonds)
                {
                    _amount = 100000;
                    _moneyBuyConfirmObject.SetActive(true);
                    _moneyPriceButtonText.text = string.Format("{0, -15:N0}", _cost);
                }

                // GameManager.Instance.SetDiamonds('-', cost);
                // GameManager.Instance.SetMoney('+', amount);
                break;
            case 1:
                _cost = 1000;
                if (_cost <= GameManager.Instance.diamonds)
                {
                    _amount = 2000000;
                    _moneyBuyConfirmObject.SetActive(true);
                    _moneyPriceButtonText.text = string.Format("{0, -15:N0}", _cost);
                }
                break;
            case 2:
                _cost = 2000;
                if (_cost <= GameManager.Instance.diamonds)
                {
                    _amount = 5000000;
                    _moneyBuyConfirmObject.SetActive(true);
                    _moneyPriceButtonText.text = string.Format("{0, -15:N0}", _cost);
                }
                break;
            case 3:
                _cost = 3000;
                if (_cost <= GameManager.Instance.diamonds)
                {
                    _amount = 10000000;
                    _moneyBuyConfirmObject.SetActive(true);
                    _moneyPriceButtonText.text = string.Format("{0, -15:N0}", _cost);
                }
                break;
        }

    }

    public void BuyMoney()
    {
        _moneyBuyConfirmObject.SetActive(false);
        GameManager.Instance.SetDiamonds('-', _cost);
        GameManager.Instance.SetMoney('+', _amount);
    }

    public void BuyDiamondsConfirm(int i)
    {
        string dCost;
        switch (i)
        {
            case 0:
                dCost = "2,83 €";

                _amount = 250;
                _diamondsBuyConfirmObject.SetActive(true);
                _diamondsPriceButtonText.text = dCost;


                // GameManager.Instance.SetDiamonds('-', cost);
                // GameManager.Instance.SetMoney('+', amount);
                break;
            case 1:
                dCost = "10,26 €";

                _amount = 1000;
                _diamondsBuyConfirmObject.SetActive(true);
                _diamondsPriceButtonText.text = dCost;
                break;
            case 2:
                dCost = "23,97 €";
                _amount = 2500;
                _diamondsBuyConfirmObject.SetActive(true);
                _diamondsPriceButtonText.text = dCost;
                break;
            case 3:
                dCost = "51,14 €";
                _amount = 5000;
                _diamondsBuyConfirmObject.SetActive(true);
                _diamondsPriceButtonText.text = dCost;
                break;
        }

    }

    public void BuyDiamonds()
    {
        _diamondsBuyConfirmObject.SetActive(false);
        GameManager.Instance.SetDiamonds('+', _amount);
    }

    void AbilityPrice(double a, string ability)
    {
        switch (ability)
        {
            case "infection":
                _infectionValueText.text = $"{a} x";

                if (a == 2.0f)
                {
                    _infectionButtonText.text = "MAX";
                    _infectionButtonMoneyIcon.SetActive(false);
                    break;
                }
                _infectionButtonMoneyIcon.SetActive(true);
                _infectionButtonText.text = string.Format("{0, -15:N0}", a * 135000); ;

                break;
            case "speed":
                _speedValueText.text = $"{a} x";

                if (a == 2.0f)
                {
                    _speedButtonText.text = "MAX";
                    _speedButtonMoneyIcon.SetActive(false);
                    break;
                }
                _speedButtonMoneyIcon.SetActive(true);
                _speedButtonText.text = string.Format("{0, -15:N0}", a * 145000); ;

                break;
            case "jump":
                _jumpValueText.text = $"{a} x";

                if (a == 2.0f)
                {
                    _jumpButtonText.text = "MAX";
                    _jumpButtonMoneyIcon.SetActive(false);
                    break;
                }
                _jumpButtonMoneyIcon.SetActive(true);
                _jumpButtonText.text = string.Format("{0, -15:N0}", a * 175000); ;

                break;
        }

    }

    public void InfectionSpeed()
    {
        SkinScroller.Skins[] skins = SkinScroller.Instance.skins;
        if (GameManager.Instance.money >= GameManager.Instance.infectionSpeed * 145000)
        {
            if (GameManager.Instance.infectionSpeed < 2.0)
            {
                ulong cost = System.Convert.ToUInt64(GameManager.Instance.infectionSpeed * 145000);
                GameManager.Instance.SetMoney('-', cost);
                GameManager.Instance.infectionSpeed += 0.2f;
                GameManager.Instance.infectionSpeed = Math.Round(GameManager.Instance.infectionSpeed, 2);

                skins[GameManager.Instance.selectedSkinIndex].infectionSpeed = GameManager.Instance.infectionSpeed;
                SkinScroller.Instance.SaveSkinData();
            }
        }
        AbilityPrice(GameManager.Instance.infectionSpeed, "infection");
    }

    public void PlayerSpeed()
    {
        SkinScroller.Skins[] skins = SkinScroller.Instance.skins;
        if (GameManager.Instance.money >= GameManager.Instance.playerSpeed * 135000)
        {
            if (GameManager.Instance.playerSpeed < 2.0)
            {
                ulong cost = System.Convert.ToUInt64(GameManager.Instance.playerSpeed * 135000);
                GameManager.Instance.SetMoney('-', cost);
                GameManager.Instance.playerSpeed += 0.1f;
                GameManager.Instance.playerSpeed = Math.Round(GameManager.Instance.playerSpeed, 2);
                skins[GameManager.Instance.selectedSkinIndex].playerSpeed = GameManager.Instance.playerSpeed;
                SkinScroller.Instance.SaveSkinData();
            }
        }
        AbilityPrice(GameManager.Instance.playerSpeed, "speed");
    }
    public void JumpForce()
    {
        SkinScroller.Skins[] skins = SkinScroller.Instance.skins;
        if (GameManager.Instance.money >= GameManager.Instance.jumpForce * 175000)
        {
            if (GameManager.Instance.jumpForce < 2.0)
            {
                ulong cost = System.Convert.ToUInt64(GameManager.Instance.jumpForce * 175000);
                GameManager.Instance.SetMoney('-', cost);
                GameManager.Instance.jumpForce += 0.05f;
                GameManager.Instance.jumpForce = Math.Round(GameManager.Instance.jumpForce, 2);
                skins[GameManager.Instance.selectedSkinIndex].jumpForce = GameManager.Instance.jumpForce;
                SkinScroller.Instance.SaveSkinData();
            }
        }
        AbilityPrice(GameManager.Instance.jumpForce, "jump");
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
