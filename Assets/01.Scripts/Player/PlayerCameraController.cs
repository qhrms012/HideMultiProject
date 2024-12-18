using Photon.Pun;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    public GameObject playerCamera; // Player �����տ� �ٿ����� ī�޶�
    private PhotonView photonView;

    void Start()
    {
        photonView = GetComponent<PhotonView>();

        // LocalPlayer���� ī�޶� Ȱ��ȭ
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
