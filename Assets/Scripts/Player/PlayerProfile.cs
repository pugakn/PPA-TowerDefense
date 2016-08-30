using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerProfile : MonoBehaviour {

	// Use this for initialization
	public int life;
	private GameObject mainControler;
	private Text playerLife;
	void Start () {
		mainControler = GameObject.Find("Main Camera");
		playerLife =  GameObject.Find("PlayerLife").GetComponent<Text>();
	}

	// Update is called once per frame
	void Update () {

	}
	int GetLife(){
		return life;
	}
	void Death(){
		Destroy(gameObject);
		mainControler.GetComponent<WaveScreen>().stopGame();
		SceneManager.LoadScene("GameOver");

	}
	void takeDamage(){
		life -- ;
		playerLife.text = "Player: " + life.ToString();
		if (life <= 0 ){
			Death();
		}
	}
	void OnTriggerEnter2D(Collider2D coll){
		if (coll.tag == "ENEMY"){
			takeDamage();
		}

	}
}
