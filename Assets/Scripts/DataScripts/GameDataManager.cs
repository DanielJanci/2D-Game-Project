using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using PlayFab;
using PlayFab.ClientModels;
using Unity.VisualScripting;
using UnityEngine.Events;

public class GameDataManager : MonoBehaviour
{
    private RecordScoreData _recordScoreData;
    private LeaderboardData _leaderboardData;
    private SettingsData _settingsData;
    private InventoryData _inventoryData;
    private ShopData _shopData;
    private UserData _userData;
    private DifficultyData _difficultyData;
    
    public RecordScoreData RecordScoreData => _recordScoreData;
    public LeaderboardData LeaderboardData => _leaderboardData;
    public SettingsData SettingsData => _settingsData;
    public InventoryData InventoryData => _inventoryData;
    public ShopData ShopData => _shopData;
    public UserData UserData => _userData;
    public DifficultyData DifficultyData => _difficultyData;

    public static UnityAction<string> OnRecordScoreDataLoaded;
    public static UnityAction<string> OnLeaderboardDataLoaded;
    public static UnityAction OnSettingsDataLoaded;
    public static UnityAction OnInventoryDataLoaded;
    public static UnityAction OnShopDataLoaded;
    public static UnityAction OnUserLoggedIn;
    public static UnityAction OnPurchasedItem;
    


    public static GameDataManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        _userData = new UserData();
        _shopData = new ShopData();
        _inventoryData = new InventoryData();
        _settingsData = new SettingsData();
        _recordScoreData = new RecordScoreData();
        _leaderboardData = new LeaderboardData();
        _difficultyData = new DifficultyData();

