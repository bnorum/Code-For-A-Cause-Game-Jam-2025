using UnityEngine;

public class PersonSprite : MonoBehaviour
{
    public SpriteRenderer eyesSR;
    public SpriteRenderer headSR;
    public SpriteRenderer torsoSR;
    public SpriteRenderer legsSR;

    public Sprite[] spriteList1;
    public Sprite[] spriteList2;
    public Sprite[] spriteList3;
    public Sprite[] spriteList4;

    public GameObject parent;
    public Sprite LABRAT;

    void Start()
    {
        AssignRandomSprites();
    }

    void AssignRandomSprites()
    {
        if(parent.GetComponent<Person>().personSchema.personName == "Cheese Cheddar")
        {
            eyesSR.sprite = null;
            headSR.sprite = null;
            legsSR.sprite = null;
            torsoSR.sprite = LABRAT;
            torsoSR.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
            Vector3 newPos = torsoSR.transform.position;
            newPos.y += .08f;
            torsoSR.transform.position = newPos;
            return;
        }
        if (spriteList1.Length > 0 && eyesSR != null)
        {
            eyesSR.sprite = spriteList1[Random.Range(0, spriteList1.Length)];
        }

        if (spriteList2.Length > 0 && headSR != null)
        {
            headSR.sprite = spriteList2[Random.Range(0, spriteList2.Length)];
        }

        if (spriteList3.Length > 0 && torsoSR != null)
        {
            torsoSR.sprite = spriteList3[Random.Range(0, spriteList3.Length)];
        }

        if (spriteList3.Length > 0 && legsSR != null)
        {
            legsSR.sprite = spriteList4[Random.Range(0, spriteList4.Length)];
        }
    }
}
