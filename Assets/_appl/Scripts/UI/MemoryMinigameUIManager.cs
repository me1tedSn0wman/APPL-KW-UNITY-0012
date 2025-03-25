using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MemoryMinigame
{
    public class MemoryMinigameUIManager : MonoBehaviour
    {
        [SerializeField] private MemoryMinigameManager manager;

        [SerializeField] private GameObject canvas_StartGameWindow;
        [SerializeField] private TextMeshProUGUI text_CountOfPairs;
        [SerializeField] private Slider slider_CountOfPairs;
        [SerializeField] private Button button_StartGame;

        [SerializeField] private GameOverUI gameOverUI;

        [SerializeField] private Button button_MainMenu;

        int countOfPairs;

        public void Awake()
        {
            canvas_StartGameWindow.SetActive(true);
        }

        public void Start()
        {
            UpdateCountOfPairs(countOfPairs);
            slider_CountOfPairs.onValueChanged.AddListener(UpdateCountOfPairs);

            button_StartGame.onClick.AddListener(() =>
            {
                manager.StartGame(countOfPairs);
                canvas_StartGameWindow.SetActive(false);
            });

            button_MainMenu.onClick.AddListener(() =>
            {
                SceneManager.LoadScene("MainMenu");
            });
        }

        public void UpdateCountOfPairs(float count)
        {
            countOfPairs = Mathf.FloorToInt(count);
            text_CountOfPairs.text = "Число пар: " + countOfPairs.ToString();
        }

        public void GameOverUI() {
            gameOverUI.SetActive(true);
        }
    }
}