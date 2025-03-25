using MemoryMinigame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Puzzle15Minigame
{
    public enum GameState
    {
        PiecesSpawn,
        Gameplay,
        Showcase
    }

    public class Puzzle15Manager : MonoBehaviour
    {
        public GameState gameState;

        [SerializeField] private Puzzle15Manager uiManager;

        [SerializeField] private Transform spawnPos;

        [SerializeField] private int gridSize;

        [SerializeField] private float x_step, y_step;
        [SerializeField] private float moveDelay;

        [SerializeField] private PuzzlePiece prefab_PuzzlePiece;

        [SerializeField] private Vector2Int emptyGridPos;
        Vector2 gridOffset;

        [SerializeField] private int randomShuffleSteps = 50;

        [SerializeField] private Dictionary<Vector2Int, PuzzlePiece> dictOfPieces;

        List<Vector2Int> availMoves;
        public void Start()
        {

        }

        public void StartGame(int gridSize) {
            gameState = GameState.PiecesSpawn;
            this.gridSize = gridSize;

            availMoves = new List<Vector2Int>();
            SpawnDeck();

            ShuffleDeck();
            //StartCoroutine(ShuffleDeckAnimate());
        }

        public void SpawnDeck() {
            dictOfPieces = new Dictionary<Vector2Int, PuzzlePiece>();

            gridOffset = new Vector2(
                ((float)gridSize) / 2-gridSize,
                ((float)gridSize) / 2-gridSize
                );

            for (int i = 0; i < gridSize; i++) { 
                for (int j=0; j< gridSize; j++)
                {
                    if (i == gridSize - 1 && j == gridSize - 1)
                        continue;
                                       
                    SpawnPiece(new Vector2Int(i, (gridSize-1) - j), j * gridSize + (i + 1));
      
                }
            }
            emptyGridPos = new Vector2Int(gridSize-1, 0);
            dictOfPieces.Add(emptyGridPos, null);
        }

        public void ShuffleDeck() {
            for (int i = 0; i < randomShuffleSteps; i++) {
                MoveIntoEmptyCellRandomPiece();
            }
            gameState = GameState.Gameplay;
        }

        public IEnumerator ShuffleDeckAnimate() {
            for (int i = 0; i < randomShuffleSteps; i++) {
                MoveIntoEmptyCellRandomPiece();
                yield return new WaitForSeconds(moveDelay);
            }
            gameState = GameState.Gameplay;
        }

        public void MoveIntoEmptyCellRandomPiece() {
            availMoves.Clear();

            if ((emptyGridPos.x - 1) >= 0 && (emptyGridPos.x - 1) <= (gridSize - 1) && (emptyGridPos.y - 0) >= 0 && (emptyGridPos.y - 0) <= (gridSize - 1))
                availMoves.Add(new Vector2Int(emptyGridPos.x - 1, emptyGridPos.y + 0));
            if ((emptyGridPos.x + 1) >= 0 && (emptyGridPos.x + 1) <= (gridSize - 1) && (emptyGridPos.y - 0) >= 0 && (emptyGridPos.y - 0) <= (gridSize - 1))
                availMoves.Add(new Vector2Int(emptyGridPos.x + 1, emptyGridPos.y + 0));

            if ((emptyGridPos.x - 0) >= 0 && (emptyGridPos.x - 0) <= (gridSize - 1) && (emptyGridPos.y - 1) >= 0 && (emptyGridPos.y - 1) <= (gridSize - 1))
                availMoves.Add(new Vector2Int(emptyGridPos.x - 0, emptyGridPos.y - 1));
            if ((emptyGridPos.x + 0) >= 0 && (emptyGridPos.x + 0) <= (gridSize - 1) && (emptyGridPos.y + 1) >= 0 && (emptyGridPos.y + 1) <= (gridSize - 1))
                availMoves.Add(new Vector2Int(emptyGridPos.x + 0, emptyGridPos.y + 1));

            int rndInd = Random.Range(0, availMoves.Count);
            MovePieceToEmptyCell(dictOfPieces[availMoves[rndInd]], false);
        }

        public void SpawnPiece(Vector2Int gridPos, int number) {
            PuzzlePiece newPiece = Instantiate(prefab_PuzzlePiece, spawnPos);
            newPiece.SetUpPiece(number, gridPos);
            newPiece.SetUpManager(this);
            
            dictOfPieces.Add(gridPos, newPiece);
        }

        public void ClickedPiece(PuzzlePiece piece) {
            if (gameState != GameState.Gameplay) return;

            if (false
                || Mathf.Abs(emptyGridPos.x - piece.gridPos.x) == 1 && Mathf.Abs(emptyGridPos.y - piece.gridPos.y) == 0
                || Mathf.Abs(emptyGridPos.x - piece.gridPos.x) == 0 && Mathf.Abs(emptyGridPos.y - piece.gridPos.y) == 1
                ) 
            {
                MovePieceToEmptyCell(piece, true);
                StartCoroutine(WaitMoveDelay());
            }
        }

        public void MovePieceToEmptyCell(PuzzlePiece piece, bool isAnimate) {
            Debug.Log(piece.gridPos.x + " _ " + piece.gridPos.y + " _ moving to empty pos" + emptyGridPos.ToString());

            Vector2Int temp = piece.gridPos;
            piece.MoveToGridPos(emptyGridPos, isAnimate);
            emptyGridPos = temp;

            dictOfPieces[piece.gridPos] = piece;
            dictOfPieces[emptyGridPos] = null;
        }

        public Vector3 GridPosToWorldPos(Vector2Int gridPos) {
            Vector3 pos = new Vector3(
                (gridOffset.x + gridPos.x) * x_step,
                (gridOffset.y + gridPos.y) * y_step,
                0
                );

            return pos;
        }

        private IEnumerator WaitMoveDelay()
        {
            gameState = GameState.Showcase;
            yield return new WaitForSeconds(moveDelay);
            CheckEndGame();
            gameState = GameState.Gameplay;
        }

        public void CheckEndGame() {
            foreach (KeyValuePair<Vector2Int, PuzzlePiece> pieces in dictOfPieces) {
                if (pieces.Value == null) {
                    continue;
                }
                if (!pieces.Value.IsOnStartPos())
                {
                    return;
                }
            }
            GameOver();
        }

        public void GameOver()
        {
            uiManager.GameOver();
            Debug.LogError("GameOver");
        }
    }
}