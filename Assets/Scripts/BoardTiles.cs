using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardTiles : MonoBehaviour
{
    [SerializeField] private List<Transform> tiles;
    public GameObject player;
    private int playerIndex = 0;
    public float cooldown = 1f;
    private float currentTime = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       player.transform.position = tiles[0].position; 
    }

    // Update is called once per frame
    void Update()
    {
        if(currentTime <= 0)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                MovePlayer();
                currentTime = cooldown;
            }
        }
        else
        {
            currentTime -= Time.deltaTime;
        }
    }

    private void MovePlayer()
    {
        Debug.Log("trying to move");
        playerIndex = (playerIndex + 1) % tiles.Count;
        StartCoroutine(LerpPosition(player.transform, tiles[playerIndex].position, cooldown));
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
