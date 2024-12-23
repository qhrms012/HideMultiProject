using Photon.Pun;
using System.Collections;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    private bool inSensor; // ���� ���� ������ �ִ��� ����
    private Coroutine warningCoroutine; // �ڷ�ƾ �ڵ鷯

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            PhotonView enemyView = collision.GetComponent<PhotonView>();
            // ���� �����Ǿ�����, ���� �� ĳ����(���� �����ϴ� ��ü)�� �ƴ� ��츸 UI ����
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
        while (inSensor) // ���� ���� ������ �ִ� ����
        {
            UIManager.Instance.warningObject.SetActive(true);
            yield return new WaitForSeconds(2f);
        }

        UIManager.Instance.warningObject.SetActive(false);
    }
}
