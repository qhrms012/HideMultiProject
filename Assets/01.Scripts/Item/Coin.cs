using System;
using UnityEngine;

public class Coin : MonoBehaviour , ISubject
{
    Rigidbody2D rb;
    private event Action observers;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            Debug.Log("부서짐");
        }
    }
    // 옵저버 등록
    public void AddObserver(Action observer)
    {
        observers += observer;
    }
    // 옵저버 제거
    public void RemoveObserver(Action observer)
    {
        observers -= observer;
    }
    // 모든 옵저버에게 알림
    public void NotifyObservers()
    {
        observers.Invoke();
    }
}
public interface ISubject
{
    void AddObserver(Action observer);
    void RemoveObserver(Action observer);
    void NotifyObservers();
}
