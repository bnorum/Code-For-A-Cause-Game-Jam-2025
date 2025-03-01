using System.Collections;
using System.Collections.Generic;
using Extensions;
using UnityEngine;

public class BoardTiles : MonoBehaviour
{
    public int width = 6;
    public int height = 3;


    public TArray<GameObject> grid;
    public GameObject tilePrefab; // Prefab for the tile

    public float moveSpeed = 0.5f;

    void Awake()
    {
        grid = new TArray<GameObject>(width, height);
    }



    void CreateGrid() {
        Vector3 gridCenter = new Vector3((width - 1) / 2f, (height - 1) / 2f, 0);
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
            GameObject tile = Instantiate(tilePrefab, new Vector3(x, y, 0) - gridCenter, Quaternion.identity);
            tile.transform.SetParent(transform); // Set parent to the board
            tile.name = $"Tile {x},{y}"; // Name the tile
            grid[x, y] = tile; // Store the tile in the grid
            }
        }

    }


    void Start()
    {
        CreateGrid(); // Initialize the grid
        ValidateTileArrays(); // Ensure tile arrays are valid
    }

    private void ValidateTileArrays()
    {
        for (int i = 0; i < grid.Length; i++)
        {
            if (grid.Length != height)
            {
                continue; // Skip if invalid column
            }
        }
    }



    public GameObject GetTileAt(int x, int y) //wrapped y is confusion but it just circles around to other side of the board.
    {
        if (x < 0 || x >= width)
        {
            return null;
        }

        int wrappedY = y % height;

        if (wrappedY < 0 || wrappedY >= grid.Length || grid[x, wrappedY] == null)
        {
            return null;
        }

        return grid[x, wrappedY];
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
