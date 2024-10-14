using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class PlayerInputController : TopDownController
{
    [SerializeField] private CharacterSkin SkinHolder;
    [SerializeField] private Text PlayerName;
    [SerializeField] private PlayerInteractUI playerInteractUI;

    private TopDownAnimationController topDownAnimationController;

#pragma warning disable CS0108 // ¸â¹ö°¡ »ó¼ÓµÈ ¸â¹ö¸¦ ¼û±é´Ï´Ù. new Å°¿öµå°¡ ¾ø½À´Ï´Ù.
    private Camera camera;
#pragma warning restore CS0108 // ¸â¹ö°¡ »ó¼ÓµÈ ¸â¹ö¸¦ ¼û±é´Ï´Ù. new Å°¿öµå°¡ ¾ø½À´Ï´Ù.

    private bool blockControl;

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
    public void BlockControl(bool setVal)
    {
        if (setVal == true)
        {
            blockControl = true;
            CallMoveEvent(Vector2.zero);
        }
        else
        {
            blockControl = false;
        }
    }

    protected void FixedUpdate()
    {
        Vector2 newAim = ((Vector2)camera.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position).normalized;
        CallLookEvent(newAim);
    }
    protected override void Update()
    {
        base.Update();

        if (blockControl == false && GameManager.Instance.CanInteractWithTutor)
        {
            playerInteractUI.SetActivate(true);
        }
        else
        {
            playerInteractUI.SetActivate(false);
        }
    }
    protected void LateUpdate()
    {
        camera.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }

    public void OnMove(InputValue value)
    {
        if (blockControl) return;

        Vector2 moveInput = value.Get<Vector2>().normalized;
        CallMoveEvent(moveInput);
    }

    public void OnFire(InputValue value)
    {
        if (blockControl) return;

        isAttacking = value.isPressed;
    }

    public void OnInteract(InputValue value)
    {
        if (blockControl) return;

        if (GameManager.Instance.CanInteractWithTutor)
        {
            BlockControl(true);
            playerInteractUI.SetActivate(false);
            playerInteractUI.OpenInteractWithTutor();
        }
    }
}
