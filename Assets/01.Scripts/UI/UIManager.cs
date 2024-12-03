using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    private int coinCount = 0;

    // 코인 획득시 호출 메서드
    private void OnCoinCollected()
    {
        coinCount++;
        coinText.text = coinCount.ToString();
    }

    public void SubscribeToCoin(Coin coin)
    {
        coin.AddObserver(OnCoinCollected); // 코인 옵저버 등록
    }
    public void UnsubscribeFromCoin(Coin coin)
    {
        coin.RemoveObserver(OnCoinCollected); // 코인 옵저버제거
    }
}
