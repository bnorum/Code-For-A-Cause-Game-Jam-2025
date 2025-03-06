using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverCanvas;
    public TextMeshProUGUI summaryText;
    public AudioSource textSFX;
    public AudioSource determinationSFX;
    public GameObject backbutton;
    public UnityEngine.UI.Image determination;
    public Sprite Hired;
    public Sprite Fired;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (PersistentData.isGameOver) {
            StartCoroutine(DisplaySummary());

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator DisplaySummary() {
        gameOverCanvas.SetActive(true);
        summaryText.text = "Employment Summary";

        yield return new WaitForSeconds(5);
        yield return new WaitForSeconds(1);
        summaryText.text += "\n\nPeople Judged Correctly: "; //then get this number
        textSFX.time = 0;
        textSFX.Play();
        yield return new WaitForSeconds(1);
        summaryText.text += "\nPeople Judged Incorrectly: "; //then get this number
        textSFX.time = 0;
        textSFX.Play();
        yield return new WaitForSeconds(1);
        summaryText.text += "\nCoworkers Friendliness Index: " + PersistentData.coworkerFriendlinessScore;
        textSFX.time = 0;
        textSFX.Play();
        yield return new WaitForSeconds(1);
        summaryText.text += "\nBest Throw Speed: " + PersistentData.bestLinearVelocity;
        textSFX.time = 0;
        textSFX.Play();
        yield return new WaitForSeconds(1);
        summaryText.text += "\n\n Descision: ";
        yield return new WaitForSeconds(3);
        //based on the previous stats formulate a grade
        if (DetermineGrade() > 350) {
            //do stuff to say hired
            determination.sprite = Hired;
            determination.gameObject.SetActive(true);
            determinationSFX.Play();
            StartScreen.Instance.OnWin();
        }
        else {
            //do stuff to say fired
            determination.sprite = Fired;
            determination.gameObject.SetActive(true);
            determinationSFX.Play();
            StartScreen.Instance.OnLose();
        }

        yield return new WaitForSeconds(3);
        backbutton.SetActive(true);
    }

    int DetermineGrade() {
        int score = 0;
        score += PersistentData.peopleDeterminedCorrectly * 20;
        score -= (20 - PersistentData.peopleDeterminedCorrectly) * 20;
        score += PersistentData.coworkerFriendlinessScore * 3;
        score += (int)PersistentData.bestLinearVelocity * 2;

        return score;
    }


}
