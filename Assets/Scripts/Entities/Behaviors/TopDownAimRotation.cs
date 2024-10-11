using System;
using UnityEngine;

public class TopDownAimRotation : MonoBehaviour
{
    [SerializeField] private CharacterSkin SkinHolder;

    [SerializeField] private Transform weaponHolder;

    private TopDownController controller;

    private void Awake()
    {
        controller = GetComponent<TopDownController>();
    }

    private void Start()
    {
        controller.OnLookEvent += OnAim;
    }

    private void OnAim(Vector2 direction)
    {
        RotateArm(direction);
    }

    private void RotateArm(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        bool flip = MathF.Abs(rotZ) > 90;
        SkinHolder.SetFlipX(flip);
        weaponHolder.rotation = Quaternion.Euler(0f, 0f, rotZ);
    }
}
