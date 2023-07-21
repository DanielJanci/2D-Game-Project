using System;
using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour
{
    public ItemData itemData;
    public Image itemImage;
    public Button buyButton;
    public TMP_Text buyButtonText;
    public TMP_Text itemPriceText;

    private GameDataManager _gameDataManager;

    public void Start()
    {
        _gameDataManager = FindObjectOfType<GameDataManager>();
    }


    public void OnBuyButton()
    {
        AudioManager.OnButtonClick?.Invoke();
        _gameDataManager.PurchaseItem(itemData);
    }
    
    
}
