using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour,IUnityAdsListener
{
    private string googleID = "3459604";
    private string placementRevival = "revival";
    private Player Player;

    private void Start()
    {
        Advertisement.Initialize(googleID, false);
        Advertisement.AddListener(this);
        Player = FindObjectOfType<Player>();
    }

    public void ShowADRevival()
    {
        if (Advertisement.IsReady(placementRevival))
        {
            Advertisement.Show(placementRevival);
        }
    }
    //廣告準備完成
    public void OnUnityAdsReady(string placementId)
    {
    }
    //廣告錯誤
    public void OnUnityAdsDidError(string message)
    {
    }
    //廣告開始
    public void OnUnityAdsDidStart(string placementId)
    {
    }
    //廣告完成
    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (placementId == placementRevival)
        {
            switch (showResult)
            {
                case ShowResult.Failed:
                   // print("失敗");
                    break;
                case ShowResult.Skipped:
                    //print("略過");
                    break;
                case ShowResult.Finished:
                    //print("完成");
                    GameObject.Find("鼠王").GetComponent<Player>().Revival();
                    Player.Revival();
                    break;
            }
        }
    }
}
