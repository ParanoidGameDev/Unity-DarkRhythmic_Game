using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMotion : MonoBehaviour {
    // ! Attributes
    public float moveSpeed = 5f;

    // ! Movement
    private  Tilemap tilemap;
    private Vector3Int currentCell;
    private Vector3Int targetCell;
    private bool isMoving = false;
    private bool alternateRow = false;

    // ! Inputs
    [SerializeField] private _InputManager input;

    private void Awake() {
        InitializePlayer();
    }

    private void InitializePlayer() {
        InitializeWorld();
        InitializePosition();
    }

    private void InitializeWorld()
    {
        tilemap = GameObject.Find("tilemap_Obj").GetComponent<Tilemap>();
    }

    private void Update() {
        CheckMovement();
    }

    private void CheckMovement() {
        if (!isMoving) {

            Vector3Int direction = Vector3Int.zero;

            if (input.W_press) direction = Vector3Int.right; // Arriba
            else {
                if (input.S_press) direction = Vector3Int.left; // Abajo
                else if (alternateRow) {
                    direction = GetSameRowDirection();
                } else {
                    direction = GetDiffRowDirection();
                }
            }
            if (direction != Vector3Int.zero) {
                Move(direction);
            }
        }
    }

    private void InitializePosition() {
        currentCell = tilemap.WorldToCell(transform.position);
        transform.position = tilemap.CellToWorld(currentCell) + tilemap.tileAnchor;
    }
    
    private Vector3Int GetSameRowDirection() {
        Vector3Int lateralDir = Vector3Int.zero;

        if (input.Q_press) lateralDir = new Vector3Int(1, -1, 0); // Arriba izquierda
        else if (input.E_press) lateralDir = new Vector3Int(1, 1, 0); // Derecha Arriba
        else if (input.A_press) lateralDir = new Vector3Int(0, -1, 0); // Izquierda
        else if (input.D_press) lateralDir = new Vector3Int(0, 1, 0); // Derecha

        return lateralDir;
    }

    private Vector3Int GetDiffRowDirection() {
        Vector3Int lateralDir = Vector3Int.zero;

        if (input.Q_press) lateralDir = new Vector3Int(0, -1, 0); // Arriba izquierda
        else if(input.E_press) lateralDir = new Vector3Int(0, 1, 0); // Derecha Arriba
        else if(input.A_press) lateralDir = new Vector3Int(-1, -1, 0); // Izquierda
        else if(input.D_press) lateralDir = new Vector3Int(-1, 1, 0); // Derecha

        return lateralDir;
    }

    private void Move(Vector3Int direction) {
        targetCell = currentCell + direction;

        if (tilemap.GetTile(targetCell) != null && IsCellPassable(targetCell)) {
            if (direction != Vector3Int.left && direction != Vector3Int.right) {
                alternateRow = !alternateRow;
            }
            StartCoroutine(MoveToCell(targetCell));
        }
    }

    private IEnumerator MoveToCell(Vector3Int cell) {
        isMoving = true;
        Vector3 targetPos = tilemap.CellToWorld(cell) + tilemap.tileAnchor;
        float elapsedTime = 0.0f;

        while (elapsedTime < moveSpeed) {
            transform.position = Vector3.Lerp(transform.position, targetPos, elapsedTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;
        currentCell = cell;
        isMoving = false;
    }

    private bool IsCellPassable(Vector3Int cell) {
        return true;
    }
}