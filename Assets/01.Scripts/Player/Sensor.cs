using Photon.Pun;
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
            PhotonView enemyView = collision.GetComponent<PhotonView>();
            // 적이 감지되었지만, 적이 내 캐릭터(내가 조종하는 객체)가 아닐 경우만 UI 실행
            if (!inSensor && (enemyView == null || !enemyView.IsMine))
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
