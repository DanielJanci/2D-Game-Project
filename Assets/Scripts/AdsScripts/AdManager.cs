using System;
using UnityEngine;


public class AdManager : MonoBehaviour
{
    private readonly string _appKey = "1b0aab69d";
    private bool _isBannerLoaded;
    private bool _isInterstitialLoaded;
    
    public static AdManager instance;
    
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
        _isBannerLoaded = false;
        _isInterstitialLoaded = false;
        
        IronSource.Agent.init(_appKey);
        IronSource.Agent.setMetaData("AdMob_TFCD","false");
        IronSource.Agent.setMetaData("AdMob_TFUA","false");
        IronSource.Agent.setMetaData("AdMob_MaxContentRating","MAX_AD_CONTENT_RATING_PG");

        IronSourceEvents.onSdkInitializationCompletedEvent += SdkInitCompletedEvent;
        
        IronSourceBannerEvents.onAdLoadedEvent += BannerOnAdLoadedEvent;
        IronSourceBannerEvents.onAdLoadFailedEvent += BannerOnAdLoadFailedEvent;
        IronSourceBannerEvents.onAdClickedEvent += BannerOnAdClickedEvent;
        IronSourceBannerEvents.onAdScreenPresentedEvent += BannerOnAdScreenPresentedEvent;
        IronSourceBannerEvents.onAdScreenDismissedEvent += BannerOnAdScreenDismissedEvent;
        IronSourceBannerEvents.onAdLeftApplicationEvent += BannerOnAdLeftApplicationEvent;
        
        IronSourceInterstitialEvents.onAdReadyEvent += InterstitialOnAdReadyEvent;
        IronSourceInterstitialEvents.onAdLoadFailedEvent += InterstitialOnAdLoadFailed;
        IronSourceInterstitialEvents.onAdOpenedEvent += InterstitialOnAdOpenedEvent;
        IronSourceInterstitialEvents.onAdClickedEvent += InterstitialOnAdClickedEvent;
        IronSourceInterstitialEvents.onAdShowSucceededEvent += InterstitialOnAdShowSucceededEvent;
        IronSourceInterstitialEvents.onAdShowFailedEvent += InterstitialOnAdShowFailedEvent;
        IronSourceInterstitialEvents.onAdClosedEvent += InterstitialOnAdClosedEvent;

        Player.OnGameStarted += LoadBannerAd;
        PauseResume.OnGameResumed += HideBannerAd;
        PauseResume.OnGamePaused += ShowBannerAd;
        Player.OnGameStarted += LoadInterstitialAd;
        Player.OnPlayerDeath += ShowInterstitialAd;
    }
    private void SdkInitCompletedEvent()
    {
    }
    
    private void LoadBannerAd()
    {
        IronSource.Agent.loadBanner(IronSourceBannerSize.SMART, IronSourceBannerPosition.BOTTOM);
    }
    private void ShowBannerAd()
    {
        if (_isBannerLoaded && PauseResume.IsGamePaused)
        {
            IronSource.Agent.displayBanner();
        }
    }
    private void HideBannerAd()
    {
        IronSource.Agent.hideBanner();
    }

    private void LoadInterstitialAd()
    {
        IronSource.Agent.loadInterstitial();
    }
    private void ShowInterstitialAd()
    {
        if (_isInterstitialLoaded)
        {
            IronSource.Agent.showInterstitial();
        }
    }
    
    void BannerOnAdLoadedEvent(IronSourceAdInfo adInfo)
    {
        _isBannerLoaded = true;
    }
    void BannerOnAdLoadFailedEvent(IronSourceError ironSourceError) 
    {
    }
    void BannerOnAdClickedEvent(IronSourceAdInfo adInfo) 
    {
    }
    void BannerOnAdScreenPresentedEvent(IronSourceAdInfo adInfo) 
    {
    }
    void BannerOnAdScreenDismissedEvent(IronSourceAdInfo adInfo) 
    {
    }
    void BannerOnAdLeftApplicationEvent(IronSourceAdInfo adInfo) 
    {
    }
    
    
    
    
    void InterstitialOnAdReadyEvent(IronSourceAdInfo adInfo)
    {
        _isInterstitialLoaded = true;
    }
    void InterstitialOnAdLoadFailed(IronSourceError ironSourceError) 
    {
    }
    void InterstitialOnAdOpenedEvent(IronSourceAdInfo adInfo) 
    {
    }
    void InterstitialOnAdClickedEvent(IronSourceAdInfo adInfo) 
    {
    }
    void InterstitialOnAdShowFailedEvent(IronSourceError ironSourceError, IronSourceAdInfo adInfo) 
    {
    }
    void InterstitialOnAdClosedEvent(IronSourceAdInfo adInfo) 
    {
    }
    void InterstitialOnAdShowSucceededEvent(IronSourceAdInfo adInfo) 
    {
    }
}
