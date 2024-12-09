using UnityEngine;
using UnityEngine.Tilemaps;

public class ExitObject : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(GameManager.Instance.Coincount < 50)
            {
                UIManager.Instance.coinObject.SetActive(true);
            }
            else
            {
                UIManager.Instance.winObject.SetActive(true);
            }
            
        }
    }

}
