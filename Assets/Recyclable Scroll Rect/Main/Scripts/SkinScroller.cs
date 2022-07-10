using System.Collections.Generic;
using System;
using UnityEngine;
using PolyAndCode.UI;

/// <summary>
/// Demo controller class for Recyclable Scroll Rect. 
/// A controller class is responsible for providing the scroll rect with datasource. Any class can be a controller class. 
/// The only requirement is to inherit from IRecyclableScrollRectDataSource and implement the interface methods
/// </summary>

//Dummy Data model for demostraion
public struct SkinInfo
{
    public string Name;
    public string Price;
    public Sprite Image;
}

public class SkinScroller : MonoBehaviour, IRecyclableScrollRectDataSource
{
    [SerializeField]
    RecyclableScrollRect _recyclableScrollRect;

    [SerializeField]
    private int _dataLength;

    //Dummy data List
    List<SkinInfo> _contactList = new List<SkinInfo>();

    [SerializeField]
    List<Sprite> _sprites = new List<Sprite>();

    //Recyclable scroll rect's data source must be assigned in Awake.
    private void Awake()
    {
        _dataLength = _sprites.Count;

        InitData();
        _recyclableScrollRect.DataSource = this;
    }

    //Initialising _contactList with dummy data 
    private void InitData()
    {
        if (_contactList != null) _contactList.Clear();

        string[] skinNames = { "Zombie", "Pirate Zombie", "Skeleton", "Pirate Skeleton" };
        long[] priceTags = { 0, 10000, 99999, 999999999999 };
        for (int i = 0; i < _dataLength; i++)
        {
            SkinInfo obj = new SkinInfo();

            priceTags[i] = Math.Abs(priceTags[i]);
            if (priceTags[i] == 0)
            {
                obj.Price = "Owned";
            }
            else obj.Price = string.Format("{0, -15:N0}", priceTags[i]);

            obj.Name = skinNames[i];
            obj.Image = _sprites[i];

            _contactList.Add(obj);
        }
    }

    #region DATA-SOURCE

    /// <summary>
    /// Data source method. return the list length.
    /// </summary>
    public int GetItemCount()
    {
        return _contactList.Count;
    }

    /// <summary>
    /// Data source method. Called for a cell every time it is recycled.
    /// Implement this method to do the necessary cell configuration.
    /// </summary>
    public void SetCell(ICell cell, int index)
    {
        //Casting to the implemented Cell
        var item = cell as SkinCell;
        item.ConfigureCell(_contactList[index], index);
    }

    #endregion
}