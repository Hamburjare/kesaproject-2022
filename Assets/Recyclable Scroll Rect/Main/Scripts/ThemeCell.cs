using UnityEngine;
using UnityEngine.UI;
using TMPro;
using PolyAndCode.UI;

//Cell class for demo. A cell in Recyclable Scroll Rect must have a cell class inheriting from ICell.
//The class is required to configure the cell(updating UI elements etc) according to the data during recycling of cells.
//The configuration of a cell is done through the DataSource SetCellData method.
//Check RecyclableScrollerDemo class
public class ThemeCell : MonoBehaviour, ICell
{
    //UI
    public TextMeshProUGUI priceLabel;
    public TextMeshProUGUI nameLabel;
    public Image ImageLabel;

    public Button cellButton;

    //Model
    private ThemeInfo _ThemeInfo;
    private int _cellIndex;

    private void Start()
    {
        //Can also be done in the inspector
        cellButton.GetComponent<Button>().onClick.AddListener(ButtonListener);
    }

    //This is called from the SetCell method in DataSource
    public void ConfigureCell(ThemeInfo ThemeInfo,int cellIndex)
    {
        _cellIndex = cellIndex;
        _ThemeInfo = ThemeInfo;

        priceLabel.text = ThemeInfo.Price;
        nameLabel.text = ThemeInfo.Name;
        ImageLabel.sprite = ThemeInfo.Image;
    }

    
    private void ButtonListener()
    {
        Debug.Log($"Index : {_cellIndex}, Price : {_ThemeInfo.Price}, Name : {_ThemeInfo.Name}, Image Name : {_ThemeInfo.Image.name}");
    }
}
