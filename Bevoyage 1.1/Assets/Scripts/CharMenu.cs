using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Display the first member of the party (slot 1)
        //  calls char select once
    }

    private void OpenCharMenu()
    {
        // On button press (c)
        //  Display the character menu and allow interactions
    }

    private void CloseCharMenu()
    {
        // On click of Exit button (hope for toggle)
        //  Hide the canvas from the player and make interation locked
    }

    private void CharSelect()
    {
        // On click of Character Portrait
        //  Show the character in the middle panel and show stats and equipment on the right panel
    }

    private void ItemSelect()
    {
        // On click of an item equipment on the right
        //  Inventory of just that equipment type will pop up
        //  Currently set on 2 different types, armor and weapon types
    }
}
