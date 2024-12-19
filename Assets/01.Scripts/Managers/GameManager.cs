using Photon.Pun;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager Instance = null;
    
    public Player player;

    private bool isDead = false;
    public int Coincount;

    [SerializeField]
    GameObject[] map;


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
        RandomMap();
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
        else if (PhotonNetwork.LocalPlayer.ActorNumber == 3)
        {
            PhotonNetwork.Instantiate("Player", new Vector3(0, 3, -10), Quaternion.identity);
        }
        else if (PhotonNetwork.LocalPlayer.ActorNumber == 4)
        {
            PhotonNetwork.Instantiate("Player", new Vector3(3, 3, -10), Quaternion.identity);
        }
    }

    private void Update()
    {
        UpdateDieTime(player.dieTime);
        if (player.dieTime >= 5f && !isDead)
        {
            HandleGameEnd(false);
        }

    }

    public void HandleGameEnd(bool isWin)
    {
        int actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;

        if ((actorNumber == 1 || actorNumber == 3 || actorNumber == 4) == isWin)
        {
            WinPlayer();
        }
        else
        {
            LosePlayer();
        }
    }

    public void LosePlayer()
    {
        isDead = true;
        player.playerSpeed = 0;
        player.gameObject.SetActive(false);
        AudioManager.Instance.PlayBgm(false);
        UIManager.Instance.loseObject.SetActive(true);
        AudioManager.Instance.PlaySfx(AudioManager.Sfx.Lose);
        player.dieTime = 0;
    }

    public void WinPlayer()
    {
        player.playerSpeed = 0;
        player.gameObject.SetActive(false);
        AudioManager.Instance.PlayBgm(false);
        UIManager.Instance.winObject.SetActive(true);
        AudioManager.Instance.PlaySfx(AudioManager.Sfx.Win);
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
    public void RandomMap()
    {
        // map 배열에서 랜덤한 인덱스를 선택
        if (map.Length > 0)
        {
            int randomIndex = Random.Range(0, map.Length);

            // 모든 grid 요소를 비활성화한 뒤 선택된 요소만 활성화
            foreach (GameObject map in map)
            {
                map.SetActive(false);
            }

            map[randomIndex].SetActive(true);
        }
    }
    [PunRPC]
    private void UpdateDieTime(float newDieTime)
    {
        player.dieTime = newDieTime;
    }


}
