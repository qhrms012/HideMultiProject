using Photon.Pun;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    public GameObject playerCamera; // Player 프리팹에 붙여놓은 카메라
    private PhotonView photonView;

    void Start()
    {
        photonView = GetComponent<PhotonView>();

        // LocalPlayer에만 카메라 활성화
        if (photonView.IsMine)
        {
            playerCamera.SetActive(true);
        }
        else
        {
            playerCamera.SetActive(false);
        }
    }
}
