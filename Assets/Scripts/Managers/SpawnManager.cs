using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    [SerializeField]
    private GameObject _enemyPrefab;

    [SerializeField]
    private GameObject _enemyContainer;

    [SerializeField]
    private GameObject[] powerUps;

    [SerializeField]
    private GameObject _asteroidPrefab;
    
    private bool _isSpawning = true;

    private bool _isAsteroid = false;


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(_asteroidPrefab,new Vector3(Random.Range(-8.0f, 8.0f), 4.0f, 0), Quaternion.identity);
        StartCoroutine(ToggleAsteroid());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator ToggleAsteroid()
    {
       while (!_isAsteroid)
       {
            yield return new WaitForSeconds(2);
       }
    }

    IEnumerator SpawnRoutine()
    {

        while (_isSpawning && _isAsteroid)
        {
            GameObject newEnemy = Instantiate(_enemyPrefab, transform.position + new Vector3(Random.Range(-8.0f, 8.0f), 8.0f, 0), Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(2);
        }
    }

    IEnumerator SpawnPowerUps()
    {

        while (_isSpawning && _isAsteroid)
        {
            int index = Random.Range(0, 3);
            GameObject TripleShotNewPowerUp = Instantiate(powerUps[index], transform.position + new Vector3(Random.Range(-8.0f, 8.0f), 8.0f, 0), Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(12, 25));

        }
    }
    public void OnPlayerDeath()
    {
        _isSpawning = false;
    }

    public void ToggleSpawn()
    {
        _isAsteroid = true;
        StartCoroutine(SpawnRoutine());
        StartCoroutine(SpawnPowerUps());
    }
}
