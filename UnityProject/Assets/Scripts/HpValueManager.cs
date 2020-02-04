using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HpValueManager : MonoBehaviour
{
    private Image hpBar;
    private Text hpText;

    private void Start()
    {
        hpBar = transform.GetChild(1).GetComponent<Image>();
        hpText = transform.GetChild(2).GetComponent<Text>();
    }

    private void Update()
    {
        fixedAngle();
    }

    private void fixedAngle()
    {
        transform.eulerAngles = new Vector3(40, 0, 0);
    }

    public void SetHp(float hpcrrent, float hpMax)
    {
        hpBar.fillAmount = hpcrrent / hpMax;
    }

    public IEnumerator ShowValue(float value, string mark, Color color)
    {
        hpText.text = mark + value;
        color.a = 0;
        hpText.color = color;

        for (int i = 0; i < 20; i++)
        {
            hpText.color += new Color(0, 0, 0, 0.05f);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
