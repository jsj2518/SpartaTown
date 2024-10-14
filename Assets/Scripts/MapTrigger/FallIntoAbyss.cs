using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallIntoAbyss : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private UIController uiController;

    private PlayerInputController controller;
    private TopDownMovement moveAttribute;
    private Rigidbody2D rb;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        controller = collision.GetComponent<PlayerInputController>();
        moveAttribute = collision.GetComponent<TopDownMovement>();
        rb = collision.GetComponent<Rigidbody2D>();

        controller.BlockControl(true);
        moveAttribute.enabled = false;
        rb.gravityScale = 1f;
        rb.freezeRotation = false;
        Transform skinHolder = player.Find("SkinHolder");
        if (skinHolder != null && skinHolder.childCount > 0)
        {
            Transform skin = skinHolder.GetChild(0);
            Animator animator = skin.GetComponent<Animator>();
            if (animator != null ) animator.speed = 0f;
        }
        Invoke("SetVelocityZero", 0.01f);

        uiController.PutAwayAll();
    }

    private void Update()
    {
        if (rb != null)
        {
            player.Rotate(new Vector3(0, 0, 0.1f));
            if (player.position.y < -175)
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit(); // 어플리케이션 종료
#endif
            }
        }
    }

    private void SetVelocityZero()
    {
        rb.velocity = Vector3.zero;
    }
}
