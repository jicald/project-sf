using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;

public class admob : MonoBehaviour
{
    // IOSのバナー広告ユニットID
    public string IosBannerUnitId;

    // Androidのバナー広告ユニットID
    public string AndroidBannerUnitId;

    // バナー広告
    private BannerView bannerView;

   

    void Start()
    {
        this.requestBanner();
    }

    // OnDestroy
    void OnDestroy()
    {
        this.bannerView.Destroy();
    }

    // バナー広告をリクエストする。
    private void requestBanner()
    {
#if UNITY_ANDROID
        string bannerUnitId = AndroidBannerUnitId;
#elif UNITY_IPHONE
    string bannerUnitId = IosBannerUnitId;
#else
    string bannerUnitId = "unexpected_platform";
#endif
        // 画面下部に広告バナーを表示する。
        this.bannerView = new BannerView(bannerUnitId, AdSize.SmartBanner, AdPosition.Bottom);
        AdRequest request = new AdRequest.Builder().Build();
        this.bannerView.LoadAd(request);
    }
}