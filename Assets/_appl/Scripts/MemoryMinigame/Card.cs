using DG.Tweening;
using MemoryMinigame;
using UnityEngine;

public class Card : MonoBehaviour {


    [SerializeField] protected SpriteRenderer spriteRenderer;

    [Header("Movement param")]
    [SerializeField] protected float movementTimeDuration = 0.1f;
    


    [Header("Ser Dynamically")]
    [Header("Card Definition")]
    public string suit;
    public int rank;

    public Sprite spriteFront;
    public Sprite spriteBack;

    
    private bool _isFrontSide;
    public bool isFrontSide {
        get { return _isFrontSide; }
        set {
            if (value)
            {
                _isFrontSide = true;
                spriteRenderer.sprite = spriteFront;
            }
            else {
                _isFrontSide = false;
                spriteRenderer.sprite = spriteBack;
            }
        }
    }

    public virtual void Awake() { 
        _isFrontSide = false;
    }

    public void SetUpCard(CardDefinition cardDef, Sprite spriteBack)
    {
        SetUpCard(
            cardDef.suit,
            cardDef.rank,
            cardDef.spriteFront,
            spriteBack
            );
    }

    public void SetUpCard(string suit, int rank, Sprite spriteFront, Sprite spriteBack) {
          this.suit = suit;
          this.rank = rank;
          this.spriteFront = spriteFront;
          this.spriteBack = spriteBack;

          gameObject.name = suit + string.Format("{0:D2}", rank);
          spriteRenderer.sprite = spriteFront;
    }

    public virtual void MoveTo(Vector3 position) {
        transform.DOMove(position, movementTimeDuration);
    }
}
