using UnityEngine;

public class Building : MonoBehaviour
{
    private BuildingPoint buildingPoint;
    private HealthComponent healthComponent;
    private void Awake()
    {
        healthComponent = GetComponent<HealthComponent>();
        healthComponent.HealthLost.AddListener(DestroyBuilding);
    }
    private void DestroyBuilding()
    {
        Destroy(gameObject);
        //Destroy particles
    }
    public void SetBuildingPoint(BuildingPoint _buildingPoint)
    {
        buildingPoint = _buildingPoint;
        
    }
    private void OnDestroy()
    {
        buildingPoint?.gameObject.SetActive(true);
    }
}