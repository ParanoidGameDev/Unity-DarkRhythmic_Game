using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IDragHandler, IPointerUpHandler {
    // ! Attributes
    public Vector2 displayOffset;
    public bool hover;
    public bool selected;

    // ! Managing
    public PlayerDeck deck;

    private void Start() {
        // Initialize its deck
        this.deck = this.transform.parent.GetComponent<PlayerDeck>();
    }

    private void Update() {
    }

    public void OnPointerEnter(PointerEventData eventData) {
        // Detecting hovering
        if(!deck.cardSelected) this.hover = true;
    }

    public void OnPointerExit(PointerEventData eventData) {
        // Leaving hovering
        this.hover = false;
    }

    public void OnPointerDown(PointerEventData eventData) {
        // Click over card
        if (!deck.cardSelected) this.selected = true;
    }

    public void OnPointerUp(PointerEventData eventData) {
        // Stop clicking
        this.selected = false;
    }

    public void OnBeginDrag(PointerEventData eventData) {
    }

    public void OnDrag(PointerEventData eventData) {
        if (this.selected) {
            // Convert mouse position to the Canvas's local position
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(this.transform.parent as RectTransform, eventData.position, null, out localPoint);
            this.transform.localPosition = localPoint - this.displayOffset;
        }
    }
}
