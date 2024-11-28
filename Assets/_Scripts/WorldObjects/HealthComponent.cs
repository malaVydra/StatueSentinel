using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour
{
    [HideInInspector] public UnityEvent HealthLost = new UnityEvent();
    
    [SerializeField] private float maxHealth;
    private float health;
    public float Health => health;
    public void ChangeHealth(float _amount)
    {
        health += _amount;

        if (health <= 0)
        {
            HealthLost?.Invoke();
        }
        else if(health > maxHealth)
        {
            health = maxHealth;
        }
    }
    private void Awake()
    {
        health = maxHealth;
    }
    public void SetHealth(float loadWellHealth)
    {
        health = loadWellHealth;
    }
}
