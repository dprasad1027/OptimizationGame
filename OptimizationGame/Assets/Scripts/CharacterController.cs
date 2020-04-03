using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    private GameObject player;
    private Rigidbody2D rb;

    private float horizontalMove = 0.0f;
    private float verticalMove = 0.0f;

    public Animator animator;
    public SpriteRenderer spriteRenderer;  

    public GameObject groundCheck;  

    public LayerMask groundLayer;

    public float rayDist = .03f;

    public float moveSpeed = 10.0f;

    public float fallSpeed = 10.0f;

    private float initialFallSpeed;

    public float jumpForce = 2000.0f;

    private float fallTimer = 0.0f;

    private bool jumped = false;

    public float playerHealth = 100.0f;

    private float maxHealth;

    public bool isDead = false;

    private GameObject gameManager;
    
    void Start()
    {
        //Get basic values
        player = this.gameObject;
        rb = GetComponent<Rigidbody2D>();
        initialFallSpeed = fallSpeed;
        maxHealth = playerHealth;
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        
        //Handle Jumping
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Force);
            jumped = true;
            animator.SetBool("jumped", jumped);
            Debug.Log("jumped2: " + animator.GetBool("jumped"));
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Force);
        }

        if(Input.GetButtonDown("Attack"))
        {
            animator.SetBool("attack", true);
        }

        //Horizontal Movement
        horizontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed;            
        animator.SetFloat("velocityX", Mathf.Abs(horizontalMove));

        //Set grounded in animator to transition between Idle and Running
        animator.SetBool("grounded", IsGrounded());

        Debug.Log(playerHealth);
        
       

    }

    public void HandleHealth(float value)
    {
        Debug.Log(value);

        playerHealth += value;

        gameManager.GetComponent<UIManager>().UpdateHPSlider();

        //Check if dead
        if(playerHealth <= 0)
        {
            isDead = true;
            Destroy(gameObject);
        }
        else
        {
            isDead = false;
        }
    }

    private void OnDestroy()
    {
        gameManager.GetComponent<GameManager>().RespawnPlayer();
    }

    

    bool IsGrounded()
    {
        //Raycast check for ground
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.transform.position, -Vector2.up, rayDist , groundLayer);
        Debug.DrawRay(groundCheck.transform.position, -Vector2.up * rayDist, Color.green);

        if (hit.collider != null)
        {
            //Return true if hit ground

            //Reset fallTimer and fall speed
            fallTimer = 0.0f;
            fallSpeed = initialFallSpeed;
            animator.SetBool("falling", false);
            return true;
        }
        
        //Increase falltimer while falling
        fallTimer += Time.deltaTime;

        //Increase fallspeed based on fall Timer
        fallSpeed += fallTimer/3;

        return false;
    }

    private void FixedUpdate()
    {        
        if (IsGrounded())
        {
            //Normal gravity while on ground
            rb.gravityScale = 1f;



            //Reset jump parameter in animation to false
            animator.SetBool("jumped", false);

            //Horizontal movement
            rb.velocity = new Vector2(horizontalMove, 0);

            //Flip player depending on which way the player moves
            if (horizontalMove < 0)
            {
                player.transform.localScale = new Vector3(-1, 1, 1);
                
            }
            else if (horizontalMove > 0)
            {
                player.transform.localScale = new Vector3(1, 1, 1);
                
            }            
        }
        else
        {
            //Clamp fall speed
            

            //Gravity while falling
            rb.gravityScale = Mathf.Clamp(fallSpeed,1,25);

            if (!jumped)
            {
                animator.SetBool("falling", true); 
            }
            
            //Restrict horizontal movement while in the air by 25%
            rb.velocity = new Vector2(horizontalMove * .75f, verticalMove);

            if (horizontalMove < 0)
            {
                player.transform.localScale = new Vector3(-1, 1, 1);
                
            }
            else if (horizontalMove > 0)
            {
                player.transform.localScale = new Vector3(1, 1, 1);
                
            }
            
        }

    }
}
