using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> players;
    public int currentPlayerIndex = 0;
    public int turn = 1;

    [Header("UI")]
    public TextMeshProUGUI turnText;
    public TextMeshProUGUI currentPlayerText;
    public TextMeshProUGUI p1ScoreText;
    public TextMeshProUGUI p2ScoreText;

    private BoardTiles board;

    void Start()
    {
        board = FindFirstObjectByType<BoardTiles>();

        turnText.text = "Turn: " + turn;
        currentPlayerText.text = $"Player {currentPlayerIndex + 1}, make your move";

        // Place players on starting tile
        foreach (var player in players)
        {
            player.transform.position = board.GetTilePosition(1, 0);
            player.GetComponent<Player>().tileX = 1;
            player.GetComponent<Player>().tileY = 0;
            var position = player.transform.position;
            position.z -= 1f;
            player.transform.position = position;
        }
    }
    private float moveCooldown;

    void Update() 
    {
        if (moveCooldown > 0)
        {
            moveCooldown -= Time.deltaTime;
            return;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveCurrentPlayer(Vector2Int.up);
            moveCooldown = board.moveSpeed;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveCurrentPlayer(Vector2Int.left);
            moveCooldown = board.moveSpeed;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveCurrentPlayer(Vector2Int.right);
            moveCooldown = board.moveSpeed;
        }
    }

    public void MoveCurrentPlayer(Vector2Int direction)
    {
        board.TryMovePlayer(players[currentPlayerIndex], direction);
        currentPlayerIndex++;

        if (currentPlayerIndex >= players.Count)
        {
            currentPlayerIndex = 0;
            turn++;
            turnText.text = "Turn: " + turn;
        }

        currentPlayerText.text = $"Player {currentPlayerIndex + 1}, make your move>:()";
    }

}
