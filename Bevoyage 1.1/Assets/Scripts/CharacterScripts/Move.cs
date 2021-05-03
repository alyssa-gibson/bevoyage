using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    //For all moves
    public string moveName;
    public string attribute;
    public string moveType; // i.e.: attack move, support, etc
    public int weight; //minimum: 1, max: 5
    public string moveOwner; //for attack animations
    public int moveID; //for database

    //For attack moves
    public float power;
    public float critRate;
    public int hitCount;

    //For healing moves
    public float healAmt;

    //Method that checks attribute effectiveness. Returns a double for damage modifier.
    public double isEffective(string unitAttribute) {
    	double modifier = 0;
    	switch(attribute) {
    		case "sweet":
    			if (unitAttribute == "spiced") {
    				modifier = 1.5;
    			}
    			if (unitAttribute == "bitter") {
    				modifier = 0.5;
    			}
    			break;
    		case "sour":
	    		if (unitAttribute == "bitter") {
    				modifier = 1.5;
    			}
    			if (unitAttribute == "smoky") {
    				modifier = 0.5;
    			}
    			break;
    		case "bitter":
    			if (unitAttribute == "sweet") {
    				modifier = 1.5;
    			}
    			if (unitAttribute == "sour") {
    				modifier = 0.5;
    			}
    			break;
    		case "spiced":
    			if (unitAttribute == "smoky") {
    				modifier = 1.5;
    			}
    			if (unitAttribute == "sweet") {
    				modifier = 0.5;
    			}
    			break;
    		case "smoky":
    			if (unitAttribute == "sour") {
    				modifier = 1.5;
    			}
    			if (unitAttribute == "spiced") {
    				modifier = 0.5;
    			}
    			break;
    		case "neutral":
    			if (unitAttribute == "stale") {
    				modifier = 1.5;
    			}
    			break;
            default:
                modifier = 1.0;
                break;
    	}
    	return modifier;
    }
}
