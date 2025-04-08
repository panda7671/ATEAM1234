using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{

    public Transform player;
    public float distance = 6.0f;
    public float height = 3.0f;
    public float smoothSpeed = 5.0f;

    void LateUpdate()
    {
        // 플레이어의 뒤쪽 위치 계산 (방향 따라감)
        Vector3 desiredPosition = player.position - player.forward * distance + Vector3.up * height;

        // 부드럽게 이동
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothSpeed);

        // 항상 플레이어 바라보게 회전
        transform.LookAt(player.position + Vector3.up * 1.5f); // 약간 위쪽 바라보게
    }
}
