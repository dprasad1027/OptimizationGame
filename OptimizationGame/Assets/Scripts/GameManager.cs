using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerSpawn;
    public GameObject player;
    public GameObject mainCamera;

    private GameObject[] enemies;

    private GameObject player_inst;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void RespawnPlayer()
    {
        player_inst = Instantiate(player, playerSpawn.transform);
        player_inst.transform.localPosition = new Vector3(0, 0, 1);
        mainCamera.GetComponent<CameraFollow>().AssignCamera();
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for(int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<EnemyController>().AssignPlayer();
        }

        gameObject.GetComponent<UIManager>().UpdateHPSlider();        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
