using UnityEngine;
using System.Collections.Generic;
using System;
using TMPro;


public class CommandmentsManager : MonoBehaviour
{
    public static CommandmentsManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private string commandmentText;

    void Start()
    {
        //EMAIL PEOPLE
        commandmentText = "";
        //PARAMETER PEOPLE
        SetAllParams();
        if(PersistentData.currentDay==1)
        {
            SelectDailyParametersDayOne();
            CheckDailyParameterMatches();
            SetManualText();
        }
        else
        {
            SelectDailyParameters(4);
            CheckDailyParameterMatches();
            SetManualText();
        }
    }


    public bool isLefty;
    private string p1 = "All Lefties, unless their address contains a 5,6, or 7";
    public bool P1(PersonSchema person)
    {
        if(person.isLefty && (!person.address.Contains("5") || !person.address.Contains("6") || !person.address.Contains("7")))
            return true;
        else
            return false;
    }
    public bool isBillionaire;
    private string p2 = "Anyone in the 1%.";
    public bool P2(PersonSchema person)
    {
        if(person.netWorth>=4)
            return true;
        else
            return false;
    }
    public bool isDivorded3PlusTimes;
    private string p3 = " with 3+ Divorces";
    public bool P3(PersonSchema person)
    {
        if(person.divorceNum>=3)
            return true;
        else
            return false;
    }
    public bool isMiddleClassAndNotMarried;
    private string p4 = "Anyone who is Middle Class & Not currently married";
    public bool P4(PersonSchema person)
    {
        if(person.netWorth==2 && person.marriageNum==person.divorceNum)
            return true;
        else
            return false;
    }
    public bool isCarpenter;
    private string p5 = "People who like Video Games or who have cheated";
    public bool P5(PersonSchema person)
    {
        if(person.hobby == "Video Games" || person.worstThing.Contains("Cheat"))
            return true;
        else
            return false;
    }
    public bool isNameBeginsWithC;
    private string p6 = "Anyone who's First name starts with C, or M";
    public bool P6(PersonSchema person)
    {
        if (person.name.StartsWith("C") || person.name.StartsWith("M") )
            return true;
        else
            return false;
    }
    public bool isTeenagerandMillionaire;
    private string p7 = "All Upper-class Teenagers";
    public bool P7(PersonSchema person)
    {
        if (person.netWorth>=3 && person.age>13 && person.age<20)
            return true;
        else
            return false;
    }
    public bool isNameBeginsWithGandIsPoor;
    private string p8 = "All Lower & Middle-class, if their name begins with G";
    public bool P8(PersonSchema person)
    {
        if (person.name.StartsWith("G") && (person.netWorth==0 ||person.netWorth==1) )
            return true;
        else
            return false;
    }
    public bool hasBeenToPrisonOrisOlderThan75;
    private string p9 = "Anyone older than 75 but younger than 85";
    public bool P9(PersonSchema person)
    {
        if (person.age>75 && person.age <85)
            return true;
        else
            return false;
    }
    public bool is40to49andMarried;
    private string p10 = "Anyone in their 40s and they've been married before";
    public bool P10(PersonSchema person)
    {
        if (person.age >=40 && person.age<50 && person.marriageNum >0)
            return true;
        else
            return false;
    }
    public bool isGovernmentEmployee;
    private string p11 ="Anyone who works for the government";
    public bool P11(PersonSchema person)
    {
        if(person.emailDomain.Contains("gov"))
            return true;
        else
            return false;
    }
    public bool isZipCodeContains4;
    private string p12 ="Anyone who's zip code or email contains a 4";
    public bool P12(PersonSchema person)
    {
        if (person.zipCode.Contains("4") || person.emailHandle.Contains("4") || person.emailDomain.Contains("4"))
            return true;
        else
            return false;
    }
    public bool isUsingYahooOrHobbyCoding;
    private string p13 ="Anyone who uses Yoohoo and has been married.";
    public bool P13(PersonSchema person)
    {
        if (person.emailDomain.Contains("yoohoo") || person.marriageNum >0)
            return true;
        else
            return false;
    }
    public bool isNameBeginsWithVowelAndMiddleClass;
    private string p14 = "Anyone who's name starts with a vowel, and they're middle class";
    public bool P14(PersonSchema person)
    {
        if ((person.name.StartsWith("A") || person.name.StartsWith("E") ||  person.name.StartsWith("I")||person.name.StartsWith("O")|| person.name.StartsWith("U") || person.name.StartsWith("Y")) && person.netWorth == 2)
            return true;
        else
            return false;
    }
    public bool isRightyAndLowerClass;
    private string p15 = "People who are Righties and lower class";
    public bool P15(PersonSchema person)
    {
        if (person.netWorth==0 && !person.isLefty)
            return true;
        else
            return false;
    }
    public bool isDivordedAndEmailDoesntContainANumber;
    private string p16 = "If Someone Has divorced, unless their email handle contains a number";
    public bool P16(PersonSchema person)
        {
            if (person.divorceNum > 0 && (person.emailHandle.Contains("1") || person.emailHandle.Contains("2") || person.emailHandle.Contains("3") || person.emailHandle.Contains("4") || person.emailHandle.Contains("5") || person.emailHandle.Contains("6") ||  person.emailHandle.Contains("7") || person.emailHandle.Contains("8") || person.emailHandle.Contains("9") || person.emailHandle.Contains("0")))
            {   
                return true;
            }
            else
                return false;
        }
    public bool isInvolvedWithCrypto;
    private string p17 = "If someone lives on a 'Lane', or is a Married Student";
    public bool P17(PersonSchema person)
        {
            if (person.address.Contains("Lane") || person.occupation.Contains("Student") && person.marriageNum>0)
                return true;
            else
                return false;
        }
    public bool isZipHasA2ButNotA1;
    private string p18 = "If Someone's zip does not contain a 1, 2, 3, 5, or 8";
    public bool P18(PersonSchema person)
        {
            if (!person.zipCode.Contains("1") && !person.zipCode.Contains("2")&& !person.zipCode.Contains("3")&& !person.zipCode.Contains("5")&& !person.zipCode.Contains("8"))
                return true;
            else
                return false;
        }
    public bool isAgeLessThan21;
    private string p19 = "Anyone who isn't legally allowed to drink and uses Hmail,";
    public bool P19(PersonSchema person)
        {
            if (person.age <21 && person.emailDomain.Contains("hmail"))
                return true;
            else
                return false;
        }
    public bool isTotalMarriages0BracketLower;
    private string p20 = "All in the lower class who are yet to marry";
    public bool P20(PersonSchema person)
        {
            if (person.marriageNum == 0 && person.netWorth==0)
                return true;
            else
                return false;
        }    
    void SetAllParams()
    {
        PersistentData.allParameters.Add(p1);
        PersistentData.allParameters.Add(p2);
        PersistentData.allParameters.Add(p3);
        PersistentData.allParameters.Add(p4);
        PersistentData.allParameters.Add(p5);
        PersistentData.allParameters.Add(p6);
        PersistentData.allParameters.Add(p7);
        PersistentData.allParameters.Add(p8);
        PersistentData.allParameters.Add(p9);
        PersistentData.allParameters.Add(p10);
        PersistentData.allParameters.Add(p11);
        PersistentData.allParameters.Add(p12);
        PersistentData.allParameters.Add(p13);
        PersistentData.allParameters.Add(p14);
        PersistentData.allParameters.Add(p15);
        PersistentData.allParameters.Add(p16);
        PersistentData.allParameters.Add(p17);
        PersistentData.allParameters.Add(p18);
        PersistentData.allParameters.Add(p19);
        PersistentData.allParameters.Add(p20);
    }

