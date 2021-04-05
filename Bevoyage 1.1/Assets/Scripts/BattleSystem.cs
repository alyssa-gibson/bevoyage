using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

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
        SetUpBattle();
    }

    void SetUpBattle() {
    	GameObject playerGO = Instantiate(playerPrefab, playerStation);
    	playerUnit = playerGO.GetComponent<Unit>();
    	GameObject enemyGO = Instantiate(enemyPrefab, enemyStation);
    	enemyUnit = enemyGO.GetComponent<Unit>();
    }

}
