using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardTiles : MonoBehaviour
{
    public int width = 3;
    public int height = 6;

    [Header("Tile Columns")] // Tile storage for each column
    [SerializeField] public GameObject[] column0;
    [SerializeField] public GameObject[] column1;
    [SerializeField] public GameObject[] column2;
    private GameObject[][] columns;

    public float moveSpeed = 0.5f;

    void Awake()
    {
        columns = new GameObject[][] { column0, column1, column2 }; // Initialize tile columns
    }

    void Start()
    {
        ValidateTileArrays(); // Ensure tile arrays are valid
    }

    private void ValidateTileArrays()
    {
        for (int i = 0; i < columns.Length; i++)
        {
            if (columns[i] == null || columns[i].Length != height)
            {
                continue; // Skip if invalid column
            }
        }
    }

    public GameObject GetTileAt(int x, int y) //wrapped y is confusion but it just circles around to other side of the board.
    {
        if (columns == null || x < 0 || x >= width)  
        {
            return null;
        }

        int wrappedY = y % height;

        if (columns[x] == null || wrappedY < 0 || wrappedY >= columns[x].Length || columns[x][wrappedY] == null)
        {
            return null; 
        }

        return columns[x][wrappedY];
    }

    public Vector3 GetTilePosition(int x, int y)
    {
        GameObject tile = GetTileAt(x, y);
        return tile ? tile.transform.position : Vector3.zero; // Return tile position or zero vector
    }

    public void TryMovePlayer(GameObject gameObject, Vector2Int direction)
    {
        Player player = gameObject.GetComponent<Player>(); // Get player component
        int newX = player.tileX + direction.x;
        int newY = player.tileY + direction.y;

        GameObject newTile = GetTileAt(newX, newY);

        if (newTile && newTile.GetComponent<Tile>().isWalkable && !newTile.GetComponent<Tile>().isOccupied)
        {
            StopAllCoroutines(); // Ensure only one movement at a time
            StartCoroutine(MovePlayer(gameObject, newTile.transform.position)); 
            player.tileX = newX;
            player.tileY = newY;
        }
    }


    private IEnumerator MovePlayer(GameObject gameObject, Vector3 position)
    {
        float startTime = Time.time;
        Vector3 startPosition = gameObject.transform.position;
        position.z -= 1f; // offset z for rendering order
        while (Time.time - startTime < moveSpeed)
        {
            gameObject.transform.position = Vector3.Lerp(startPosition, position, (Time.time - startTime) / moveSpeed);
            yield return null;
        }

        gameObject.transform.position = position; 
    }
}
