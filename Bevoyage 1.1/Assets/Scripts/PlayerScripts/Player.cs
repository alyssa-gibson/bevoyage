using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{
    
    public GameObject char1;
    public GameObject char2;
    public GameObject char3;
    public GameObject char4;
    public GameObject saveMenu;
    Vector3 position;

    // May not even be needed here...
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }
    // closes the save menu
    public void CloseSave()
    {   
        saveMenu.SetActive(false);
    }
    // may not even be needed here...
    public void LoadPlayer()
    {
       PlayerData data = SaveSystem.LoadPlayer();
       transform.position = new Vector3(data.position[0], data.position[1], 0);
    }
}
