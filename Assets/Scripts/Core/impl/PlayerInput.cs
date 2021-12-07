using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour, IPlayerInput
{
    private const string FireButton = "Fire1";
    private const string MouseScrollWheel = "Mouse ScrollWheel";

    public event Action Fire;
    public event Action Reload;
    public event Action<float> MouseWheelScrolled;
    public event Action<int> KeyAlphaPressed;
    public event Action Pause;
    public event Action<Vector2> Rotate;
    public event Action<Vector2> Move;

    private void Update()
    {
        ListenToFire();
        ListenToReload();
        ListenToMouseScrollWheel();
        ListenToKeyAlpha();
        ListenToPause();
        ListenToRotate();
        ListenToMove();
    }

    private void ListenToFire()
    {
        if (Input.GetButton(FireButton))
        {
            Fire();
        }
    }

    private void ListenToReload()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    private void ListenToMouseScrollWheel()
    {
        var axisValue = Input.GetAxis(MouseScrollWheel);
        if (axisValue != 0f)
        {
            MouseWheelScrolled(axisValue);
        }
    }

    private void ListenToKeyAlpha()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            KeyAlphaPressed(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            KeyAlphaPressed(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            KeyAlphaPressed(3);
        }
    }

    private void ListenToPause()
    {
        //throw new NotImplementedException();
    }

    private void ListenToRotate()
    {
        //throw new NotImplementedException();
    }

    private void ListenToMove()
    {
        //throw new NotImplementedException();
    }
}
