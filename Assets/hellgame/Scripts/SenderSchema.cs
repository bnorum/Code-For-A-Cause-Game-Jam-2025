using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SenderSchema", order = 1)]
public class SenderSchema : ScriptableObject
{
    public string senderEmail;
    public string senderName;
    public Sprite profileImage;

}