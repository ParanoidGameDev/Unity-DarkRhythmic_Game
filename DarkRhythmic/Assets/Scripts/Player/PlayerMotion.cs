using System;
using System.Collections;
using TMPro;
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

    // ! Camera
    public float cameraSmooth = 0.125f;
    public Vector3 cameraOffset;
    public Vector3 currentTilePosition;

    private void Update() {
        // Common updates
        this.CheckMovement();
    }

    private void LateUpdate() {
        // Camera
        UpdateCameraPosition();

        // CDs
        this.UpdateCDs();
    }

    private void UpdateCameraPosition() {
        // New camera position based on Player
        Vector3 newPosition = this.transform.position + this.cameraOffset;
        newPosition.y = Camera.main.transform.position.y;

        // Movement interpolation
        Vector3 smoothedPosition = Vector3.Lerp(Camera.main.transform.position, newPosition, this.cameraSmooth);
        Camera.main.transform.position = smoothedPosition;
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
                if (input.moveKeys[i].Enabled && this.IsCellValid(this.moveDirections[i])) {
                    this.StartCoroutine(this.MoveToCell(this.currentTilePosition));
                    break;
                }
            }
        }
    }

    private bool IsCellValid(Vector2 checkPosition) {
        // Setting local 3D position from 2D movement input
        this.currentTilePosition = this.transform.position;
        this.currentTilePosition.x += checkPosition.x;
        this.currentTilePosition.y += 2.0f;
        this.currentTilePosition.z += checkPosition.y;

        // Tile object found
        TileObject tile = this.TileAtPos(this.currentTilePosition);

        // Move when cell is valid
        if (tile && tile.isPassable) {
            this.currentTilePosition.y = tile.tileHeight;
            return true;
        } else {
            Debug.LogWarning("Tile not available");
            return false;
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

        // Calculate new rotation
        Vector3 lookDirection = nextPosition - this.transform.position;
        lookDirection.y = 0.0f;
        Quaternion newRotation = Quaternion.LookRotation(lookDirection);
        Quaternion startRotation = this.transform.rotation;

        // Calculate position
        float verticalDifference = nextPosition.y - startPosition.y;
        float maxHeight = Mathf.Abs(verticalDifference) * 1.25f + 0.25f;

        // Move animation
        while (elapsedTime < this.moveSpeed) {
            elapsedTime += Time.deltaTime;

            // Calculate fraction of journey completed
            float fracJourney = elapsedTime / this.moveSpeed;

            // Animate movement
            Vector3 interpolatedPosition = Vector3.Lerp(startPosition, nextPosition, fracJourney);
            this.transform.rotation = Quaternion.Slerp(startRotation, newRotation, elapsedTime / 0.1f);

            // Calculate height for hopping effect
            float height = maxHeight * (-4 * Mathf.Pow(fracJourney - 0.5f, 2) + 1);
            interpolatedPosition.y = startPosition.y + height + verticalDifference * fracJourney;

            // Apply interpolated position
            this.transform.position = interpolatedPosition;
            yield return null;
        }

        // Setting the exact final position
        this.transform.position = nextPosition;

        // Setting the exact final rotation
        this.transform.rotation = newRotation;

        // Marking move availability
        this.moveCD = 0.05f;
        this.isMoving = false;
    }
}