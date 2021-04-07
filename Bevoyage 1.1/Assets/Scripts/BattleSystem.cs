using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

/* Future Notes: 
 * May change enum states to be move select phase + damage phase.
 * Refer to pseudocode sheet for game-specific mechanics.
 * Scale up to include multiple characters.
 */

public class BattleSystem : MonoBehaviour
{
	public GameObject playerPrefab;
	public GameObject enemyPrefab;

	public Transform playerStation;
	public Transform enemyStation;

	Unit playerUnit;
	Unit enemyUnit;

	public BattleState state;
    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetUpBattle());
    }

    IEnumerator SetUpBattle() {
        //Spawn players and enemies
    	GameObject playerGO = Instantiate(playerPrefab, playerStation);
    	playerUnit = playerGO.GetComponent<Unit>();
    	GameObject enemyGO = Instantiate(enemyPrefab, enemyStation);
    	enemyUnit = enemyGO.GetComponent<Unit>();

        //Tutorial inserts code to change dialogue box to include enemy names, but may not need it

        yield return new WaitForSeconds(3f);

        //Change to Player Phase
        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerAttack() {
        //deal damage

        yield return new WaitForSeconds(3f);

        //check if enemy is dead
        //change state accordingly

    }

    //Method to represent player move selection.
    void PlayerTurn() {

    }

    public void OnAttackButton() {
        if (state != BattleState.PLAYERTURN) {
            return;
        } else {
            StartCoroutine(PlayerAttack());
        }
    }
}
