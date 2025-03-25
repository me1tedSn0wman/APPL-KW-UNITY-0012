using UnityEngine;


namespace MemoryMinigame
{
    public class MemoryCard : Card
    {
        private MemoryMinigameManager manager;

        public void SetUpManager(MemoryMinigameManager manager) {
            this.manager = manager;
        }

        public virtual void OnMouseDown() {
            manager.ClickedThisCard(this);
        }

        public void ShowCard() {
            isFrontSide = true;
        }

        public void HideCard() {
            isFrontSide = false;
        }
    }
}