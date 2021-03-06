using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour, IPlayerInput
{
    private const string FireButton = "Fire1";
    private const string MouseScrollWheel = "Mouse ScrollWheel";

    public event Action Fire = () => { };
    public event Action Reload = () => { };
    public event Action<float> MouseWheelScrolled = axisValue => { };
    public event Action<int> KeyAlphaPressed = keyValue => { };
    public event Action Escape = () => { };
    public event Action<Vector3> MousePositionUpdated = mousePos => { };
    public event Action<Vector2> Move = moveVector => { };

    private void Update()
    {
        ListenToFire();
        ListenToReload();
        ListenToMouseScrollWheel();
        ListenToKeyAlpha();
        ListenToEscape();
        ListenToMousePos();
        ListenToMove();
    }

    public void Enable()
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
    }

    public void Disable()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
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

    private void ListenToEscape()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Escape();
        }
    }

    private void ListenToMousePos()
    {
        MousePositionUpdated(Input.mousePosition);
    }

    private void ListenToMove()
    {
        var moveVector = Vector2.zero;
        moveVector.x = Input.GetAxisRaw("Horizontal");
        moveVector.y = Input.GetAxisRaw("Vertical");

        Move(moveVector.normalized);
    }
}
