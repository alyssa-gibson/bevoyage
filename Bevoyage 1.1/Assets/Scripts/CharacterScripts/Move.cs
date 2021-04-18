using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public string moveName;
    public string attribute;
    public int weight;
    public float power;
    public float critRate;
    //public int numberOfHits;

    //For support moves, heal percentage?
    //public float healAmt;

    //Method that checks attribute effectiveness. Returns a float for damage modifier.
    // public float isEffective(string unitAttribute) {
    // 	float modifier = 1;
    // 	if( /*find a more effective way to compare strings jfc*/ ) {
    // 		modifier = 1.5;
    // 	}
    // 	return modifier;
    // } 
}
