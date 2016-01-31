using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.IO;

public class Greeting {
	public string word;
	public int time;

	public Greeting(string word, int time) {
		this.word = word;
		this.time = time;
	}
}

public class Top : MonoBehaviour {

	public GameObject startBtnObj;
	public static List<Greeting> listA;

	// Use this for initialization
	void Start () {
		var reader = new StreamReader(File.OpenRead(Application.streamingAssetsPath + "/greeting.csv"));
		listA = new List<Greeting>();
		while (!reader.EndOfStream)
		{
			var line = reader.ReadLine();
			var values = line.Split(',');
			//Debug.Log (values [0]);
			//Debug.Log (values [1]);
			listA.Add(new Greeting(values[0].ToString(),int.Parse(values[1].ToString())));
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(GameController.Instance.state == GameController.STATE.MENU && !startBtnObj.activeSelf){
			startBtnObj.SetActive(true);
		}
	}

	public GameObject helpObj;
	public void Start1(){
		helpObj.SetActive(true);
	}

	public void StartGame(){
		helpObj.SetActive(false);
		GameController.Instance.PlayEnterKeySe();
		Debug.Log("StartGame");
		Game.queueingPerson = 0;
		GameController.Instance.state = GameController.STATE.START;
		startBtnObj.SetActive(false);
		SceneManager.LoadScene("Game", LoadSceneMode.Additive);
	}

	public void SpecerOnOff(){
		GameController.Instance.ChangeVolume();
	}
}
