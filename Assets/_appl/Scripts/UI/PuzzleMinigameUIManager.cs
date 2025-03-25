using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Puzzle15Minigame
{
    public class Puzzle15UIManager : MonoBehaviour
    {
        [SerializeField] private Puzzle15Manager manager;

        [SerializeField] private GameObject canvas_StartGameWindow;
        [SerializeField] private TextMeshProUGUI text_GridSize;
        [SerializeField] private Slider slider_GridSize;
        [SerializeField] private Button button_StartGame;

        [SerializeField] private GameOverUI gameOverUI;

        [SerializeField] private Button button_MainMenu;

        int gridSize=4;

        public void Awake()
        {
            canvas_StartGameWindow.SetActive(true);
        }

        public void Start()
        {
            UpdateCountOfPairs(gridSize);
            slider_GridSize.onValueChanged.AddListener(UpdateCountOfPairs);

            button_StartGame.onClick.AddListener(() =>
            {
                manager.StartGame(gridSize);
                canvas_StartGameWindow.SetActive(false);
            });

            button_MainMenu.onClick.AddListener(() =>
            {
                SceneManager.LoadScene("MainMenu");
            });
        }

        public void UpdateCountOfPairs(float count)
        {
            gridSize = Mathf.FloorToInt(count);
            text_GridSize.text = "Размер поля: " + gridSize.ToString();
        }

        public void GameOverUI()
        {
            gameOverUI.SetActive(true);
        }
    }
}