        LoginAndGetUserData();
        OnUserLoggedIn += LoadAll;
    }

    public void LoadAll()
    {
        LoadRecordScoreData("ScoreLevel01");
        LoadRecordScoreData("ScoreLevel02");
        LoadRecordScoreData("ScoreLevel03");
        LoadLeaderboardData("ScoreLevel01");
        LoadLeaderboardData("ScoreLevel02");
        LoadLeaderboardData("ScoreLevel03");
        LoadDifficultyData();
        LoadSettingsData();
        LoadInventoryData();
        LoadShopData();
    }
    
    /* USER DATA */
    private void LoginAndGetUserData()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            }
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginError);
    }
    private void OnLoginSuccess(LoginResult result)
    {
        _userData.userPlayFabId = result.PlayFabId;
        _userData.isLoggedIn = true;
        
        if (result.InfoResultPayload.PlayerProfile != null)
        {
            _userData.userDisplayName = result.InfoResultPayload.PlayerProfile.DisplayName;
        }
        OnUserLoggedIn?.Invoke();
        Debug.Log("Successful login/account create.");
    }
    private void OnLoginError(PlayFabError error)
    {
        Debug.Log("Error while logging in/creating account.");
        Debug.Log(error.GenerateErrorReport());
    }

    
    public void UpdatePlayFabUserName(string newName)
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = newName
        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnNicknameUpdateSuccess, OnNicknameUpdateError);
    }
    private void OnNicknameUpdateSuccess(UpdateUserTitleDisplayNameResult result)
    {
        _userData.userDisplayName = result.DisplayName;
        Debug.Log("Display name updated.");
    }

    private void OnNicknameUpdateError(PlayFabError error)
    {
        Debug.Log("Couldn't update display name.");
        Debug.Log(error.GenerateErrorReport());
    }
    
    
    
    /* DIFFICULTY DATA */

    public void LoadDifficultyData()
    {
        PlayFabClientAPI.GetTitleData(new GetTitleDataRequest(), OnGetTitleDataSuccess, OnGetTitleDataError);
    }
    private void OnGetTitleDataSuccess(GetTitleDataResult result)
    {
        if (result.Data != null)
        {
            if (result.Data.TryGetValue("secondsToMaxDifficulty", out var secondsToMaxDifficultyValue))
            {
                _difficultyData.secondsToMaxDifficulty = float.Parse(secondsToMaxDifficultyValue, CultureInfo.InvariantCulture.NumberFormat);
            }
            if (result.Data.TryGetValue("minObstacleSpeed", out var minObstacleSpeedValue))
            {
                _difficultyData.minObstacleSpeed = float.Parse(minObstacleSpeedValue, CultureInfo.InvariantCulture.NumberFormat);
            }
            if (result.Data.TryGetValue("maxObstacleSpeed", out var maxObstacleSpeedValue))
            {
                _difficultyData.maxObstacleSpeed = float.Parse(maxObstacleSpeedValue, CultureInfo.InvariantCulture.NumberFormat);
            }
            if (result.Data.TryGetValue("startIntervalBetweenSpawns", out var startIntervalBetweenSpawnsValue))
            {
                _difficultyData.startIntervalBetweenSpawns = float.Parse(startIntervalBetweenSpawnsValue, CultureInfo.InvariantCulture.NumberFormat);
            }
            if (result.Data.TryGetValue("endIntervalBetweenSpawns", out var endIntervalBetweenSpawnsValue))
            {
                _difficultyData.endIntervalBetweenSpawns = float.Parse(endIntervalBetweenSpawnsValue, CultureInfo.InvariantCulture.NumberFormat);
            }
            if (result.Data.TryGetValue("playerSpeed", out var playerSpeedValue))
            {
                _difficultyData.playerSpeed = float.Parse(playerSpeedValue, CultureInfo.InvariantCulture.NumberFormat);
            }
        }
    }
    private void OnGetTitleDataError(PlayFabError error)
    {
        Debug.Log("Title data couldn't load.");
        Debug.Log(error.GenerateErrorReport());
    }
    
    
    
    
    /* SHOP DATA */
    public void LoadShopData()
    {
        var request = new GetCatalogItemsRequest
        {
            CatalogVersion = "Skins"
        };
        PlayFabClientAPI.GetCatalogItems(request, OnGetShopItemsSuccess, OnGetShopItemsError);
    }
    private void OnGetShopItemsSuccess(GetCatalogItemsResult result)
    {

        foreach (var item in result.Catalog)
        {
            var findItem = _shopData.items.Find(obj => obj.itemId == item.ItemId);
            if (findItem == null)
            {
                _shopData.items.Add(new ItemData()
                {
                    catalog = item.CatalogVersion,
                    displayName = item.DisplayName,
                    itemId = item.ItemId,
                    price = (int)item.VirtualCurrencyPrices["CN"],
                    isEquipped = false,
                    isOwned = false
                });
            }
        }
        _shopData.isLoaded = true;
        OnShopDataLoaded?.Invoke();
        Debug.Log($"Shop items ({ShopData.items.Count}) loaded.");
    }

    private void OnGetShopItemsError(PlayFabError error)
    {
        Debug.Log("Shop items couldn't load.");
        Debug.Log(error.GenerateErrorReport());
    }
    
    public void PurchaseItem(ItemData itemData)
    {
        var request = new PurchaseItemRequest
        {
            CatalogVersion = itemData.catalog,
            ItemId = itemData.itemId,
            VirtualCurrency = "CN",
            Price = itemData.price
        };
        PlayFabClientAPI.PurchaseItem(request, OnPurchaseItemSuccess, OnPurchaseItemError);
    }

    private void OnPurchaseItemSuccess(PurchaseItemResult result)
    {
        foreach (var item in result.Items)
        {
            _inventoryData.coins -= (int)item.UnitPrice;
            _inventoryData.items.Add(new ItemData()
            {
                catalog = item.CatalogVersion,
                displayName = item.DisplayName,
                itemId = item.ItemId,
                price = (int)item.UnitPrice,
                isEquipped = false,
                isOwned = true
            });

            var findShopItem = _shopData.items.Find(obj => obj.itemId == item.ItemId);
            if (findShopItem != null)
            {
                findShopItem.isOwned = true;
            }
        }
        OnPurchasedItem?.Invoke();
        Debug.Log("Purchase completed.");
    }

    private void OnPurchaseItemError(PlayFabError error)
    {
        Debug.Log("Purchase failed.");
        Debug.Log(error.GenerateErrorReport());
    }



    /* INVENTORY DATA */
    public void LoadInventoryData()
    {
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), OnGetUserInventorySuccess, OnGetUserInventoryError);
    }
    private void OnGetUserInventorySuccess(GetUserInventoryResult result)
    {
        _inventoryData.coins = result.VirtualCurrency["CN"];
        
        foreach (var item in result.Inventory)
        {
            var findItem = _inventoryData.items.Find(obj => obj.itemId == item.ItemId);
            if (findItem == null)
            {
                _inventoryData.items.Add(new ItemData()
                {
                    catalog = item.CatalogVersion,
                    displayName = item.DisplayName,
                    itemId = item.ItemId,
                    price = (int)item.UnitPrice,
                    isEquipped = false,
                    isOwned = true,
                });
            }
        }
        _inventoryData.isLoaded = true;
        OnInventoryDataLoaded?.Invoke();
        Debug.Log($"Inventory items ({InventoryData.items.Count}) loaded.");
    }
    private void OnGetUserInventoryError(PlayFabError error)
    {
        Debug.Log("Cannot load players inventory.");
        Debug.Log(error.GenerateErrorReport());
    }

    public void AddCoinsToInventoryPLayFabData(int coins)
    {
        var request = new AddUserVirtualCurrencyRequest()
        {
            VirtualCurrency = "CN",
            Amount = coins
        };
        PlayFabClientAPI.AddUserVirtualCurrency(request, OnUpdateCoinsSuccess, OnUpdateCoinsError);
    }

    private void OnUpdateCoinsSuccess(ModifyUserVirtualCurrencyResult result)
    {
        _inventoryData.coins = result.Balance;
        Debug.Log("Players coins updated.");
    }

    private void OnUpdateCoinsError(PlayFabError error)
    {
        Debug.Log("Couldn't update players coins");
        Debug.Log(error.GenerateErrorReport());
    }
    
    
    /* SETTINGS DATA */
    public void LoadSettingsData()
    {
        string json;
        string path = Application.persistentDataPath + "/settings_data.json";
        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                json = reader.ReadToEnd();
            }
            JsonUtility.FromJsonOverwrite(json, _settingsData);
        }
        else
        {
            json = JsonUtility.ToJson(_settingsData);
            FileStream fileStream = new FileStream(path, FileMode.Create);
            using (StreamWriter writer = new StreamWriter(fileStream))
            {
                writer.Write(json);
            }
        }
        OnSettingsDataLoaded?.Invoke();
        Debug.Log($"Volume settings loaded. Volume = {SettingsData.volume}");
    }
    public void SaveSettingsData()
    {
        string json = JsonUtility.ToJson(_settingsData);
        string path = Application.persistentDataPath + "/settings_data.json";
        FileStream fileStream = new FileStream(path, FileMode.Create);
        using (StreamWriter writer = new StreamWriter(fileStream))
        {
            writer.Write(json);
        }
    }
    
    
    
    /* RECORD SCORE DATA */
    public void LoadRecordScoreData(string level)
    {
        var requestAround = new GetLeaderboardAroundPlayerRequest
        {
            StatisticName = level,
            MaxResultsCount = 1
        };
    PlayFabClientAPI.GetLeaderboardAroundPlayer(requestAround, OnLeaderboardGetAroundSuccess, OnLeaderboardGetAroundError);
    }
    private void OnLeaderboardGetAroundSuccess(GetLeaderboardAroundPlayerResult result)
    {        
        string json = result.Request.ToJson();
        string level = ExtractStatisticNameFromJsonResult(json);
        
        foreach (var item in result.Leaderboard)
        {
            _recordScoreData.recordScores[level] = item;
            _recordScoreData.isDataLoaded[level] = true;
        }
        OnRecordScoreDataLoaded?.Invoke(level);
        Debug.Log($"Record score in {level} loaded.");
    }
    
    private void OnLeaderboardGetAroundError(PlayFabError error)
    {
        Debug.Log("Couldn't load record score in ScoreLevel01.");
        Debug.Log(error.GenerateErrorReport());
    }

    public void UpdateLocalScoreAndLeadboardData(int score, string level)
    {        
        _recordScoreData.recordScores[level].StatValue = score;
        bool isContained = false;
        var findEntry = _leaderboardData.leaderboard[level].Find(obj => obj.PlayFabId == _recordScoreData.recordScores[level].PlayFabId);
        if (findEntry != null)
        {
            findEntry.StatValue = score;
            isContained = true;
        }
        else
        {
            if (_leaderboardData.leaderboard[level].Count < 10)
            {
                _leaderboardData.leaderboard[level].Add(_recordScoreData.recordScores[level]);
                isContained = true;
            }
        }
        var sortedList = _leaderboardData.leaderboard[level].OrderByDescending(x => x.StatValue).ToList();
        
        for (int i = 0; i < _leaderboardData.leaderboard[level].Count; i++)
        {
            if (!isContained && sortedList[i].StatValue < _recordScoreData.recordScores[level].StatValue)
            {
                sortedList.Insert(i, _recordScoreData.recordScores[level]);
                isContained = true;
            }
            sortedList[i].Position = i;
        }
        _leaderboardData.leaderboard[level] = sortedList;

    }
    
    
    
    /* LEADERBOARD DATA */
    public void LoadLeaderboardData(string level)
    {
        var requestTop = new GetLeaderboardRequest
        {
            StatisticName = level,
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(requestTop, OnLeaderboardGetTopSuccess, OnLeaderboardGetTopError);
    }
    private void OnLeaderboardGetTopSuccess(GetLeaderboardResult result)
    {        
        string json = result.Request.ToJson();
        string level = ExtractStatisticNameFromJsonResult(json);

        foreach (var item in result.Leaderboard)
        {
            _leaderboardData.leaderboard[level].Add(item);
        }
        _leaderboardData.isDataLoaded[level] = true;
        OnLeaderboardDataLoaded?.Invoke(level);
        Debug.Log($"Leaderboard in {level} loaded.");
    }

    private void OnLeaderboardGetTopError(PlayFabError error)
    {
        Debug.Log($"Couldn't load top players leaderboard.");
        Debug.Log(error.GenerateErrorReport());
    }
    
    public void UpdatePlayFabLeaderboard(string level)
    {
        int score = _recordScoreData.recordScores[level].StatValue;
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = level,
                    Value = score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnLeaderboardError);
    }

    private void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Successful leaderboard sent.");
    }

    private void OnLeaderboardError(PlayFabError error)
    {
        Debug.Log("Error while updating leaderboard.");
        Debug.Log(error.GenerateErrorReport());
    }

    private string ExtractStatisticNameFromJsonResult(string json)
    {
        string extractedWord = "";
        string pattern = @"\""StatisticName\"":\""(.*?)\"",";
        Match match = Regex.Match(json, pattern);

        if (match.Success)
        {
            extractedWord = match.Groups[1].Value;
        }
        return extractedWord;
    }
}
