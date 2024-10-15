using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private LayerMask levelCollisionLayer;

    private GameObject shooter;
    private bool isReady;

#pragma warning disable CS0108 // 멤버가 상속된 멤버를 숨깁니다. new 키워드가 없습니다.
    private Rigidbody2D rigidbody;
#pragma warning restore CS0108 // 멤버가 상속된 멤버를 숨깁니다. new 키워드가 없습니다.
    private SpriteRenderer spriteRenderer;

    private RangedAttackSO attackData;
    private float currentDuration;
    private Vector2 direction;

    private bool fxOnDestroy = true;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (isReady == false)
        {
            return;
        }

        currentDuration += Time.deltaTime;

        Color color = attackData.projectileColor;
        color.a = attackData.projectileColor.a * (attackData.duration - currentDuration) / attackData.duration;
        spriteRenderer.color = color;

        if (currentDuration > attackData.duration)
        {
            DestroyProjectile(transform.position, false);
        }
    }

    public void InitializeAttack(GameObject subject, Vector2 direction, RangedAttackSO attackData)
    {
        shooter = subject;

        this.attackData = attackData;
        this.direction = direction;

        UpdateProjectileSprite();
        currentDuration = 0;
        spriteRenderer.color = attackData.projectileColor;

        transform.right = this.direction;

        rigidbody.velocity = direction * attackData.speed;

        isReady = true;
    }

    private void UpdateProjectileSprite()
    {
        transform.localScale = Vector3.one * attackData.size;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsLayerMatched(levelCollisionLayer.value, collision.gameObject.layer))
        {
            Vector2 destoyPosition = collision.ClosestPoint(transform.position) - direction * 0.2f;
            DestroyProjectile(destoyPosition, fxOnDestroy);
        }
        else if ((IsLayerMatched(attackData.target1.value, collision.gameObject.layer) ||
                 IsLayerMatched(attackData.target2.value, collision.gameObject.layer))/* && collision.gameObject != shooter*/)
        {
            HealthSystem healthSystem = collision.GetComponent<HealthSystem>();
            if (healthSystem != null)
            {
                bool isAttackApplied = healthSystem.ChangeHealth(-attackData.power);

                if (isAttackApplied && attackData.isOnKnockBack)
                {
                    ApplyKnockback(collision);
                }
            }

            //DestroyProjectile(collision.ClosestPoint(transform.position), fxOnDestroy); 없애지 않음
        }
    }

    private bool IsLayerMatched(int value, int layer)
    {
        return value == (value | 1 << layer);
    }

    private void ApplyKnockback(Collider2D collision)
    {
        TopDownMovement movement = collision.GetComponent<TopDownMovement>();
        if (movement != null)
        {
            movement.ApplyKnockback(transform, attackData.knockbackPower, attackData.knockbackTime);
        }
    }

    private void DestroyProjectile(Vector3 position, bool createFx)
    {
        if (createFx)
        {
            // TODO : 충돌시 파티클
        }
        gameObject.SetActive(false);
    }
}
