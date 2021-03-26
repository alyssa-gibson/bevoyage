using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMenu : MonoBehaviour
{
    public GameObject charaMenu;
    // Start is called before the first frame update
    void Start()
    {
        // Display the first member of the party (slot 1)
        //  calls char select once
    }

    public void OpenCharMenu()
    {
        // On button press (c)
        //  Display the character menu and allow interactions
        if (charaMenu.activeSelf == false) {
            charaMenu.SetActive(true);  
        }
    }

    public void CloseCharMenu()
    {
        // On click of Exit button (hope for toggle)
        //  Hide the canvas from the player and make interation locked
        if (charaMenu.activeSelf == true) {
            charaMenu.SetActive(false);  
        }
    }

    public void CharSelect()
    {
        // On click of Character Portrait
        //  Show the character in the middle panel and show stats and equipment on the right panel
    }

    public void ItemSelect()
    {
        // On click of an item equipment on the right
        //  Inventory of just that equipment type will pop up
        //  Currently set on 2 different types, armor and weapon types
    }
}
