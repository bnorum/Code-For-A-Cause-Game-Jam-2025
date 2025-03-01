using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/EmailSchema", order = 1)]
public class EmailSchema : ScriptableObject
{
    public string senderEmail;
    public string senderName;
    public string subject;
    public string body;
    public Sprite profileImage;
    public Sprite attachment;

}