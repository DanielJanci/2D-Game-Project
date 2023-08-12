using System;
using UnityEngine;


 
public class AdManager : MonoBehaviour
{
    private readonly string _appKey = "1b0aab69d";
    private bool _isBannerLoaded;
    
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
        IronSource.Agent.init(_appKey);
        IronSourceEvents.onSdkInitializationCompletedEvent += SdkInitCompletedEvent;
        IronSourceBannerEvents.onAdLoadedEvent += ShowBannerAd;
    }


    private void SdkInitCompletedEvent()
    {
        //IronSource.Agent.validateIntegration();
        LoadBannerAd();
    }

    private void LoadBannerAd()
    {
        IronSource.Agent.loadBanner(IronSourceBannerSize.SMART, IronSourceBannerPosition.BOTTOM);
    }

    public void ShowBannerAd(IronSourceAdInfo ironSourceAdInfo)
    {
        IronSource.Agent.displayBanner();
    }

    public void HideBannerAd()
    {
        IronSource.Agent.hideBanner();
    }
}
