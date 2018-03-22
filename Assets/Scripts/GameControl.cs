using UnityEngine;
using System.Collections;
using UnityEngine.UI;    // открывает доступ к редактированию, конкретно здесь, текста в загруз экране

//NB Объекты OnDestroyOnLoad запускают следующую последовательность событий: isLoadingLevel в прудыдущей сцене в послед
//нем кадре; в новой сцене: OnDisable, Awake, OnLevelWasLoaded. В самой первой сцене запускается OnEnable, Awake
public class GameControl : MonoBehaviour {

	public Transform loadingScreen1Pref; // сюда в инспекторе добавляем префаб загрузочного экрана
	public int count; 
	public Transform gameMenuPref; // в эту ссылку вешаем префаб меню, ничего не поделаешь. его нужно синициализировать


	private Transform loadingScreenTemp; // через эту ссылку обращаемся к объекту префаба и его компонентам, 
										 //ввел, потому что  хотел изменять имя созданного объекта
	private static GameControl _instance;





	//Singleton property
	public static GameControl instance{
		get {
			if (_instance == null){
				_instance = FindObjectOfType<GameControl>() as GameControl;
				if(_instance != null){

				}
				DontDestroyOnLoad(_instance.gameObject);
			}
			return _instance;
		}
	}



	void Awake (){
	
		// Singleton
		if (_instance == null) {
			_instance = this;
			DontDestroyOnLoad (this.gameObject);
		} else if (_instance!=this){
			Destroy (this.gameObject);
		}

		//Инициализируем блок меню
		var mainMenu = GameObject.Find ("GameMenu");

		if (mainMenu == null) {

			Transform MT = GameObject.Instantiate (gameMenuPref);
			MT.name = "GameMenu";
		}
	}
	
	void onEnable(){

	}
	void OnLevelWasLoaded(int level){

		GameControl.instance.count = 0;

	}


	void Start(){

	}

	


	public  void LoadNextLevel(){
		if (Application.loadedLevel < Application.levelCount - 1) {
			LoadLevel (Application.loadedLevel + 1);
		} else {
			Debug.Log ("It was the last Level");
		}
	}



	public  void LoadLevel(int index){
		StartCoroutine (LoadingScreen (index));
	}



	IEnumerator LoadingScreen (int index){

		Time.timeScale = 0.0f;

		float t = Time.realtimeSinceStartup;
		while (Time.realtimeSinceStartup < t+0.5f) {
			yield return null;
		}
		Application.LoadLevel (index);

	if (Application.loadedLevel < Application.levelCount - 2) {// если уровень предпоследний, то не выдает загрузочный экран

		loadingScreenTemp = Instantiate (loadingScreen1Pref) as Transform;
		loadingScreenTemp.name = loadingScreen1Pref.name;
		Image loadingImg = loadingScreenTemp.gameObject.GetComponentInChildren(typeof(Image)) as Image;
		Text textLvl = loadingScreenTemp.gameObject.GetComponentInChildren (typeof(Text)) as Text;
		textLvl.text = "Stage " + (Application.loadedLevel + 1).ToString ();
	
		DontDestroyOnLoad (loadingScreenTemp);

		t = Time.realtimeSinceStartup;
		Color hey = new Color32(0,0,0,255);

		
			t = Time.realtimeSinceStartup;
			while (Time.realtimeSinceStartup < t+3.0f) {
				yield return null;
			}
			t = Time.realtimeSinceStartup;
			while (Time.realtimeSinceStartup < t+1.0f) {
				hey = Color.Lerp (new Color(0,0,0,1), new Color(0,0,0,0),Time.realtimeSinceStartup-t);
				loadingImg.color = hey;
				yield return null;
			}

	}
		Time.timeScale = 1.0f;
		yield return new WaitForSeconds (0.1f);
		if (loadingScreenTemp!= null) Destroy (loadingScreenTemp.gameObject);//когда уровень предпоследний, загр экран не создавался, а ссылка есть. поэтому сключил проверку на наличие объекта для разрушения
	}



	public void QuitGame(){
		Application.Quit ();
	}

}
