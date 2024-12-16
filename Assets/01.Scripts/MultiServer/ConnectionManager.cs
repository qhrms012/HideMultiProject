using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConnectionManager : MonoBehaviourPunCallbacks
{
    private ConnectionManager connectionManager;
    public ConnectionManager Instance { get { return connectionManager; } }

    [SerializeField] TMP_InputField inputText;
    [SerializeField] Button inputButton;

    void Start()
    {
        //������ ����Ǿ�����
        inputText.onValueChanged.AddListener(OnValueChanged);
        //������ ����������
        inputText.onSubmit.AddListener(OnSubmit);
        //Ŀ���� �ٸ����� ������
        inputText.onEndEdit.AddListener(
            (string s) =>
            {
                Debug.Log("OnEndmit" + s);
            }
        );
        inputButton.onClick.AddListener(OnClickConnect);
    }

    void OnValueChanged(string s)
    {
        inputButton.interactable = s.Length > 0;
    }
    void OnSubmit(string s)
    {
        Debug.Log("OnSubmit " + s);
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("������ ���� ���� ����");

        //���� �̸��� ���濡 ����
        PhotonNetwork.NickName = inputText.text;
        //�κ�����
        PhotonNetwork.JoinLobby();
    }
    //Lobby ������ ���������� ȣ��Ǵ� �Լ�
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();

        //�κ� ������ �̵�
        PhotonNetwork.LoadLevel("LobbyScene");

        print("�κ� ���� ����");

    }
    public void OnClickConnect()
    {
        // ������ ���� ���� ��û
        PhotonNetwork.ConnectUsingSettings();
    }   
}
