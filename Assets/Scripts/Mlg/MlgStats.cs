using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MlgStats : MonoBehaviour {

    private Vector2 _coords;
    public int life;
    private GameObject mainControler;
    private Text towerLife;

	// Use this for initialization
	void Start () {
    	mainControler = GameObject.Find("Main Camera");
      towerLife =  GameObject.Find("TowerLife").GetComponent<Text>();
	}

	// Update is called once per frame
	void Update () {

	}

    public Vector2 GetPosition()
    {
        return _coords;
    }

    public void SetPosition(Vector2 coords)
    {
        _coords = coords;
    }

    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.gameObject.tag == "ENEMY")
        {
            Debug.Log("Tower Atacked");
            Destroy(obj.gameObject);
            life -- ;
            towerLife.text = "Tower: " + life.ToString();
            if (life <= 0 ){
              Death();
            }
        }
    }

    void Death(){
      Destroy(gameObject);
      mainControler.GetComponent<WaveScreen>().stopGame();
      SceneManager.LoadScene("GameOver");

    }

}
