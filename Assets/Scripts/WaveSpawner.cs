using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public Wave[] waves;
    public static int enemiesAlive = 0;

    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private TMP_Text waveCountdownText;
    [SerializeField] private GameObject bossEnemyPrefab;

    private void Start()
    {
        PlayerStats.roundsSurvived = 0;
        StartCoroutine(SpawnWavesForever());
    }

    private IEnumerator SpawnWavesForever()
    {
        while (true)
        {
            yield return StartCoroutine(SpawnWave());

            yield return new WaitUntil(() => enemiesAlive <= 0);

            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }

    private IEnumerator SpawnWave()
    {
        PlayerStats.roundsSurvived++;
        waveCountdownText.text = "Wave: " + Convert.ToString(PlayerStats.roundsSurvived);

        Wave wave = GetRandomWave();

        int extraEnemies = PlayerStats.roundsSurvived / 5;

        int minEnemies = wave.minNumberOfEnemies + extraEnemies;
        int maxEnemies = wave.maxNumberOfEnemies + extraEnemies;

        int numberOfEnemies = UnityEngine.Random.Range(minEnemies, maxEnemies + 1);

        for (int i = 0; i < numberOfEnemies; i++)
        {
            // Spawn boss halfway through every 10th wave
            if (PlayerStats.roundsSurvived % 10 == 0 && i == numberOfEnemies / 2)
            {
                SpawnBossEnemy();
            }

            GameObject enemyToSpawn = GetWeightedEnemyForCurrentWave(wave);
            SpawnEnemy(enemyToSpawn);

            yield return new WaitForSeconds(0.25f / wave.spawnRate);
        }
    }

    private GameObject GetWeightedEnemyForCurrentWave(Wave wave)
    {
        int standardWeight;
        int fastWeight;
        int toughWeight;

        int currentWave = PlayerStats.roundsSurvived;

        if (currentWave <= 4)
        {
            standardWeight = 85;
            fastWeight = 15;
            toughWeight = 0;
        }
        else if (currentWave <= 9)
        {
            standardWeight = 70;
            fastWeight = 25;
            toughWeight = 5;
        }
        else if (currentWave <= 19)
        {
            standardWeight = 60;
            fastWeight = 25;
            toughWeight = 15;
        }
        else if (currentWave <= 39)
        {
            standardWeight = 50;
            fastWeight = 30;
            toughWeight = 20;
        }
        else
        {
            standardWeight = 45;
            fastWeight = 35;
            toughWeight = 20;
        }

        // Boss waves get lighter support so the boss stays the main threat
        if (currentWave % 10 == 0)
        {
            standardWeight = 60;
            fastWeight = 30;
            toughWeight = 10;
        }

        int totalWeight = standardWeight + fastWeight + toughWeight;
        int roll = UnityEngine.Random.Range(0, totalWeight);

        if (roll < standardWeight)
        {
            return wave.standardEnemy;
        }

        roll -= standardWeight;

        if (roll < fastWeight)
        {
            return wave.fastEnemy;
        }

        return wave.toughEnemy;
    }

    private void SpawnBossEnemy()
    {
        enemiesAlive++;
        Instantiate(bossEnemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    private void SpawnEnemy(GameObject enemy)
    {
        if (enemy == null)
        {
            Debug.LogWarning("Enemy prefab is missing in Wave setup.");
            return;
        }

        enemiesAlive++;
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }

    private Wave GetRandomWave()
    {
        int randomIndex = UnityEngine.Random.Range(0, waves.Length);
        return waves[randomIndex];
    }
}