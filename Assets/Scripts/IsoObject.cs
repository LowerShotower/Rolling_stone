using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif
[ExecuteInEditMode]
public class IsoObject : MonoBehaviour {

	public bool isPassable;
	public bool isCoverable;
	public bool isInterPlayable;

	SpriteRenderer spriteRenderer;


	private static Vector2 tempVector2;



	void Awake(){
		spriteRenderer = GetComponent<SpriteRenderer>();

	}


	void Update () {
		CheckBoxBehaviour (isPassable);
	
	}


	void LateUpdate () {
		ObjectsOrderOnScreen ();
	}


	
	public void ObjectsOrderOnScreen(){
		spriteRenderer.sortingOrder = (Camera.main.pixelHeight + 1) + (int)Camera.main.WorldToScreenPoint (spriteRenderer.transform.position).y * -1;
		//Debug.Log (gameObject.name + " on " + spriteRenderer.sprite.pivot);
		if (isCoverable) {
			spriteRenderer.sortingOrder -=1;
		}
	}


	public void CheckBoxBehaviour(bool isPassable){
		this.isPassable = isPassable;
		if (!this.isPassable) {
			isCoverable = false;
			isInterPlayable = false;
		}
	}
	

	public static Vector2 TwoDToIso(Vector2 twoD){
		
		tempVector2.x = twoD.x - twoD.y;
		tempVector2.y = (twoD.x + twoD.y) / 2;
		return tempVector2;
	}

	
	public static Vector2 IsoToTwoD (Vector2 iso) {
		
		tempVector2.x = (2 * iso.y + iso.x) / 2;
		tempVector2.y = (2 * iso.y - iso.x) / 2;
		return tempVector2;
	}
}