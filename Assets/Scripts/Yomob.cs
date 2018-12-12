using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Together;
using System.Text.RegularExpressions;

public class Yomob : MonoBehaviour {

	private string[] scenes;
	private int sceneIndex = 0;
	private string[] sceneNames;

	void Awake()
	{
		TGSDK.SetDebugModel(true);
		TGSDK.SDKInitFinishedCallback = SDKInitFinishedCallback;
#if UNITY_IOS && !UNITY_EDITOR
		TGSDK.Initialize ("hP7287256x5z1572E5n7");
#elif UNITY_ANDROID && !UNITY_EDITOR
		TGSDK.Initialize ("59t5rJH783hEQ3Jd7Zqr");
#endif
	}

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		
	}

	void SDKInitFinishedCallback(string msg)
	{
		TGSDK.TagPayingUser(TGPayingUser.TGMediumPaymentUser, "CNY", 0, 0);
		Debug.Log ("TGSDK GetUserGDPRConsentStatus = " + TGSDK.GetUserGDPRConsentStatus ());
		TGSDK.SetUserGDPRConsentStatus ("yes");
		Debug.Log ("TGSDK GetIsAgeRestrictedUser = " + TGSDK.GetIsAgeRestrictedUser ());
		TGSDK.SetIsAgeRestrictedUser ("no");
		float bannerHeight = (float)(Screen.height) * 0.123f;
		TGSDK.SetBannerConfig("banner0", "TGBannerNormal", 0, Display.main.systemHeight - bannerHeight, Display.main.systemWidth, bannerHeight, 30);
		TGSDK.SetBannerConfig("banner1", "TGBannerNormal", 0, Display.main.systemHeight - 2*bannerHeight, Display.main.systemWidth, bannerHeight, 30);
		TGSDK.SetBannerConfig("banner2", "TGBannerNormal", 0, Display.main.systemHeight - 3*bannerHeight, Display.main.systemWidth, bannerHeight, 30);
		PreloadAd();
	}

	void PreloadAdSuccessCallback(string msg)
	{
		Debug.Log ("PreloadAdSuccessCallback : " + msg);
		scenes = Regex.Split(msg, ",", RegexOptions.IgnoreCase);
		sceneNames = new string[scenes.Length];
		for (int i = 0; i < scenes.Length; i++) {
			string scene = scenes[i];
			string sceneName = TGSDK.GetSceneNameById(scene);
			sceneNames[i] = sceneName+"("+scene.Substring(0, 4)+")";
		}
	}

    public void PreloadAd()
    {
		TGSDK.PreloadAdSuccessCallback = PreloadAdSuccessCallback;
        TGSDK.PreloadAd();
    }

	public void ShowAD(int sceneIndex)
	{
		this.sceneIndex = sceneIndex;
		string sceneid = scenes[sceneIndex];
		if (TGSDK.CouldShowAd (sceneid))
			TGSDK.ShowAd(sceneid);
		else
			Debug.Log ("Scene "+sceneid+" could not to show");
	}

	public void ShowTestView()
	{
		string sceneid = scenes[sceneIndex];
		TGSDK.ShowTestView (sceneid);
	}

	public void CloseBanner(int sceneIndex)
	{
		string sceneid = scenes[sceneIndex];
		TGSDK.CloseBanner(sceneid);
	}
}
