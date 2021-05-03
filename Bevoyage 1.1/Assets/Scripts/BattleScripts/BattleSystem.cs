using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, DAMAGE, WON, LOST }

/* Future Notes: 
 * Refer to pseudocode sheet for game-specific mechanics.
 * Assign UI elements to respective variables.
 */


public class BattleSystem : MonoBehaviour
{
	public GameObject playerPrefab1;
    public GameObject playerPrefab2;
    public GameObject playerPrefab3;
    public GameObject playerPrefab4;
	public GameObject enemyPrefab1;
    public GameObject enemyPrefab2;
    public GameObject enemyPrefab3;
    public GameObject enemyPrefab4;

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

    public Button moveSlot1;
    public Button moveSlot2;
    public Button moveSlot3;
    public Button moveSlot4; 
    public Button moveSlot5;

    public BattleState state;

    public GameObject BattleUI;
    public GameObject AttackUI;

    public MoveDisplay AttackChoice;

    public BattleHUD battleStatusBar;
    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public Text dialogueText;

    Stack partyDeck = new Stack(); //for card selection
    Stack enemyDeck = new Stack();
    private Move[] partyFullDeck = new Move[20];
    private Move[] enemyFullDeck = new Move[20];
    private Move[] partyTurn = new Move[5]; //holds the cards for a specific turn
    private Move[] enemyTurn = new Move[5];
    //public LinkedList <Move> partySelectedMoves, enemySelectedMoves = new LinkedList <Move>(); //code's borked
    private Move[] enemySelectedMoves = new Move[5];
    private Move[] partySelectedMoves = new Move[5]; //holds the cards that are selected
    int selectIndex = 0; // show how many cards are chosen

    int partyCounter, enemyCounter = 0;
    private int[] partyGraveyard = new int[4]{ 0, 0, 0, 0}; // dead = 1 | alive = 0
    private int[] enemyGraveyard = new int[4]{ 0, 0, 0, 0};

    float weightCap = 0;
    float weightCapEnemy = 0;
    float currentWeight = 0;

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

