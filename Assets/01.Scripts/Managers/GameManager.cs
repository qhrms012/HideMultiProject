using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public int Coincount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioManager.Instance.PlayBgm(true);
    }
    
    public void GameQuit()
    {
        Application.Quit();
    }


}
