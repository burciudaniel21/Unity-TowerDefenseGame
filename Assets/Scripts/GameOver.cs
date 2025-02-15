using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] TMP_Text roundsSurvived;
    [SerializeField] string menuScene = "MainMenu";
    void OnEnable()
    {
        roundsSurvived.text = PlayerStats.roundsSurvived.ToString();
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        SceneManager.LoadScene(menuScene);
    }
}
