using System.Collections;
using System.Collections.Generic;
//using Assets;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleHUD : MonoBehaviour
{
	//UI elements
    public GameObject BattleUI;
    public GameObject AttackUI;

    //From tutorial, modify to match project
    public Text charName1;
    //public Text char1Lvl;
    public GameObject char1Image;
    public Slider char1Slider;

    public Text charName2;
    //public Text char2Lvl;
    public GameObject char2Image;
    public Slider char2Slider;

    public Text charName3;
    //public Text char3Lvl;
    public GameObject char3Image;
    public Slider char3Slider;

    public Text charName4;
    //public Text char4Lvl;
    public GameObject char4Image;
    public Slider char4Slider;
    /*
    public Text enemyName1;
    public Text enemy1Lvl;
    public Slider enemy1Slider;

    public Text enemyName2;
    public Text enemy2Lvl;
    public Slider enemy2Slider;

    public Text enemyName3;
    public Text enemy3Lvl;
    public Slider enemy3Slider;

    public Text enemyName4;
    public Text enemy4Lvl;
    public Slider enemy4Slider;
    */
    public void setHUD(Unit unit1, Unit unit2, Unit unit3, Unit unit4) {
        Debug.Log("Now Setting Up The Hud");
        charName1.text = unit1.unitName;
        //char1Lvl.text = unit1.unitLevel.ToString("R");
        char1Slider.maxValue = unit1.maxHP;
        char1Slider.value = unit1.currentHP;
        char1Image.GetComponent<Image>().sprite = unit1.image;

        charName2.text = unit2.unitName;
        //char2Lvl.text = unit2.unitLevel.ToString("R");
        char2Slider.maxValue = unit2.maxHP;
        char2Slider.value = unit2.currentHP;
        char2Image.GetComponent<Image>().sprite = unit2.image;

        charName3.text = unit3.unitName;
        //char3Lvl.text = unit3.unitLevel.ToString("R");
        char3Slider.maxValue = unit3.maxHP;
        char3Slider.value = unit3.currentHP;
        char3Image.GetComponent<Image>().sprite = unit3.image;

        charName4.text = unit4.unitName;
        //char4Lvl.text = unit4.unitLevel.ToString("R");
        char4Slider.maxValue = unit4.maxHP;
        char4Slider.value = unit4.currentHP;
        char4Image.GetComponent<Image>().sprite = unit4.image;
    }

    public void setHP(int hp, Slider hpSlider){
        hpSlider.value = hp;
    }
}
