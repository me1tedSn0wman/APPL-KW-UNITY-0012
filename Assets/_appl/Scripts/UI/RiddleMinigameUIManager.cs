using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace RiddleMinigame
{
    public class RiddleMinigameUIManager : MonoBehaviour
    {
        [SerializeField] private RiddleManager manager;

        [SerializeField] private GameOverUI gameOverUI;

        [SerializeField] private Button button_MainMenu;

        public void Awake()
        {
        }

        public void Start()
        {

            button_MainMenu.onClick.AddListener(() =>
            {
                SceneManager.LoadScene("MainMenu");
            });
        }

        public void GameOverUI()
        {
            gameOverUI.SetActive(true);
        }
    }
}