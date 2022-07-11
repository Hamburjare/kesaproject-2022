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

    string _skinData;

    public Skins[] skins {get; set;}

    public static SkinScroller Instance;

    //Recyclable scroll rect's data source must be assigned in Awake.
    private void Awake()
    {

        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        LoadSkinData();
        _dataLength = skins.Length;
        SaveSkinData();


        InitData();
        _recyclableScrollRect.DataSource = this;
    }

    //Initialising _contactList with dummy data 
    private void InitData()
    {
        if (_contactList != null) _contactList.Clear();

        for (int i = 0; i < _dataLength; i++)
        {
            SkinInfo obj = new SkinInfo();

            skins[i].price = Math.Abs(skins[i].price);
            if (skins[i].owned)
            {
                obj.Price = "Owned";
            }
            else obj.Price = string.Format("{0, -15:N0}", skins[i].price);

            obj.Name = skins[i].name;
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


    [System.Serializable]

    public class Skins
    {
        public long price;
        public string name;
        public bool owned;
    }

    public void SaveSkinData()
    {
        Skins[] skinInstance = new Skins[_dataLength];

        for (int i = 0; i < _dataLength; i++)
        {
            skinInstance[i] = new Skins();
            skinInstance[i].name = skins[i].name;
            skinInstance[i].owned = skins[i].owned;
            skinInstance[i].price = skins[i].price;
        }



        //Convert to JSON
        string json = JsonHelper.ToJson(skinInstance, true);
        File.WriteAllText(Application.persistentDataPath + "/skins.json", json);
    }

    public void LoadSkinData()
    {

        string path = Application.persistentDataPath + "/skins.json";
        if (File.Exists(path))
        {

            string json = File.ReadAllText(path);
            skins = JsonHelper.FromJson<Skins>(json);

        }
    }

}