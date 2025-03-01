using System.Collections.Generic;
using TMPro;
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
    private float moveCooldown = 0;

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
                remainingSteps.text = $"Remaining Steps: {movesThisTurn}";
            }  
        }
        else if (movesThisTurn > 0)
        {
            if (moveCooldown > 0)
            {
                moveCooldown -= Time.deltaTime;
                return;
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                MoveCurrentPlayer(Vector2Int.up);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                MoveCurrentPlayer(Vector2Int.left);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                MoveCurrentPlayer(Vector2Int.right);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                MoveCurrentPlayer(Vector2Int.down);
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                CurrentPlayerAttack(Vector2Int.up);
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                CurrentPlayerAttack(Vector2Int.left);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                CurrentPlayerAttack(Vector2Int.right);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                CurrentPlayerAttack(Vector2Int.down);
            }

        }
    }

    private void CurrentPlayerAttack(Vector2Int direction)
    {
        board.TryAttackPlayer(players[currentPlayerIndex]);
    }

    public void MoveCurrentPlayer(Vector2Int direction)
    {
        board.TryMovePlayer(players[currentPlayerIndex], direction);
        movesThisTurn--;

        remainingSteps.text = $"Remaining Steps: {movesThisTurn}";
        moveCooldown = board.moveSpeed;

        if (movesThisTurn <= 0)
        {
            EndTurn();
        }
    }

    private void EndTurn()
    {
        currentPlayerIndex++;

        if (currentPlayerIndex >= players.Count)
        {
            currentPlayerIndex = 0;
            turn++;
            turnText.text = "Turn: " + turn;
        }

        hasRolledDice = false;
        movesThisTurn = 0;
        rollText.text = "";
        remainingSteps.text = "";
        currentPlayerText.text = $"Player {currentPlayerIndex + 1}, Roll Your Dice!";
    }
}
