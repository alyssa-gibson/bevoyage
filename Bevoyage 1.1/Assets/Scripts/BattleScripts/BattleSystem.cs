using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

/* Future Notes: 
 * May change enum states to be move select phase + damage phase.
 * Refer to pseudocode sheet for game-specific mechanics.
 * Scale up to include multiple characters.
 */

public class BattleSystem : MonoBehaviour
{

	public GameObject playerPrefab1;
    public GameObject playerPrefab2;
    public GameObject playerPrefab3;
    public GameObject playerPrefab4;
	public GameObject enemyPrefab;

	public Transform playerStation1;
    public Transform playerStation2;
    public Transform playerStation3;
    public Transform playerStation4;
	public Transform enemyStation;

	Unit playerUnit1;
    Unit playerUnit2;
    Unit playerUnit3;
    Unit playerUnit4;
	Unit enemyUnit;

	public BattleState state;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public Text dialogueText;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetUpBattle());
    }

    IEnumerator SetUpBattle() {
        //Spawn players and enemies
    	GameObject playerGO1 = Instantiate(playerPrefab1, playerStation1);
    	playerUnit1 = playerGO1.GetComponent<Unit>();
        GameObject playerGO2 = Instantiate(playerPrefab2, playerStation2);
        playerUnit2 = playerGO2.GetComponent<Unit>();
        GameObject playerGO3 = Instantiate(playerPrefab3, playerStation3);
        playerUnit3 = playerGO3.GetComponent<Unit>();
        GameObject playerGO4 = Instantiate(playerPrefab4, playerStation4);
        playerUnit4 = playerGO4.GetComponent<Unit>();

    	GameObject enemyGO = Instantiate(enemyPrefab, enemyStation);
    	enemyUnit = enemyGO.GetComponent<Unit>();

        dialogueText.text = "Encountered a " + enemyUnit.unitName + ", prepare for battle!";

        playerHUD.setHUD(playerUnit1);
        enemyHUD.setHUD(enemyUnit);

        yield return new WaitForSeconds(3f);

        //Change to Player Phase
        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    // IEnumerator PlayerAttack()
    // {
        // bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

        // enemyHUD.SetHP(enemyUnit.currentHP);
        // dialogueText.text = "The attack is successful!";

        // yield return new WaitForSeconds(3f);

        // if(isDead)
        // {
        //     state = BattleState.WON;
        //     EndBattle();
        // } else {
        //     state = BattleState.ENEMYTURN;
        //     StartCoroutine(EnemyTurn());
        // }
    // }

    // IEnumerator EnemyTurn()
    // {
    //     dialogueText.text = enemyUnit.unitName + " attacks!";

    //     yield return new WaitForSeconds(1f);

    //     bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

    //     playerHUD.SetHP(playerUnit.currentHP);

    //     yield return new WaitForSeconds(1f);

    //     if(isDead)
    //     {
    //         state = BattleState.LOST;
    //         EndBattle();
    //     } else {
    //         state = BattleState.PLAYERTURN;
    //         PlayerTurn();
    //     }

    //}

    void EndBattle()
    {
        if(state == BattleState.WON)
        {
            dialogueText.text = "You won the battle!";
        } else if (state == BattleState.LOST)
        {
            dialogueText.text = "You were defeated.";
        }
    }

    //Method to represent player move selection.
    void PlayerTurn() {
        dialogueText.text = "Choose an action:";
    }

    public void OnAttackButton() {
        if (state != BattleState.PLAYERTURN) {
            return;
        } else {
            //StartCoroutine(PlayerAttack());
        }
    }
}
