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

    public Transform Target { get; set; }
    public Camera CameraComponent => GetComponent<Camera>();

    private void Awake()
    {
        transform.rotation = Quaternion.Euler(cameraAngle);
    }

    private void FixedUpdate()
    {
        // if target null
        var desiredPosition = Target.position + offset;
        var smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
