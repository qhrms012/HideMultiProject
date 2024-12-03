using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    private int coinCount = 0;

    // ���� ȹ��� ȣ�� �޼���
    private void OnCoinCollected()
    {
        coinCount++;
        coinText.text = coinCount.ToString();
    }

    public void SubscribeToCoin(Coin coin)
    {
        coin.AddObserver(OnCoinCollected); // ���� ������ ���
    }
    public void UnsubscribeFromCoin(Coin coin)
    {
        coin.RemoveObserver(OnCoinCollected); // ���� ����������
    }
}
