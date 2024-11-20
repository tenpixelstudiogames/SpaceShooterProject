using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2.5f;

    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private GameObject _enemyPrefab;

    [SerializeField]
    private int _lives = 3;

    [SerializeField]
    private GameObject PlayerShield;

    [SerializeField]
    private GameObject _engineDamagePrefab;


    [SerializeField]
    private float _firerate = 0.5f;
    private float nextFire = 0f;

    [SerializeField]
    private GameObject _tripleShotPrefab;

    private bool isTripleShot = false;
    private bool isSpeedUp = false;
    private bool isShield = false;

    private GameObject LeftEngine;
    private GameObject RightEngine;


    void Start()
    {
        //Assign the position of character on starting
        transform.position = new Vector3(0,0,0);
        PlayerShield.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        Shooting();

    }

    void CalculateMovement()
    {
        if (isSpeedUp)
        {
            _speed = 8.5f;
        }
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.96f, 5.86f), 0f);
        if (transform.position.x <= -11.25f || transform.position.x >= 11.25f)
        {
            transform.position = new Vector3(-transform.position.x, transform.position.y, 0);
        }
    }

    void Shooting()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextFire)
        {
            if (isTripleShot == true)
            {
                Instantiate(_tripleShotPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
            }
            else
            {
               Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
            }
            nextFire = Time.time + _firerate;
            AudioManager.instance.PlayLaserSound();
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "TripleShot")
        {
            isTripleShot = true;
            Destroy(other.gameObject);
            AudioManager.instance.PlayPowerupSound();
            StartCoroutine(TripleShotCoolDown());

        }
        else
        {
            if(other.tag == "SpeedUp")
            {
                isSpeedUp = true;
                Destroy(other.gameObject);
                AudioManager.instance.PlayPowerupSound();
                StartCoroutine(SpeedUpCoolDown());
            }
            if(other.tag == "Shield")
            {
                isShield = true;
                Destroy(other.gameObject);
                PlayerShield.SetActive(true);
                AudioManager.instance.PlayPowerupSound();
                StartCoroutine(ShieldCoolDown());
            }
        }
    }

    public void Damage()
    {
        if (isShield)
        {
            return;
        }
        _lives--;
        EngineDamage();
        LivesManager.instance.UpdateLives(_lives);
        if(_lives <= 0)
        {
            SpawnManager.instance.OnPlayerDeath();
            LivesManager.instance.UpdateGameOver();
            GameManager.instance.GameOver();
            ScoreManager.instance.UpdateHighScore();
            Destroy(LeftEngine);
            Destroy(RightEngine);
            Destroy(this.gameObject);
        }
    }

    public void EngineDamage()
    {
        if(_lives == 2)
        {
            LeftEngine = Instantiate(_engineDamagePrefab, transform.position + new Vector3(-0.8f, -2.5f, 0), Quaternion.identity);
            LeftEngine.transform.parent = this.transform;
        }
        else
        {
            if(_lives == 1)
            {
                RightEngine = Instantiate(_engineDamagePrefab, transform.position + new Vector3(-0.8f, -2.5f, 0), Quaternion.identity);
                RightEngine.transform.parent = this.transform;
                LeftEngine = Instantiate(_engineDamagePrefab, transform.position + new Vector3(+0.8f, -2.5f, 0), Quaternion.identity);
                LeftEngine.transform.parent = this.transform;
            }
        }
    }

    IEnumerator TripleShotCoolDown()
    { 
       yield return new WaitForSeconds(5);
       isTripleShot = false;
    }
    IEnumerator SpeedUpCoolDown()
    {
        yield return new WaitForSeconds(5);
        isSpeedUp = false;
        _speed = 5.0f;
    }
    IEnumerator ShieldCoolDown()
    {
        yield return new WaitForSeconds(5);
        isShield = false;
        PlayerShield.SetActive(false);
    }
}
