using System;
using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.Json;
using TMPro;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public TMP_Text coinsValue;
    public GameObject inventoryItemPreFab;
    public Transform inventoryItemsParent;

    private GameDataManager _gameDataManager;

    private void Start()
    {
        _gameDataManager = FindObjectOfType<GameDataManager>();
        MainMenuUI.OnInventoryClicked += ShowInventory;
        InventoryItemUI.OnEquipItem += ShowInventory;
    }

    private void ShowInventory()
    {
        StartCoroutine(GetLoadedInventory());
    }
    
    private IEnumerator GetLoadedInventory()
    {
        yield return new WaitUntil(() => _gameDataManager.InventoryData != null);
        yield return new WaitUntil(() => _gameDataManager.InventoryData.isLoaded);
        GetInventory();
    }
    
    private void GetInventory()
    {
        coinsValue.text = $"COINS: { _gameDataManager.InventoryData.coins}";
        foreach (Transform row in inventoryItemsParent)
        {
            Destroy(row.gameObject);
        }
        
        foreach (var item in _gameDataManager.InventoryData.items)
        {
            GameObject newGameObject = Instantiate(inventoryItemPreFab, inventoryItemsParent);
            var itemHolder = newGameObject.GetComponent<InventoryItemUI>();
            itemHolder.itemData = item;
            itemHolder.itemImage.color = ItemColor.ItemIDToColor(itemHolder.itemData.itemId);
            if (itemHolder.itemData.isEquipped)
            {
                itemHolder.equipItemButtonText.text = "Unequip";
            }
            else
            {
                itemHolder.equipItemButtonText.text = "Equip";
            }
        }
    }
    
    private void OnDestroy()
    {
        MainMenuUI.OnInventoryClicked -= ShowInventory;
        InventoryItemUI.OnEquipItem -= ShowInventory;
    }
    
    public void OnReturnButton()
    {
        AudioManager.OnButtonClick?.Invoke();
        //gameObject.SetActive(false);
    }
}
