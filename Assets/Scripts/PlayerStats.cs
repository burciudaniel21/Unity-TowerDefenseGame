using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Money;
    public static int Lives;
    public static int roundsSurvived;

    public int startMoney = 400;
    public int startLives = 20;

    public static PlayerStats playerStats;

    private void Awake()
    {
        if (playerStats == null)
        {
            playerStats = this;

            Money = startMoney;
            Lives = startLives;
            roundsSurvived = 0;
        }
        else if (playerStats != this)
        {
            Destroy(gameObject);
        }
    }

    public void ReduceHP(int amount)
    {
        Lives -= amount;
    }
}