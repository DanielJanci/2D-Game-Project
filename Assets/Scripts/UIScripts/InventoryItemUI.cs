using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InventoryItemUI : MonoBehaviour
{
    public ItemData itemData;
    public Image itemImage;
    public Button equipItemButton;
    public TMP_Text equipItemButtonText;
    public static UnityAction OnEquipItem;

    private GameDataManager _gameDataManager;
    
    private void Start()
    {
        _gameDataManager = FindObjectOfType<GameDataManager>();
    }

    public void OnEquipButton()
    {
        AudioManager.OnButtonClick?.Invoke();
        if (itemData.isEquipped)
        {
            itemData.isEquipped = false;        
            _gameDataManager.UserData.currentPlayerColor = ItemColor.ItemIDToColor(default);
        }
        else
        {
            foreach (var item in _gameDataManager.InventoryData.items)
            {
                if (itemData.itemId == item.itemId)
                {
                    itemData.isEquipped = true;
                }
                else
                {
                    item.isEquipped = false;
                }
            }
            _gameDataManager.UserData.currentPlayerColor = ItemColor.ItemIDToColor(itemData.itemId);
        }
        OnEquipItem?.Invoke();
    }
}
