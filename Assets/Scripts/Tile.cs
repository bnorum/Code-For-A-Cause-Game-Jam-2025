using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool isWalkable = true;
    public bool isOccupied = false;
    [SerializeField] private List<Sprite> tileSprites;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = tileSprites[Random.Range(0, tileSprites.Count)];
    }
}
