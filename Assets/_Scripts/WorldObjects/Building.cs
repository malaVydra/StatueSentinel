using UnityEngine;

public class Building : MonoBehaviour
{
    private BuildingPoint buildingPoint;
    
    public void SetBuildingPoint(BuildingPoint _buildingPoint)
    {
        buildingPoint = _buildingPoint;
    }
    private void OnDestroy()
    {
        buildingPoint.gameObject.SetActive(true);
    }
}
