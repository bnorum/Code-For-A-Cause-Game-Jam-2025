using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> players;
    public int currentPlayerIndex = 0;
    public int turn = 1;
    private bool hasRolledDice = false;
    private int movesThisTurn = 0;
    [Header("UI")]
    public TextMeshProUGUI turnText;
    public TextMeshProUGUI currentPlayerText;
    public TextMeshProUGUI rollText;
    public TextMeshProUGUI remainingSteps;
    private BoardTiles board;


    void Start()
    {
        board = FindFirstObjectByType<BoardTiles>();

        turnText.text = "Turn: " + turn;
        currentPlayerText.text = $"Player {currentPlayerIndex + 1}, Roll Your Dice!";

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
        if (!hasRolledDice)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                hasRolledDice = true;
                movesThisTurn = Random.Range(1, 7);
                currentPlayerText.text = $"Player {currentPlayerIndex + 1}, Move Your Character!";
                rollText.text = $"Rolled a {movesThisTurn}!";
            }  
        }
        else if(movesThisTurn > 0)
        {
            if (moveCooldown > 0)
            {
                moveCooldown -= Time.deltaTime;
                return;
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                MoveCurrentPlayer(Vector2Int.down);
                moveCooldown = board.moveSpeed;
                movesThisTurn--;
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                MoveCurrentPlayer(Vector2Int.up);
                moveCooldown = board.moveSpeed;
                movesThisTurn--;
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                MoveCurrentPlayer(Vector2Int.left);
                moveCooldown = board.moveSpeed;
                movesThisTurn--;
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                MoveCurrentPlayer(Vector2Int.right);
                moveCooldown = board.moveSpeed;
                movesThisTurn--;
            }
        }
    }

    public void MoveCurrentPlayer(Vector2Int direction)
    {
        board.TryMovePlayer(players[currentPlayerIndex], direction);
        if(movesThisTurn == 0)
        {
            currentPlayerIndex++;
        }
        if (currentPlayerIndex >= players.Count)
        {
            currentPlayerIndex = 0;
            turn++;
            hasRolledDice = false;
            movesThisTurn = 0;
            turnText.text = "Turn: " + turn;
            rollText.text = "";
        }

        currentPlayerText.text = $"Player {currentPlayerIndex + 1}, make your move>:()";
    }

}
