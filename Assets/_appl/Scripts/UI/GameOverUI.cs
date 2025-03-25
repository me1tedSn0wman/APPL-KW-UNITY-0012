using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Button button_Restart;

    public void Awake()
    {
        SetActive(false);

        button_Restart.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("MemoryMinigame");
        });
    }

    public void SetActive(bool value) { 
        gameObject.SetActive(value);
    }
}
