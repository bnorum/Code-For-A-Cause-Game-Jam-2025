using System;
using UnityEngine;

public class Person : MonoBehaviour
{
    public PersonSchema personSchema;
    public string personName;
    public int age;
    public string occupation;
    public int netWorth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        personName = personSchema.personName;
        age = personSchema.age;
        occupation = personSchema.occupation;
        netWorth = personSchema.netWorth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
