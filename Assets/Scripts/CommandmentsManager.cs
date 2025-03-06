using UnityEngine;
using System.Collections.Generic;
public class JudgementParameters : MonoBehaviour
{
    public bool isLefty;
    public string p1 = "Send all Lefties to Hell Today";
    public bool P1(PersonSchema person)
    {
        if(person.isLefty)
            return true;
        else
            return false;
    }
    public bool isBillionaire;
    public string p2 = "Send all Billionaires to Hell Today";
    public bool P2(PersonSchema person)
    {
        if(person.netWorth>=4)
            return true;
        else
            return false;
    }
    public bool isDivorded3PlusTimes;
    public string p3 = "Send Anyone with 3+ Divorces to Hell Today";
    public bool P3(PersonSchema person)
    {
        if(person.divorceNum>=3)
            return true;
        else
            return false;
    }
    public bool isMiddleClassAndNotMarried;
    public string p4 = "Send Anyone who is Middle Class & Not Married to Hell Today";
    public bool P4(PersonSchema person)
    {
        if(person.netWorth==2 && person.marriageNum==0)
            return true;
        else
            return false;
    }
    public bool isCarpenter;
    public string p5 = "Send All Carpenters to Hell Today";
    public bool P5(PersonSchema person)
    {
        if(person.occupation == "Carpenter")
            return true;
        else
            return false;
    }
    public bool isNameBeginsWithC;
    public string p6 = "Firstname that starts with the letter C? Stright to hell.";
    public bool P6(PersonSchema person)
    {
        if (person.name.StartsWith("C"))
            return true;
        else
            return false;
    }
    public bool isTeenagerandMillionaire;
    public string p7 = "Send All millionaire teenagers to hell";
    public bool P7(PersonSchema person)
    {
        if (person.netWorth>=3 && person.age>13 && person.age<20)
            return true;
        else
            return false;
    }
    public bool isNameBeginsWithGandIsPoor;
    public string p8 = "Firstname that starts with the letter G & They are poor? Stright to hell.";
    public bool P8(PersonSchema person)
    {
        if (person.name.StartsWith("G") && person.netWorth==0)
            return true;
        else
            return false;
    }
    public bool hasBeenToPrisonOrisOlderThan75;
    public string p9 = "Been to prison Or is older than 75? Straight to hell.";
    public bool P9(PersonSchema person)
    {
        if (person.hasBeenToPrison || person.age>75)
            return true;
        else
            return false;
    }
    public bool is40to49andMarried;
    public string p10 = "Anyone in their 40s and they've been married before? To hell with them.";
    public bool P10(PersonSchema person)
    {
        if (person.age >=40 && person.age<50 && person.marriageNum >0)
            return true;
        else
            return false;
    }

    void Start()
    { 
        SelectDailyParameters(3);
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
    }

    public void SelectDailyParameters(int num)
    {
        SetAllParams();
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
            }
        }
    }

    public TMPro.TMP_Text manualText;

    public void SetManualText()
    {
        string parametersCombined = "Daily Parameters:\n";

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

        manualText.text = parametersCombined;
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
    }
}
