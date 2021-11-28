using UnityEngine;

public class GameCamera : MonoBehaviour, IGameCamera
{
    [Range(0f, 30f)]
    [SerializeField]
    private float smoothSpeed = 10f;

    [SerializeField]
    private Vector3 offset;

    [SerializeField]
    private Vector3 cameraAngle;

    private Transform target;

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    private void Start()
    {
        transform.rotation = Quaternion.Euler(cameraAngle);
    }

    private void FixedUpdate()
    {
        var desiredPosition = target.position + offset;
        var smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
