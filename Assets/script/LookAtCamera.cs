using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    void Update()
    {
        if (Camera.main != null)
            transform.forward = Camera.main.transform.forward;
    }
}
