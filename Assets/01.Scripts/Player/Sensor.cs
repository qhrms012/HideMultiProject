using UnityEngine;

public class Sensor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            AudioManager.Instance.PlaySfx(AudioManager.Sfx.Sensor);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            AudioManager.Instance.StopSfx(AudioManager.Sfx.Sensor);
        }
    }
}
