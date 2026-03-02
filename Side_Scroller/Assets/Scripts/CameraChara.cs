using UnityEngine;

public class CameraChara : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.2f;
    public Vector3 offset = new Vector3(0f, 0f);

    public float distance = 7f;

    Vector3 velocity = Vector3.zero; 
    void LateUpdate()
    {
        if (target == null) return;

        Vector3 targetPosition = target.position + offset;
        GetComponent<Camera>().orthographicSize = distance;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
