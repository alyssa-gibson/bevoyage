using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public string attribute;
    public float unitLevel;
    public float weightCap; //for battle cap only
    //public float currentWeight;

    public float power;
    public float critRate;
    public float defense;

    public float maxHP;
    public float currentHP;

    public Sprite image;

    //move list variable, can either be array or linked list
    public Move[] moveDeck = new Move[5];
    public LinkedList <Move> moveList = new LinkedList <Move>();

    //method to add a move from a unit's known move list to their active deck
    public void addMove(Move moveToActive, int pos) {
        moveDeck[pos] = moveToActive;
        Debug.Log("Move successfully added");
    }

    //method to add a move to a unit's known move list
    public void learnMove(Move moveToLearn) {
        moveList.AddLast(moveToLearn);
        Debug.Log("Move successfully learned");
    }

    // public void addMove(Move moveFromInventory) {
    //     //if move to select will overflow weight cap, don't allow
    //     if (currentWeight + moveFromInventory.weight > weightCap) {
    //         Debug.Log("Move not added, exceeds weight cost");
    //     }
    //     else {
    //         //add to list
    //         moveDeck.AddLast(moveFromInventory);
    //         Debug.Log("Move successfully added");
    //         currentWeight += moveFromInventory.weight;
    //     }
    // }

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
