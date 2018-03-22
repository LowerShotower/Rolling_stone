using UnityEngine;
using System.Collections;

public class Example : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log ("from Start in " + Application.loadedLevelName + gameObject.name);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void Awake (){
		Debug.Log ("from Awake in " + Application.loadedLevelName+ gameObject.name);
	}

	void OnDisable (){Debug.Log ("from OnDisable in " + Application.loadedLevelName+ gameObject.name);}
	void OnEnable (){Debug.Log ("from OnEnable in " + Application.loadedLevelName+ gameObject.name);}
	void OnLevelWasLoaded(){Debug.Log ("from OnLevelWasLoaded in " + Application.loadedLevelName+ gameObject.name);}

}
