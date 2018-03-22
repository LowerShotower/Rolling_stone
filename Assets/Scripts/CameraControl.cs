using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	public float smooth;
	Transform player;
	Transform backgroundStars1;
	Vector2 cameraPixelP, playerPixelP;
	Vector3 newCamPosition;
	bool cameraIsMoved;
	Player playerScript;




	void Awake () {
		player = GameObject.Find ("Player").transform;
		backgroundStars1 = GameObject.Find ("BackgroundStars1").transform;
		playerScript = player.gameObject.GetComponent<Player> ();
		cameraIsMoved = false;

	}
	
	
	void FixedUpdate () {
		if (Screen.orientation == ScreenOrientation.Portrait||Screen.orientation == ScreenOrientation.PortraitUpsideDown){
			Camera.main.orthographicSize = 10.0f;
		}
		if (Screen.orientation == ScreenOrientation.LandscapeLeft|| Screen.orientation == ScreenOrientation.LandscapeRight){
			Camera.main.orthographicSize = 5.0f;
		}  



		cameraPixelP = Camera.main.pixelRect.center;
		playerPixelP = Camera.main.WorldToScreenPoint (player.position);


		if (Mathf.Abs((playerPixelP - cameraPixelP).magnitude) > Camera.main.pixelWidth/3){
			if(!cameraIsMoved){
				StartCoroutine(Move()); 
			}
		}
	}

	IEnumerator Move()
	{
		cameraIsMoved = true;
		playerScript.checkIfNotMove = false;
		newCamPosition=new Vector3(player.position.x,player.position.y,transform.position.z);
		while (((Vector2)Camera.main.transform.position-(Vector2)player.position).sqrMagnitude >float.Epsilon) {


			transform.position = Vector3.MoveTowards(transform.position,newCamPosition, 
			                                               smooth * Time.fixedDeltaTime);
			backgroundStars1.localPosition -=(newCamPosition-transform.position).normalized*Time.fixedDeltaTime;

		
			yield return null;
		}
		playerScript.checkIfNotMove = true;
		cameraIsMoved = false;
		
	}

		



}
