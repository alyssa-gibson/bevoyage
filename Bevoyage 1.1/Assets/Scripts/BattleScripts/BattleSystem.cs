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

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public Text dialogueText;

    Stack partyDeck, enemyDeck = new Stack(); //for card selection
    public Move[] partyFullDeck, enemyFullDeck = new Move[20];
    public Move[] partyTurn, enemyTurn = new Move[5]; //holds the cards for a specific turn
    //public LinkedList <Move> partyTurnList, enemyTurnList = new LinkedList <Move>(); //code's borked
    public Move[] partyTurnList, enemyTurnList = new Move[5];
    public Move[] selectedMoves = new Move[5]; //holds the cards that are selected
    int selectIndex = 0; // show how many cards are chosen

    int partyCounter, enemyCounter, partyGraveyard, enemyGraveyard = 0;

    float weightCap = 0;

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

    	GameObject enemyGO1 = Instantiate(enemyPrefab1, enemyStation1);
    	enemyUnit1 = enemyGO1.GetComponent<Unit>();
        GameObject enemyGO2 = Instantiate(enemyPrefab2, enemyStation2);
        enemyUnit2 = enemyGO2.GetComponent<Unit>();
        GameObject enemyGO3 = Instantiate(enemyPrefab3, enemyStation3);
        enemyUnit3 = enemyGO3.GetComponent<Unit>();
        GameObject enemyGO4 = Instantiate(enemyPrefab4, enemyStation4);
        enemyUnit4 = enemyGO4.GetComponent<Unit>();

        weightCap = weightCap = playerUnit1.weightCap + playerUnit2.weightCap + playerUnit3.weightCap + playerUnit4.weightCap;

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

        //shuffle stacks
        //partyDeck = partyDeck.OrderBy(x => rnd.Next());
        //enemyDeck = enemyDeck.OrderBy(x => rnd.Next());

        dialogueText.text = "Encountered enemies, prepare for battle!";
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

        yield return new WaitForSeconds(3f); // Need a return bc it's ienumerator but don't know what to return! -Emily

        //insert method to display moves in UI here!

        //check to see if stack is empty - if yes, restock w shuffled array
        if (partyDeck.Count == 0) {
            Debug.Log("Stack empty, reshuffling!");
            //restock code here
        }

        //insert method to select moves here!
        // ^- This would be the time to allow the user to choose their moves

        //player finishes move selection, move to enemy selection phase
        Debug.Log("Player move selection finished");
        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator EnemyTurn()
    {
        dialogueText.text = "Waiting for enemy move selection...";
        BackButtonSwitch(); // Incase the Player went first, switch menus

        yield return new WaitForSeconds(3f);

    	//insert enemy move array filling code here, similar to players

    	//selection finished, move to damage phase
    	Debug.Log("Enemy move selection finished");
    	state = BattleState.DAMAGE;
    	StartCoroutine(damageCalc());

    }

    IEnumerator damageCalc() {
    	Debug.Log("Made it to damage phase");
        BackButtonSwitch(); // Switch menus to see the field
        //toggle UI menus back to the main battle menu

        //compare lists, execute damage calculation in order, use unit functions

        /* TO DO:
    	 * 
    	 */
        for (int i = 0; i < 5; i++) {
    		float advantage = 1;
    		float critDamage = 0;
    		float critCheck;
    		bool isDead;

    		Move playerMove = partyTurnList[i];
    		Move enemyMove = enemyTurnList[i];
    		Unit attacker, defender;
    		if(playerMove.weight < enemyMove.weight) {
    			Debug.Log("Player moves first!");
    			//assign attacker to corresponding player
    			if(playerUnit1.unitName == playerMove.moveOwner) {
    				attacker = playerUnit1;
    			} else if(playerUnit2.unitName == playerMove.moveOwner) {
    				attacker = playerUnit2;
    			} else if(playerUnit3.unitName == playerMove.moveOwner) {
    				attacker = playerUnit3;
    			} else {
    				attacker = playerUnit4;
    			}

    			//assign defender to corresponding enemy
    			if(enemyUnit1.unitName == enemyMove.moveOwner) {
    				defender = enemyUnit1;
    			} else if(enemyUnit2.unitName == enemyMove.moveOwner) {
    				defender = enemyUnit2;
    			} else if(enemyUnit3.unitName == enemyMove.moveOwner) {
    				defender = enemyUnit3;
    			} else {
    				defender = enemyUnit4;
    			}
    		} else {
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

    			//assign defender to corresponding player
    			if(playerUnit1.unitName == playerMove.moveOwner) {
    				defender = playerUnit1;
    			} else if(playerUnit2.unitName == playerMove.moveOwner) {
    				defender = playerUnit2;
    			} else if(playerUnit3.unitName == playerMove.moveOwner) {
    				defender = playerUnit3;
    			} else {
    				defender = playerUnit4;
    			}
    		}
    		//if attacker is dead, don't calc damage
    		if(attacker.currentHP <= 0) {
    			dialogueText.text = attacker + "is incapacitated.";
    		} else {
    			//check to see if move has attribute advantage

		    	//check to see if critical hit lands for attacker
				critCheck = Random.Range(1,101);
				if(critCheck > (100 - attacker.critRate)) {
					critDamage = attacker.power * 1.5f; // f added to make it a float value
				}

				//actual damage calculation for attacker
		   		int damage = (int)((attacker.power * advantage + critDamage) - defender.defense);

		        //if damage is negative, set to 0 (no damage taken)
		        if (damage < 0) {
		            damage = 0;
		        }

		        //apply damage
		        isDead = defender.TakeDamage(damage);
		        if (damage == 0) {
		        	Debug.Log("No damage taken!");
		        }
		        if(playerMove.weight < enemyMove.weight) {
		        	enemyHUD.SetHP(defender.currentHP, defender.unitName);
	        	} else {
	        		playerHUD.SetHP(defender.currentHP, defender.unitName);
	        	}

	        	dialogueText.text = attacker.unitName + "attacks " + defender.unitName + "for " + damage + "!";

	        	yield return new WaitForSeconds(2f);

	        	//if dead, add to respective graveyard
	        	if(isDead) {
	        		dialogueText.text = defender.unitName + "is defeated!";
	        		if(defender.type == 'p') {
	        			partyGraveyard++;
	        		} else {
	        			enemyGraveyard++;
	        		}
	        	}
    		}

    		//defender's turn, same rules as attacker
    		if(attacker.currentHP == 0) {
                dialogueText.text = attacker.unitName + "is incapacitated.";
    		} else {
    			//check to see if move has attribute advantage

		    	//check to see if critical hit lands for attacker
				critCheck = Random.Range(1,101);
				if(critCheck > (100 - defender.critRate)) {
					critDamage = defender.power * 1.5f;
				}

				//actual damage calculation for attacker
		   		int damage = (int)((defender.power * advantage + critDamage) - attacker.defense);

		        //if damage is negative, set to 0 (no damage taken)
		        if (damage < 0) {
		            damage = 0;
		        }

		        //apply damage
		        isDead = defender.TakeDamage(damage);
		        if (damage == 0) {
		        	Debug.Log("No damage taken!");
		        }
		        if(playerMove.weight < enemyMove.weight) {
		        	playerHUD.SetHP(attacker.currentHP, attacker.unitName);
	        	} else {
	        		enemyHUD.SetHP(attacker.currentHP, attacker.unitName);
	        	}

	        	dialogueText.text = defender.unitName + "attacks " + attacker.unitName + "for " + damage + "!";

	        	yield return new WaitForSeconds(2f);

	        	//if dead, add to respective graveyard
	        	if(isDead) {
	        		dialogueText.text = attacker.unitName + "is defeated!";
	        		if(attacker.type == 'p') {
	        			partyGraveyard++;
	        		} else {
	        			enemyGraveyard++;
	        		}
	        	}
    		}
    	}
    	//check both parties to see if all are incapacitated, if so determine win/loss
		if(partyGraveyard == 4) {
			state = BattleState.LOST;
        	EndBattle();
		}

		if(enemyGraveyard == 4) {
			state = BattleState.WON;
        	EndBattle();
		}

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
        dialogueText.text = "Choose an action:";
    }

    public void OnAttackButton() {
        if (state != BattleState.PLAYERTURN) {
            return;
        } else {
            StartCoroutine(PlayerAttack());
        }
    }

    public void AttackSelect(Button button)
    {
        //calculate current weight of chosen moves
        /*
        int currentWeight = 0;
        for(int i=0; i<selectIndex; i++)
        {
            currentWeight = currentWeight + selectedMoves[i].weight;
        }
        */
        // check which move it was and see if it will fit the weightcap 
        if (button.name == "MoveChoice1") {
            Debug.Log("In if 1");
            /*
            if(currentWeight + partyTurn[0] >= weightCap){
                Debug.Log("Cannot Select!");
                break
            }
            */
            // selectedMoves[selectIndex] = partyTurn[0];
        }
        else if (button.name == "MoveChoice2"){
            Debug.Log("In if 2");
            /*
            if (currentWeight + partyTurn[1] >= weightCap){
                Debug.Log("Cannot Select!");
                break
            }
            */
            // selectedMoves[selectIndex] = partyTurn[1];
        }
        else if (button.name == "MoveChoice3"){
            Debug.Log("In if 3");
            /*
            if (currentWeight + partyTurn[2] >= weightCap){
                Debug.Log("Cannot Select!");
                break
            }
            */
            // selectedMoves[selectIndex] = partyTurn[2];
        }
        else if (button.name == "MoveChoice4"){
            Debug.Log("In if 4");
            /*
            if (currentWeight + partyTurn[3] >= weightCap){
                Debug.Log("Cannot Select!");
                break
            }
            */
            // selectedMoves[selectIndex] = partyTurn[3];
        }
        else
        {
            Debug.Log("In else 5");
            /*
            if (currentWeight + partyTurn[4] >= weightCap){
                Debug.Log("Cannot Select!");
                break
            }
            */
            // selectedMoves[selectIndex] = partyTurn[4];
        }

        button.interactable = false;
        selectIndex++;
        Debug.Log("Attack Selected " + button.name);
        Debug.Log("Number of moves selected:" + selectIndex);

    }

    public void ResetAttack()
    {
        moveSlot1.interactable = true;
        moveSlot2.interactable = true;
        moveSlot3.interactable = true;
        moveSlot4.interactable = true;
        moveSlot5.interactable = true;
        Debug.Log("Reset Clicked");
        //selectedMoves.clear()
        //selectedMoves = new Moves[5]
        selectIndex = 0;
    }
}
