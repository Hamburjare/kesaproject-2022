﻿using UnityEngine;
using UnityEngine.UI;
using TMPro;
using PolyAndCode.UI;

//Cell class for demo. A cell in Recyclable Scroll Rect must have a cell class inheriting from ICell.
//The class is required to configure the cell(updating UI elements etc) according to the data during recycling of cells.
//The configuration of a cell is done through the DataSource SetCellData method.
//Check RecyclableScrollerDemo class
public class SkinCell : MonoBehaviour, ICell
{
    //UI
    public TextMeshProUGUI priceLabel;
    public TextMeshProUGUI nameLabel;
    public Image ImageLabel;

    public GameObject lockedPanel;

    public TextMeshProUGUI ownedLabel;

    public Button cellButton;

    //Model
    private SkinInfo _SkinInfo;
    private int _cellIndex;


    private void Start()
    {
        //Can also be done in the inspector
        cellButton.GetComponent<Button>().onClick.AddListener(ButtonListener);
    }

    //This is called from the SetCell method in DataSource
    public void ConfigureCell(SkinInfo SkinInfo, int cellIndex)
    {
        _cellIndex = cellIndex;
        _SkinInfo = SkinInfo;

        priceLabel.text = SkinInfo.price;
        nameLabel.text = SkinInfo.name;
        ImageLabel.sprite = SkinInfo.image;
        lockedPanel.SetActive(SkinInfo.locked);
        ownedLabel.text = SkinInfo.owned;
    }

    private void ButtonListener()
    {
        UIManager.Instance.skinIndex = _cellIndex;
        UIManager.Instance.SkinButton();
    }

}
