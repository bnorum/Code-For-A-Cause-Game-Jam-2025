using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool isWalkable = true;
    public bool isOccupied = false;
    public Vector2Int gridPosition;
    [SerializeField] private List<Sprite> tileSprites;
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = tileSprites[Random.Range(0, tileSprites.Count)];
    }
}
