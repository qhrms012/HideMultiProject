using Photon.Pun;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ExitObject : MonoBehaviourPunCallbacks
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
                GameManager.Instance.HandleGameEnd(true);
            }
            
        }
    }

}
