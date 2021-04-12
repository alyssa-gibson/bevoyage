using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BattleHUD : MonoBehaviour
{
	//UI elements
    public GameObject BattleUI;
    public GameObject AttackUI;

    //From tutorial, modify to match project
    public Text nameText;
    public Text levelText;
    public Slider hpSlider;

    // Start is called before the first frame update
    void Start()
    {
        BattleUI.SetActive(true);
        AttackUI.SetActive(false);
    }

    void attackMenuSwitch() {
        BattleUI.SetActive(false);
        AttackUI.SetActive(true);
    }

    public void setHUD(Unit unit) {
        nameText.text = unit.unitName;
        levelText.text = unit.unitLevel.ToString("R");
        hpSlider.maxValue = unit.maxHP;
        hpSlider.value = unit.currentHP;
    }

    public void setHP(int hp){
        hpSlider.value = hp;
    }
}
