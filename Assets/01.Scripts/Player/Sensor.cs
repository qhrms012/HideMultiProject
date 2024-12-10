using System.Collections;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    private bool inSensor; // 적이 센서 범위에 있는지 여부
    private Coroutine warningCoroutine; // 코루틴 핸들러

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (!inSensor)
            {
                inSensor = true;
                AudioManager.Instance.PlaySfx(AudioManager.Sfx.Popup);
                AudioManager.Instance.PlaySfx(AudioManager.Sfx.Sensor);
                warningCoroutine = StartCoroutine(OnWarningUI());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            inSensor = false;
            AudioManager.Instance.StopSfx(AudioManager.Sfx.Sensor);

            if (warningCoroutine != null)
            {
                StopCoroutine(warningCoroutine);
                warningCoroutine = null;
            }

            UIManager.Instance.warningObject.SetActive(false);
        }
    }

    public IEnumerator OnWarningUI()
    {
        while (inSensor) // 적이 센서 범위에 있는 동안
        {
            UIManager.Instance.warningObject.SetActive(true);
            yield return new WaitForSeconds(2f);
        }

        UIManager.Instance.warningObject.SetActive(false);
    }
}
