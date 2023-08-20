using System;
using System.Collections;
using UnityEngine;


public class AdManager : MonoBehaviour
{
    private readonly string _appKey = "1b0aab69d";
    private bool _isInterstitialLoaded;
    private bool _interstitialEnded;
    
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
        _isInterstitialLoaded = false;
        _interstitialEnded = false;

        IronSource.Agent.init(_appKey);
        IronSource.Agent.setMetaData("AdMob_TFCD","false");
        IronSource.Agent.setMetaData("AdMob_TFUA","false");
        IronSource.Agent.setMetaData("AdMob_MaxContentRating","MAX_AD_CONTENT_RATING_PG");

        IronSourceEvents.onSdkInitializationCompletedEvent += SdkInitCompletedEvent;
        IronSourceInterstitialEvents.onAdReadyEvent += InterstitialOnAdReadyEvent;
        IronSourceInterstitialEvents.onAdShowSucceededEvent += InterstitialOnAdShowSucceededEvent;
        IronSourceInterstitialEvents.onAdClosedEvent += InterstitialOnAdClosedEvent;

        Player.OnPlayerDeath += ShowInterstitialAd;
    }
    
    private void SdkInitCompletedEvent()
    {
        StartCoroutine(LoadInterstitial());
    }
    private IEnumerator LoadInterstitial()
    {
        _isInterstitialLoaded = false;
        _interstitialEnded = false;
        IronSource.Agent.loadInterstitial();
        yield return new WaitUntil(() => _interstitialEnded);
        yield return new WaitForSeconds(240);
    }
    private void ShowInterstitialAd()
    {
        if (_isInterstitialLoaded)
        {
            IronSource.Agent.showInterstitial();
        }
    }
    void InterstitialOnAdReadyEvent(IronSourceAdInfo adInfo)
    {
        _isInterstitialLoaded = true;
    }
    void InterstitialOnAdClosedEvent(IronSourceAdInfo adInfo)
    {
        _interstitialEnded = true;
    }
    void InterstitialOnAdShowSucceededEvent(IronSourceAdInfo adInfo) 
    {
        _interstitialEnded = true;
    }
    
}
