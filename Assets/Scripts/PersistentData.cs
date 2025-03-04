using System.Collections.Generic;

public static class PersistentData
{
    public static int currentDay = 0;
    public static List<PersonSchema> peopleCorrectlyServed = new List<PersonSchema>();
    public static List<PersonSchema> peopleIncorrectlyServed = new List<PersonSchema>();
    public static int coworkerFriendlinessScore = 0;

    //NOTE: Total score is calculated as follows:
    // coworkerFriendlinessScore + peopleCorrectlyServed.alloftheirchallengesvalues * 10 - peopleIncorrectlyServed.alloftheirchallengesvalues * 10

}