using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Puzzle15Minigame
{
    public class PuzzlePiece : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text_Number;
        [SerializeField] private float movementTimeDuration = 0.5f;

        public int id;

        public Vector2Int gridPos;
        private Vector2Int startGridPos;

        public Puzzle15Manager manager;

        public void SetUpPiece(int number, Vector2Int gridPos) {
            text_Number.text = number.ToString();
            startGridPos = gridPos;
            MoveToGridPos(gridPos, false);
        }

        public void SetUpManager(Puzzle15Manager manager) {
            this.manager = manager;
        }

        public void MoveToGridPos(Vector2Int gridPos, bool isAnimate) { 
            this.gridPos = gridPos;
            if (isAnimate)
            {
                MoveTo(manager.GridPosToWorldPos(gridPos));
            }
            else {
                transform.position = manager.GridPosToWorldPos(gridPos);
            }
        }

        public void MoveTo(Vector3 pos) {
            transform.DOMove(pos, movementTimeDuration);
        }

        public virtual void OnMouseDown()
        {
            manager.ClickedPiece(this);
        }

        public bool IsOnStartPos() {
            if (gridPos == startGridPos) {
                return true;
            }
            return false;
        }
    }
}
