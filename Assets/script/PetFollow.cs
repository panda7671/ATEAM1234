using UnityEngine;

public class PetFollow : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 3f;
    public float followThreshold = 0.1f;

    private Animator animator;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        if (animator == null)
        {
            Debug.LogError("❗ Animator를 찾을 수 없습니다. 자식 오브젝트에 Animator가 있는지 확인하세요.");
        }
    }

    void Update()
    {
        if (target == null)
        {
            Debug.LogWarning("❗ PetFollow: target이 비어 있습니다.");
            return;
        }

        // Y 제외한 평면 거리 계산
        Vector3 petPosFlat = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 targetPosFlat = new Vector3(target.position.x, 0, target.position.z);
        float distance = Vector3.Distance(petPosFlat, targetPosFlat);

        Debug.Log($"📍 [PetFollow] Distance to target: {distance}");
        Debug.Log($"📍 [PetFollow] Pet Position: {transform.position}, Target Position: {target.position}");

        bool shouldMove = distance > followThreshold;

        if (shouldMove)
        {
            // 이동
            Vector3 direction = (targetPosFlat - petPosFlat).normalized;
            transform.position += new Vector3(direction.x, 0, direction.z) * moveSpeed * Time.deltaTime;

            // 회전
            Vector3 lookTarget = new Vector3(target.position.x, transform.position.y, target.position.z);
            transform.LookAt(lookTarget);
        }

        // 애니메이션 제어
        if (animator != null)
        {
            animator.SetBool("isMoving", shouldMove);

            // 디버그용: 현재 상태 체크
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            Debug.Log($"🎞️ Animator State: {stateInfo.shortNameHash}");
            Debug.Log($"🎞️ isMoving 값: {animator.GetBool("isMoving")}");
            Debug.Log($"🎞️ Is 'Run'?: {stateInfo.IsName("Run")}");
        }

        if (!shouldMove)
        {
            Debug.Log("🛑 멈춤");
        }
    }
}
