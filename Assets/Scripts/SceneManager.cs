using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadNewDay(int daynum)
    {
        PersistentData.currentDay = daynum;
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void LoadMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void LoadBetweenDay()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
}
