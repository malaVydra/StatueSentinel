using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> possibleResourceObjectPrefabs;
    [SerializeField] private float respawnTime = 15f; 
    
    private ResourceObject resourceObject;
    private void Start()
    {
        SpawnRandomResourceObject();
    }
    private void SpawnRandomResourceObject()
    {
        GameObject randomObject = possibleResourceObjectPrefabs[Random.Range(0, possibleResourceObjectPrefabs.Count)];
        resourceObject = Instantiate(randomObject, transform).GetComponent<ResourceObject>();
        resourceObject.transform.localPosition = Vector2.zero;
        
        resourceObject.Destroyed.AddListener(() => StartCoroutine(TimedSpawnCoroutine(respawnTime)));
    }

    private IEnumerator TimedSpawnCoroutine(float _timeToSpawn)
    {
        yield return new WaitForSeconds(_timeToSpawn);
        SpawnRandomResourceObject();
    }
}
