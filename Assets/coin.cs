using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Text를 사용하려면 필요
using TMPro;

public class coin : MonoBehaviour
{
    public TMP_Text messageText;  // Unity에서 연결할 UI 텍스트

    public int textcoin = 0;

    void Start()
    {
        messageText.text = "";  // 시작 시 텍스트 비우기
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            textcoin += 1;
            messageText.text = $"{textcoin}";
            Destroy(gameObject); // 코인 오브젝트 제거
        }
    }


}
