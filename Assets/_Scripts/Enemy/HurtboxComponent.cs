using System.Collections;
using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
public class HurtboxComponent : MonoBehaviour
{
    [SerializeField] private HitboxLayer hurtboxLayer;
    [SerializeField] private Material flashMaterial, defaultMaterial;

    private SpriteRenderer spriteRenderer;
    private HealthComponent healthComponent;
    public HitboxLayer Layer => hurtboxLayer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        healthComponent = GetComponent<HealthComponent>();
    }
    public void TakeDamage(float _damage)
    {
        healthComponent.ChangeHealth(-_damage);

        StartCoroutine(FlashSprite());
    }

    private IEnumerator FlashSprite()
    {
        spriteRenderer.material = flashMaterial;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.material = defaultMaterial;
    }
}
