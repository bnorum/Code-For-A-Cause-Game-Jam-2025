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

    void Start()
    {
        AssignRandomSprites();
    }

    void AssignRandomSprites()
    {
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
