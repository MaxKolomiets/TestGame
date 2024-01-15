using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class SceneManager : MonoBehaviour
{
    [SerializeField] private WaveCounter waveCounter;
    [SerializeField] private Enemy smallEnemie;
    public static SceneManager Instance;
    

    public Player Player;
    public List<Enemy> Enemies;
    public GameObject Lose;
    public GameObject Win;

    private int currWave = 0;
    [SerializeField] private LevelConfig Config;
    [SerializeField] private LevelConfig SmallGoblinsWave;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SpawnWave();
    }

    public void AddEnemie(Enemy enemie)
    {
        Enemies.Add(enemie);
    }

    public void RemoveEnemie(Enemy enemie)
    {
        if (enemie.IsBig) {
            SpawnSmallEnemiesWave(enemie);
        }
        Enemies.Remove(enemie);
        if(Enemies.Count == 0)
        {
            SpawnWave();
        }
    }

    public void GameOver()
    {
        Lose.SetActive(true);
    }

    private void SpawnWave()
    {
        if (currWave >= Config.Waves.Length)
        {
            Win.SetActive(true);
            return;
        }
        waveCounter.SetCounter(currWave+1, Config.Waves.Length);
        var wave = Config.Waves[currWave];
        foreach (var character in wave.Characters)
        {
            Vector3 pos = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
            Instantiate(character, pos, Quaternion.identity);
        }
        currWave++;

    }
    private void SpawnSmallEnemiesWave(Enemy enemie ) {
        foreach (var character in SmallGoblinsWave.Waves[0].Characters)
        {
            Vector3 pos = enemie.gameObject.transform.position+ new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1));
            Instantiate(character, pos, Quaternion.identity);
        }
    }


    public void Reset()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
    

}
