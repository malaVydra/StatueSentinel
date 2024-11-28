using UnityEngine;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour
{
    public UnityEvent DeathEvent = new UnityEvent();
    
    [SerializeField] private float speed = 5f;
    
    private Rigidbody2D rb2D;
    private HealthComponent healthComponent;
    private HitboxComponent hitboxComponent;
    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        healthComponent = GetComponent<HealthComponent>();
        hitboxComponent = GetComponent<HitboxComponent>();
        
        healthComponent.HealthLost.AddListener(Death);
    }

    private void FixedUpdate()
    {
        Vector2 direction = Vector2.zero - rb2D.position;
        direction.Normalize();

        rb2D.velocity = direction * speed;
    }

    private void Death()
    {
        DeathEvent?.Invoke();
        Destroy(gameObject);
    }
}