    IEnumerator SetUpBattle(){ 
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

    	GameObject enemyGO1 = Instantiate(enemyPrefab1, enemyStation1);
    	enemyUnit1 = enemyGO1.GetComponent<Unit>();
        GameObject enemyGO2 = Instantiate(enemyPrefab2, enemyStation2);
        enemyUnit2 = enemyGO2.GetComponent<Unit>();
        GameObject enemyGO3 = Instantiate(enemyPrefab3, enemyStation3);
        enemyUnit3 = enemyGO3.GetComponent<Unit>();
        GameObject enemyGO4 = Instantiate(enemyPrefab4, enemyStation4);
        enemyUnit4 = enemyGO4.GetComponent<Unit>();

        weightCap = playerUnit1.weightCap + playerUnit2.weightCap + playerUnit3.weightCap + playerUnit4.weightCap;
        weightCapEnemy = enemyUnit1.weightCap + enemyUnit2.weightCap + enemyUnit3.weightCap + enemyUnit4.weightCap;
        
        AttackChoice.moveBarSetup(weightCap, currentWeight);
        //add all moves to player and enemy decks
        buildPlayerDeck();
        buildEnemyDeck();

        dialogueText.text = "Encountered enemies, prepare for battle!";
        // May change up some more of the code here to get the data to show
        // may even just have set up 
        battleStatusBar.setHUD(playerUnit1, playerUnit2, playerUnit3, playerUnit4); // refer to the PlayerCharStatusBar
        playerHUD.setHUD(playerUnit1, playerUnit2, playerUnit3, playerUnit4); // refer to the PlayerCharStatusBar
        enemyHUD.setHUD(enemyUnit1, enemyUnit2, enemyUnit3, enemyUnit4); // refer to the EnemyStatus Bar

        yield return new WaitForSeconds(3f);

        //Change to Player Phase
        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    public void buildPlayerDeck()
    {
        partyCounter = 0;
        foreach (Move mov in playerUnit1.moveDeck)
        {
            partyFullDeck[partyCounter] = mov;
            partyCounter++;
            partyDeck.Push(mov);
        }
        foreach (Move mov in playerUnit2.moveDeck)
        {
            partyFullDeck[partyCounter] = mov;
            partyCounter++;
            partyDeck.Push(mov);
        }
        foreach (Move mov in playerUnit3.moveDeck)
        {
            partyFullDeck[partyCounter] = mov;
            partyCounter++;
            partyDeck.Push(mov);
        }
        foreach (Move mov in playerUnit4.moveDeck)
        {
            partyFullDeck[partyCounter] = mov;
            partyCounter++;
            partyDeck.Push(mov);
        }
        //shuffle stacks
        //partyDeck = partyDeck.OrderBy(x => rnd.Next());
    }

    public void buildEnemyDeck()
    {
        enemyCounter = 0;
        foreach (Move mov in enemyUnit1.moveDeck)
        {
            enemyFullDeck[enemyCounter] = mov;
            enemyCounter++;
            enemyDeck.Push(mov);
        }
        foreach (Move mov in enemyUnit2.moveDeck)
        {
            enemyFullDeck[enemyCounter] = mov;
            enemyCounter++;
            enemyDeck.Push(mov);
        }
        foreach (Move mov in enemyUnit3.moveDeck)
        {
            enemyFullDeck[enemyCounter] = mov;
            enemyCounter++;
            enemyDeck.Push(mov);
        }
        foreach (Move mov in enemyUnit4.moveDeck)
        {
            enemyFullDeck[enemyCounter] = mov;
            enemyCounter++;
            enemyDeck.Push(mov);
        }
        //shuffle stacks
        //enemyDeck = enemyDeck.OrderBy(x => rnd.Next());
    }

    //move inside code to damageCalc(), this method is just to select moves.
    //for move select, do not allow dead character moves to be selected.
    IEnumerator PlayerAttack()
    {
        //check to see if stack is empty - if yes, restock w shuffled array
        if (partyDeck.Count == 0)
        {
            Debug.Log("Stack empty, reshuffling!");
            buildPlayerDeck();
        }
        //pop first 5 moves off stack into turn array
        for (int i = 0; i < partyTurn.Length; i++) {
            partyTurn[i] = (Move) partyDeck.Pop();
        }

        // Display Moves
        AttackChoice.setMoveDisplay(partyTurn);
        
        //restock code here
        yield return new WaitForSeconds(2f); // Need a return bc it's ienumerator, may change 
    }

    IEnumerator EnemyTurn()
    {
        BackButtonSwitch(); // Incase the Player went first, switch menus
        dialogueText.text = "Waiting for enemy move selection...";
        currentWeight = 0;
        selectIndex = 0;
        yield return new WaitForSeconds(3f);

        //check to see if stack is empty - if yes, restock w shuffled array
        if (enemyDeck.Count == 0)
        {
            Debug.Log("Stack empty, reshuffling!");
            buildEnemyDeck();
        }

        //pop first 5 moves off stack into turn array
        for (int i = 0; i < enemyTurn.Length; i++){
            enemyTurn[i] = (Move)enemyDeck.Pop();
        }

        // move through elements and pull their weights, see if they fit in weightCapEnemy
        for (int i = 0; i < enemyTurn.Length; i++){
            if( (currentWeight + enemyTurn[i].weight) < weightCapEnemy){
                currentWeight += enemyTurn[i].weight;
                enemySelectedMoves[selectIndex] = enemyTurn[i];
                selectIndex++;
            }
        }

        //selection finished, move to damage phase
        Debug.Log("Enemy move selection finished");
    	state = BattleState.DAMAGE;
    	StartCoroutine(execOrder());

    }

    IEnumerator execOrder() {
    	Debug.Log("Made it to damage phase");

        //compare lists, execute damage calculation in order, use unit functions

        /* TO DO:
    	 * we need a medic
    	 */
        for (int i = 0; i < 5; i++) {

    		Move playerMove = partySelectedMoves[i];
    		Move enemyMove = enemySelectedMoves[i];
            //Debug.Log(playerMove);
            //Debug.Log(enemyMove);
            Unit attacker = null;
            Unit defender = null;

            if(playerMove == null && enemyMove == null){break;}

            if (enemyMove == null || playerMove.weight < enemyMove.weight) {
    			Debug.Log("Player moves first!");
                //assign attacker to corresponding player
                if(playerMove != null) {
                    if (playerUnit1.unitName == playerMove.moveOwner) {
    				    attacker = playerUnit1;
    			    } else if(playerUnit2.unitName == playerMove.moveOwner) {
    				    attacker = playerUnit2;
    			    } else if(playerUnit3.unitName == playerMove.moveOwner) {
    				    attacker = playerUnit3;
    			    } else {
    				    attacker = playerUnit4;
    			    }

                    if (enemyMove == null){
                        int theChosen = Random.Range(0, 4);
                        while(enemyGraveyard[theChosen] != 0){
                            theChosen = Random.Range(0, 4);
                        }
                        if(theChosen == 0) { defender = enemyUnit1; }
                        else if(theChosen == 1) { defender = enemyUnit2; }
                        else if(theChosen == 2) { defender = enemyUnit3; }
                        else { defender = enemyUnit4; }
                    }
                    else { 
                        //assign defender to corresponding enemy
                        if (enemyUnit1.unitName == enemyMove.moveOwner) {
    				        defender = enemyUnit1;
    			        } else if(enemyUnit2.unitName == enemyMove.moveOwner) {
    				        defender = enemyUnit2;
    			        } else if(enemyUnit3.unitName == enemyMove.moveOwner) {
    				        defender = enemyUnit3;
    			        } else {
    				        defender = enemyUnit4;
    			        }
                    }
                    //do damage
                    if(playerMove != null) { 
                        damageCalc(attacker, defender, playerMove);
                        yield return new WaitForSeconds(3f);
                    }
                    if (enemyMove != null) { 
                        damageCalc(defender, attacker, enemyMove);
                        Debug.Log("attacker: " + attacker.unitName +" HP: " + attacker.currentHP);
                        battleStatusBar.SetHP(attacker.currentHP, attacker.unitName);
                        yield return new WaitForSeconds(3f);
                    }
                    //adjust HUDs
                    playerHUD.SetHP(attacker.currentHP, attacker.unitName);
                    enemyHUD.SetHP(defender.currentHP, defender.unitName);
                }
            } else {
                if (enemyMove != null) { 
                    Debug.Log("Enemy moves first!");
    			    //assign attacker to corresponding enemy
    			    if(enemyUnit1.unitName == enemyMove.moveOwner) {
    				    attacker = enemyUnit1;
    			    } else if(enemyUnit2.unitName == enemyMove.moveOwner) {
    				    attacker = enemyUnit2;
    			    } else if(enemyUnit3.unitName == enemyMove.moveOwner) {
    				    attacker = enemyUnit3;
    			    } else {
    				    attacker = enemyUnit4;
    			    }

                    if (playerMove == null){
                        int theChosen = Random.Range(0, 4);
                        while (enemyGraveyard[theChosen] != 0){
                            theChosen = Random.Range(0, 4);
                        }
                        if (theChosen == 0) { defender = playerUnit1; }
                        else if (theChosen == 1) { defender = playerUnit2; }
                        else if (theChosen == 2) { defender = playerUnit3; }
                        else { defender = playerUnit4; }
                    }
                    else { 
                        //assign defender to corresponding player
                        if (playerUnit1.unitName == playerMove.moveOwner) {
    				        defender = playerUnit1;
    			        } else if(playerUnit2.unitName == playerMove.moveOwner) {
    				        defender = playerUnit2;
    			        } else if(playerUnit3.unitName == playerMove.moveOwner) {
    				        defender = playerUnit3;
    			        } else {
    				        defender = playerUnit4;
    			        }
                    }
                    //do damage
                    if(enemyMove != null) { 
                        damageCalc(attacker, defender, enemyMove);
                        Debug.Log("defender: " + defender.unitName + " HP: " + defender.currentHP);
                        battleStatusBar.SetHP(defender.currentHP, defender.unitName);
                        yield return new WaitForSeconds(3f);
                    }
                    if(playerMove != null) { 
                        damageCalc(defender, attacker, playerMove);
                        yield return new WaitForSeconds(3f);
                    }
                    //adjust HUDs
                    enemyHUD.SetHP(attacker.currentHP, attacker.unitName);
                    playerHUD.SetHP(defender.currentHP, defender.unitName);
                    //battleStatusBar.SetHP(defender.currentHP, defender.unitName);
                }
            }
    	}
        //check both parties to see if all are incapacitated, if so determine win/loss
        bool partyDeathFlag = true;
		for(int i = 0; i< partyGraveyard.Length; i++){
            if(partyGraveyard[i] == 0){
                partyDeathFlag = false;
            }
        }
        if(partyDeathFlag == true) {
			state = BattleState.LOST;
        	EndBattle();
		}

        bool enemyDeathFlag = true;
        for (int i = 0; i < enemyGraveyard.Length; i++)
        {
            if (enemyGraveyard[i] == 0)
            {
                enemyDeathFlag = false;
            }
        }
        if (enemyDeathFlag == true) {
			state = BattleState.WON;
        	EndBattle();
		}

        yield return new WaitForSeconds(3f);
        //if both parties aren't dead, continue back to player turn
        state = BattleState.PLAYERTURN;
		PlayerTurn();
        
    }

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
        currentWeight = 0;
        selectIndex = 0;
        AttackChoice.moveBarSet(0);
        StartCoroutine(PlayerAttack());
        dialogueText.text = "Choose an action:";
    }

