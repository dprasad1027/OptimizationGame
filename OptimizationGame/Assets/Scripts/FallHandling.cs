using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallHandling : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ResetLanded()
    {
        //Resets landed bool on animation event in Idle and Run animations
        animator.SetBool("landed", false);
    }

    public void IsFalling()
    {
        //Sets IsFalling bool on Jump and Falling animation events
        animator.SetBool("landed", false);
        animator.SetBool("falling", true);
        
    }

    public void Landed()
    {
        //Sets Landed bool on Landing animation event
        animator.SetBool("falling", false);
        animator.SetBool("jumped", false);        
        animator.SetBool("landed", true);
        
    }
}
