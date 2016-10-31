using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
public class Waves_timer : MonoBehaviour
{
    public int  iWave_waveduration;
    public static List<GameObject> enemyes;

    private float fSpawntime_Acum;
    private GameObject controller;
    private float ffWave_timer;
    public GameObject prefEnemy;
    public float enemySpawnTime;
    private bool spawnEnemyes = true;

    void Start(){
      enemyes = new List<GameObject>();
      controller = GameObject.FindWithTag("MainCamera");
      ffWave_timer = 0;
      fSpawntime_Acum = enemySpawnTime;
      enemyes.Clear();
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
              var enemy = Instantiate(prefEnemy, transform.position, Quaternion.identity) as GameObject;
              enemyes.Insert (0, enemy);
              fSpawntime_Acum = 0;
            }
          }
          if (ffWave_timer > iWave_waveduration)
          {
            spawnEnemyes = false;
            if (enemyes.Count == 0){
              controller.GetComponent<WaveScreen>().stopGame();
            }
          }
        }
    }
}
