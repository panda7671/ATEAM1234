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
        // �÷��̾��� ���� ��ġ ��� (���� ����)
        Vector3 desiredPosition = player.position - player.forward * distance + Vector3.up * height;

        // �ε巴�� �̵�
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothSpeed);

        // �׻� �÷��̾� �ٶ󺸰� ȸ��
        transform.LookAt(player.position + Vector3.up * 1.5f); // �ణ ���� �ٶ󺸰�
    }
}
