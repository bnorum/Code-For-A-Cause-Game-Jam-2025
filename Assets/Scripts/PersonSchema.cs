using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PersonSchema", order = 1)]
public class PersonSchema : ScriptableObject
{
    public string personName;
    public int age;
    public string occupation;
    /// <summary>
    /// 1-poor, 2-middle class, 3-millionaire, 4-billionaire
    /// </summary>
    public int netWorth;
    public bool isLefty;
    public int marriageNum;
    public int divorceNum;
    public bool hasBeenToPrison;
    public List<EmailSchema> relatedEmails;
    public List<bool> relatedEmailsUsed = new List<bool>(4);

    public bool shouldGoToHeaven;

}