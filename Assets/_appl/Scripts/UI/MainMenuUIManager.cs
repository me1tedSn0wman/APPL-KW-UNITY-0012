using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField] private Button button_MemoryMinigame;
    [SerializeField] private Button button_Puzzle15Minigame;
    [SerializeField] private Button button_RiddleMinigame;

    public void Start()
    {
        button_MemoryMinigame.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("MemoryMinigame");
        });
        button_Puzzle15Minigame.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Puzzle15Minigame");
        });
        button_RiddleMinigame.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("RiddleMinigame");
        });
    }
}