    private void SelectDailyParametersDayOne()
    {//DAY ONE's PARAMETERS, preselected so it isnt too difficult.
        isBillionaire = true;
        isMiddleClassAndNotMarried = true;
        isDivordedAndEmailDoesntContainANumber = true;
        isZipCodeContains4 = true;
    }

    public void SelectDailyParameters(int num)
    {
        List<int> selectedIndices = new List<int>();
        System.Random rand = new System.Random();

        while (selectedIndices.Count < num)
        {
            int index = rand.Next(1, 11);
            if (!selectedIndices.Contains(index))
            {
                selectedIndices.Add(index);
            }
        }

        foreach (int index in selectedIndices)
        {
            switch (index)
            {
            case 1:
                isLefty = true;
                PersistentData.allParameters.Remove(p1);
               break;
            case 2:
                isBillionaire = true;
                PersistentData.allParameters.Remove(p2);
                break;
            case 3:
                isDivorded3PlusTimes = true;
                PersistentData.allParameters.Remove(p3);
                break;
            case 4:
                isMiddleClassAndNotMarried = true;
                PersistentData.allParameters.Remove(p4);
                break;
            case 5:
                isCarpenter = true;
                PersistentData.allParameters.Remove(p5);
                break;
            case 6:
                isNameBeginsWithC = true;
                PersistentData.allParameters.Remove(p6);
                break;
            case 7:
                isTeenagerandMillionaire = true;
                PersistentData.allParameters.Remove(p7);
                break;
            case 8:
                isNameBeginsWithGandIsPoor = true;
                PersistentData.allParameters.Remove(p8);
                break;
            case 9:
                hasBeenToPrisonOrisOlderThan75 = true;
                PersistentData.allParameters.Remove(p9);
                break;
            case 10:
                is40to49andMarried = true;
                PersistentData.allParameters.Remove(p10);
                break;
            case 11:
                isGovernmentEmployee = true;
                PersistentData.allParameters.Remove(p11);
                break;
            case 12:
                isZipCodeContains4 = true;
                PersistentData.allParameters.Remove(p12);
                break;
            case 13:
                isUsingYahooOrHobbyCoding = true;
                PersistentData.allParameters.Remove(p13);
                break;
            case 14:
                isNameBeginsWithVowelAndMiddleClass = true;
                PersistentData.allParameters.Remove(p14);
                break;
            case 15:
                isRightyAndLowerClass = true;
                PersistentData.allParameters.Remove(p15);
                break;
            case 16:
                isDivordedAndEmailDoesntContainANumber = true;
                PersistentData.allParameters.Remove(p16);
                break;
            case 17:
                isInvolvedWithCrypto = true;
                PersistentData.allParameters.Remove(p17);
                break;
            case 18:
                isZipHasA2ButNotA1 = true;
                PersistentData.allParameters.Remove(p18);
                break;
            case 19:
                isAgeLessThan21 = true;
                PersistentData.allParameters.Remove(p19);
                break;
            case 20:
                isTotalMarriages0BracketLower = true;
                PersistentData.allParameters.Remove(p20);
                break;
            }
        }
    }


