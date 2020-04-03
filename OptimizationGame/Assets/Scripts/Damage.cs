using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public float damage = 25.0f;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && tag.Equals("Enemy"))
        {
            other.gameObject.GetComponent<CharacterController>().playerHealth -= damage;
        }else if(other.gameObject.CompareTag("Enemy") && tag.Equals("Player"))
        {
            other.gameObject.GetComponent<EnemyController>().enemyHealth -= damage;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
