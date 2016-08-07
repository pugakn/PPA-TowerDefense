using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class Waves_timer : MonoBehaviour
{
    public int nWaves;
    public int actualWave;
    public int enemyHealth;
    public int iWave_waveduration;
    //private WaveScreen waveScreen;
    private GameObject[] enemyes;


    private float fSpawntime_Acum;
    private GameObject controller;
    private float ffWave_timer;
    public GameObject Plane_test_enemy;
    public float enemySpawnTime;
    public bool bWave_active = true;
    private bool spawnEnemyes = true;

    void Start(){
      controller = GameObject.FindWithTag("MainCamera");
      ffWave_timer = 0;
      fSpawntime_Acum = enemySpawnTime;
    }
    void Update()
    {
        if (controller.GetComponent<WaveScreen>().onPlay){
          if (spawnEnemyes == true)
          {
            ffWave_timer += Time.deltaTime;
            fSpawntime_Acum += Time.deltaTime;
            if (fSpawntime_Acum >= enemySpawnTime)
            {
              GameObject enemy = Instantiate(Plane_test_enemy, transform.position, Quaternion.identity) as GameObject;
              enemy.GetComponent<EnemyProfile>().Health = enemyHealth;
              fSpawntime_Acum = 0;
            }
          }
          if (ffWave_timer > iWave_waveduration)
          {
            enemyes = GameObject.FindGameObjectsWithTag("ENEMY");
            spawnEnemyes = false;
            if (enemyes.Length == 0){
              controller.GetComponent<WaveScreen>().stopGame();
            }
          }
        }
    }
}
