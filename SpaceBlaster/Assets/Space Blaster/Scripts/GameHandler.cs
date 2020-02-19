using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public bool gameOver = true;
    public GameObject player;
    private UIManager _uiManager;

    

    private void Start()
    {
        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();

    }
    void Update()
    {
        if (gameOver == true)
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(player, Vector3.zero, Quaternion.identity);
                gameOver = false;
                _uiManager.OfftitleScreen();
                
                 
            }
        }
    }
   
}
