using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private Player player;
    private bool isDead = false;
    public int Coincount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioManager.Instance.PlayBgm(true);
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
    }
    public void GameQuit()
    {
        Application.Quit();
    }


}
