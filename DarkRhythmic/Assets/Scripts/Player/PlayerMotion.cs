using System.Collections;
using UnityEngine;

public class PlayerMotion : MonoBehaviour
{
    // ! Attributes
    public float moveSpeed = 0.25f;
    public float moveCD = 1.0f;
    public float moveReset = 0.05f;
    //public float movePenalty = 0.05f;

    // ! Movement
    public Vector2[] moveDirections;
    private bool isMoving = false;

    // ! Inputs
    [SerializeField] private _InputManager input;

    private void Update() {
        // Common updates
        this.CheckMovement();

        // CDs
        this.UpdateCDs();
    }

    private void UpdateCDs() {
        // Move
        if (this.moveCD > 0) this.moveCD -= Time.deltaTime;
        else this.moveCD = 0;
    }

    private void CheckMovement() {
        // Check if can move
        if (!this.isMoving && this.moveCD == 0) {
            // Checking input for movement
            for (int i = 0; i < input.moveKeys.Count; i++) {
                // Move to correct directions
                if (input.moveKeys[i].Enabled) {
                    this.IsCellValid(this.moveDirections[i]);
                    break;
                }
            }
        }
    }

    private void IsCellValid(Vector2 checkPosition) {
        // Setting local 3D position from 2D movement input
        Vector3 cellPosition = this.transform.position;
        cellPosition.x += checkPosition.x;
        cellPosition.y += 2.0f;
        cellPosition.z += checkPosition.y;

        // Tile object found
        TileObject tile = this.TileAtPos(cellPosition);

        Debug.Log(cellPosition);
        Debug.Log(tile);

        // Move when cell is valid
        if (tile.isPassable) {
            cellPosition.y = tile.tileHeight;
            this.StartCoroutine(this.MoveToCell(cellPosition));
        } else {
            Debug.LogWarning("Tile not available");
        }
    }

    private TileObject TileAtPos(Vector3 cellPosition) {
        // Raycast for any tile object with hitbox
        Ray ray = new(cellPosition, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 4.0f)) {
            if(hit.collider.TryGetComponent<TileObject>(out TileObject tileFound)) {
                return tileFound;
            }
        }
        // No object found
        Debug.LogWarning("No GameObject found or TileObject component is missing.");
        return null;
    }

    private IEnumerator MoveToCell(Vector3 nextPosition) {
        // Lock movement overwriting
        this.isMoving = true;

        // Setting transition values
        float elapsedTime = 0.0f;
        Vector3 startPosition = this.transform.position;

        // Calculate vertical difference and set maximum height accordingly
        float verticalDifference = nextPosition.y - startPosition.y;
        float maxHeight = Mathf.Abs(verticalDifference) * 1.25f + 0.25f;

        // Move animation
        while (elapsedTime < this.moveSpeed) {
            elapsedTime += Time.deltaTime;

            // Calculate fraction of journey completed
            float fracJourney = elapsedTime / this.moveSpeed;

            // Animate movement
            Vector3 interpolatedPosition = Vector3.Lerp(startPosition, nextPosition, fracJourney);

            // Calculate height for hopping effect
            float height = maxHeight * (-4 * Mathf.Pow(fracJourney - 0.5f, 2) + 1);
            interpolatedPosition.y = startPosition.y + height + verticalDifference * fracJourney;

            // Apply interpolated position
            this.transform.position = interpolatedPosition;
            yield return null;
        }

        // Ensure final position is set
        this.transform.position = nextPosition;

        // Marking move availability
        this.moveCD = 0.05f;
        this.isMoving = false;
    }
}