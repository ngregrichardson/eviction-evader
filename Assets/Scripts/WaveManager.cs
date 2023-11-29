using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class WaveManager : MonoBehaviour
{

    #region Singleton
    public static WaveManager instance;

    void Awake()
    {
        instance = this;
    }
    #endregion

    public enum SpawnState { SPAWNING, WAITING, COUNTING };

    [System.Serializable]
    public class Wave
    {
        public string name;
        public GameObject enemy;
        public int count;
        public float rate;
    }

    public Wave[] waves;
    private int nextWave = 0;

    public Transform[] spawnPoints;

    public float timeBetweenWaves = 5f;
    public float waveCountdown;

    private float searchCountdown = 1f;

    public SpawnState state = SpawnState.COUNTING;

    public Text waveNumberText;
    public GameObject waveCompletedText;
    public Text waveCountdownText;
    public Image crosshair;

    void Start()
    {
        waveCountdown = timeBetweenWaves;
    }

    void Update()
    {
        if (state == SpawnState.WAITING)
        {
            if (EnemyIsAlive())
            {
                return;
            }

            WaveCompleted();
        }

        if (waveCountdown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
            waveCountdownText.text = Mathf.Round(waveCountdown).ToString();
        }
    }

    void WaveCompleted()
    {
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        waveCompletedText.SetActive(true);
        crosshair.enabled = false;

		FindObjectOfType<AudioManager>().Play("WaveComplete");

		if (nextWave + 1 > waves.Length - 1)
        {
            GameManager.instance.Win();
        }else
        {
            nextWave++;
        }
        waveNumberText.text = " Wave " + nextWave.ToString();
    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
			searchCountdown = 1f;
            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        waveCompletedText.SetActive(false);
        crosshair.enabled = true;
        state = SpawnState.SPAWNING;

        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy(GameObject _enemy)
    {
        if(spawnPoints.Length != 0)
        {
            Transform sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(_enemy, sp.position, sp.rotation);
        }
    }
}
