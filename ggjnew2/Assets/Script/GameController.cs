using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : SingletonMonoBehaviourFast<GameController> {
	
	public enum STATE{
		OPNING,
		MENU,
		START,
		PLAY,
		GAMEOVER,
		RESULT,
	}

	public AudioSource audioSource1;
	public AudioSource audioSource2;
	public List<AudioClip> clickSes;
	public List<AudioClip> catClickSes;

	public AudioClip mainBgm;
	public AudioClip opBgm;

	public AudioClip backspaceKeySe;
	public AudioClip enterKeySe;
	public AudioClip enterFailSe;
	public AudioClip enterSuccessSe;
	public AudioClip gameoverSe;


	public void PlayMainBgm(){
		audioSource1.clip = mainBgm;
		audioSource1.Play();
	}

	public void PlayEnterKeySe(){
		audioSource2.PlayOneShot(enterKeySe);
	}
	public void PlayBackspaceKeySe(){
		audioSource2.PlayOneShot(backspaceKeySe);
	}
	public void PlayFailSe(){
		audioSource2.PlayOneShot(enterFailSe);
	}
	public void PlaySuccessSe(){
		audioSource2.PlayOneShot(enterSuccessSe);
	}
	public void PlayGameOverSe(){
		audioSource2.PlayOneShot(gameoverSe);
	}

	public STATE state = STATE.MENU;
	public bool isMute = false;
	public GameObject muteImageObj;

	public enum MODE{
		NOMAL,
		NEKO,
	}
	public MODE mode = MODE.NOMAL;

	//Score
	public int score;
	public int bestScore;

	override protected void Awake ()
	{
		Debug.Log ("Awake");

		//60FPS
		Application.targetFrameRate = 60;

		//Check Instance
		if (!CheckInstance ()) {
			Destroy (gameObject);
			return;
		}
		DontDestroyOnLoad (gameObject);
	}

	// Use this for initialization
	void Start () {
		isMute = false;
		int mute = PlayerPrefs.GetInt("isMute", 0);
		if(mute > 0){
			this.ChangeVolume();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(isMute && !muteImageObj.activeSelf){
			muteImageObj.SetActive(true);
		}
		if(!isMute && muteImageObj.activeSelf){
			muteImageObj.SetActive(false);
		}
	}

	public void PlayClickSe(int no){
		audioSource2.PlayOneShot(clickSes[no]);
	}


	public void PlayCatClickSe(int no){
		audioSource2.PlayOneShot(catClickSes[no]);
	}

	public void ChangeVolume(){
		isMute = !isMute;
		if(isMute){
			audioSource1.volume = 0.0f;
			audioSource2.volume = 0.0f;
			PlayerPrefs.SetInt("isMute", 1);
		}else{
			audioSource1.volume = 1.0f;
			audioSource2.volume = 1.0f;
			PlayerPrefs.SetInt("isMute", 0);
		}
		PlayerPrefs.Save();
	}
}
