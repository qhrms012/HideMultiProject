using TMPro;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{

    public TextMeshProUGUI coinText;
    public GameObject winObject;
    public GameObject loseObject;
    public GameObject coinObject;
    public TextMeshProUGUI coinShortAgeText;

    public void UpdateCoinUI()
    {
        GameManager.Instance.Coincount++;
        coinText.text = GameManager.Instance.Coincount.ToString();
    }
    public void UpdateCoinShortAgeUI()
    {
        int Coinsa = GameManager.Instance.Coincount - 50;
        coinShortAgeText.text = $"코인이 {Coinsa} 만큼 부족합니다.";
    }

    private void LateUpdate()
    {
        UpdateCoinShortAgeUI();
    }
}
