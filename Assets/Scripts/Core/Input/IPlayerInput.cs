using System;
using UnityEngine;

public interface IPlayerInput
{
    event Action Fire;
    event Action Reload;
    event Action<float> MouseWheelScrolled;
    event Action<int> KeyAlphaPressed;
    event Action Escape;
    event Action<Vector3> MousePositionUpdated;
    event Action<Vector2> Move;

    void Disable();
    void Enable();
}
