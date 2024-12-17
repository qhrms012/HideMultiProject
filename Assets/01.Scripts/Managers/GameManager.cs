using Photon.Pun;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager Instance = null;

    public Player player;
    private bool isDead = false;
    public int Coincount;



    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }else if(Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        SpawnCharacter();
        Application.targetFrameRate = 60;
        AudioManager.Instance.PlayBgm(true);
        player = FindAnyObjectByType<Player>();
    }


    void SpawnCharacter()
    {

        // ActorNumber로 역할 분배: 1번 플레이어는 Player, 2번 플레이어는 Enemy
        if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
        {
            PhotonNetwork.Instantiate("Player", new Vector3(0, 0, -10), Quaternion.identity);
        }
        else if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
        {
            PhotonNetwork.Instantiate("Enemy", new Vector3(5, 0, -10), Quaternion.identity);
        }
    }

    private void Update()
    {

        if (player.dieTime >= 5f && !isDead)
        {
            DiePlayer();
        }

    }
    private void DiePlayer()
    {
        isDead = true;
        player.playerSpeed = 0;
        player.gameObject.SetActive(false);
        UIManager.Instance.loseObject.SetActive(true);
        AudioManager.Instance.PlaySfx(AudioManager.Sfx.Lose);
        player.dieTime = 0;
    }
    public void GameQuit()
    {
        Application.Quit();
    }

    public void GameLobby()
    {
        SceneManager.LoadScene("LobbyScene");
    }
}
