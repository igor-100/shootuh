using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [Range(0f, 30f)]
    [SerializeField]
    private float smoothSpeed = 10f;

    [SerializeField]
    private Vector3 offset;

    public Vector3 GetOffset()
    {
        return offset;
    }

    private void FixedUpdate()
    {
        var desiredPosition = target.position + offset;
        var smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
