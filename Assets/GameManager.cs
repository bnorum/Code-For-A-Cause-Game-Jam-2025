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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        turnText.text = "Turn: " + turn;
        currentPlayerText.text = $"Player: {players[currentPlayerIndex]}, make your move!";
        foreach(var player in players)
        {
            player.transform.position = FindFirstObjectByType<BoardTiles>().tiles[0].transform.position;
        }
    }
    void Update()
    {
        while(currentPlayerIndex <= players.Count)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                FindFirstObjectByType<BoardTiles>().TryMovePlayer(players[currentPlayerIndex]);
                currentPlayerIndex++;
                if(currentPlayerIndex == players.Count)
                {
                    currentPlayerIndex = 0;
                    turn++;
                    turnText.text = "Turn: " + turn;
                    break;
                }
            }
        }  
    }
}
