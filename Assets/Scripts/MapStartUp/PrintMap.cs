using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PrintMap : MonoBehaviour {

private enum sprt_name{
	ENEMY = 0,
	WALL_1,
	WALL_2,
	WALL_3,
	FLOOR_1,
	FLOOR_2,
	FLOOR_3,
	PLAYER,
	SPAWNER,
	TORRET_1,
	TORRET_2,
	TORRET_3,
	VOID,
	SPRITES_COUNT
};

public GameObject[] prefabs;
public GameObject ply_;
public List<Vector2> badPositions;
public float height;
public float width;

// Use this for initialization
public void print_map () {

			badPositions = new List<Vector2>();
			height = prefabs [1].GetComponent<Renderer> ().bounds.size.y;
			width = prefabs [1].GetComponent<Renderer> ().bounds.size.x;

	Vector3 playerpos = new Vector3(0,0,0);

	ply_ =  			(GameObject)Instantiate(prefabs[(int)sprt_name.PLAYER], playerpos, Quaternion.identity);
	//GameObject Enemy  = (GameObject)Instantiate (prefabs [(int)sprt_name.ENEMY], playerpos, Quaternion.identity);
	//Buscar todos los caracteres dentro del arreglo
	for (int i = 0; i < GetComponent<LoadMap> ().m_rows; i++) {
		for (int j = 0; j < GetComponent<LoadMap> ().m_cols; j++) {
			for (int k = 0; k < (int)sprt_name.SPRITES_COUNT; k++) {
				if (GetComponent<LoadMap> ().Grid [i] [j] == (char)((int)'a'+ k)) {
					Vector3 position_ = new Vector3 (j * width, -i * height, 0);
					if (GetComponent<LoadMap> ().Grid [i] [j] == 'b') {
													Vector2 temp = new Vector2((int)j, (int)i);
													badPositions.Add(temp);
					}
					if (GetComponent<LoadMap> ().Grid [i] [j] == 'h') {//h es el caracter representativo del player

						ply_.name = "Player_1";
						ply_.transform.Translate (position_);
						Instantiate (prefabs [(int)sprt_name.FLOOR_1], position_, Quaternion.identity);

					} else {

						Instantiate (prefabs [k], position_, Quaternion.identity);
                        if(k == 9)
                            {
                                Vector2 coords = new Vector2(j, i);
                                GameObject.Find("Mlg(Clone)").GetComponent<MlgStats>().SetPosition(coords);
                            }


					}
					k = (int)sprt_name.SPRITES_COUNT + 1;
				}
			}
		}
	}
}
}
