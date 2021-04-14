using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public string attribute;
    public float unitLevel;
    public float weightCap;
    public float currentWeight;

    public float power;
    public float critRate;
    public float defense;

    public float maxHP;
    public float currentHP;

    //function to create move list
    //if move to select will overflow weight cap, don't allow

    public bool TakeDamage(int dmg) //may change int to float
    {

        currentHP -= dmg;

        if (currentHP <= 0)
            return true;
        else
            return false;
    }

    public void Heal(int amount)
    {
        currentHP += amount;
        if (currentHP > maxHP)
            currentHP = maxHP;
    }

}
