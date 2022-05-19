using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{

    private int level;
    private float currentLevelExperience;
    private float experienceToLevel;


    public void AddExperience(float exp)
    {
        currentLevelExperience += exp;
        if (currentLevelExperience >= experienceToLevel)
        {
            currentLevelExperience -= experienceToLevel;
            LevelUp();
        }

        Debug.Log("EXP: " + currentLevelExperience + " Level: " + level);
    }

    private void LevelUp()
    {
        level++;
        experienceToLevel = level * 50 + Mathf.Pow(level * 2, 2);

        AddExperience(0);
    }

    // Start is called before the first frame update
    void Start()
    {
        LevelUp();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
