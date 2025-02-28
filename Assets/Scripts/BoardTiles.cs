using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardTiles : MonoBehaviour
{
    public List<GameObject> tiles;
    private int playerIndex = 0;
    public float cooldown = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void TryMovePlayer(GameObject player)
    {
        if(tiles[(playerIndex + 1) % tiles.Count].GetComponent<Tile>().isWalkable && !tiles[playerIndex].GetComponent<Tile>().isOccupied)
        {
            MovePlayer(player);
        }
    }

    private void MovePlayer(GameObject player)
    {   
        player.GetComponent<Player>().tile = playerIndex;
        playerIndex = (playerIndex + 1) % tiles.Count;
        StartCoroutine(LerpPosition(player.transform, tiles[playerIndex].transform.position, cooldown));
    }
    private IEnumerator LerpPosition(Transform target, Vector3 end, float duration)
        {
            Vector3 start = target.position;
            float time = 0;
    
            while (time < duration)
            {
                target.position = Vector3.Lerp(start, end, time / duration);
                time += Time.deltaTime;
                yield return null;
            }
            target.position = end;
        }
}
