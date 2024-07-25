using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler {
    // ! Attributes
    public Vector2 displayOffset;
    public bool hover;
    public bool selected;

    // ! Managing
    public PlayerDeck deck;

    // ! Effects
    [SerializeField] public enum Type {
        
    };

    private void Start() {
        // Initialize its deck
        this.deck = this.transform.parent.GetComponent<PlayerDeck>();
    }

    private void Update() {
    }

    public void OnPointerEnter(PointerEventData eventData) {
        // Detecting hovering
        //if(!deck.cardSelected) 
        this.hover = true;
    }

    public void OnPointerExit(PointerEventData eventData) {
        // Leaving hovering
        this.hover = false;
    }

    public void OnPointerDown(PointerEventData eventData) {
        // Click over card
        this.selected = true;
        /*
        if (!deck.cardSelected) {
            this.GetComponent<Image>().raycastTarget = false;
        }
        */
    }

    public void OnPointerUp(PointerEventData eventData) {
        // Stop clicking
        this.selected = false;
        //this.GetComponent<Image>().raycastTarget = true;
    }

    /*
    public void OnDrag(PointerEventData eventData) {
        if (this.selected) {
            // Moving card with cursor input
            RectTransformUtility.ScreenPointToLocalPointInRectangle(this.transform.parent as RectTransform, eventData.position, null, out Vector2 localPoint);
            this.transform.localPosition = Vector2.Lerp(this.transform.localPosition, localPoint - this.displayOffset, 0.5f);

            // Resetting card rotation
            this.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Lerp(this.transform.localRotation.z, 0.0f, 1.0f));
        }
    }
    */
}
