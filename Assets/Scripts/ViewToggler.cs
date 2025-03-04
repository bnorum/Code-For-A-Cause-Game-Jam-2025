using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewToggler : MonoBehaviour
{

    public Sprite deskView;
    public Sprite computerView;
    public Camera mainCamera;
    public GameObject DeskRoute;
    public GameObject ComputerRoute;
    public GameObject PapersRoute;
    public List<Image> buttonImages;

    public int viewnum = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (viewnum == 0) {

            //ugliest code ever
            //but it works so who cares
            mainCamera.transform.position = Vector3.Lerp(
                new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z),
                new Vector3(DeskRoute.transform.position.x, DeskRoute.transform.position.y, mainCamera.transform.position.z),
                Time.deltaTime * 5f);

            buttonImages[0].color = new Color(1f, 0f, 0f, 1f);
            buttonImages[1].color = new Color(1f, 1f, 1f, 1f);
            buttonImages[2].color = new Color(1f, 1f, 1f, 1f);
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, 5f, Time.deltaTime * 5f);

        } else if (viewnum == 1) {

            mainCamera.transform.position = Vector3.Lerp(
                new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z),
                new Vector3(ComputerRoute.transform.position.x, ComputerRoute.transform.position.y, mainCamera.transform.position.z),
                Time.deltaTime * 5f);


            buttonImages[0].color = new Color(1f, 1f, 1f, 1f);
            buttonImages[1].color = new Color(1f, 0f, 0f, 1f);
            buttonImages[2].color = new Color(1f, 1f, 1f, 1f);
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, 3.42f, Time.deltaTime * 5f);

        } else if (viewnum == 2) {
            mainCamera.transform.position = Vector3.Lerp(
                new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z),
                new Vector3(PapersRoute.transform.position.x, PapersRoute.transform.position.y, mainCamera.transform.position.z),
                Time.deltaTime * 5f);


            buttonImages[0].color = new Color(1f, 1f, 1f, 1f);
            buttonImages[1].color = new Color(1f, 1f, 1f, 1f);
            buttonImages[2].color = new Color(1f, 0f, 0f, 1f);
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, 3.42f, Time.deltaTime * 5f);
        }


    }

    public void ToggleView(int viewnumber) {
        viewnum = viewnumber;
    }
}
