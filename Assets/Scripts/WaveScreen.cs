using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class WaveScreen : MonoBehaviour {
	public GameObject towePlace;
	private GameObject place;
	private GameObject[] places;
	public bool onPlay = false;
	public GameObject floor;
	public GameObject block;
	private WaveScreenCamera waveCamera;
	private GameCamera playCamera;
	private PlayerControl playerControl;

	private GameObject spawnerPlace;
	private GameObject[] spawners;
	public GameObject spawner;

	public int towers;

	public GameObject towerText;
	public Canvas canvas;
	public Canvas menu;

	public int actualWave;
	public int nWaves;

	// Use this for initialization
	void Start () {
		canvas = GameObject.Find("Pantalla Torretas").GetComponent<Canvas>();
		menu = GameObject.Find("Pantalla Compras").GetComponent<Canvas>();
		waveCamera = GetComponent<WaveScreenCamera>();
		playCamera = GetComponent<GameCamera>();
		playerControl = GameObject.FindWithTag("PLAYER").GetComponent<PlayerControl>();
		actualWave = 0;
	}

	// Update is called once per frame
	void Update () {
		towerText.GetComponent<Text>().text = towers.ToString();
	}

	public void startGame(){
		canvas.enabled = false;
		menu.enabled = false;
		onPlay = true;
		waveCamera.enabled = false;
		playCamera.enabled = true;
		playerControl.enabled = true;

		places = GameObject.FindGameObjectsWithTag("PLACE_N");
		foreach(GameObject place in places){
			Instantiate(floor,place.transform.position,place.transform.rotation);
			  Destroy(place,0);
		}
		places = GameObject.FindGameObjectsWithTag("PLACE_B");
		foreach(GameObject place in places){
			Instantiate(block, place.transform.position,place.transform.rotation);
				Destroy(place,0); ;
		}
		spawners = GameObject.FindGameObjectsWithTag("SPAWNER");
		foreach(GameObject spawnerPlace in spawners){
			Instantiate(spawner, spawnerPlace.transform.position,spawnerPlace.transform.rotation);
			Destroy(spawnerPlace,0);
		}

	}
	 public void stopGame(){
		if(actualWave != nWaves){
			playerControl.enabled = false;
			waveCamera.enabled = true;
			playCamera.enabled = false;
			menu.enabled = true;
			onPlay = false;

			places = GameObject.FindGameObjectsWithTag("WALL");
			foreach(GameObject place in places){
				Instantiate(towePlace,place.transform.position,place.transform.rotation);
					Destroy(place,0);
			}
			actualWave ++;
		}else{
					SceneManager.LoadScene("GameOver_win");
		}

	}
}