    public void SetManualText()
    {
        string parametersCombined = "";
        if (isLefty) parametersCombined += p1 + "\n";
        if (isBillionaire) parametersCombined += p2 + "\n";
        if (isDivorded3PlusTimes) parametersCombined += p3 + "\n";
        if (isMiddleClassAndNotMarried) parametersCombined += p4 + "\n";
        if (isCarpenter) parametersCombined += p5 + "\n";
        if (isNameBeginsWithC) parametersCombined += p6 + "\n";
        if (isTeenagerandMillionaire) parametersCombined += p7 + "\n";
        if (isNameBeginsWithGandIsPoor) parametersCombined += p8 + "\n";
        if (hasBeenToPrisonOrisOlderThan75) parametersCombined += p9 + "\n";
        if (is40to49andMarried) parametersCombined += p10 + "\n";
        if (isGovernmentEmployee) parametersCombined += p11 + "\n";
        if (isZipCodeContains4) parametersCombined += p12 + "\n";
        if (isUsingYahooOrHobbyCoding) parametersCombined += p13 + "\n";
        if (isNameBeginsWithVowelAndMiddleClass) parametersCombined += p14 + "\n";
        if (isRightyAndLowerClass) parametersCombined += p15 + "\n";
        if (isDivordedAndEmailDoesntContainANumber) parametersCombined += p16 + "\n";
        if (isInvolvedWithCrypto) parametersCombined += p17 + "\n";
        if (isZipHasA2ButNotA1) parametersCombined += p18 + "\n";
        if (isAgeLessThan21) parametersCombined += p19 + "\n";
        if (isTotalMarriages0BracketLower) parametersCombined += p20 + "\n";
        if (PersistentData.currentDay == 5) parametersCombined += "Faith lies about Charity, \nCharity lies about Hope, \nJustice lies about Hope, \nHope lies about herself. \nBut it's only one of them. \nAnd forget about lying this time.";
        // List<PersonSchema> people = GameManager.Instance.chosenEmailPeople;
        // foreach (PersonSchema person in people)
        // {
        //     if (UnityEngine.Random.Range(0,2) == 0) {
        //         commandmentText += "\n" + person.commandment;
        //     }
        // }
        Debug.Log(parametersCombined);
        Manual.Instance.manualText.Add($"Daily Hell Parameters\nHere's todays list of what to send to hell:\n\n\n {parametersCombined}");
    }

