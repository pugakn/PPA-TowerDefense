using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class TowerAttack : MonoBehaviour {


public GameObject bullet;
public float bulletSpeed;
public float attackSpeed;
private GameObject bulletG;
private  float xPos;
private float yPos;
private float unitFactor;
private float rotationZ;
public GameObject wave;
private float timer = 0;
private  Waves_timer  _spawner;
public float towerRange;
    
	void Start () {
		wave = GameObject.Find("Main Camera");
        InvokeRepeating("SelectTarget", 0, 0.1f);
    }
	void FixedUpdate () {
        if (wave.GetComponent<WaveScreen>().onPlay){
			if (targetEnemy != null){
				timer += Time.deltaTime;
				//Rotacion
				rotationZ = Mathf.Atan2((targetEnemy.transform.position.y - transform.position.y), (targetEnemy.transform.position.x - transform.position.x))* Mathf.Rad2Deg;
				transform.eulerAngles = new Vector3(0,0,rotationZ);
				//Ataque
				if (timer >= attackSpeed){
					bulletG = Instantiate(bullet,transform.position,transform.rotation) as GameObject;
					Debug.Log(targetEnemy);
					xPos = targetEnemy.transform.position.x - transform.position.x;
					yPos = targetEnemy.transform.position.y - transform.position.y;
					unitFactor = Mathf.Sqrt((xPos*xPos)+(yPos*yPos));
					bulletG.GetComponent<Rigidbody2D>().AddForce(new Vector2(xPos/unitFactor,yPos/unitFactor) * 10 * bulletSpeed);
					DamageEnemy();
					Destroy(bulletG, 3);
					timer=0;
				}
			}else{
				transform.eulerAngles = new Vector3(0,0,0);
			}
		}
    }


    private GameObject targetEnemy;
    private Vector3 pos;
    private float relativePosX;
    private float relativePosY;
    private float dist;
    private float dist2 = 0;
    void SelectTarget(){

        if (Waves_timer.enemyes.Count > 1){
        for (var it = 0; it <  Waves_timer.enemyes.Count; it++)
            {
            GameObject enemy = Waves_timer.enemyes[it];
            pos = Waves_timer.enemyes[it].transform.position;
            dist2 = dist;
				relativePosX = pos.x - transform.position.x;
				relativePosY = pos.y - transform.position.y;
				dist = relativePosX*relativePosX  + relativePosY*relativePosY;
				if (dist < dist2 && dist < towerRange)
					targetEnemy = enemy;
                else targetEnemy = null;

            }
		} else if (Waves_timer.enemyes.Count == 1){
			pos = Waves_timer.enemyes[0].transform.position;
			relativePosX = pos.x - transform.position.x;
			relativePosY = pos.y - transform.position.y;
			dist = relativePosX*relativePosX  + relativePosY*relativePosY;
			if(dist < towerRange){ targetEnemy = Waves_timer.enemyes[0]; }
            else targetEnemy = null; 
		}
    }

    void DamageEnemy(){
		targetEnemy.GetComponent<EnemyProfile>().TakingDamage(1);
	}


}
