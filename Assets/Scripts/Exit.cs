using UnityEngine;
using System.Collections;

public class Exit : MonoBehaviour {
	
	public int unlockingCount;
	private Animator anim;
	private IsoObject isoObj;



	void Awake(){
		anim = GetComponent<Animator> ();
		isoObj = GetComponent<IsoObject> ();
	}



	void Update(){
		if (GameControl.instance.count >= unlockingCount) {
			anim.SetBool ("exitState", true);
			isoObj.isPassable = true;
			isoObj.isInterPlayable = true;
		}
	}


	void OnTriggerStay2D (Collider2D other){
		if (Mathf.Abs ((other.transform.position - transform.position).sqrMagnitude) < float.Epsilon) {
			GameControl.instance.LoadNextLevel();
		}
	}

}
