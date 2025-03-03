using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class CoworkerManager : MonoBehaviour
{
    public List<CoworkerSchema> coworkers;
    private int coworkerIndex;
    public GameObject coworkerHolder;
    private Vector3 coworkerStartPosition;
    public Transform stopPosition;
    public Image coworkerImage;
    public TextMeshProUGUI coworkerText;
    public TextMeshProUGUI responseOption1;
    public TextMeshProUGUI responseOption2;
    public bool isActive = false;
    public bool isMoving = false;
    public bool playerHasResponded = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        coworkerStartPosition = coworkerHolder.transform.position;

        coworkerText.transform.parent.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) {
            SummonCoworker();
        }


        if (isMoving) {
            Vector3 targetPosition = stopPosition.position;
            float speed = 2f;
            float step = speed * Time.deltaTime;
            coworkerHolder.transform.position = Vector3.MoveTowards(coworkerHolder.transform.position, targetPosition, step);

            if (Vector3.Distance(coworkerHolder.transform.position, targetPosition) < 0.001f) {
                isMoving = false;
                if (coworkers[coworkerIndex].forwardImage != null) coworkerImage.sprite = coworkers[coworkerIndex].forwardImage;
                TalkToPlayer();
            }

        }
        if (playerHasResponded) {

            coworkerImage.sprite = coworkers[coworkerIndex].coworkerImage;
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
    }

    public void SummonCoworker() {
        coworkerHolder.transform.position = coworkerStartPosition;
        isActive = true;
        isMoving = true;
        int randomIndex = Random.Range(0, coworkers.Count);
        coworkerIndex = randomIndex;
        coworkerImage.sprite = coworkers[randomIndex].coworkerImage;
        coworkerText.text = coworkers[randomIndex].coworkerSpeech;
        responseOption1.text = coworkers[randomIndex].responseOption1;
        responseOption2.text = coworkers[randomIndex].responseOption2;
        coworkerText.transform.parent.gameObject.SetActive(false);

    }

    public void TalkToPlayer() {
        isMoving = false;
        coworkerText.transform.parent.gameObject.SetActive(true);
    }

    public void Respond1() {

        playerHasResponded = true;
        responseOption1.transform.parent.gameObject.SetActive(false);
        responseOption2.transform.parent.gameObject.SetActive(false);
        coworkerText.text = coworkers[coworkerIndex].response1Response;
    }

    public void Respond2() {
        playerHasResponded = true;
        responseOption1.transform.parent.gameObject.SetActive(false);
        responseOption2.transform.parent.gameObject.SetActive(false);
        coworkerText.text = coworkers[coworkerIndex].response2Response;
    }
    //coworker behavior//
    //walks to Vector3(-650, 0, 0);
    //stops
    //looks at player
    //says something
    //waits for player to click
    //says something else while moving away
}
