using System.Collections.Generic;

public static class PersistentData
{
    public static int currentDay = 0;
    public static List<PersonSchema> remainingPeople = new List<PersonSchema>();
    public static List<PersonSchema> peopleDamned = new List<PersonSchema>();
    public static List<PersonSchema> peopleSaved = new List<PersonSchema>();
    public static int coworkerFriendlinessScore = 0;
    public static float difficultyScale = 1f;

    //NOTE: Total score is calculated as follows:
    // coworkerFriendlinessScore

}