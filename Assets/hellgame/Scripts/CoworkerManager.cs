using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class CoworkerManager : MonoBehaviour
{
    public List<CoworkerSchema> coworkers;
    public GameObject coworkerHolder;
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
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) {
            SummonCoworker();
        }


        if (isMoving) {
            Vector3 targetPosition = new Vector3(-650, 0, 0);
            float speed = 5f;
            float step = speed * Time.deltaTime;
            coworkerHolder.transform.position = Vector3.MoveTowards(coworkerHolder.transform.position, targetPosition, step);

            if (Vector3.Distance(coworkerHolder.transform.position, targetPosition) < 0.001f) {
                isMoving = false;
                TalkToPlayer();
            }

        }
    }

    public void SummonCoworker() {
        coworkerHolder.transform.position = new Vector3(-1400, 0, 0);
        isActive = true;
        isMoving = true;
        int randomIndex = Random.Range(0, coworkers.Count);
        coworkerImage.sprite = coworkers[randomIndex].coworkerImage;
        coworkerText.text = coworkers[randomIndex].coworkerSpeech;
        responseOption1.text = coworkers[randomIndex].responseOption1;
        responseOption2.text = coworkers[randomIndex].responseOption2;
        responseOption1.gameObject.SetActive(false);
        responseOption2.gameObject.SetActive(false);
        coworkerText.gameObject.SetActive(false);

    }

    public void TalkToPlayer() {
        isMoving = false;
        coworkerText.gameObject.SetActive(true);
        responseOption1.gameObject.SetActive(true);
        responseOption2.gameObject.SetActive(true);
    }

    //coworker behavior//
    //walks to Vector3(-650, 0, 0);
    //stops
    //looks at player
    //says something
    //waits for player to click
    //says something else while moving away
}
