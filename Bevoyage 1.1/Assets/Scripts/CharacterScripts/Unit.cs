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

    //move list variable, can either be array or linked list
    //public Move[] moveDeck;
    //public LinkedList <Move> moveDeck = new LinkedList <Move>();

    public void addMove(Move moveFromInventory) {
        //if move to select will overflow weight cap, don't allow
        if (currentWeight + moveFromInventory.weight > weightCap) {
            Debug.Log("Move not added, exceeds weight cost");
        }
        else {
            //add to list
            Debug.Log("Move successfully added");
            currentWeight += moveFromInventory.weight;
        }
    }

    /* Stat modifier functions */

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
