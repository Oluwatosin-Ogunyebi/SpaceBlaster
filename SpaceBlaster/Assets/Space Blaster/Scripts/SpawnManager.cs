using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyprefab;
    public GameObject[] powerups;
    public float spawntime = 5.0f;
    private GameHandler _gameHandler;
    

    public void Start()
    {
        _gameHandler = GameObject.Find("GameManager").GetComponent<GameHandler>();
        StartCoroutine(SpawnThings()); 
        StartCoroutine("PowerupspawnRoutine");
    }
    public void StartRoutines()
    {
        StartCoroutine(SpawnThings());
        StartCoroutine("PowerupspawnRoutine");
    }

    public IEnumerator SpawnThings()
    {
        while (_gameHandler.gameOver == false)
        {
            Instantiate(enemyprefab, new Vector3(Random.Range(-7.7f,7.7f), 6.14f, 0f), Quaternion.identity);
            yield return new WaitForSeconds(spawntime);
        }
       
       
    }
    public IEnumerator PowerupspawnRoutine()
    {
        while (_gameHandler.gameOver == false)
        {
            int randompowerup = Random.Range(0, 3);
            Instantiate(powerups[randompowerup], new Vector3(Random.Range(-7.7f, 7.7f), 6.14f, 0f), Quaternion.identity);
            yield return new WaitForSeconds(5);
        }

    }
}
