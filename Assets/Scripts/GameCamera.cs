using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour {
	public GameObject player;

	void Start () {
		player = GameObject.FindWithTag("PLAYER");
	}

	void Update () {
			if (player != null){

				transform.position = player.transform.position + new Vector3 (0,0,-5);
			}

	}
}
