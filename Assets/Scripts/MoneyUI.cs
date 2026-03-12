using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] TMP_Text moneyText;
    private void Update()
    {
        moneyText.text = "Gold:" +PlayerStats.Money.ToString();
    }
}
