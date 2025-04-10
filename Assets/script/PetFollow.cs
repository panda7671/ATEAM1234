using UnityEngine;

public class PetFollow : MonoBehaviour
{
    public Transform target;            // 따라갈 캐릭터
    public float followSpeed = 5f;      // 따라가는 속도
    public Vector3 followOffset = new Vector3(0, 5f, -3f); // 위에서 내려다보는 위치

    void Update()
    {
        if (target == null) return;

        // 따라갈 위치 = 타겟 위치 + 오프셋 (로컬 좌표 기준)
        Vector3 targetPosition = target.position + target.TransformDirection(followOffset);

        // 부드럽게 따라가기
        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            Time.deltaTime * followSpeed
        );

        // 타겟 바라보기 (아래에서 위가 아니라 자연스럽게)
        transform.LookAt(target.position + Vector3.up * 1.5f); // 살짝 위를 바라보게
    }
}
