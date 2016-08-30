using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public class AI_ : MonoBehaviour {

    private enum Target { PLAYER,MLG};
    private enum Mocos { VERDES,ROJOS,SANGRADOS};
    private List<Vector2> movsxy;
	private float blockSize = 0.5f;
    private int movs = 0;
    private int nextTarget = 0;
    private bool endOfPath = false;
    private Vector2 nextTargetPos;
    private Rigidbody2D rgbod;
    private Target currentTarget;
    private float time;
    public float Speed;

    // Use this for initialization
    void Start () {
        rgbod = GetComponent<Rigidbody2D>();
        currentTarget = UpdateTarget();
        time = Time.time;
        SeekTarget(currentTarget);
	}

	// Update is called once per frame
	void Update () {
		FollowPath ();
	}

	void SeekTarget(Target target){

        Vector2 currentPos = new Vector2 (Mathf.RoundToInt (transform.position.x / blockSize),
										  Mathf.RoundToInt (-transform.position.y / blockSize));

        movsxy = new List<Vector2>();

        Vector2 targetPos = new Vector2();

        switch (target)
        {
            case Target.PLAYER:
                targetPos = GameObject.Find("Player_1").GetComponent<PlayerControl>().GetPosition();
                break;
            case Target.MLG:
                targetPos = GameObject.Find("Mlg(Clone)").GetComponent<MlgStats>().GetPosition();
                break;
        }

        movsxy = GameObject.Find("Main Camera").GetComponent<PathToPlayer>().GetPathToPlayer(currentPos, targetPos);

        nextTarget = 0;

        if (movsxy.Count > 1) {
            nextTargetPos = new Vector2 ((float)movsxy[1].x, -(float)movsxy[1].y);
            endOfPath = false;
        } else {
            endOfPath = true;
        }

        movs = movsxy.Count - 1;

    }

    void OnTriggerStay2D(Collider2D obj)
    {
        if (obj.gameObject.tag == "PLAYER")
        {
            Debug.Log("Player violado");
            Destroy(gameObject);
        }
    }

    Vector2 GetPosition(){
		Vector2 position;
		position.x = Mathf.RoundToInt(transform.position.x / 0.5f);
		position.y = -Mathf.RoundToInt(transform.position.y / 0.5f);
		return position;
	}

	void FollowPath(){

        if(Time.time - time > 1.5)
        {
            Target newTarget = UpdateTarget();
            if (newTarget != currentTarget) {
                currentTarget = newTarget;
                SeekTarget(currentTarget);
            }
        }

		if (endOfPath) {
			SeekTarget(UpdateTarget ());
			return;
		}

		float relativeSpeed = Speed * Time.deltaTime;

		if ((transform.position.x < nextTargetPos.x * .5f + relativeSpeed*0.5f &&
			transform.position.x > nextTargetPos.x * .5f - relativeSpeed*0.5f) &&
			(transform.position.y <  nextTargetPos.y * .5f + relativeSpeed*0.5f &&
			transform.position.y > nextTargetPos.y * .5f - relativeSpeed*0.5f)) {

			nextTarget++;

			if (nextTarget >= movs) {
				endOfPath = true;
				return;
			}

			nextTargetPos = new Vector2 (movsxy [nextTarget].x, -movsxy[nextTarget].y);
		}

		rgbod.position = Vector2.MoveTowards (transform.position, nextTargetPos * 0.5f, relativeSpeed);

	}

    Target UpdateTarget() {
        Vector2 currentCoors = GetPosition();
        Vector2 playerCoords = GameObject.Find("Player_1").GetComponent<PlayerControl>().GetPosition();
        Vector2 mlgCoords = GameObject.Find("Mlg(Clone)").GetComponent<MlgStats>().GetPosition();
        float distToPlayer = (playerCoords.x - currentCoors.x) * (playerCoords.x - currentCoors.x) +
                             (playerCoords.y - currentCoors.y) * (playerCoords.y - currentCoors.y);

        float distToMlg =    (mlgCoords.x - currentCoors.x) * (mlgCoords.x - currentCoors.x) +
                             (mlgCoords.y - currentCoors.y) * (mlgCoords.y - currentCoors.y);

        if (distToPlayer < distToMlg)
            return Target.PLAYER;
        else
            return Target.MLG;

    }

}
