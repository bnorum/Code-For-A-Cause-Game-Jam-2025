using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/EmailSchema", order = 1)]
public class EmailSchema : ScriptableObject
{
    public SenderSchema sender;
    public string subject;
    public string body;
    public Sprite attachment;
    public PersonSchema relatedPerson;

}