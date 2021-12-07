using System;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField] private int selectedWeaponId = 0;

    public event Action<Weapon> SelectedWeaponChanged = weapon => { };

    private IPlayerInput PlayerInput;

    private Weapon currentWeapon;

    public Weapon CurrentWeapon { get => currentWeapon; }

    private void Awake()
    {
        PlayerInput = CompositionRoot.GetPlayerInput();
        PlayerInput.MouseWheelScrolled += OnMouseScrollWheel;
        PlayerInput.KeyAlphaPressed += OnKeyAlpha;
    }

    private void Start()
    {
        SelectWeapon();
    }

    private void OnMouseScrollWheel(float axisValue)
    {
        MouseWheelSelect(axisValue);
        SelectWeapon();
    }

    private void OnKeyAlpha(int keyAlpha)
    {
        KeyAlphaSelect(keyAlpha);
        SelectWeapon();
    }

    private void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weaponTransform in transform)
        {
            if (i == selectedWeaponId)
            {
                weaponTransform.gameObject.SetActive(true);
                currentWeapon = weaponTransform.GetComponent<Weapon>();
            }
            else
            {
                weaponTransform.gameObject.SetActive(false);
            }
            i++;
        }
        SelectedWeaponChanged(currentWeapon);
    }

    private void MouseWheelSelect(float axisValue)
    {
        if (axisValue > 0f)
        {
            if (selectedWeaponId >= transform.childCount - 1)
            {
                selectedWeaponId = 0;
            }
            else
            {
                selectedWeaponId++;
            }
        }
        else
        {
            if (selectedWeaponId <= 0)
            {
                selectedWeaponId = transform.childCount - 1;
            }
            else
            {
                selectedWeaponId--;
            }
        }
    }


    private void KeyAlphaSelect(int keyAlpha)
    {
        switch (keyAlpha)
        {
            case 1:
                selectedWeaponId = 0;
                break;
            case 2:
                selectedWeaponId = 1;
                break;
            case 3:
                selectedWeaponId = 2;
                break;
        }
    }
}
