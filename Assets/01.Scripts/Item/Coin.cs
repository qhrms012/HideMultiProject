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
            NotifyObservers(); // ���� ��Ȱ��ȭ �� �˸�
            Debug.Log("�μ���");
        }
    }
    // ������ ���
    public void AddObserver(Action observer)
    {
        observers += observer;
    }
    // ������ ����
    public void RemoveObserver(Action observer)
    {
        observers -= observer;
    }
    // ��� ���������� �˸�
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
