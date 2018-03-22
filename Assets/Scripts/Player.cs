using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	Rigidbody2D rb2D ;
	[HideInInspector]public  bool checkIfNotMove;
	float inputX, inputY;
	Animator anim;
	public uint moveSpeed;
	public uint distance;


	void Awake (){
		rb2D = GetComponent<Rigidbody2D> ();
		anim = (Animator)GetComponent ("Animator");
		checkIfNotMove = true;
		Physics2D.queriesStartInColliders = false;
	}


	void FixedUpdate() {
		inputX = InputControl.x;
		inputY = InputControl.y;
		if (inputX!=0^inputY!=0){
			if (checkIfNotMove) {
				MoveInGrid (inputX,inputY, moveSpeed, distance,(int) transform.localPosition.z);
			}
		}
	}


	public void MoveInGrid (float twoDXDir, float twoDYDir, uint moveSpeed, uint distance, int gZ){


			Vector2 start = transform.position;
			Vector2 isoVector = IsoObject.TwoDToIso (new Vector2 (twoDXDir, twoDYDir)); 


			int groundLayer9Mask = 1 << 8;

			RaycastHit2D[] groundTilesHits = Physics2D.RaycastAll (start, isoVector, 
			                                        isoVector.magnitude * distance,groundLayer9Mask,gZ,gZ); 

			while ((groundTilesHits.Length) != distance){
				distance = distance - 1;
				groundTilesHits = Physics2D.RaycastAll (start, isoVector, 
				                                        isoVector.magnitude * distance,groundLayer9Mask,gZ,gZ);
				}


			int objectsLayer10Mask = 1<<9;

			RaycastHit2D objectHit = Physics2D.Raycast(start, isoVector, 
			                                           isoVector.magnitude * distance, objectsLayer10Mask,gZ,gZ);


			if (objectHit.transform!=null){
				if (objectHit.transform.gameObject.GetComponent<IsoObject>()!=null){

					IsoObject hittedObject = objectHit.transform.gameObject.GetComponent<IsoObject>();

					if (!hittedObject.isPassable){

						while (objectHit.transform!= null){
							distance = distance - 1;
							objectHit = Physics2D.Raycast(start, isoVector, isoVector.magnitude * distance, objectsLayer10Mask,gZ,gZ);
						}

					} 
					else if (hittedObject.isInterPlayable){
							while (objectHit.transform!= null){
								distance = distance - 1;
								objectHit = Physics2D.Raycast(start, isoVector, isoVector.magnitude * distance, objectsLayer10Mask,gZ,gZ);
							}
						distance =distance +1;
						}
				} 
				else { Debug.Log ("YoU hit "+objectHit.transform.gameObject.name+"," 
					  				+"as it's on objectsLayer you have to add IsoObject script to ");
				}
			} 


			Debug.DrawRay(start,isoVector*distance,Color.red,0.5f);

			Vector2 end = start + distance*isoVector;


				StartCoroutine (MoveTo (end,moveSpeed));
				
	
	}



	IEnumerator MoveTo (Vector2 endPosition){
		StartCoroutine (MoveTo (endPosition, 1.0f));
		yield return null;
	}

	IEnumerator MoveTo (Vector2 endPosition, float speed) {
		float sqrtRemainingDistance = (rb2D.position - endPosition).sqrMagnitude;
		checkIfNotMove = false;
		anim.SetFloat ("maxWalkingSpeed",1);
		while (sqrtRemainingDistance > float.Epsilon) {
			Vector2 newPosition = Vector2.MoveTowards (rb2D.position, endPosition, Time.fixedDeltaTime*speed);
			rb2D.MovePosition (newPosition);
			sqrtRemainingDistance = (rb2D.position - endPosition).sqrMagnitude;
			yield return null;
		}
	anim.SetFloat ("maxWalkingSpeed",0);
		checkIfNotMove = true;
	}



}