using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, DAMAGE, WON, LOST }

/* Future Notes: 
 * May change enum states to be move select phase + damage phase.
 * Refer to pseudocode sheet for game-specific mechanics.
 * Assign UI elements to respective variables.
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

    public GameObject BattleUI;
    public GameObject AttackUI;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public Text dialogueText;

    Stack partyDeck, enemyDeck = new Stack();
    public LinkedList <Move> partyTurnList, enemyTurnList = new LinkedList <Move>();

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetUpBattle());
    }

    IEnumerator SetUpBattle() {
        //Activate menus
        BattleUI.SetActive(true);
        AttackUI.SetActive(false);

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

    	//add all moves to player and enemy move stacks
        foreach(Move mov in playerUnit1.moveDeck) {
            partyDeck.Push(mov);
        }
        foreach(Move mov in playerUnit2.moveDeck) {
            partyDeck.Push(mov);
        }
        foreach(Move mov in playerUnit3.moveDeck) {
            partyDeck.Push(mov);
        }
        foreach(Move mov in playerUnit4.moveDeck) {
            partyDeck.Push(mov);
        }

        foreach(Move mov in enemyUnit.moveDeck) {
            partyDeck.Push(mov);
        }

        //shuffle the stacks
        //partyDeck = partyDeck.OrderBy(x => rnd.Next());
        //enemyDeck = enemyDeck.OrderBy(x => rnd.Next());

        //dialogueText.text = "Encountered a " + enemyUnit.unitName + ", prepare for battle!";

        playerHUD.setHUD(playerUnit1);
        enemyHUD.setHUD(enemyUnit);

        yield return new WaitForSeconds(3f);

        //Change to Player Phase
        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    //move inside code to damageCalc(), this method is just to select moves.
    //for move select, do not allow dead character moves to be selected.
    IEnumerator PlayerAttack()
    {
        float advantage = 1;
        float critDamage = 0;

        //check to see if move has attribute advantage

        //check to see if critical hit lands

        //actual damage calculation
        int damage = (int)((playerUnit1.power * advantage + critDamage) - enemyUnit.defense);

        //prevent negative damage
        if (damage < 0) {
            damage = 0;
        }

        //apply damage
        bool isDead = enemyUnit.TakeDamage(damage);

        //enemyHUD.SetHP(enemyUnit.currentHP);
        dialogueText.text = "The attack is successful!";

        yield return new WaitForSeconds(3f);

        if(isDead)
        {
            state = BattleState.WON;
            EndBattle();
        } else {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EnemyTurn()
    {
        dialogueText.text = enemyUnit.unitName + " attacks!";

        yield return new WaitForSeconds(3f);

        float advantage = 1;
        float critDamage = 0;

        //actual damage calculation
        int damage = (int)((enemyUnit.power * advantage + critDamage) - playerUnit1.defense);

        //actual damage calculation
        bool isDead = playerUnit1.TakeDamage(damage);

        //playerHUD.SetHP(playerUnit1.currentHP);

        yield return new WaitForSeconds(3f);

        if(isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        } else {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }

    }

    // IEnumerator damageCalc() {
    //    //compare lists, execute damage calculation in order, use unit functions 
    //    //check to see if characters are dead in each run
    //    //if the character that's about to move is dead, display this and do nothing
    // }

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
            StartCoroutine(PlayerAttack());
        }
    }
}
