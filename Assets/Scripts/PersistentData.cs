using System.Collections.Generic;
using System.Numerics;

public static class PersistentData
{
    public static int currentDay = 0;
    public static List<PersonSchema> remainingEmailPeople = new List<PersonSchema>();
    public static List<PersonSchema> remainingParameterPeople = new List<PersonSchema>();
    public static List<PersonSchema> peopleDamned = new List<PersonSchema>();
    public static List<PersonSchema> peopleSaved = new List<PersonSchema>();
    public static List<CoworkerSchema> remainingCoworkers = new List<CoworkerSchema>();
    public static List<string> allParameters = new List<string> {};
    public static int peopleDeterminedCorrectly = 0;
    public static int coworkerFriendlinessScore = 0;
    public static float difficultyScale = 1f;
    public static float bestLinearVelocity = 0f;
    public static float bestDistance = 0f;
    public static float bestAngularVelocity = 0f;
    public static bool isGameOver = false;

    public static List<PersonSchema> peopleSavedToday = new List<PersonSchema>();
    public static List<PersonSchema> peopleDamnedToday = new List<PersonSchema>();
    public static List<PersonSchema> peopleShouldveSavedToday = new List<PersonSchema>();
    public static List<PersonSchema> peopleShouldveDamnedToday = new List<PersonSchema>();



}