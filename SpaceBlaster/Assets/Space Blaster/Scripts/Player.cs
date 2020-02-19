using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject explodePlayerPrefab;
    public int playerHealth = 3;
    public GameObject visualShield;
    [SerializeField]
    private float _fireRate = 0.25f;

    private float _nextFire = 0.0f;

    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private float _speed = 5.0f;

    [SerializeField]
    private GameObject[] _engines;
    public bool isShieldUp = false;

    public bool tripleShot = false;
    public bool isSpeedUP = false;
    public GameObject _tripleShotPrefab;

    private UIManager _uimanager;
    private GameHandler _gameHandler;
    private SpawnManager _spawnManager;
    private AudioSource _audioSource;
    private int hitCount = 0;
    
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _uimanager = GameObject.Find("UIManager").GetComponent<UIManager>();
        _gameHandler = GameObject.Find("GameManager").GetComponent<GameHandler>();
        _spawnManager = GameObject.Find("Spwan_Manager").GetComponent<SpawnManager>();
        _audioSource = GetComponent<AudioSource>();

        if(_uimanager!= null)
        {
            _uimanager.UpdateLives(playerHealth);
        }
        if(_spawnManager != null)
        {
            _spawnManager.StartRoutines();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
       
        PlayerMovement();
        if ( (Input.GetKeyDown(KeyCode.Space)) || Input.GetMouseButtonDown(0) ) 
        {
            laserShoot();
            
        }

        
        
        
        
    }
    private void laserShoot()
    {
        _audioSource.Play();
        if (tripleShot)
        {
            if(Time.time > _nextFire)
            {
                Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
                _nextFire = Time.time + _fireRate;
            }
        } else if (!tripleShot)
        {
            if (Time.time > _nextFire)
            {
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
                _nextFire = Time.time + _fireRate;
            }
        }
        
    }
    private void PlayerMovement()
    {
        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");
        if (isSpeedUP)
        {
            transform.Translate(Vector3.right * _speed * 2.0f * horizontalAxis * Time.deltaTime);
            transform.Translate(Vector3.up * _speed * 2.0f * verticalAxis * Time.deltaTime);
        }   else if (!isSpeedUP)
        {
            transform.Translate(Vector3.right * _speed * horizontalAxis * Time.deltaTime);
            transform.Translate(Vector3.up * _speed * verticalAxis * Time.deltaTime);
        }
        

        if (transform.position.y > 0f)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
        else if (transform.position.y < -4.2f)
        {
            transform.position = new Vector3(transform.position.x, -4.2f, transform.position.z);
        }

        /*if (transform.position.x < -8.3f)
        {
            transform.position = new Vector3(-8.3f, transform.position.y, transform.position.z);
        }else if (transform.position.x > 8.3f)
        {
            transform.position = new Vector3(8.3f, transform.position.y, transform.position.z);
        }*/
        if (transform.position.x < -9.45f)
        {
            transform.position = new Vector3(8.3f, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > 9.45f)
        {
            transform.position = new Vector3(-8.3f, transform.position.y, transform.position.z);
        }
    }

    public void PowerUpTripleShot()
    {
        tripleShot = true;
        StartCoroutine(PlayerTripleShot());
       
    }
    public void PlayerSpeedUpOn()
    {
        isSpeedUP = true;
        StartCoroutine(PlayerSpeedUp());
    }

   public IEnumerator PlayerTripleShot()
    {
        yield return new WaitForSeconds(5.0f);
        tripleShot = false;
    }
    public IEnumerator PlayerSpeedUp()
    {
        yield return new WaitForSeconds(5.0f);
        isSpeedUP = false;
    }
    public void PlayerDeath()
    {   
        if (isShieldUp)
        {
            isShieldUp = false;
            visualShield.SetActive(false);
            return;
        }
        hitCount++;
        if (hitCount == 1)
        {
            _engines[0].SetActive(true);
        }
        else if (hitCount == 2)
        {
            _engines[1].SetActive(true);
        }
        playerHealth--;
        _uimanager.UpdateLives(playerHealth);

        if (hitCount == 1)
        {
            _engines[0].SetActive(true);
        }
        else if (hitCount == 2)
        {
            _engines[1].SetActive(true);
        }
        if (playerHealth <= 0)
        {
            _uimanager.ShowTitleScreen();
            _gameHandler.gameOver = true;
            Debug.Log("Player Dead");
            Destroy(this.gameObject);
            Instantiate(explodePlayerPrefab, transform.position, Quaternion.identity);
        }
    }
    public void ShieldOn()
    {
        isShieldUp = true;
        visualShield.SetActive(true);

    }
    
}
