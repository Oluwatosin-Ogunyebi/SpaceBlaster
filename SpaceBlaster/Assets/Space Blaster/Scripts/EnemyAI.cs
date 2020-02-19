using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    public GameObject enemyExplosion;
    private UIManager _uimanager;
    [SerializeField]
    private AudioClip _audioClip;
    void Start()
    {
        _uimanager = GameObject.Find("UIManager").GetComponent<UIManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if(transform.position.y < -6.1f)
        {
            float randomX = Random.Range(-7.6f, 7.6f);
            float randomY = Random.Range(6.0f, 8.0f);
            transform.position = new Vector3(randomX,randomY , 0f);
        }

      
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.name);
        if (other.tag == "Player")
        {

            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.PlayerDeath();
                Debug.Log("Player Health"+ player.playerHealth);
                
                EnemyDeath();
                player.isShieldUp = false;
            }
        }
        else if (other.tag == "Bullet")
        {
            Laser laser = other.GetComponent<Laser>();
            if (laser != null)
            {
                _uimanager.UpdateScore();
                laser.DestroyLaser();
                
                EnemyDeath();
            }
        }


    }
    public void EnemyDeath()
                
    {
        AudioSource.PlayClipAtPoint(_audioClip, Camera.main.transform.position);
        Destroy(this.gameObject);
        Instantiate(enemyExplosion, transform.position, Quaternion.identity);
    }

}