    public void ResetAllParams()
    {
        isLefty = false;
        isBillionaire = false;
        isDivorded3PlusTimes = false;
        isMiddleClassAndNotMarried = false;
        isCarpenter = false;
        isNameBeginsWithC = false;
        isTeenagerandMillionaire = false;
        isNameBeginsWithGandIsPoor = false;
        hasBeenToPrisonOrisOlderThan75 = false;
        is40to49andMarried = false;
        isGovernmentEmployee = false;
        isZipCodeContains4 = false;
        isUsingYahooOrHobbyCoding = false;
        isNameBeginsWithVowelAndMiddleClass = false;
        isRightyAndLowerClass = false;
        isDivordedAndEmailDoesntContainANumber = false;
        isInvolvedWithCrypto = false;
        isZipHasA2ButNotA1 = false;
        isAgeLessThan21 = false;
        isTotalMarriages0BracketLower = false;
    }

    public void CheckDailyParameterMatches()
    {
        foreach (var person in GameManager.Instance.chosenParameterPeople)
        {
            person.failedDailyParameters = false;
            bool failed = false;
            if (isLefty && P1(person)) 
            {
                failed = true;
                Debug.Log($"{person.name} failed because they are left-handed.");
            }
            if (isBillionaire && P2(person)) 
            {
                failed = true;
                Debug.Log($"{person.name} failed because they are a billionaire.");
            }
            if (isDivorded3PlusTimes && P3(person)) 
            {
                failed = true;
                Debug.Log($"{person.name} failed because they have been divorced 3 or more times.");
            }
            if (isMiddleClassAndNotMarried && P4(person)) 
            {
                failed = true;
                Debug.Log($"{person.name} failed because they are middle class and not married.");
            }
            if (isCarpenter && P5(person)) 
            {
                failed = true;
                Debug.Log($"{person.name} failed because they are a carpenter.");
            }
            if (isNameBeginsWithC && P6(person)) 
            {
                failed = true;
                Debug.Log($"{person.name} failed because their name begins with 'C'.");
            }
            if (isTeenagerandMillionaire && P7(person)) 
            {
                failed = true;
                Debug.Log($"{person.name} failed because they are a millionaire teenager.");
            }
            if (isNameBeginsWithGandIsPoor && P8(person)) 
            {
                failed = true;
                Debug.Log($"{person.name} failed because their name begins with 'G' and they are poor.");
            }
            if (hasBeenToPrisonOrisOlderThan75 && P9(person)) 
            {
                failed = true;
                Debug.Log($"{person.name} failed because they have been to prison or are older than 75.");
            }
            if (is40to49andMarried && P10(person)) 
            {
                failed = true;
                Debug.Log($"{person.name} failed because they are in their 40s and have been married before.");
            }
            if (isGovernmentEmployee && P11(person)) 
            {
                failed = true;
                Debug.Log($"{person.name} failed because they are a government employee.");
            }
            if (isZipCodeContains4 && P12(person)) 
            {
                failed = true;
                Debug.Log($"{person.name} failed because their zip code or email contains a 4.");
            }
            if (isUsingYahooOrHobbyCoding && P13(person)) 
            {
                failed = true;
                Debug.Log($"{person.name} failed because they use Yahoo or like to code.");
            }
            if (isNameBeginsWithVowelAndMiddleClass && P14(person)) 
            {
                failed = true;
                Debug.Log($"{person.name} failed because their name begins with a vowel and they are middle class.");
            }
            if (isRightyAndLowerClass && P15(person)) 
            {
                failed = true;
                Debug.Log($"{person.name} failed because they are a righty and lower class.");
            }
            if (isDivordedAndEmailDoesntContainANumber && P16(person)) 
            {
                failed = true;
                Debug.Log($"{person.name} failed because they are divorced and their email handle doesn't contain a number.");
            }
            if (isInvolvedWithCrypto && P17(person)) 
            {
                failed = true;
                Debug.Log($"{person.name} failed because they are involved with crypto.");
            }
            if (isZipHasA2ButNotA1 && P18(person)) 
            {
                failed = true;
                Debug.Log($"{person.name} failed because their zip code has a 1 but not a 2.");
            }
            if (isAgeLessThan21 && P19(person)) 
            {
                failed = true;
                Debug.Log($"{person.name} failed because they are under 21.");
            }
            if (isTotalMarriages0BracketLower && P20(person)) 
            {
                failed = true;
                Debug.Log($"{person.name} failed because they have never been married and are in the lower class.");
            }
            if(failed)
                person.failedDailyParameters = failed;
        }
    }
}