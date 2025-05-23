using UnityEngine;

public class PetFollow : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 3f;
    public float followThreshold = 0.1f; // 멈추는 최소 거리
    public float minDistance = 1.5f;     // 너무 가까이 붙지 않도록 유지할 최소 거리
    public float runSpeedMultiplier = 1.8f; // 플레이어 달릴 때 펫 속도 증가 배율

    private Animator animator;
    private float initialY;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        if (animator == null)
        {
            Debug.LogError("❗ Animator를 찾을 수 없습니다. 자식 오브젝트에 Animator가 있는지 확인하세요.");
        }

        initialY = transform.position.y;
    }

    void Update()
    {
        if (target == null)
        {
            Debug.LogWarning("❗ PetFollow: target이 비어 있습니다.");
            return;
        }

        Vector3 petPosFlat = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 targetPosFlat = new Vector3(target.position.x, 0, target.position.z);
        float distance = Vector3.Distance(petPosFlat, targetPosFlat);

        // 달리기 여부 체크 (LeftShift 눌렀는지)
        bool isPlayerRunning = Input.GetKey(KeyCode.LeftShift);

        // 현재 이동 속도 계산
        float currentSpeed = moveSpeed * (isPlayerRunning ? runSpeedMultiplier : 1f);

        bool shouldMove = (distance > followThreshold) && (distance > minDistance);

        if (shouldMove)
        {
            Vector3 direction = (targetPosFlat - petPosFlat).normalized;
            Vector3 newPosition = transform.position + new Vector3(direction.x, 0, direction.z) * currentSpeed * Time.deltaTime;
            newPosition.y = initialY; // y 고정
            transform.position = newPosition;

            Vector3 lookTarget = new Vector3(target.position.x, transform.position.y, target.position.z);
            transform.LookAt(lookTarget);
        }
        else
        {
            Vector3 pos = transform.position;
            pos.y = initialY;
            transform.position = pos;
        }

        if (animator != null)
        {
            animator.SetBool("isMoving", shouldMove);
        }
    }
}
