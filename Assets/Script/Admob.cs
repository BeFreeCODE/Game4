using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api; // 구글 애드몹 API 네임 스페이스

public class Admob : MonoBehaviour
{
	BannerView mBannerView = null; // 배너 출력
	InterstitialAd mInterstitialAd = null; //전면광고

	public string AndroidBannerViewKey; //AndroidBanner
	public string AndroidInterstitialAdKey; //AndroidInterstitial
	public string IOSBannerViewKey;
	public string IOSInterstitialAdKey;



	private string mBannerView_Key; // 배너 Key
	private string mInterstitialAd_Key; //전면광고 Key

	private string SEE_YOUR_LOGCAT_TO_GET_YOUR_DEVICE_ID = "3EB94BF573AD95FB";

	void Awake() 
	{
#if UNITY_ANDROID
		mBannerView_Key = AndroidBannerViewKey;
		mInterstitialAd_Key = AndroidInterstitialAdKey;
#elif UNITY_IOS
		mBannerView_Key = IOSBannerViewKey;
		mInterstitialAd_Key = IOSInterstitialAdKey;
#endif



		DontDestroyOnLoad(this.gameObject);
       
        // BannerView(애드몹 사이트에 등록된 아이디, 크기, 위치) / AdSize.SmartBanner : 화면 해상도에 맞게 늘임, AdPosition.Bottom : 화면 바닥에 붙음
        mBannerView = new BannerView(mBannerView_Key, AdSize.SmartBanner, AdPosition.Bottom);
        mInterstitialAd = new InterstitialAd(mInterstitialAd_Key);
        
        // 애드몹 리퀘스트 초기화
        AdRequest request = new AdRequest.Builder()
            .AddTestDevice(AdRequest.TestDeviceSimulator)       // Simulator.
            .AddTestDevice(SEE_YOUR_LOGCAT_TO_GET_YOUR_DEVICE_ID)  // My test device.
            .Build();
        
        // 테스트 할 때는 테스트 디바이스로 등록을 해야한다고 합니다. 테스트를 상용으로 하면 광고가 안나올 수 도 있다고 하더군요.
        //AdRequest request = builder.Build();

        // 애드몹 배너 광고를 로드합니다.
        mBannerView.LoadAd(request); //배너 광고 요청
        mInterstitialAd.LoadAd(request);

       
       //InterstitialAd.OnAdClosed += OnAdClosed;

    }


  //  private void OnAdClosed(object sender, System.EventArgs e)
  //  {
		//Debug.Log("adcloused : " + sender + " " + e);

		//ReLoadInterstitialAd();

  //  }

    void Start()
    {
        mBannerView.Show();  // 배너 광고 출력
    }
    //void ReLoadInterstitialAd()
    //{
    //    mInterstitialAd.Destroy();
    //    mInterstitialAd = new InterstitialAd(mInterstitialAd_Key);

    //    AdRequest.Builder builder = new AdRequest.Builder();
    //    AdRequest request = builder.AddTestDevice(AdRequest.TestDeviceSimulator).AddTestDevice(SEE_YOUR_LOGCAT_TO_GET_YOUR_DEVICE_ID).Build();
    //    mInterstitialAd.LoadAd(request);
    //    mInterstitialAd.OnAdClosed += OnAdClosed;
    //}
    //public void ShowInterstitialAd()
    //{
    //    if ((gameCount % 3) == 0)
    //        mInterstitialAd.Show();
    //    gameCount++;
    //}

}