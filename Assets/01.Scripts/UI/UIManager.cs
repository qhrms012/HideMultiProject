using Photon.Pun;
using TMPro;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{

    public TextMeshProUGUI coinText;
    public GameObject winObject;
    public GameObject loseObject;
    public GameObject coinObject;
    public GameObject warningObject;
    public GameObject playerDieTimeObject;
    public TextMeshProUGUI warningText;
    public TextMeshProUGUI coinShortAgeText;
    public TextMeshProUGUI playerDieTime;

    private void Start()
    {
        //if (PhotonNetwork.LocalPlayer.ActorNumber == GameManager.Instance.enemyActor)
        //{
        //    coinObject.SetActive(false);
        //    playerDieTimeObject.SetActive(false);
        //}
    }
    public void UpdateCoinUI()
    {
        GameManager.Instance.Coincount++;
        coinText.text = GameManager.Instance.Coincount.ToString();
    }
    public void UpdateCoinShortAgeUI()
    {
        int Coinsa = GameManager.Instance.Coincount - 50;
        coinShortAgeText.text = $"코인이 {Mathf.Abs(Coinsa)} 만큼 부족합니다.";
    }

    public void UpdatePlayerDieTime()
    {
        if (PhotonNetwork.LocalPlayer.ActorNumber != GameManager.Instance.enemyActor)
        {
            int seconds = Mathf.FloorToInt(GameManager.Instance.player.dieTime);
            float decimals = (GameManager.Instance.player.dieTime % 1f) * 100;
            playerDieTime.text = string.Format("{0:00} : {1:00}", seconds, Mathf.FloorToInt(decimals));
        }
    }
    private void Update()
    {
        if (GameManager.Instance.player.isHit) 
        {
            UpdatePlayerDieTime();
        }

        
    }
    private void LateUpdate()
    {
        UpdateCoinShortAgeUI();
    }
}
