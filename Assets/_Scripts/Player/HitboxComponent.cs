using System;
using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public enum HitboxLayer
{
    PlayerHitbox,
    EnemyHitbox
}
public class HitboxComponent : MonoBehaviour
{
    [SerializeField] private HitboxLayer hitBoxLayer;
    [SerializeField] private bool isActive = false;
    private bool alwaysActive = false;
    [SerializeField] private float damage = 5f;
    [SerializeField] private float damageCooldown = 2f;
    [SerializeField] private Collider2D hitboxCollider;
    
    private bool coolDownActive = false;
    
    private void Awake()
    {
        alwaysActive = isActive;
    }
    public void SetActive(bool _active, float _damage)
    {
        if (coolDownActive) return;

        damage = _damage;
        isActive = _active;
        hitboxCollider.enabled = _active;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!isActive) return;

        if (other.TryGetComponent(out HurtboxComponent _hurtboxComponent) && !other.CompareTag("Player"))
        {
            if(hitBoxLayer != _hurtboxComponent.Layer) return;
            
            _hurtboxComponent.TakeDamage(damage);
            SetActive(false, damage);
            StartCoroutine(DamageCooldown());
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if(coolDownActive || !alwaysActive) return;
        
        if (other.TryGetComponent(out HurtboxComponent _hurtboxComponent))
        {
            if(hitBoxLayer != _hurtboxComponent.Layer) return;
            
            _hurtboxComponent.TakeDamage(damage);
            SetActive(false, damage);
            StartCoroutine(DamageCooldown());
        }
    }

    private IEnumerator DamageCooldown()
    {
        coolDownActive = true;
        yield return new WaitForSeconds(damageCooldown);
        coolDownActive = false;
        
        hitboxCollider.enabled = true;
    }
}