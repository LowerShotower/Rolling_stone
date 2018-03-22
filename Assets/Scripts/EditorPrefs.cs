using UnityEngine;
using System.Collections;

#if UNITY_EDITOR 
using UnityEditor;

[ExecuteInEditMode]
#endif
public class EditorPrefs : MonoBehaviour {
	
	SpriteRenderer spriteRenderer;
	string groundNo;
	Vector3 tR;

	void Awake(){
		#if UNITY_EDITOR
		if ( GetComponent<PolygonCollider2D>() == null){
			PolygonCollider2D coll= gameObject.AddComponent<PolygonCollider2D>();
			coll.isTrigger = true;
		}
		MakeIsoRombPolygonCollider2D ();
		#endif
	}


	void Update () {


		 

		tR = transform.localPosition;
		if (tR.z < 1) {
			tR.z = 1;
		} 
		if (tR.z > 6) {
			tR.z=6;
		}
		tR.z = Mathf.RoundToInt (tR.z);

		spriteRenderer= GetComponent<SpriteRenderer>();
		if(GetComponent<SpriteRenderer>()!=null){
			spriteRenderer.sortingLayerName = "Ground"+tR.z.ToString();
		} else {
			tR.z=0;
		}

		transform.localPosition = tR;
		#if UNITY_EDITOR 

		if (EditorApplication.isPlaying) {
			return;
		}

		SnapToXYZGrid ();
		#endif
	}


	
	#if UNITY_EDITOR 
	public void SnapToXYZGrid(){
		if (Mathf.Abs (tR.y) % 1 < 0.5f) {
			tR = new Vector3 (Mathf.RoundToInt (tR.x),
			                                       Mathf.RoundToInt (tR.y),
			                                       transform.localPosition.z);
		}
		if (Mathf.Abs (tR.y) % 1 > 0.5f && Mathf.Abs (tR.y) % 1 < 1.0f) { 
			if (tR.y > 0.0f) {
				tR = new Vector3 (Mathf.RoundToInt(tR.x), 
				                                       Mathf.RoundToInt(tR.y) - 0.5f,
				                                       tR.z);
			}
			if (tR.y < 0.0f){
				tR = new Vector3 (Mathf.RoundToInt(tR.x), 
				                                       Mathf.RoundToInt(tR.y) + 0.5f,
				                                       tR.z);
			} 
		}
		transform.localPosition = tR;
	}



	public void MakeIsoRombPolygonCollider2D (){
		PolygonCollider2D pC2D = GetComponent<PolygonCollider2D>();

		if(pC2D!=null){
			Vector2[] points = new Vector2[4]{new Vector2(-0.9f,0.0f),new Vector2(0.0f,0.4f),
											  new Vector2(0.9f,0.0f),new Vector2(0,-0.4f)};
			pC2D.SetPath (0,points);
			//Vector2[] v = pC2D.GetPath (0);
			//Debug.Log (v[0]+ ",    "+v[1]+ ",    "+v[2]+ ",    "+v[3]+ ",   of "+ gameObject.name );
		}
	}
    #endif
}
