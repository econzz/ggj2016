using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Result : MonoBehaviour {

	public Button btn;
//    public Button btn2;

//	private Game gameScript ;

	public Text scoreText;

	// Use this for initialization
	void Start () {
        GameController.Instance.PlayGameOverSe();
        btn.interactable = false;
//        btn2.interactable = false;
//		this.gameScript = GameObject.Find("GameCanvas").GetComponent<Game>();
		this.scoreText.text = ""+GameController.Instance.score;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void AnimeEnd(){
		btn.interactable = true;
	}

	public void ReturnTop(){
		GameController.Instance.state = GameController.STATE.MENU;
		SceneManager.UnloadScene("Game");
		SceneManager.UnloadScene("Result");
	}
}
