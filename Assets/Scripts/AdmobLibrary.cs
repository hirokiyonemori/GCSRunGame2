using GoogleMobileAds.Api;
using System;
using System.Globalization;
using UnityEngine;

/// <summary>
/// adMobを使用するためのクラス
/// </summary>
public class AdmobLibrary
{
    private BannerView _bannerView;
    private InterstitialAd _interstitial;
    private RewardedAd _rewardedAd;
    public Action<double> OnReward;
    public Action<bool> onIntersitial;

    /// <summary>
    /// 初回に呼ぶ
    /// </summary>
    public void FirstSetting()
    {
        MobileAds.Initialize(initStatus => { });
        InitInterstitial();
        //InitRewarded();
    }

    public void InitReward()
    {
        //InitRewarded();
    }


    /// <summary>
    /// バナー広告を生成
    /// </summary>
    /// <param name="size"></param>
    /// <param name="position"></param>
    public void RequestBanner(AdSize size, AdPosition position)
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-8148356110096114/5144181083";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-8148356110096114/8449068481";
#else
		string adUnitId = "unexpected_platform";
#endif
        // Create a 320x50 banner at the top of the screen.
        this._bannerView = new BannerView(adUnitId, size, position);

        // Called when an ad request has successfully loaded.
        this._bannerView.OnAdLoaded += this.HandleOnAdLoaded;

        // Called when an ad request failed to load.
        this._bannerView.OnAdFailedToLoad += this.HandleOnAdFailedToLoad;

        // Called when an ad is clicked.
        this._bannerView.OnAdOpening += this.HandleOnAdOpened;

        // Called when the user returned from the app after an ad click.
        this._bannerView.OnAdClosed += this.HandleOnAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this._bannerView.LoadAd(request);

    }

    /// <summary>
    /// バナー広告削除
    /// </summary>
    public void DestroyBanner()
    {
        if (this._bannerView != null)
        {
            _bannerView.Destroy();
        }
    }

    /// <summary>
    /// インタースティシャル読み込み
    /// </summary>
    private void InitInterstitial()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-8148356110096114/7314221842";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-8148356110096114/9550026031";
#else
        string adUnitId = "unexpected_platform";
#endif
        // Initialize an InterstitialAd.
        this._interstitial = new InterstitialAd(adUnitId);

        // Called when an ad request has successfully loaded.
        this._interstitial.OnAdLoaded += HandleOnAdLoaded;

        // Called when an ad request failed to load.
        this._interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;

        // Called when an ad is shown.
        this._interstitial.OnAdOpening += HandleOnAdOpening;

        // Called when the ad is closed.
        this._interstitial.OnAdClosed += HandleOnAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the interstitial with the request.
        this._interstitial.LoadAd(request);
    }

    /// <summary>
    /// インタースティシャルを出す
    /// </summary>
    public void PlayInterstitial()
    {

        LogSystem.Log(" this._interstitial.IsLoaded( " + this._interstitial.IsLoaded());
        if (this._interstitial.IsLoaded())
        {
            LogSystem.Log(" インタースティシャルの読み込み ");
            this._interstitial.Show();
        }

    }

    /// <summary>
    /// インタースティシャル削除
    /// </summary>
    public void DestroyInterstitial()
    {
        if (this._interstitial != null)
        {
            this._interstitial.Destroy();
        }
    }

    /// <summary>
    /// リワード広告
    /// </summary>
    private void InitRewarded()
    {
        string adUnitId;
#if UNITY_ANDROID
        adUnitId = "ca-app-pub-8148356110096114/1294083291";
#elif UNITY_IPHONE
        adUnitId = "ca-app-pub-8148356110096114/6467315087";
#else
		adUnitId = "unexpected_platform";
#endif
        this._rewardedAd = new RewardedAd(adUnitId);

        // Called when an ad request has successfully loaded.
        this._rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;

        // Called when an ad request failed to load.
        this._rewardedAd.OnAdFailedToLoad += (sender, args) =>
        {
            Debug.Log($"rewardedAd Failed {args.LoadAdError.GetMessage()}");
        };

        // Called when an ad is shown.
        this._rewardedAd.OnAdOpening += HandleRewardedAdOpening;

        // Called when an ad request failed to show.
        this._rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;

        // Called when the user should be rewarded for interacting with the ad.
        this._rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;

        // Called when the ad is closed.
        this._rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the rewarded ad with the request.
        this._rewardedAd.LoadAd(request);
    }

    /// <summary>
    /// リワード広告を作成
    /// </summary>
    public void ShowReawrd()
    {
        InitRewarded();
        //this._rewardedAd.Show();
    }

    /// <summary>
    /// リワード削除
    /// </summary>
    public void DestroyReward()
    {
        if (this._rewardedAd != null)
        {
            this._rewardedAd.Destroy();
        }
    }

    private void HandleOnAdLoaded(object sender, EventArgs args)
    {
        Debug.Log("HandleAdLoaded event received");
    }

    private void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        Debug.Log("HandleFailedToReceiveAd event received with message: "
                            + args.LoadAdError.GetMessage());
    }

    private void HandleOnAdOpened(object sender, EventArgs args)
    {
        Debug.Log("HandleAdOpened event received");
    }

    private void HandleOnAdClosed(object sender, EventArgs args)
    {
        Debug.Log("HandleAdClosed event received");
        onIntersitial(true);
    }

    private void HandleOnAdOpening(object sender, EventArgs args)
    {
        Debug.Log("HandleAdOpening event received");
    }





    private void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        Debug.Log("HandleRewardedAdLoaded event received");
        this._rewardedAd.Show();
    }

    private void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
    {
        this._rewardedAd.Show();
    }

    private void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        Debug.Log("HandleRewardedAdOpening event received");
    }

    private void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        Debug.Log(
            "HandleRewardedAdFailedToShow event received with message: "
                             + args.Message);
    }

    private void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        Debug.Log("HandleRewardedAdClosed event received");
    }

    /// <summary>
    /// 報酬獲得
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    private void HandleUserEarnedReward(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        Debug.Log(
            "HandleRewardedAdRewarded event received for "
                        + amount.ToString(CultureInfo.InvariantCulture) + " " + type);
        OnReward?.Invoke(amount);
    }
}
