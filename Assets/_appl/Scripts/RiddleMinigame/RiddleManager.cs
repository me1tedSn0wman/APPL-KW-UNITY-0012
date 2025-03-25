using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RiddleMinigame
{
    public class RiddleManager : MonoBehaviour
    {
        [SerializeField] private RiddleMinigameUIManager uIManager;

        [SerializeField] private Image riddleImage;
        [SerializeField] private InputField inputField;
        [SerializeField] private Transform spawnPos;
        [SerializeField] private RiddleDataDef[] riddlesData;
        [SerializeField] private RiddlePiece prefab_RiddleCell;
        [SerializeField] private List<RiddlePiece> listOfPieces;
        [SerializeField] private float x_step;
        RiddleDataDef crntRiddle;

        string crntWord;
        string inputString;

        public void Start()
        {
            MakeRiddle();
            inputField.onValueChanged.AddListener((value) =>
            {
                UpdateInputString();
            });
        }

        public void MakeRiddle(int riddleIndex = -1)
        {
            int ind = riddleIndex;
            if (riddleIndex == -1)
            {
                ind = Random.Range(0, riddlesData.Length);
            }
            crntRiddle = riddlesData[ind];

            riddleImage.sprite = crntRiddle.riddleImage;

            listOfPieces = new List<RiddlePiece>();
            crntWord = crntRiddle.riddleWorld.ToUpper();
            for (int i = 0; i < crntWord.Length; i++)
            {
                RiddlePiece newPiece = Instantiate(prefab_RiddleCell, spawnPos);

                Vector3 pos = new Vector3(
                    x_step * (i - (0.5f * (crntWord.Length-1))),
                    spawnPos.position.y,
                    0
                    );
                newPiece.SetUpPiece(pos, crntWord[i]);
                listOfPieces.Add(newPiece);
            }
            
        }

        public void UpdateInputString() {
            int crntLength = Mathf.Min(crntWord.Length, inputField.text.Length);

            inputString = inputField.text.ToUpper().Substring(0, crntLength);


            for (int i = 0; i < crntLength; i++) {
                listOfPieces[i].WriteLetter(inputString[i]);
            }
            for (int i = crntLength; i < listOfPieces.Count; i++) {
                listOfPieces[i].RemoveLetter();
            }
            CheckEndGame();
        }

        public void CheckEndGame()
        {
            if (crntWord.Length != inputString.Length) return;

            for(int i=0;)
        }

        public void GameOver() {
            uIManager.GameOverUI();
            Debug.LogError("GameOver");
        }
    }
}