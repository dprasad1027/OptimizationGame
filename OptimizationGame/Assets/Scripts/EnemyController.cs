using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameObject player;
    private GameObject enemy;
    private bool dropLoot;
    public float enemyHealth = 75.0f;
    public float moveSpeed = 1.0f;

    public GameObject groundCheck;
    public GameObject heightCheck;

    public LayerMask groundLayer;
    public LayerMask playerLayer;

    public float rayDist = 1.0f;
    public float sightDist = 2.0f;
    private Rigidbody2D rb;
    private float horizontalMove = 1.0f;

    public float attackRange = .5f;
    public float swordDamage;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        AssignPlayer();
        enemy = this.gameObject;
        dropLoot = (Random.value > 0.5f);
        
        rb.gravityScale = 10.0f;
    }

    public void AssignPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    


    // Update is called once per frame
    void Update()
    {
        //Various Raycasts
        FloorCheck();
        HeightCheck();
        AttackRange();
        EnemySight();



        animator.SetFloat("velocityX", Mathf.Abs(horizontalMove));
    }


    private void FixedUpdate()
    {
        rb.velocity = new Vector2(Mathf.Clamp(horizontalMove,-4,4), 0);
    }

    private void AttackRange()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * enemy.transform.localScale.x, attackRange, playerLayer);
       // Debug.DrawRay(transform.position, Vector2.right * attackRange * enemy.transform.localScale.x, Color.blue);

        if(hit.collider != null)
        {
            moveSpeed = 0;
            horizontalMove *= moveSpeed;
            animator.SetBool("attack", true);
        }
        else
        {
            horizontalMove = enemy.transform.localScale.x;
            animator.SetBool("attack", false);
        }
    }

    private void EnemySight()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * enemy.transform.localScale.x, sightDist, playerLayer);
       // Debug.DrawRay(transform.position, Vector2.right * sightDist * enemy.transform.localScale.x, Color.yellow);
        if(hit.collider != null)
        {
            Debug.Log("hit player");
            moveSpeed = 2.0f;
            horizontalMove *= moveSpeed;
        }
        else
        {
            moveSpeed = 1;
            
        }

        
    }

    public void DealDamage()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * enemy.transform.localScale.x, attackRange, playerLayer);
        Debug.DrawRay(transform.position, Vector2.right * attackRange * enemy.transform.localScale.x, Color.green);
        
        if(hit.collider != null && CheckPlayerHP())
        {
            //make sure entered value is negative to deal damage.
            swordDamage = Mathf.Abs(swordDamage) * -1;

            player.GetComponent<CharacterController>().HandleHealth(swordDamage);
        }
    }

    public void HandleDamage(float value)
    {
        enemyHealth -= value;

        if(enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private bool CheckPlayerHP()
    {
        float playerHP = player.GetComponent<CharacterController>().playerHealth;

        //Returns true if damagable
        if(playerHP > 0)
        {
            return true;
        }
        return false;
    }

    private void FloorCheck()
    {
        //Raycast check if the floor continues or not
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.transform.position, -Vector2.up, rayDist, groundLayer);
        Debug.DrawRay(groundCheck.transform.position, -Vector2.up * rayDist, Color.green);

        if(hit.collider == null)
        {
            
            enemy.transform.localScale = new Vector3((enemy.transform.localScale.x * -1), 1, 1);
            horizontalMove = enemy.transform.localScale.x * moveSpeed;
        }


    }

    private void HeightCheck()
    {
        //Check if running into environment 
        RaycastHit2D hit = Physics2D.Raycast(heightCheck.transform.position, Vector2.right * enemy.transform.localScale.x, rayDist * .25f, groundLayer);
        Debug.DrawRay(heightCheck.transform.position, Vector2.right * .25f * enemy.transform.localScale.x, Color.red);

        if (hit.collider != null)
        {
            //turn around if floor ends
            enemy.transform.localScale = new Vector3((enemy.transform.localScale.x * -1), 1, 1);
            horizontalMove = enemy.transform.localScale.x * moveSpeed;

        }



    }
}
