using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    //public GameObject player;
    public GameObject char1;
    public GameObject char2;
    public GameObject char3;
    public GameObject char4;

    public float[] position;
    /* keeping just in case the loading of the objects themselves does not keep the correct data
    public int char1Level;
    public int char1Health;

    public int char2Level;
    public int char2Health;

    public int char3Level;
    public int char3Health;

    public int char4Level;
    public int char4Health;
    
 */
    public PlayerData(Player player)
    {
        char1 = player.char1;
        char2 = player.char2;
        char3 = player.char3;
        char4 = player.char4;

        position = new float[2];
        position[0] = player.transform.position.x;
        position[2] = player.transform.position.y;
    }
}
