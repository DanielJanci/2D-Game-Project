using System;
using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.Json;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ShopUI : MonoBehaviour
{
    public TMP_Text coinsValue;
    public GameObject shopItemPreFab;
    public Transform shopItemsParent;
    
    private int _coins;
    private GameDataManager _gameDataManager;

    public void Start()
    {
        _gameDataManager = FindObjectOfType<GameDataManager>();
        MainMenuUI.OnShopClicked += ShowShop;
        GameDataManager.OnPurchasedItem += ShowShop;
    }

    private void ShowShop()
    {
        StartCoroutine(GetLoadedShop());
        
    }
    
    private IEnumerator GetLoadedShop()
    {
        yield return new WaitUntil(() => _gameDataManager.ShopData != null);
        yield return new WaitUntil(() => _gameDataManager.InventoryData != null);
        yield return new WaitUntil(() => _gameDataManager.ShopData.isLoaded);
        yield return new WaitUntil(() => _gameDataManager.InventoryData.isLoaded);
        GetShop();
    }

    private void GetShop()
    {
        coinsValue.text = $"COINS: { _gameDataManager.InventoryData.coins}";
        foreach (Transform row in shopItemsParent)
        {
            Destroy(row.gameObject);
        }
        foreach (var item in _gameDataManager.ShopData.items)
        {
            var findItem = _gameDataManager.InventoryData.items.Find(obj => obj.itemId == item.itemId);
            if (findItem != null)
            {
                item.isOwned = true;
            }
            GameObject newGameObject = Instantiate(shopItemPreFab, shopItemsParent);
            var itemHolder = newGameObject.GetComponent<ShopItemUI>();
            itemHolder.itemData = item;
            itemHolder.itemPriceText.text = $"Price: {itemHolder.itemData.price}";
            itemHolder.itemImage.color = ItemColor.ItemIDToColor(itemHolder.itemData.itemId);
            if (itemHolder.itemData.isOwned)
            {
                itemHolder.buyButton.interactable = false;
                itemHolder.buyButtonText.text = "Owned";
            }
            else
            {
                itemHolder.buyButton.interactable = true;
                itemHolder.buyButtonText.text = "Buy";
            }
        }
    }
    
    private void OnDestroy()
    {
        MainMenuUI.OnShopClicked -= ShowShop;
        GameDataManager.OnPurchasedItem -= ShowShop;
    }

    public void OnReturnButton()
    {
        AudioManager.OnButtonClick?.Invoke();
        gameObject.SetActive(false);
    }
}
