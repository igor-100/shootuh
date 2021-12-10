using UnityEngine;

public interface IGameCamera
{
    Transform Target { get; set; }
    Camera CameraComponent { get; }
}
