using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    // Start is called before the first frame update
   
    [SerializeField]
    GameObject _explosionPrefab;

    [SerializeField]
    GameObject _asteroidPrefab;

    private void Awake()
    {
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * 25.0f * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Laser")
        {
            Destroy(this.gameObject);
            Destroy(other.gameObject);
            GameObject Explosion = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(Explosion, 2.0f);
            AudioManager.instance.PlayExplosionSound();
            SpawnManager.instance.ToggleSpawn();
        }
        else
        {
            if (other.tag == "Player")
            {
                GameObject Explosion = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
                Destroy(Explosion, 2.0f);
                AudioManager.instance.PlayExplosionSound();
                SpawnManager.instance.ToggleSpawn();
            }
        }
    }
}

