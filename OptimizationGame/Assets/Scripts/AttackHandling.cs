using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHandling : MonoBehaviour
{

    Animator animator;
    private float attackRange = .5f;
    private GameObject player;
    public LayerMask enemyLayer;
    public float attackDamage = 25.0f;
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, Vector2.right * attackRange * player.transform.localScale.x, Color.green);
    }

    public void DealDamage()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * player.transform.localScale.x, attackRange, enemyLayer);
        

        if(hit.collider != null)
        {
            Debug.Log("hit enemy");
            hit.transform.gameObject.GetComponent<EnemyController>().HandleDamage(attackDamage);
        }
    }

    public void EndAttack()
    {
        animator.SetBool("attack", false);
    }
}
