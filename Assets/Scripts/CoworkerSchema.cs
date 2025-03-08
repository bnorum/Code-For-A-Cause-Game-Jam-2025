using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CoworkerSchema", order = 1)]
public class CoworkerSchema : ScriptableObject
{
    public Sprite coworkerImage;
    public Sprite armsImage;
    public string coworkerSpeech;
    public string responseOption1;
    public string responseOption2;
    public string response1Response;
    public string response2Response;
    public int friendlinessScore;
    public bool isCorrectResponse1;

}