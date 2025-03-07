using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SpriteAnimator : MonoBehaviour {
    public Sprite[] sprites;
    public bool isLooping = true;
    public float minInterval = 0.5f;
    public float maxInterval = 2f;
    public Image spriteRenderer;

    private float timer = 0f;
    private float currentInterval;
    private int currentSpriteIndex = 0;

    void Start() {
        if (sprites.Length > 0)
            spriteRenderer.sprite = sprites[0];
        if (isLooping)
            SetRandomInterval();
    }

    void Update() {
        if (!isLooping)
            return;
        timer += Time.deltaTime;
        if (timer >= currentInterval) {
            NextSprite();
            SetRandomInterval();
            timer = 0f;
        }
    }

    void NextSprite() {
        currentSpriteIndex = (currentSpriteIndex + 1) % sprites.Length;
        spriteRenderer.sprite = sprites[currentSpriteIndex];
    }

    void SetRandomInterval() {
        currentInterval = Random.Range(minInterval, maxInterval);
    }

    public void PlayAnim() {
            Debug.Log("sprite playing");
            StartCoroutine(PlayAnimCoroutine(minInterval));
    }

    IEnumerator PlayAnimCoroutine(float duration) {
        float timePerSprite = duration / sprites.Length;
        for (int i = 0; i < sprites.Length; i++) {
            spriteRenderer.sprite = sprites[i];
            yield return new WaitForSeconds(timePerSprite);
        }
        spriteRenderer.sprite = sprites[0];
        currentSpriteIndex = 0;
    }
}
