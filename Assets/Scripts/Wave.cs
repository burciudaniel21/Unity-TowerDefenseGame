using UnityEngine;

[System.Serializable]
public class Wave
{
    [Header("Enemy Prefabs")]
    public GameObject standardEnemy;
    public GameObject fastEnemy;
    public GameObject toughEnemy;

    [Header("Wave Settings")]
    public float spawnRate = 1f;
    public int minNumberOfEnemies = 3;
    public int maxNumberOfEnemies = 6;
}