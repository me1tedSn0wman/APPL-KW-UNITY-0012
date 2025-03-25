using TMPro;
using UnityEngine;

namespace RiddleMinigame
{
    public class RiddlePiece : MonoBehaviour
    {

        [SerializeField] private TextMeshProUGUI text_RiddleChar;

        [SerializeField] private char crntChar;
        [SerializeField] private char solutionChar;

        public void SetUpPiece(Vector3 pos, char solutionChar)
        {
            transform.position = pos;
            this.solutionChar = solutionChar;
        }

        public void WriteLetter(char letter)
        {
            this.crntChar = letter;
            text_RiddleChar.text = letter.ToString();
        }

        public void RemoveLetter()
        {
            text_RiddleChar.text = "";
        }

        public bool IsEnteredCorrectChar() {
            if (crntChar == solutionChar) {
                return true;
            }
            return false;
        }
    }
}