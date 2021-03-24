using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject inventory;

    // Start is called before the first frame update
    void Start(){
        //gameObject.GetComponent<Button>().onClick.AddListener();
    }

    private void OpenInventory(){
        // On button press (tab)
        //  Display the inventory menu and allow interactions
    }

    private void CloseInventory(){
        // On click of Exit button
        //  Hide the canvas from the player and make interation locked
    }

    private void ItemBoxSelect(){
        // On click of item box
        //  Show the item and correlating description in the description panel
        //  Button to prompt the player to use the item
    }

    private void UseItem(){
        // On click of use button
        //  If item is key type, then stays in inventory
        //  If item is consumable type, then disapears from inventory (-1 to item count total if stacked)
    }

}