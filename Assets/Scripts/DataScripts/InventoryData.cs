using System;
using System.Collections.Generic;
using PlayFab.ClientModels;
using UnityEngine.Serialization;

[Serializable]
public class InventoryData
{
    public int coins;
    public List<ItemData> items;
    public bool isLoaded;
    
    public InventoryData()
    {
        isLoaded = false;
        items = new List<ItemData>();
    }
}