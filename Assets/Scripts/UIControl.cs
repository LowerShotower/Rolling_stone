using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class UIControl : MonoBehaviour {

	public GameObject mainMenu;
	public GameObject pauseMenu;
	public GameObject endMenu;

	private static UIControl _instance;
	public static UIControl instance{
		get{
			if (_instance ==null){
				_instance = GameObject.FindObjectOfType<UIControl>() as UIControl;
				DontDestroyOnLoad(_instance.gameObject);
			}
			return _instance;

		}
	}


	void Awake (){
		//Sisngleton для UIControl
		if (_instance == null){
			_instance = this;
			DontDestroyOnLoad(this.gameObject);
		}
		if (_instance!=this){
			Destroy (this.gameObject);
		}

		if (Application.loadedLevel == 0) {
			PauseMenuActivation(false);
			MainMenuActivation(true);
			EndMenuActivation(false);
			
		}
		if (Application.loadedLevel >0&& Application.loadedLevel < Application.levelCount-1) {
			PauseMenuActivation(false);
			MainMenuActivation(false);
			EndMenuActivation(false);
		}
		
		
		if (Application.loadedLevel == Application.levelCount - 1) {
			PauseMenuActivation(false);
			MainMenuActivation(false);
			EndMenuActivation(true);
		}
	
	}


	void Update(){
	
	}

	void OnLevelWasLoaded(int level){

	}


	public void NewGame(){
		GameControl.instance.LoadLevel (1);
	}

	public void QuitGame(){
		GameControl.instance.QuitGame ();
	}

	public void MainMenuLevel (){
		GameControl.instance.LoadLevel (0);
	}



	public void MainMenuActivation (bool on){
		if(mainMenu ==null) mainMenu = GameObject.Find ("MainMenu");
		if(mainMenu !=null) mainMenu.SetActive(on);
	}



	public void PauseMenuActivation(bool on){
		if(pauseMenu ==null) pauseMenu = GameObject.Find ("PauseMenu");
		if(pauseMenu !=null) pauseMenu.SetActive(on);

	}

	public void EndMenuActivation (bool on){
		if(endMenu ==null) endMenu = GameObject.Find ("EndMenu");
		if(endMenu !=null) endMenu.SetActive(on);

	}
}