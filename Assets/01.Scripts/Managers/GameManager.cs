using Photon.Pun;
using System;
using System.Collections;
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
    private PhotonView pv;
    [SerializeField]
    GameObject[] map;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        RandomMap();   
        Application.targetFrameRate = 60;
        AudioManager.Instance.PlayBgm(true);
        
        pv = GetComponent<PhotonView>();
        if (PhotonNetwork.IsMasterClient)
        {
            AssignEnemyRole();
        }
        StartCoroutine(WaitForEnemyRole());

    }
    void AssignEnemyRole()
    {
        int randomPlayer = Random.Range(1, PhotonNetwork.PlayerList.Length + 1);
        PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "EnemyActor", randomPlayer } });
    }

    IEnumerator WaitForEnemyRole()
    {
        while (!PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("EnemyActor"))
        {
            yield return null;
        }
        SpawnCharacter();
        player = FindAnyObjectByType<Player>();
    }
    void SpawnCharacter()
    {
        if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("EnemyActor"))
        {
            int enemyActor = (int)PhotonNetwork.CurrentRoom.CustomProperties["EnemyActor"];

            if (PhotonNetwork.LocalPlayer.ActorNumber == enemyActor)
            {
                PhotonNetwork.Instantiate("Enemy", new Vector3(-15, -3, -10), Quaternion.identity);
            }
            else
            {
                Vector3 spawnPos = new Vector3(0, 3, -10);

                switch (PhotonNetwork.LocalPlayer.ActorNumber)
                {
                    case 1:
                        spawnPos = new Vector3(0, 0, -10);
                        break;
                    case 3:
                        spawnPos = new Vector3(0, 3, -10);
                        break;
                    case 4:
                        spawnPos = new Vector3(3, 3, -10);
                        break;
                }

                PhotonNetwork.Instantiate("Player", spawnPos, Quaternion.identity);
            }
        }
        else
        {
            // EnemyActor가 설정되지 않았을 때, 기본 Player로 스폰
            PhotonNetwork.Instantiate("Player", new Vector3(0, 0, -10), Quaternion.identity);
        }
        //// ActorNumber로 역할 분배: 1번 플레이어는 Player, 2번 플레이어는 Enemy
        //if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
        //{
        //    PhotonNetwork.Instantiate("Player", new Vector3(0, 0, -10), Quaternion.identity);
        //}
        //else if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
        //{
        //    PhotonNetwork.Instantiate("Enemy", new Vector3(5, 0, -10), Quaternion.identity);
        //}
        //else if (PhotonNetwork.LocalPlayer.ActorNumber == 3)
        //{
        //    PhotonNetwork.Instantiate("Player", new Vector3(0, 3, -10), Quaternion.identity);
        //}
        //else if (PhotonNetwork.LocalPlayer.ActorNumber == 4)
        //{
        //    PhotonNetwork.Instantiate("Player", new Vector3(3, 3, -10), Quaternion.identity);
        //}
    }


    private void Update()
    {
        if (player.dieTime >= 5f && !isDead)
        {
            HandleGameEnd(false);
        }

        if (PhotonNetwork.IsMasterClient)
        {
            CheckAllPlayersDead();
        }

    }

    public void HandleGameEnd(bool isWin)
    {
        int actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;

        if ((actorNumber == 1 || actorNumber == 3 || actorNumber == 4) == isWin)
        {
            WinPlayer();
        }
        else if (actorNumber == 2 && !isWin)
        {
            WinPlayer();
        }
        else
        {
            LosePlayer();
        }
    }

    void CheckAllPlayersDead()
    {
        int alivePlayers = 0;

        foreach (var p in PhotonNetwork.PlayerList)
        {
            if (p.ActorNumber != 2) // Enemy 제외
            {
                Player playerObj = FindPlayerByActorNumber(p.ActorNumber);
                if (playerObj != null && playerObj.gameObject.activeSelf)
                {
                    alivePlayers++;
                }
            }
        }

        if (alivePlayers == 0)
        {
            photonView.RPC("HandleEnemyWin", RpcTarget.All);
        }
    }
    
    Player FindPlayerByActorNumber(int actorNumber)
    {
        Player[] players = FindObjectsOfType<Player>();
        foreach (var p in players)
        {
            if (p.pv.Owner.ActorNumber == actorNumber)
            {
                return p;
            }
        }
        return null;
    }

    public void LosePlayer()
    {
        player.gameObject.SetActive(false);
        isDead = true;
        player.playerSpeed = 0;        
        AudioManager.Instance.PlayBgm(false);
        UIManager.Instance.loseObject.SetActive(true);
        AudioManager.Instance.PlaySfx(AudioManager.Sfx.Lose);
        player.dieTime = 0;
    }

    public void WinPlayer()
    {
        player.gameObject.SetActive(false);
        player.playerSpeed = 0;       
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
        if (PhotonNetwork.IsMasterClient)
        {
            int randomIndex = Random.Range(0, map.Length);

            // 모든 클라이언트에 랜덤 맵 인덱스를 전달
            photonView.RPC("SetMap", RpcTarget.All, randomIndex);
        }
    }
    [PunRPC]
    private void SetMap(int index)
    {
        // 모든 grid 요소 비활성화
        foreach (GameObject map in map)
        {
            map.SetActive(false);
        }

        // 선택된 맵만 활성화
        if (index >= 0 && index < map.Length)
        {
            map[index].SetActive(true);
        }

    }

    [PunRPC]
    void HandleEnemyWin()
    {
        if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
        {
            WinPlayer();
        }
    }
}
