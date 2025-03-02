using UnityEngine;
using UnityEngine.UI;
public class ViewToggler : MonoBehaviour
{

    public Sprite deskView;
    public Sprite computerView;
    public Camera mainCamera;
    public GameObject DeskRoute;
    public GameObject ComputerRoute;

    public bool isDeskView = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDeskView) {
            GetComponent<Image>().sprite = deskView;

            //ugliest code ever
            //but it works so who cares
            mainCamera.transform.position = Vector3.Lerp(
                new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z),
                new Vector3(DeskRoute.transform.position.x, DeskRoute.transform.position.y, mainCamera.transform.position.z),
                Time.deltaTime * 5f);

            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, 5f, Time.deltaTime * 5f);

        } else {
            GetComponent<Image>().sprite = computerView;

            mainCamera.transform.position = Vector3.Lerp(
                new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z),
                new Vector3(ComputerRoute.transform.position.x, ComputerRoute.transform.position.y, mainCamera.transform.position.z),
                Time.deltaTime * 5f);

            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, 3.5f, Time.deltaTime * 5f);

        }


    }

    public void ToggleView() {
        isDeskView = !isDeskView;
    }
}
