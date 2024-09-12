using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsListener
{
    [SerializeField] string androidGameId;
    [SerializeField] string iOSGameId;
    [SerializeField] bool testMode = true;
    string adId = null;

    void Awake()
    {
        CheckPlatform();
    }

    void CheckPlatform()
    {
        string gameId = null;

#if UNITY_IOS
    {
        gameId = iOSGameId;
        adId = "Rewarded_iOS";
    }
#elif UNITY_ANDROID || UNITY_EDITOR
    { 
        gameId = androidGameId;
        adId = "Rewarded_Android";
    }
#endif
        Advertisement.Initialize(gameId, testMode, false, this);
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads successfully initialised!");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads initialisation failed: {error.ToString()} - {message}"); // Dollar sign for concatenation

    }
    public void OnUnityAdsReady(string placementId)
    {

    }

    public void OnUnityAdsDidError(string message)
    {
       
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
       if (showResult == ShowResult.Finished)
        {
            // Reward
        }
       else if (showResult == ShowResult.Skipped)
        {
            // Don't reward
        }
       else if (showResult == ShowResult.Failed)
        {
            Debug.LogWarning("Ad not finished due to an error.");
        }

        Advertisement.Load(placementId);
    }
    IEnumerator WaitForAds()
    {
        while(!Advertisement.isInitialized)
        {
            yield return null;
        }
        LoadAd();
    }
    void LoadAd()
    {
        Advertisement.AddListener(this);
        Advertisement.Load(adId);
    }

    public void WatchAd()
    {
        Advertisement.Show(adId);
    }
}
