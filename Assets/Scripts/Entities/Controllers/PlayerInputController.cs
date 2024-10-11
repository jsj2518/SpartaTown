using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerInputController : TopDownController
{
    [SerializeField] private CharacterSkin SkinHolder;
    [SerializeField] private Text PlayerName;

    private TopDownAnimationController topDownAnimationController;

#pragma warning disable CS0108 // ����� ��ӵ� ����� ����ϴ�. new Ű���尡 �����ϴ�.
    private Camera camera;
#pragma warning restore CS0108 // ����� ��ӵ� ����� ����ϴ�. new Ű���尡 �����ϴ�.

    private Vector2 worldMousePos;

    protected override void Awake()
    {
        base.Awake();

        topDownAnimationController = GetComponent<TopDownAnimationController>();
        camera = Camera.main;
    }

    protected void Start()
    {
        ResetPlayerObject();
    }

    public void ResetPlayerObject()
    {
        PlayerName.text = GameManager.Instance.PlayerName;
        SkinHolder.SetSkin();
        topDownAnimationController.SetAnimator(SkinHolder.GetAnimator());
    }

    protected void FixedUpdate()
    {
        Vector2 newAim = (worldMousePos - (Vector2)transform.position).normalized;
        CallLookEvent(newAim);
    }

    protected void LateUpdate()
    {
        camera.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }

    public void OnMove(InputValue value)
    {
        Vector2 moveInput = value.Get<Vector2>().normalized;
        CallMoveEvent(moveInput);
    }

    public void OnLook(InputValue value)
    {
        Vector2 newAim = value.Get<Vector2>();
        worldMousePos = camera.ScreenToWorldPoint(newAim);
    }

    public void OnFire(InputValue value)
    {
        isAttacking = value.isPressed;
    }
}
