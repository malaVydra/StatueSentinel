using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    
    private Rigidbody2D rb2D;
    private HealthComponent healthComponent;
    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        healthComponent = GetComponent<HealthComponent>();
        
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
        Destroy(gameObject);
        
        //Add Particles
    }
}
