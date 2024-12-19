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
            else if(GameManager.Instance.Coincount >= 50)
            {
                if(PhotonNetwork.LocalPlayer.ActorNumber == 1 &&
                    PhotonNetwork.LocalPlayer.ActorNumber == 3 &&
                    PhotonNetwork.LocalPlayer.ActorNumber == 4)
                {
                    AudioManager.Instance.PlayBgm(false);
                    AudioManager.Instance.PlaySfx(AudioManager.Sfx.Win);
                    UIManager.Instance.winObject.SetActive(true);
                }
                else if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
                {
                    GameManager.Instance.LosePlayer();
                }
                
            }
            
        }
    }

}
