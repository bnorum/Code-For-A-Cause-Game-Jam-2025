using UnityEngine;

public class heswalkingby : MonoBehaviour
{
    float sinaccumulator = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0.3f, 0, 0) * Time.deltaTime;
        transform.position += new Vector3(0, Mathf.Sin(sinaccumulator) * 0.05f * Time.deltaTime, 0);

        sinaccumulator += Time.deltaTime;
    }
}
