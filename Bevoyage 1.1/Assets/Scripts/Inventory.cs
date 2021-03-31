using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] public GameObject inventory;

    // Start is called before the first frame update
    void Start(){
        //gameObject.GetComponent<Button>().onClick.AddListener();
        inventory.SetActive(false); 
    }

    void Update() {
        if (Input.GetKeyDown("tab")) {
            if (inventory.activeSelf == false) {
                inventory.SetActive(true);
            } else {
                inventory.SetActive(false); 
            }
        }
    }

    // void OpenInventory(){
    //     // On button press (tab)
    //     //  Display the inventory menu and allow interactions
    //     if (inventory.activeSelf == false) {
    //         inventory.SetActive(true);  
    //     }
    // }

    // void CloseInventory(){
    //     // On click of Exit button
    //     //  Hide the canvas from the player and make interation locked
    //     if (inventory.activeSelf == true) {
    //         inventory.SetActive(false);  
    //     }
    // }

    void ItemBoxSelect(){
        // On click of item box
        //  Show the item and correlating description in the description panel
        //  Button to prompt the player to use the item
    }

    void UseItem(){
        // On click of use button
        //  If item is key type, then stays in inventory
        //  If item is consumable type, then disapears from inventory (-1 to item count total if stacked)
    }
}
