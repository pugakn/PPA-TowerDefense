using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;


public class PlaceTorret : MonoBehaviour {
	// Use this for initialization
	private Canvas canvas;
	public	GameObject obj;
	private WaveScreen wave;

	public bool onUI;
	void Start () {
		canvas = GameObject.Find("Pantalla Torretas").GetComponent<Canvas>();
		onUI= false;
		wave = GameObject.Find("Main Camera").GetComponent<WaveScreen>();
	}

	private float currentTime;
	public float touchingTime;
	private float touchingTimeAccum;
	private bool touching = false;
	public GameObject feedback;
	private GameObject _feedback;
	void Update(){
		if (touching){
			touchingTimeAccum += Time.deltaTime;
			//Debug.Log(touchingTimeAccum);
			if(touchingTimeAccum < touchingTime && _feedback != null && !onUI){
				_feedback.transform.localScale += new Vector3(.1f,.1f,0);
			}
		}

		if((touchingTimeAccum > touchingTime)){
			canvas.enabled = true;
			enterUI();
			touchingTimeAccum = 0;
			Handheld.Vibrate();
		}
		if ((Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began &&wave.towers > 0 )){
			Vector3 click = Input.GetTouch(0).position;
			click.z = 10;
			click = GetComponent<Camera>().ScreenToWorldPoint(click);
			Debug.DrawLine ( click + new Vector3 (0, 0, 3), click, Color.red);
			RaycastHit2D hit = Physics2D.Linecast (click + new Vector3 (0, 0, 3), click );
			if(hit.collider != null){
				if (hit.collider.gameObject.tag == "PLACE_B" || hit.collider.gameObject.tag == "PLACE_N"){
					touching = true;
					if (!onUI){
						SetObj(hit.collider.gameObject);
						Debug.Log ("Coll: " + hit.collider.gameObject.name);
						_feedback = Instantiate(feedback,obj.transform.position, obj.transform.rotation) as GameObject;
					}
				}
			}
		}
		if(Input.touchCount > 0 && (Input.GetTouch(0).phase == TouchPhase.Ended)){
				touching = false;
				touchingTimeAccum = 0;
				Destroy(_feedback);
		}
		if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && onUI)){
			if(EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
			    return;
			canvas.enabled = false;
			touching = false;
			touchingTimeAccum = 0;
			onUI = false;
		}
	}

  public	void Turret(){
		obj.GetComponent<TransformTurret>().Transforming();
		onUI = false;
	}
	public void SetObj(GameObject other){
		obj = other;
	}
	public void enterUI(){
		onUI=true;
	}
}
