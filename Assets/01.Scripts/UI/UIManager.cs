using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public TextMeshProUGUI coinText;
    [SerializeField]
    private GameObject winObject;
    [SerializeField]
    private GameObject loseObject;
    [SerializeField]
    private GameObject coinObject;
    [SerializeField]
    private TextMeshProUGUI coinShortAgeText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateCoinUI()
    {
        GameManager.Instance.Coincount++;
        coinText.text = GameManager.Instance.Coincount.ToString();
    }
    public void UpdateCoinShortAgeUI()
    {
        int Coinsa = GameManager.Instance.Coincount - 50;
        coinShortAgeText.text = Coinsa.ToString();
    }

    private void LateUpdate()
    {
        UpdateCoinShortAgeUI();
    }
}
