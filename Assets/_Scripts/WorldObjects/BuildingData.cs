using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "BuildingData", menuName = "Buildings/Building Data")]
public class BuildingData : ScriptableObject
{
    [Header("Basic Data")]
    [SerializeField] private string buildingID;
    [SerializeField] private string buildingName;
    [SerializeField][Multiline] private string buildingDescription;
    [SerializeField] private GameObject buildingPrefab;
    
    [Header("Resource Cost")]
    [SerializeField] private List<Item> _recipeList;
    
    #region Public Properties
    public string BuildingID => buildingID;
    public string BuildingName => buildingName;
    public string BuildingDescription => buildingDescription;
    public GameObject BuildingPrefab => buildingPrefab;
    public List<Item> RecipeList => _recipeList;
    #endregion
}
