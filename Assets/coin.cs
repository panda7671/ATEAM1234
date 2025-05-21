using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Text�� ����Ϸ��� �ʿ�
using TMPro;

public class coin : MonoBehaviour
{
    public TMP_Text messageText;  // Unity���� ������ UI �ؽ�Ʈ

    public int textcoin = 0;

    void Start()
    {
        messageText.text = "";  // ���� �� �ؽ�Ʈ ����
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            textcoin += 1;
            messageText.text = $"{textcoin}";
            Destroy(gameObject); // ���� ������Ʈ ����
        }
    }


}
