using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
	//UI elements
    public GameObject BattleUI;
    public GameObject AttackUI;
    // Start is called before the first frame update
    void Start()
    {
        BattleUI.SetActive(true);
        AttackUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
