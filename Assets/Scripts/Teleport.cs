using UnityEngine;
using System.Collections;

public enum Type: byte {oneSide, twoSide}

public class Teleport : MonoBehaviour {

	public GameObject GOWithAnotherTeleport;
	public int countToOpenTeleport;
	public Type type;

	Animator anim;
	IsoObject isoObj;
	bool teleported;
	bool done;
	Teleport anotherT;


	void Awake (){

		teleported = false;
		isoObj = GetComponent<IsoObject> ();
		anim = GetComponent<Animator> ();
		anotherT = GOWithAnotherTeleport.GetComponent<Teleport> ();

		if (gameObject != anotherT.GOWithAnotherTeleport) {
			type = Type.oneSide;
		}



		if (GOWithAnotherTeleport == null){
			Debug.Log ("!!!You need to set up another teleport door to " + gameObject.name);
		}
		else {
			if (GOWithAnotherTeleport.GetComponent<Teleport>()==null){
				Debug.Log ("!!!Add Teleport script to " + GOWithAnotherTeleport.name);
			}

	}


	}
	void Update () {

		if (!done && countToOpenTeleport == GameControl.instance.count){
			SetOpened (true);
			anotherT.SetOpened (true);
			done = true;
		}
	}
	 


	public void SetOpened(bool state){
		anim.SetBool("teleportState",state);

		isoObj.isPassable = state;
		isoObj.isInterPlayable = state;


	}



	void OnTriggerStay2D(Collider2D other){
	if(other.transform.position.z ==gameObject.transform.position.z){

			StartCoroutine(TeleportByType (other, GOWithAnotherTeleport.transform.position, type));
		}
	}
	void OnTriggerExit2D(Collider2D other){
		teleported = false;
	}



	IEnumerator TeleportByType(Collider2D gameO, Vector3 destination, Type type){

		Animator gameOAnim = GOWithAnotherTeleport.GetComponent<Animator> ();
		Player gameOPlayerController = gameO.GetComponent<Player> ();

		if (gameOPlayerController.checkIfNotMove && anim.GetBool ("teleportState") && gameOAnim.GetBool ("teleportState")&& !teleported ) {
			yield return StartCoroutine (Teleportation (gameO, destination));

			if(type == Type.oneSide){
				SetOpened (false);
				if (gameObject == anotherT.GOWithAnotherTeleport) {
						anotherT.SetOpened (false);
				}
			}
		}

	}



	IEnumerator Teleportation(Collider2D gameO, Vector3 destination){
	
		gameO.GetComponent<Player> ().checkIfNotMove = false;
		yield return  new WaitForSeconds(0.5f);
		gameO.transform.gameObject.SetActive(false);
		gameO.transform.position = destination;
		yield return new WaitForSeconds (0.5f);
		gameO.transform.gameObject.SetActive(true);
		GOWithAnotherTeleport.GetComponent<Teleport>().teleported = true;
		gameO.GetComponent<Player> ().checkIfNotMove = true;


	}
	
}
