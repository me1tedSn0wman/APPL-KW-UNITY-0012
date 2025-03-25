using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemoryMinigame
{
    public enum GameState { 
        CardSpawn,
        Gameplay,
        Showcase
    }


    [Serializable]
    public struct CardDefinition
    {
        public string suit;
        public int rank;
        public Sprite spriteFront;
    }

    public class MemoryMinigameManager : MonoBehaviour
    {
        private GameState gameState;

        [SerializeField] private MemoryMinigameUIManager uiManager;

        [SerializeField] public CardDefinition[] cardDefinitions;

        private int pairsCount= 10;

        [SerializeField] public Transform spawnPos;
        [SerializeField] private MemoryCard prefab_MemoryCard;
        [SerializeField] private Sprite spriteBack;
        [SerializeField] private List<MemoryCard> crntDeck;

        [SerializeField] private float x_step, y_step;

        [SerializeField] private float showCardTimeOut;
        [SerializeField] private float moveCardDelay;

        MemoryCard activeCard;

        int crntPairs;
        public void Start()
        {
            
        }

        public void StartGame(int countOfPairs) {
            pairsCount = countOfPairs;
            SpawnDeck();
        }

        public void SpawnDeck() {
            gameState = GameState.CardSpawn;
            crntDeck = new List<MemoryCard>();

            for (int i = 0; i < pairsCount ; i ++) {
                SpawnCard(cardDefinitions[i]);
                SpawnCard(cardDefinitions[i]);
            }
            ShuffleCards(ref crntDeck);
            MoveCardsToSlots();
            crntPairs = pairsCount;
            gameState = GameState.Gameplay;
        }

        public void SpawnCard(CardDefinition cardDef) {
            MemoryCard newMemoryCard = Instantiate(prefab_MemoryCard, spawnPos);
            newMemoryCard.SetUpCard(cardDef, spriteBack);
            newMemoryCard.SetUpManager(this);
            newMemoryCard.HideCard();
            crntDeck.Add(newMemoryCard);
        }

        public void ShuffleCards(ref List<MemoryCard> oCards) { 
            List<MemoryCard> tCards = new List<MemoryCard>();

            int ndx;
            while (oCards.Count > 0) {
                ndx = UnityEngine.Random.Range(0, oCards.Count);
                tCards.Add(oCards[ndx]);
                oCards.RemoveAt(ndx);
            }
            oCards = tCards;
        }

        public void MoveCardsToSlots() {
            int width = Mathf.CeilToInt(Mathf.Sqrt(pairsCount));
            float x_ind = -width;
            float y_ind = ((float)width-1) / 2;
            for (int i = 0; i < crntDeck.Count; i++) {
                float x_pos = x_ind * x_step;
                float y_pos = y_ind * y_step;

                crntDeck[i].MoveTo(new Vector3(x_pos, y_pos, 0));

                x_ind += 1;
                if (x_ind > width) {
                    y_ind -= 1;
                    x_ind = -width;
                }
            }
        }

        public void ClickedThisCard(MemoryCard clickedCard) {
            if (gameState != GameState.Gameplay) return;

            if (activeCard == null)
            {
                activeCard = clickedCard;
                activeCard.ShowCard();
            }
            else if (activeCard == clickedCard)
            {
                activeCard = null;
                activeCard.HideCard();
            }
            else {

                StartCoroutine(CheckCards(activeCard, clickedCard));
            }
        }

        private IEnumerator CheckCards(MemoryCard memoryCardOne, MemoryCard memoryCardTwo) {
            gameState = GameState.Showcase;
            memoryCardOne.ShowCard();
            memoryCardTwo.ShowCard();

            yield return new WaitForSeconds(showCardTimeOut);

            if (CompareCards(memoryCardOne, memoryCardTwo))
            {
                memoryCardOne.MoveTo(spawnPos.position);
                memoryCardTwo.MoveTo(spawnPos.position);
                activeCard = null;

                StartCoroutine(WaitMoveDelay());
                crntPairs--;
                CheckEndGame();
            }
            else {
                memoryCardOne.HideCard();
                memoryCardTwo.HideCard();
                activeCard = null;
                gameState = GameState.Gameplay;
            }
        }

        private IEnumerator WaitMoveDelay() {
            yield return new WaitForSeconds(moveCardDelay);
            gameState = GameState.Gameplay;
        }


        public bool CompareCards(MemoryCard memoryCardOne, MemoryCard memoryCardTwo) {
            if (true
                && memoryCardOne.suit == memoryCardTwo.suit
                && memoryCardOne.rank == memoryCardTwo.rank
                )
                return true;
            return false;

        }

        public void CheckEndGame() {
            if (crntPairs == 0) {
                GameOver();
            }
        }

        public void GameOver() {
            uiManager.GameOverUI();
            Debug.LogError("GameOver");
        }
    }
}