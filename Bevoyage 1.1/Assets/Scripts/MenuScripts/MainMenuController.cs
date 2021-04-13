using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
	public GameObject mainMenu;
    public GameObject optionsMenu;
    public GameObject gameMenu;
    
    public void playGame() {
        SceneManager.LoadScene("OverworldMap");
    }

    public void gameSaves() {
        mainMenu.SetActive(false);
        gameMenu.SetActive(true);
    }
 
    public void options() {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void gameBack() {
        mainMenu.SetActive(true);
        gameMenu.SetActive(false);
    }

    public void optionsBack() {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }
 
    public void exitGame() {
        Application.Quit();
    }
}
