using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Player _player;
    // Start is called before the first frame update
    private float _speed = 1.5f;

    [SerializeField]
    private Animator _explosion;

    [SerializeField]
    private AnimationClip _explosionAnimation;

    

    private int r;
    private void Awake()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, -1, 0) * _speed * Time.deltaTime);
        
        if (transform.position.y < -4)
        {
            transform.position = new Vector3(Random.Range(-8f, 8f), 6f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (_player != null)
            {
                _player.Damage();
            }
            _speed = 0f;
            _explosion.SetTrigger("OnDeath");
            Destroy(this.GetComponent<Collider2D>());
            Destroy(this.gameObject, _explosionAnimation.length);
            AudioManager.instance.PlayExplosionSound();

        }

        if (other.tag == "Laser")
        {
            ScoreManager.instance.AddScore(10);
            _explosion.SetTrigger("OnDeath");
            _speed = 0f;
            Destroy(this.GetComponent<Collider2D>());
            Destroy(this.gameObject,_explosionAnimation.length);
            AudioManager.instance.PlayExplosionSound();
            Destroy(other.gameObject);
        }
    }
    
}
