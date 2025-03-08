using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PersonSchema", order = 1)]
public class PersonSchema : ScriptableObject
{
    public bool isEmailPerson;
    public string personName;
    public int age;
    public string emailHandle;
    public string emailDomain;
    public string address;
    public string zipCode;
    public string occupation;
    /// <summary>
    /// 1-lower-class, 2-middle class, 3-upper-class, 4-billionaire
    /// </summary>
    public int netWorth;
    public bool isLefty;
    public int marriageNum;
    public int divorceNum;
    public string worstThing;
    public string hobby;
    public bool hasBeenToPrison;

    public List<EmailSchema> relatedEmails;
    public List<bool> relatedEmailsUsed = new List<bool>(4);
    public bool shouldGoToHeaven;
    public bool failedDailyParameters;

    public string commandment;

}