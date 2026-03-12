using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [HideInInspector]
    public float speed;

    public float startSpeed = 10f;
    private float scaledStartSpeed;

    public float startHp;
    public float currentHp;
    private float currentScaledUpHp;

    public int moneyGain = 50;

    [SerializeField] GameObject enemyDeathEffet;
    [SerializeField] Image enemyHpBar;

    private void Start()
    {
        scaledStartSpeed = startSpeed * (1f + PlayerStats.roundsSurvived * 0.01f);
        speed = scaledStartSpeed;

        Spawn();
        currentScaledUpHp = currentHp;
    }

    private void Spawn()
    {
        currentHp = startHp * (1 + PlayerStats.roundsSurvived * 0.04f);
    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        enemyHpBar.fillAmount = currentHp / currentScaledUpHp;

        if (currentHp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);

        int reward = Mathf.RoundToInt(moneyGain * (1f + PlayerStats.roundsSurvived * 0.01f));
        PlayerStats.Money += reward;

        GameObject effect = Instantiate(enemyDeathEffet, transform.position, Quaternion.identity);
        Destroy(effect, 5f);

        WaveSpawner.enemiesAlive--;
    }

    public void Slow(float slowPercentage)
    {
        speed = scaledStartSpeed * (1f - slowPercentage);
    }
}