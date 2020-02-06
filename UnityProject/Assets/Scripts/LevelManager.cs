using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    public GameObject skill;        // 隨機技能 (遊戲物件)
    public GameObject objLight;     // 光照 (遊戲物件)

    [Header("是否自動顯示技能")]
    public bool autoShowSkill;      // 是否顯示技能
    [Header("是否自動開門")]
    public bool autoOpenDoor;       // 是否自動開門
    [Header("復活畫面")]
    public GameObject panelRevival;

    private Animator aniDoor;        // 門 (動畫)
    private Image imgCross;          // 轉場

    private void Start()
    {
        // GameObject.Find("") 無法找到隱藏物件
        aniDoor = GameObject.Find("門").GetComponent<Animator>();

        imgCross = GameObject.Find("轉場效果").GetComponent<Image>();

        // 如果 是 顯示技能 呼叫 顯示技能方法
        if (autoShowSkill) ShowSkill();

        // 如果 是 自動開門 延遲呼叫 開門方法
        if (autoOpenDoor) Invoke("OpenDoor", 3.5f);

        // 延遲調用("方法名稱"，延遲時間)
        // Invoke("OpenDoor", 6);

        // 重複調用("方法名稱"，延遲時間，重複頻率)
        // InvokeRepeating("OpenDoor", 0, 1.5f);
    }

    /// <summary>
    /// 顯示技能
    /// </summary>
    private void ShowSkill()
    {
        skill.SetActive(true);
    }

    /// <summary>
    /// 開門、光照
    /// </summary>
    private void OpenDoor()
    {
        objLight.SetActive(true);
        aniDoor.SetTrigger("開門觸發");
    }

    /// <summary>
    /// 載入下一關
    /// </summary>
    /// <returns></returns>
    public IEnumerator NextLevel()
    {
        // 迴圈
        for (int i = 0; i < 20; i++)
        {
            imgCross.color += new Color(0, 0, 0, 0.05f);        // 轉場.顏色 += new Color(0, 0, 0, 0.01f)
            yield return new WaitForSeconds(0.001f);            // 等待 0.01 秒
        }

        yield return new WaitForSeconds(0.2f);
        // 載入下一關
        SceneManager.LoadScene("關卡 2");
    }

    public IEnumerator ShowRevival()
    {
        panelRevival.SetActive(true);
        Text textSecond = panelRevival.transform.GetChild(1).GetComponent<Text>();

        for (int i = 3; i > 0; i--)
        {
            textSecond.text = i.ToString();
            yield return new WaitForSeconds(1);
        }
    }
}
