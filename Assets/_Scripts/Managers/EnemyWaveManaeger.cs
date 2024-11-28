using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveManaeger : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private GameObject enemyPrefab;
    
    private List<EnemyController> enemies = new List<EnemyController>();
    
    private int wave;
    private float timeToNextWave; // in seconds
    
    public int Wave => wave;
    public float TimeToNextWave => timeToNextWave;
    
    private void Start()
    {
        GameManager.Instance.GameSave.AddListener(SaveWave);
        if(TryToLoadData()) return;

        SetWave(1);
    }

    private void SaveWave()
    {
        SavingManager.Instance.SaveWave(wave);
    }

    private void Update()
    {
        if (timeToNextWave <= 0 && enemies.Count == 0)
        {
            StartWave();
        }
        else
        {
            timeToNextWave -= Time.deltaTime;
        }
    }
    private void StartWave()
    {
        int enemiesToSpawn = wave * 2;
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            SpawnEnemyOnRandomPosition();
        }
    }
    private void SpawnEnemyOnRandomPosition()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
        EnemyController enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity).GetComponent<EnemyController>();
        enemy.DeathEvent.AddListener(CheckIfWaveClear);
        enemies.Add(enemy);
    }

    private void CheckIfWaveClear()
    {
        int numberOfEnemies = 0;
        
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] != null)
            {
                numberOfEnemies++;
                if(numberOfEnemies > 1) return;
            }
        }
        
        enemies = new List<EnemyController>();
        SetWave(wave + 1);
    }

    private void SetWave(int _wave)
    {
        wave = _wave;
        timeToNextWave = 20 + wave * 10;
    }

    private bool TryToLoadData()
    {
        if (SavingManager.LoadInventoryAtStart)
        {
            wave = SavingManager.Instance.LoadWave();
            SetWave(wave);
            return true;
        }
        
        return false;
    }
}
