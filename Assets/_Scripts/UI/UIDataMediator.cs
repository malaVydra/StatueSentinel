using TMPro;
using UnityEngine;

public class UIDataMediator : MonoBehaviour
{
    [SerializeField] private TMP_Text healthText, waveText, timeToNextWaveText;
    [SerializeField] private HealthComponent wellHealthComponent;
    [SerializeField] private EnemyWaveManaeger waveManager;
    private void Update()
    {
        healthText.text = $"Well Health: {wellHealthComponent.Health}";
        waveText.text = $"Wave: {waveManager.Wave}";
        int timeLeft = Mathf.FloorToInt(waveManager.TimeToNextWave);
        timeToNextWaveText.text = $"Time to next wave: {(timeLeft < 0 ? 0 : timeLeft)}";
    }
}