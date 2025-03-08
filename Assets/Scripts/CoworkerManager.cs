using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class CoworkerManager : MonoBehaviour
{

    public static CoworkerManager Instance { get; private set; }

    public List<CoworkerSchema> coworkers;
    public List<CoworkerSchema> metCoworkers;
    private int coworkerIndex;
    public GameObject coworkerHolder;
    public Image coworkerArms;
    private Vector3 coworkerStartPosition;
    public Transform stopPosition;
    public Image coworkerImage;
    public TextMeshProUGUI coworkerText;
    public TextMeshProUGUI responseOption1;
    public TextMeshProUGUI responseOption2;

    public bool isActive = false;
    public bool isMoving = false;
    public bool playerHasResponded = false;

    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        coworkerStartPosition = coworkerHolder.transform.position;

        coworkerText.transform.parent.gameObject.SetActive(false);

        coworkerIndex = 0;

        if (PersistentData.currentDay == 1) {
            PersistentData.remainingCoworkers = coworkers;
        } else {
            coworkers = PersistentData.remainingCoworkers;
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (coworkers[coworkerIndex].armsImage != null) {
            coworkerArms.transform.position = coworkerHolder.transform.position;
        }
        if (coworkers[coworkerIndex].armsImage == null) {
            coworkerArms.gameObject.SetActive(false);
        }
        /*
        if (Input.GetKeyDown(KeyCode.Q)) {
            SummonCoworker();
        }
        */

        if (isMoving) {
            if (coworkers[coworkerIndex].armsImage != null) {
                coworkerArms.gameObject.SetActive(false);
            }
            coworkerImage.transform.localScale = new Vector3(1, 1, 1);
            Vector3 targetPosition = stopPosition.position;
            float speed = 2f;
            float step = speed * Time.deltaTime;
            coworkerHolder.transform.position = Vector3.MoveTowards(coworkerHolder.transform.position, targetPosition, step);

            if (Vector3.Distance(coworkerHolder.transform.position, targetPosition) < 0.001f) {
                isMoving = false;
                if (coworkers[coworkerIndex].armsImage != null) {
                    coworkerArms.gameObject.SetActive(true);
                }
                TalkToPlayer();
            }

        }
        if (playerHasResponded) {

            if (coworkerArms != null) {
                coworkerArms.gameObject.SetActive(false);
            }
            coworkerImage.transform.localScale = new Vector3(-1, 1, 1);
            Vector3 targetPosition = coworkerStartPosition;
            float speed = 2f;
            float step = speed * Time.deltaTime;
            coworkerHolder.transform.position = Vector3.MoveTowards(coworkerHolder.transform.position, targetPosition, step);

            if (Vector3.Distance(coworkerHolder.transform.position, targetPosition) < 0.001f) {
                isActive = false;
                playerHasResponded = false;
                coworkerText.transform.parent.gameObject.SetActive(false);
                coworkerHolder.transform.position = coworkerStartPosition;
            }
        }
        if (!isActive) {
            if (coworkers[coworkerIndex].armsImage != null)
                coworkerArms.gameObject.SetActive(false);
            coworkerImage.gameObject.SetActive(false);
        }
    }

    public void SummonCoworker() {
        if (metCoworkers.Count == coworkers.Count) {
            return;
        }


        int randomIndex = Random.Range(0, coworkers.Count);
        coworkerIndex = randomIndex;
        playerHasResponded = false;

        if (!metCoworkers.Contains(coworkers[randomIndex])) {
            if (coworkers[coworkerIndex].armsImage != null)
                coworkerArms.gameObject.SetActive(true);
            coworkerImage.gameObject.SetActive(true);
            metCoworkers.Add(coworkers[randomIndex]);
            PersistentData.remainingCoworkers.Remove(coworkers[randomIndex]);
            coworkerHolder.transform.position = coworkerStartPosition;
            isActive = true;
            isMoving = true;
            playerHasResponded = false;
            coworkerImage.sprite = coworkers[randomIndex].coworkerImage;
            if (coworkers[coworkerIndex].armsImage != null)
                coworkerArms.sprite = coworkers[randomIndex].armsImage;
            AlignPivot();
            coworkerText.text = coworkers[randomIndex].coworkerSpeech;
            responseOption1.text = coworkers[randomIndex].responseOption1;
            responseOption2.text = coworkers[randomIndex].responseOption2;
            coworkerText.transform.parent.gameObject.SetActive(false);
            //coworkerArms.SetNativeSize();
            //coworkerImage.SetNativeSize();
        } else {
            SummonCoworker();
        }
    }

    public void TalkToPlayer() {
        isMoving = false;
        coworkerText.text = coworkers[coworkerIndex].coworkerSpeech;
            responseOption1.text = coworkers[coworkerIndex].responseOption1;
            responseOption2.text = coworkers[coworkerIndex].responseOption2;
        coworkerText.transform.parent.gameObject.SetActive(true);
        responseOption1.transform.parent.gameObject.SetActive(true);
        responseOption2.transform.parent.gameObject.SetActive(true);
    }

    public void Respond1() {

        playerHasResponded = true;
        responseOption1.transform.parent.gameObject.SetActive(false);
        responseOption2.transform.parent.gameObject.SetActive(false);
        coworkerText.text = coworkers[coworkerIndex].response1Response;
        if (coworkers[coworkerIndex].isCorrectResponse1) {
            PersistentData.coworkerFriendlinessScore += coworkers[coworkerIndex].friendlinessScore;
        } else {
            PersistentData.coworkerFriendlinessScore -= coworkers[coworkerIndex].friendlinessScore;
        }
                Debug.Log("Friendliness score: " + PersistentData.coworkerFriendlinessScore);

    }

    public void Respond2() {
        playerHasResponded = true;
        responseOption1.transform.parent.gameObject.SetActive(false);
        responseOption2.transform.parent.gameObject.SetActive(false);
        coworkerText.text = coworkers[coworkerIndex].response2Response;
        if (!coworkers[coworkerIndex].isCorrectResponse1) {
            PersistentData.coworkerFriendlinessScore += coworkers[coworkerIndex].friendlinessScore;
        } else {
            PersistentData.coworkerFriendlinessScore -= coworkers[coworkerIndex].friendlinessScore;
        }
        Debug.Log("Friendliness score: " + PersistentData.coworkerFriendlinessScore);
    }

    public void AlignPivot() {
        if (coworkerImage != null && coworkerImage.sprite != null)
        {
            // Get the sprite's pivot
            Vector2 spritePivot = coworkerImage.sprite.pivot;

            // Get the sprite's dimensions
            float spriteWidth = coworkerImage.sprite.rect.width;
            float spriteHeight = coworkerImage.sprite.rect.height;

            // Convert to normalized RectTransform pivot
            Vector2 rectTransformPivot = new Vector2(spritePivot.x / spriteWidth, spritePivot.y / spriteHeight);

            // Set the RectTransform's pivot
            coworkerImage.rectTransform.pivot = rectTransformPivot;
        }
        else
        {
            //something here
        }

        if (coworkerArms != null && coworkerArms.sprite != null)
        {
            // Get the sprite's pivot
            Vector2 spritePivot = coworkerArms.sprite.pivot;

            // Get the sprite's dimensions
            float spriteWidth = coworkerArms.sprite.rect.width;
            float spriteHeight = coworkerArms.sprite.rect.height;

            // Convert to normalized RectTransform pivot
            Vector2 rectTransformPivot = new Vector2(spritePivot.x / spriteWidth, spritePivot.y / spriteHeight);

            // Set the RectTransform's pivot
            coworkerArms.rectTransform.pivot = rectTransformPivot;
        }
        else
        {
            //something here
        }
    }
}
