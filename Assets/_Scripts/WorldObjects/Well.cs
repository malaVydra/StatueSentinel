using UnityEngine;

public class Well : MonoBehaviour
{
    private HealthComponent healthComponent;
    private void Awake()
    {
        healthComponent = GetComponent<HealthComponent>();
        healthComponent.HealthLost.AddListener(DestroyWell);
        
        GameManager.Instance.GameSave.AddListener(SaveWell);

        if (SavingManager.LoadInventoryAtStart)
        {
            healthComponent.SetHealth(SavingManager.Instance.LoadWellHealth());
        }
    }
    private void SaveWell()
    {
        SavingManager.Instance.SaveWellData(healthComponent.Health);
    }
    private void DestroyWell()
    {
        GameManager.Instance.GameOver();
    }
}