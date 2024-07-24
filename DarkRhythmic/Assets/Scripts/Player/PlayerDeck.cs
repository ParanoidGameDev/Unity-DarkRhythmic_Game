using UnityEngine;

public class PlayerDeck : MonoBehaviour {
    // ! Actions
    public Card cardSelected;

    // ! Display
    public float radius;
    public float angleOffset;

    private void LateUpdate() {
        if (!this.cardSelected) this.DisplayCards();
        else if(!Input.GetMouseButton(0)) this.cardSelected = null;
    }

    public void DisplayCards() {
        // Get all child images
        int childCount = this.transform.childCount;
        this.angleOffset = childCount * 1.2f; // Card space 1 - 1.2

        // Expanding deck based on screen size
        this.radius = Camera.main.scaledPixelHeight * 10;
        float angleStep = this.angleOffset / childCount;
        float startAngle = -this.angleOffset / 2;

        // Looping to place card positions
        for (int i = 0; i < childCount; i++) {
            RectTransform child = this.transform.GetChild(i) as RectTransform;

            // Checking if card is valid
            if (child && child.TryGetComponent(out Card card)) {
                Vector2 multiplier = new(0.0f, -1.005f);
                if (card.selected) {
                    this.cardSelected = card;
                    continue;
                } else if (card.hover) {
                    multiplier.y = -1.0025f;
                }
                // Displaying each card based on total amount
                float angle = startAngle + ((i + .5f )* angleStep);
                Vector2 position = this.radius * multiplier + new Vector2(Mathf.Sin(Mathf.Deg2Rad * angle) * this.radius, Mathf.Cos(Mathf.Deg2Rad * angle) * this.radius);
                child.localPosition = position + Vector2.up * 0.1f;

                // Rotating cards
                child.localRotation = Quaternion.Euler(0.0f, 0.1f, -angle);
            }
        }
    }
}