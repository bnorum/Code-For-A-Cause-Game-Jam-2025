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
    }

    // Update is called once per frame
    void Update()
    {
        if (coworkerArms != null) {
            coworkerArms.transform.position = coworkerHolder.transform.position;
        }
        /*
        if (Input.GetKeyDown(KeyCode.Q)) {
            SummonCoworker();
        }
        */

        if (isMoving) {
            if (coworkerArms != null) {
                coworkerArms.gameObject.SetActive(false);
            }
            coworkerImage.transform.localScale = new Vector3(1, 1, 1);
            Vector3 targetPosition = stopPosition.position;
            float speed = 2f;
            float step = speed * Time.deltaTime;
            coworkerHolder.transform.position = Vector3.MoveTowards(coworkerHolder.transform.position, targetPosition, step);

            if (Vector3.Distance(coworkerHolder.transform.position, targetPosition) < 0.001f) {
                isMoving = false;
                if (coworkerArms != null) {
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
    }

    public void SummonCoworker() {
        if (metCoworkers.Count == coworkers.Count) {
            return;
        }

        int randomIndex = Random.Range(0, coworkers.Count);
        coworkerIndex = randomIndex;
        playerHasResponded = false;

        if (!metCoworkers.Contains(coworkers[randomIndex])) {
            metCoworkers.Add(coworkers[randomIndex]);
            coworkerHolder.transform.position = coworkerStartPosition;
            isActive = true;
            isMoving = true;
            playerHasResponded = false;
            coworkerImage.sprite = coworkers[randomIndex].coworkerImage;
            coworkerArms.sprite = coworkers[randomIndex].armsImage;
            coworkerText.text = coworkers[randomIndex].coworkerSpeech;
            responseOption1.text = coworkers[randomIndex].responseOption1;
            responseOption2.text = coworkers[randomIndex].responseOption2;
            coworkerText.transform.parent.gameObject.SetActive(false);
            coworkerArms.SetNativeSize();
            coworkerImage.SetNativeSize();
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
    }

    public void Respond2() {
        playerHasResponded = true;
        responseOption1.transform.parent.gameObject.SetActive(false);
        responseOption2.transform.parent.gameObject.SetActive(false);
        coworkerText.text = coworkers[coworkerIndex].response2Response;
    }
}
