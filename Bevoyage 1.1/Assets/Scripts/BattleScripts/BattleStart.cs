using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleStart : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ProcessCollision(collision.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ProcessCollision(collision.gameObject);
    }

    void ProcessCollision(GameObject collider) { 
        if (collider.CompareTag("Enemy"))
        {
            AttackPlayer();
        }
    }

    void AttackPlayer()
    {
        Debug.Log("Attack!");
        SceneManager.LoadScene("BattleScene");
    }
}  
