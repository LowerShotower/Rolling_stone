using UnityEngine;
using System.Collections;

public class Pickable : MonoBehaviour {

	void OnTriggerStay2D (Collider2D other){

	//	if (other.GetComponent<PlayerController> ().checkIfNotMove) {
		if ((transform.position-other.transform.position).sqrMagnitude < 0.01f){

			gameObject.SetActive (false);
			GameControl.instance.count++;
		}

	}
}