    public void OnAttackButton() {
        moveSlot1.interactable = true;
        moveSlot2.interactable = true;
        moveSlot3.interactable = true;
        moveSlot4.interactable = true;
        moveSlot5.interactable = true;
        if (state != BattleState.PLAYERTURN) {
            return;
        } else {
            Debug.Log("Player move selection finished");
            StartCoroutine(EnemyTurn());
        }
    }

    public void CalculateWeight(Move chosenMove, float currentWeight, Button button)
    {
        if (currentWeight + chosenMove.weight > weightCap)
        {
            Debug.Log("Cannot Select!");
        }
        else {
            partySelectedMoves[selectIndex] = chosenMove;
            AttackChoice.moveBarSet(currentWeight + chosenMove.weight);
            button.interactable = false;
            selectIndex++;
        }
    }

    public void damageCalc(Unit attacker, Unit defender, Move attackerMove) {
        double advantage = 1.0;
        double moveDamage = 0;
        float critCheck;
        bool isDead;
        int damage;
        //if attacker is dead, don't calc damage
        if (attacker.currentHP <= 0) {
            dialogueText.text = attacker + " is incapacitated.";
        } else {
            //set move damage to base move power
            moveDamage = attackerMove.power;

            //check to see if unit has attribute advantage
            advantage = attackerMove.isEffective(defender.attribute);

            //check to see if critical hit lands for attacker
            critCheck = Random.Range(1,101);
            if(critCheck > (100 - attacker.critRate)) {
                moveDamage += moveDamage * 1.5d; // convert to double
            }

            //actual damage calculation for attacker
            damage = (int)(((attacker.power * advantage + moveDamage) - defender.defense)/attackerMove.hitCount);

            //if damage is negative, set to 0 (no damage taken)
            if (damage < 0) {
                damage = 0;
            }    

            //apply damage
            isDead = defender.TakeDamage(damage);
            Debug.Log("Name: "+defender.unitName + " HP: " + defender.currentHP);
            if (damage == 0) {
                Debug.Log("No damage taken!");
                return;
            }

            dialogueText.text = attacker.unitName + " attacks " + defender.unitName + " for " + damage + "!";
            if(isDead) {
                dialogueText.text = defender.unitName + " is defeated!";
                if(defender.type == 'p') {
                    if(defender.unitName == playerUnit1.unitName){partyGraveyard[0] = 1;}
                    else if (defender.unitName == playerUnit2.unitName){partyGraveyard[1] = 1;}
                    else if (defender.unitName == playerUnit3.unitName){ partyGraveyard[2] = 1;}
                    else{partyGraveyard[3] = 1;}
                } 
                else {
                    if (defender.unitName == enemyUnit1.unitName){enemyGraveyard[0] = 1;}
                    else if (defender.unitName == enemyUnit2.unitName){enemyGraveyard[1] = 1;}
                    else if (defender.unitName == enemyUnit3.unitName){enemyGraveyard[2] = 1;}
                    else{enemyGraveyard[3] = 1;}
                }
            }
        }
    }

    public void AttackSelect(Button button)
    {
        currentWeight = 0;
        //calculate current weight of chosen moves
        for (int i=0; i<selectIndex; i++){
            currentWeight = currentWeight + partySelectedMoves[i].weight;
        }
        // check which move it was and see if it will fit the weightcap 
        if (button.name == "MoveChoice1") {
            CalculateWeight(partyTurn[0], currentWeight, button);
        }
        else if (button.name == "MoveChoice2"){
            CalculateWeight(partyTurn[1], currentWeight, button);
        }
        else if (button.name == "MoveChoice3"){
            CalculateWeight(partyTurn[2], currentWeight, button);
        }
        else if (button.name == "MoveChoice4"){
            CalculateWeight(partyTurn[3], currentWeight, button);
        }
        else{
            CalculateWeight(partyTurn[4], currentWeight, button);
        }
    }

    public void ResetAttack(){
        moveSlot1.interactable = true;
        moveSlot2.interactable = true;
        moveSlot3.interactable = true;
        moveSlot4.interactable = true;
        moveSlot5.interactable = true;
        AttackChoice.moveBarSet(0);
        currentWeight = 0;
        partySelectedMoves = new Move[5];
        selectIndex = 0;
    }
}
