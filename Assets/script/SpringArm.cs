using UnityEngine;

public class SpringArm : MonoBehaviour
{
    public float TargetLength = 3f;

    private void LateUpdate()
    {
        // 간단한 예시: 카메라를 뒤로 뺌
        if (Camera.main != null)
        {
            Camera.main.transform.position = transform.position - transform.forward * TargetLength;
        }
    }
}
