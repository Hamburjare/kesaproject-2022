using System.Collections.Generic;
using System;
using UnityEngine;
using PolyAndCode.UI;
using System.IO;

/// <summary>
/// Demo controller class for Recyclable Scroll Rect. 
/// A controller class is responsible for providing the scroll rect with datasource. Any class can be a controller class. 
/// The only requirement is to inherit from IRecyclableScrollRectDataSource and implement the interface methods
/// </summary>

//Dummy Data model for demostraion
public struct ThemeInfo
{
    public string name;
    public string price;
    public Sprite image;
    public bool locked;

    public string owned;

}

public class ThemeScroller : MonoBehaviour, IRecyclableScrollRectDataSource
{
    [SerializeField]
    RecyclableScrollRect _recyclableScrollRect;

    [SerializeField]
    private int _dataLength;

    //Dummy data List
    List<ThemeInfo> _contactList = new List<ThemeInfo>();

    [SerializeField]
    List<Sprite> _sprites = new List<Sprite>();

    public Themes[] themes { get; set; }

    public static ThemeScroller Instance;

    //Recyclable scroll rect's data source must be assigned in Awake.
    private void Awake()
    {

        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        LoadThemeData();
        _dataLength = themes.Length;

        InitData();
        _recyclableScrollRect.DataSource = this;

    }

    //Initialising _contactList with dummy data 
    private void InitData()
    {
        if (_contactList != null) _contactList.Clear();

        for (int i = 0; i < _dataLength; i++)
        {
            ThemeInfo obj = new ThemeInfo();

            obj.locked = true;
            obj.owned = "";
            if (themes[i].owned)
            {
                obj.owned = "Owned";
                if (themes[i].selected)
                {
                    obj.owned = "Selected";
                }
                obj.locked = false;
            }
            else obj.price = string.Format("{0, -15:N0}", themes[i].price);

            obj.name = themes[i].name;
            obj.image = _sprites[i];

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
        var item = cell as ThemeCell;
        item.ConfigureCell(_contactList[index], index);
    }

    #endregion

    public void ReloadCell()
    {
        InitData();
        _recyclableScrollRect.ReloadData();
    }

    [System.Serializable]

    public class Themes
    {
        public ulong price;
        public string name;
        public bool owned;
        public bool selected;
    }

    public void SaveThemeData()
    {
        Themes[] themeInstance = new Themes[_dataLength];
        string path = Application.persistentDataPath + "/themes.json";
        if (File.Exists(path))
        {
            for (int i = 0; i < _dataLength; i++)
            {
                themeInstance[i] = new Themes();
                themeInstance[i].name = themes[i].name;
                themeInstance[i].owned = themes[i].owned;
                themeInstance[i].price = themes[i].price;
                themeInstance[i].selected = themes[i].selected;

            }

            //Convert to JSON
            string json = JsonHelper.ToJson(themeInstance, true);
            File.WriteAllText(path, json);
        }


    }

    public void LoadThemeData()
    {

        string path = Application.persistentDataPath + "/themes.json";
        if (File.Exists(path))
        {

            string json = File.ReadAllText(path);
            themes = JsonHelper.FromJson<Themes>(json);
        }
        else
        {
            // https://jsontostring.com/
            string json = "{\"Items\":[{\"price\":0,\"name\":\"Green\",\"owned\":true,\"selected\":true},{\"price\":10000,\"name\":\"Beige\",\"owned\":false,\"selected\":false},{\"price\":99999,\"name\":\"Black\",\"owned\":false,\"selected\":false},{\"price\":999999999999,\"name\":\"NeonPurple\",\"owned\":false,\"selected\":false},{\"price\":9999999999999,\"name\":\"Christmas\",\"owned\":false,\"selected\":false}]}";
            themes = JsonHelper.FromJson<Themes>(json);
            File.WriteAllText(path, json);
        }

    }
}