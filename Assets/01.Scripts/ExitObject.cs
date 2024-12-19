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
                if(PhotonNetwork.LocalPlayer.ActorNumber != 2)
                {
                    UIManager.Instance.coinObject.SetActive(true);
                }                
            }
            else
            {
                photonView.RPC("HandleGameEndRPC", RpcTarget.All, true);
            }
            
        }
    }
    [PunRPC]
    private void HandleGameEndRPC(bool isWin)
    {
        GameManager.Instance.HandleGameEnd(isWin);
    }
}
