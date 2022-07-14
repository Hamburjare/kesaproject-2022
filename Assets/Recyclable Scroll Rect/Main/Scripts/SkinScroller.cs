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
    public string name;
    public string price;
    public Sprite image;
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

    public Skins[] skins { get; set; }

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


            if (skins[i].owned)
            {
                obj.price = "Owned";
            }
            else obj.price = string.Format("{0, -15:N0}", skins[i].price);

            obj.name = skins[i].name;
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
        var item = cell as SkinCell;
        item.ConfigureCell(_contactList[index], index);
    }


    #endregion


    public void ReloadCell()
    {
        InitData();
        _recyclableScrollRect.ReloadData();
    }


    [System.Serializable]

    public class Skins
    {
        public ulong price;
        public string name;
        public bool owned;
    }

    public void SaveSkinData()
    {
        Skins[] skinInstance = new Skins[_dataLength];

        string path = Application.persistentDataPath + "/skins.json";
        if (File.Exists(path))
        {
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



    }

    public void LoadSkinData()
    {

        string path = Application.persistentDataPath + "/skins.json";
        if (File.Exists(path))
        {

            string json = File.ReadAllText(path);
            skins = JsonHelper.FromJson<Skins>(json);
        }
        else
        {
            // https://jsontostring.com/
            string json = "{\"Items\":[{\"price\":0,\"name\":\"Zombie\",\"owned\":true},{\"price\":10000,\"name\":\"Pirate Zombie\",\"owned\":false},{\"price\":99999,\"name\":\"Skeleton\",\"owned\":false},{\"price\":999999999999,\"name\":\"Pirate Skeleton\",\"owned\":false}]}";
            skins = JsonHelper.FromJson<Skins>(json);
            File.WriteAllText(path, json);
        }
    }

}

