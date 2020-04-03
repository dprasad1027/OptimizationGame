using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private GameObject player;

    public Slider hpSlider;

    public Text goalText;

    private float restartTimer = 2.0f;
    private int safetyCount = 0;
    bool beginRestart = false;
    // Start is called before the first frame update
    void Start()
    {
        GetPlayer();
        goalText.gameObject.SetActive(false);
    }

    public void GetPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void ActivateGoalText()
    {
        goalText.gameObject.SetActive(true);
        beginRestart = true;
        
    }

   

    public void UpdateHPSlider()
    {
       GetPlayer();   
        
       hpSlider.value = player.GetComponent<CharacterController>().playerHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (beginRestart)
        {
            restartTimer -= Time.deltaTime;
        }   

        if(restartTimer <= 0)
        {
            safetyCount++;
            if(safetyCount == 1)
            {
                SceneManager.LoadScene(0);
            }
            
        }
    }
}
