using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PersonSchema", order = 1)]
public class PersonSchema : ScriptableObject
{
    public string personName;
    public int age;
    public string occupation;
    public int netWorth;

    public List<EmailSchema> relatedEmails;
    public List<bool> relatedEmailsUsed = new List<bool>(4);

    public bool shouldGoToHeaven;

}