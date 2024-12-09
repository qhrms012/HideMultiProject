using UnityEngine;
using UnityEngine.Tilemaps;

public class ExitObject : MonoBehaviour
{

    TilemapCollider2D TilemapCollider2D;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
        }
    }

}
