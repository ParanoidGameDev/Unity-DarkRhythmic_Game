using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerDeck : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    // ! Actions
    //public PlayerCard cardSelected;
    public _GameManager gameManager;
    public bool cardArea;

    // ! Display
    public float radius;
    public float angleOffset;
    public float hideSpeed;
    public float orderSpeed;

    // ! Particle System
    private ParticleSystem currentParticleSystem;

    private void Update() {
        //this.HighlightTile();
    }

    private void LateUpdate() {
        this.DeckDisplay();

        this.DisplayCards();
        /*
        if (!this.cardSelected)
        else if(!Input.GetMouseButton(0)) this.cardSelected = null;
        */
    }

    private void DeckDisplay() {
        Vector2 currentPosition = this.transform.position;
        Vector2 newPosition = this.transform.position;
        float newTimeScale;

        // Checking if calculating
        if (Input.GetKey(KeyCode.Tab)) {
            currentPosition.y = 0.0f;
            newTimeScale = 0.25f;
        } else {
            // Checking for card area
            if (this.cardArea) {
                currentPosition.y = 0.0f;
            } else {
                currentPosition.y = -144.0f;
                //this.DetectTile();
            }
            newTimeScale = 1.0f;
        }
        /*
        if (Input.GetMouseButtonUp(0) && !this.cardArea) {
            // Detect the tile under the cursor and activate its particle system
            Destroy(this.cardSelected.gameObject);
        }
        */
        // Change time step
        Time.timeScale = Mathf.Lerp(Time.timeScale, newTimeScale, Time.deltaTime);

        // Animate smooth movement
        Vector2 smoothedPosition = Vector2.Lerp(currentPosition, newPosition, this.hideSpeed);
        this.transform.position = smoothedPosition;
    }
    
    public bool ApplyEffect(int card) {
        if(gameManager.activeEffects.Count < 3) {
            gameManager.activeEffects.Add(card);
            return true;
        } else {
            return false;
        }
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
            if (child && child.TryGetComponent(out PlayerCard card)) {
                Vector2 multiplier = new(0.0f, -1.0025f);
                if (card.selected) {
                    //this.cardSelected = card;
                    continue;
                } else if (card.hover) {
                    multiplier.y = -1.0f;
                }
                // Displaying each card based on total amount
                float angle = startAngle + ((i + 0.5f) * angleStep);
                Vector2 position = this.radius * multiplier + new Vector2(Mathf.Sin(Mathf.Deg2Rad * angle) * this.radius, Mathf.Cos(Mathf.Deg2Rad * angle) * this.radius);

                child.localPosition = Vector3.Lerp(child.localPosition, position + (Vector2.up * 0.1f), this.orderSpeed);

                // Rotating cards
                child.localRotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Lerp(child.localRotation.z, -angle, this.orderSpeed * 10));
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        // Detecting hovering
        this.cardArea = true;
        //if (this.cardSelected) this.cardSelected.GetComponent<Image>().enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData) {
        // Leaving hovering
        this.cardArea = false;
        //if(this.cardSelected) this.cardSelected.GetComponent<Image>().enabled = false;
    }
    
    private void DetectTile() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit)) {
            // Check if the hit object has a Tile component
            TileObject tile = hit.collider.GetComponent<TileObject>();
            if (tile != null) {
                // Activate the particle system on the tile
                if(currentParticleSystem != tile.GetComponentInChildren<ParticleSystem>())
                {
                    if(currentParticleSystem) currentParticleSystem.Stop();

                    currentParticleSystem = tile.GetComponentInChildren<ParticleSystem>();
                    currentParticleSystem.Play();
                }
            }
        }
    }
    
    private void HighlightTile() {
        /*
        if(this.cardSelected) {
        } else {
            currentPosition.y = 0.0f;
            if (this.currentParticleSystem) this.currentParticleSystem.Stop();
        }
        */
    }

}