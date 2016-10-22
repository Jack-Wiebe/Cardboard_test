using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;


public class Test_Player_Controller : MonoBehaviour {

	public Camera cam;
	public PointerEventData pointerData;
	public GazeInputModule gaze;
	public float speed;
	public IGvrGazePointer point;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float h = Input.GetAxis ("Horizontal");
		Debug.Log (h);
		if ( h != 0) {
			
			this.transform.Rotate((this.transform.eulerAngles + new Vector3 (0.0f,h * .0000000000001f , 0.0f) * Time.deltaTime));
		}
	}

	public void SetGazedAt(bool gazedAt) {
		GetComponent<Renderer>().material.color = gazedAt ? Color.green : Color.red;
	}

	public void OnGazeEnter() {
		SetGazedAt(true);
	}

	/// Called when the user stops looking on the GameObject, after OnGazeEnter
	/// was already called.
	public void OnGazeExit() {
		SetGazedAt(false);
	}

	public void OnGazeClick()
	{
		if (pointerData == null) {
			pointerData = new PointerEventData (gaze.eventSend());
		}

		Debug.Log (pointerData);
		//Camera cam = pointerData.enterEventCamera;
		if (cam == null) {
			Debug.Log("NO CAM HOMIE!!");
		}

		float intersectionDistance = pointerData.pointerCurrentRaycast.distance + cam.nearClipPlane;
		Vector3 intersectionPosition = cam.transform.position + cam.transform.forward * intersectionDistance;

		//return intersectionPosition;
		GetComponent<Rigidbody> ().AddForceAtPosition (this.transform.eulerAngles - intersectionPosition  * 100.0f, intersectionPosition);
	}
}
