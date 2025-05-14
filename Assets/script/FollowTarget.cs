using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform target;
    public float followSpeed = 3f;
    public float stopDistance = 0.01f;
    public float yRotationOffset = 0f;
    public Transform lookAtBone;

    void Update()
    {
        if (target == null) return;

        Vector3 targetPosition = target.position;
        targetPosition.y = transform.position.y;

        Vector3 direction = target.position - transform.position;
        direction.y = 0; // ✅ 수평만 바라보게
        direction = direction.normalized;

        float distance = Vector3.Distance(transform.position, targetPosition);

        // 이동
        if (distance > stopDistance)
        {
            transform.position += direction * followSpeed * Time.deltaTime;
        }

        // 회전
        if (direction != Vector3.zero && lookAtBone != null)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            lookRotation *= Quaternion.Euler(0, yRotationOffset, 0);
            lookAtBone.localRotation = Quaternion.Slerp(lookAtBone.localRotation, lookRotation, Time.deltaTime * 5f);
        }
    }
}
