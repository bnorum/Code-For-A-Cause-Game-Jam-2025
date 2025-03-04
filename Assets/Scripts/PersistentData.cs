using System.Collections.Generic;

public static class PersistentData
{
    public static int currentDay = 0;
    public static List<PersonSchema> peopleDamned = new List<PersonSchema>();
    public static List<PersonSchema> peopleSaved = new List<PersonSchema>();
    public static int coworkerFriendlinessScore = 0;
}