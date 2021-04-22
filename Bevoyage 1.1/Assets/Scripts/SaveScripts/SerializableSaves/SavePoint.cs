using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    public GameObject saveMenu; 
    // Start is called before the first frame update
    void Start()
    {
        // Display the first member of the party (slot 1)
        //  calls char select once
        // Menu should be inactive before input.
        saveMenu.SetActive(false);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        ProcessColission(collision.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ProcessColission(collision.gameObject);
    }

    void ProcessColission(GameObject collider)
    {
        if (collider.CompareTag("SavePoint"))
        {
            AskToSave();
        }
    }

    void AskToSave()
    {
        Debug.Log("Will You Save?");
        saveMenu.SetActive(true);
        // Create a UI that will give the option to save or not
        // If Save Button Pressed 
        //Player.SavePlayer();

    }


}
