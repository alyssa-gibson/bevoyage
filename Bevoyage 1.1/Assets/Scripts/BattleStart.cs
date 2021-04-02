using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleStart : MonoBehaviour
{
    public GameObject BattleBG;
    public SpriteRenderer spriteRenderer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ProcessColission(collision.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ProcessColission(collision.gameObject);
    }

    void ProcessColission(GameObject collider) { 
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
