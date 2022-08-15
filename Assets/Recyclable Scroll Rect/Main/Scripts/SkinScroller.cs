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

    public bool locked;

    public string owned;

}

public class SkinScroller : MonoBehaviour, IRecyclableScrollRectDataSource
{
    [SerializeField]
    RecyclableScrollRect _recyclableScrollRect;

    [SerializeField]
    GameManager _gameManager;

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

            obj.locked = true;
            obj.owned = "";
            if (skins[i].owned)
            {
                obj.owned = "Owned";
                if (skins[i].selected)
                {
                    obj.owned = "Selected";
                    _gameManager.selectedSkinIndex = i;
                    _gameManager.infectionSpeed = skins[i].infectionSpeed;
                    _gameManager.playerSpeed = skins[i].playerSpeed;
                    _gameManager.jumpForce = skins[i].jumpForce;    
                }
                obj.locked = false;
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
        public bool selected;
        public double infectionSpeed;
        public double playerSpeed;
        public double jumpForce;
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
                skinInstance[i].selected = skins[i].selected;
                skinInstance[i].infectionSpeed = skins[i].infectionSpeed;
                skinInstance[i].playerSpeed = skins[i].playerSpeed;
                skinInstance[i].jumpForce = skins[i].jumpForce;
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
            string json = "{\"Items\":[{\"price\":0,\"name\":\"Zombie\",\"owned\":true,\"selected\":true,\"infectionSpeed\":1.0,\"playerSpeed\":1.0,\"jumpForce\":1.0},{\"price\":10000,\"name\":\"PirateZombie\",\"owned\":true,\"selected\":false,\"infectionSpeed\":1.0,\"playerSpeed\":1.0,\"jumpForce\":1.0},{\"price\":99999,\"name\":\"Skeleton\",\"owned\":false,\"selected\":false,\"infectionSpeed\":1.0,\"playerSpeed\":1.0,\"jumpForce\":1.0},{\"price\":999999999999,\"name\":\"PirateSkeleton\",\"owned\":false,\"selected\":false,\"infectionSpeed\":1.0,\"playerSpeed\":1.0,\"jumpForce\":1.0}]}";
            skins = JsonHelper.FromJson<Skins>(json);
            File.WriteAllText(path, json);
        }
    }

}

