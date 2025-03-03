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
    public int challengeLevel;

    public List<EmailSchema> relatedEmails;

}