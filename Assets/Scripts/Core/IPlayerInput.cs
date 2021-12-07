using System;
using UnityEngine;

public interface IPlayerInput
{
    event Action Fire;
    event Action Reload;
    event Action<float> MouseWheelScrolled;
    event Action<int> KeyAlphaPressed;
    event Action Pause;
    event Action<Vector2> Rotate;
    event Action<Vector2> Move;
}
