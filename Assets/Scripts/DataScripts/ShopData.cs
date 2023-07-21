using System;
using System.Collections.Generic;
using PlayFab.ClientModels;
using UnityEngine.Serialization;

[Serializable]
public class ShopData
{
    public List<ItemData> items;
    public bool isLoaded;
    public ShopData()
    {
        isLoaded = false;
        items = new List<ItemData>();
    }
}
