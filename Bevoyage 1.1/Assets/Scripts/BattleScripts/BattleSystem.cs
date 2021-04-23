using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

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
	public Transform enemyStation1;
    public Transform enemyStation2;
    public Transform enemyStation3;
    public Transform enemyStation4;

    Unit playerUnit1;
    Unit playerUnit2;
    Unit playerUnit3;
    Unit playerUnit4;
	Unit enemyUnit1;
    Unit enemyUnit2;
    Unit enemyUnit3;
    Unit enemyUnit4;

    public BattleState state;

    public GameObject BattleUI;
    public GameObject AttackUI;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public Text dialogueText;

    Stack partyDeck, enemyDeck = new Stack(); //for card selection
    public Move[] partyFullDeck, enemyFullDeck = new Move[20];
    public Move[] partyTurn, enemyTurn = new Move[5]; //holds the cards for a specific turn
    public LinkedList <Move> partyTurnList, enemyTurnList = new LinkedList <Move>();

    int partyCounter, enemyCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        Scene scene = gameObject.scene;
        if (scene.name == "BattleScene")
        {
            state = BattleState.START;
            StartCoroutine(SetUpBattle());
            Debug.Log("Made it to BattleSystem Start()");
        }

    }

    // Button Management Scripts Start Here
    public void attackMenuSwitch()
    {
        BattleUI.SetActive(false);
        AttackUI.SetActive(true);
    }

    public void BackButtonSwitch()
    {
        BattleUI.SetActive(true);
        AttackUI.SetActive(false);
    }

    public void FleeButton()
    {
        BattleUI.SetActive(false);
        AttackUI.SetActive(false);
        SceneManager.LoadScene("OverworldMap");
    }

    IEnumerator SetUpBattle() {
        Debug.Log("In SetUpBattle");
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

    	GameObject enemyGO1 = Instantiate(enemyPrefab, enemyStation1);
    	enemyUnit1 = enemyGO1.GetComponent<Unit>();
        GameObject enemyGO2 = Instantiate(enemyPrefab, enemyStation2);
        enemyUnit2 = enemyGO2.GetComponent<Unit>();
        GameObject enemyGO3 = Instantiate(enemyPrefab, enemyStation3);
        enemyUnit3 = enemyGO3.GetComponent<Unit>();
        GameObject enemyGO4 = Instantiate(enemyPrefab, enemyStation4);
        enemyUnit4 = enemyGO4.GetComponent<Unit>();

        //add all moves to player and enemy decks

        for (int i = 0; i < playerUnit1.moveDeck.Length; i++) {
            partyFullDeck[partyCounter] = playerUnit1.moveDeck[i];
            partyCounter++;
        }
        for (int i = 0; i < playerUnit2.moveDeck.Length; i++) {
            partyFullDeck[(i+partyCounter)] = playerUnit2.moveDeck[i];
            partyCounter++;
        }
        for (int i = 0; i < playerUnit3.moveDeck.Length; i++) {
            partyFullDeck[(i+partyCounter)] = playerUnit3.moveDeck[i];
            partyCounter++;
        }
        for (int i = 0; i < playerUnit4.moveDeck.Length; i++) {
            partyFullDeck[(i+partyCounter)] = playerUnit4.moveDeck[i];
            partyCounter++;
        }

        for (int i = 0; i < enemyUnit1.moveDeck.Length; i++) {
            enemyFullDeck[enemyCounter] = enemyUnit1.moveDeck[i];
            enemyCounter++;
        }

        for (int i = 0; i < enemyUnit2.moveDeck.Length; i++)
        {
            enemyFullDeck[enemyCounter] = enemyUnit2.moveDeck[i];
            enemyCounter++;
        }

        for (int i = 0; i < enemyUnit3.moveDeck.Length; i++)
        {
            enemyFullDeck[enemyCounter] = enemyUnit3.moveDeck[i];
            enemyCounter++;
        }

        for (int i = 0; i < enemyUnit4.moveDeck.Length; i++)
        {
            enemyFullDeck[enemyCounter] = enemyUnit4.moveDeck[i];
            enemyCounter++;
        }
        // foreach(Move mov in playerUnit1.moveDeck) {
        //     partyFullDeck[partyCounter] = mov;
        //     partyCounter++;
        //     //partyDeck.Push(mov);
        // }
        // foreach(Move mov in playerUnit2.moveDeck) {
        //     partyFullDeck[partyCounter] = mov;
        //     partyCounter++;
        //     //partyDeck.Push(mov);
        // }
        // foreach(Move mov in playerUnit3.moveDeck) {
        //     partyFullDeck[partyCounter] = mov;
        //     partyCounter++;
        //     //partyDeck.Push(mov);
        // }
        // foreach(Move mov in playerUnit4.moveDeck) {
        //     partyFullDeck[partyCounter] = mov;
        //     partyCounter++;
        //     //partyDeck.Push(mov);
        // }
        // foreach(Move mov in enemyUnit.moveDeck) {
        //     enemyFullDeck[enemyCounter] = mov;
        //     enemyCounter++;
        //     //partyDeck.Push(mov);
        // }

        //fill stacks
        //partyDeck = partyDeck.OrderBy(x => rnd.Next());
        //enemyDeck = enemyDeck.OrderBy(x => rnd.Next());


        //dialogueText.text = "Encountered a " + enemyUnit.unitName + ", prepare for battle!";
        // May change up some more of the code here to get the data to show
        // may even just have set up 
        Debug.Log("Right before playerhud set up");
        playerHUD.setHUD(playerUnit1, playerUnit2, playerUnit3, playerUnit4); // refer to the PlayerCharStatusBar
        enemyHUD.setHUD(enemyUnit1, enemyUnit2, enemyUnit3, enemyUnit4); // refer to the EnemyStatus Bar

        yield return new WaitForSeconds(3f);

        //Change to Player Phase
        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    //move inside code to damageCalc(), this method is just to select moves.
    //for move select, do not allow dead character moves to be selected.
    IEnumerator PlayerAttack()
    {
        //pop first 5 moves off stack into turn array
        for (int i = 0; i < partyTurn.Length; i++) {
            partyTurn[i] = (Move) partyDeck.Pop();
        }

        //insert method to display moves in UI here!

        //check to see if stack is empty - if yes, restock w shuffled array
        if(partyDeck.Count == 0) {
            Debug.Log("Stack empty, reshuffling!");
            //restock code here
        }

        //code to move starts here!
        float advantage = 1;
        float critDamage = 0;

        //check to see if move has attribute advantage

        //check to see if critical hit lands

        //actual damage calculation
        int damage = (int)((playerUnit1.power * advantage + critDamage) - enemyUnit1.defense);

        //prevent negative damage
        if (damage < 0) {
            damage = 0;
        }

        //apply damage
        bool isDead = enemyUnit1.TakeDamage(damage);

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
        dialogueText.text = enemyUnit1.unitName + " attacks!";

        yield return new WaitForSeconds(3f);

        float advantage = 1;
        float critDamage = 0;

        //actual damage calculation
        int damage = (int)((enemyUnit1.power * advantage + critDamage) - playerUnit1.defense);

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
