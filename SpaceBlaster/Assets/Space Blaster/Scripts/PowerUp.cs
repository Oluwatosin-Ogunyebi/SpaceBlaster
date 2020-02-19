using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private int _powerUpID;
    [SerializeField]
    private AudioClip _audioClip;
    
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if(transform.position.y < -7)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Power Up hit" + other.name);
        if (other.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(_audioClip, Camera.main.transform.position);
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                if (_powerUpID == 0)
                {
                    player.PowerUpTripleShot();

                }
                else if (_powerUpID == 1)
                {
                    player.PlayerSpeedUpOn();
                }
                else if (_powerUpID == 2)
                {
                    player.ShieldOn();
                }
            }
                Destroy(this.gameObject);
            
        }
        
        
        
    }
}
