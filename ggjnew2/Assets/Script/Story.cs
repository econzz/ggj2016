using UnityEngine;
using System.Collections;

public class Story : MonoBehaviour {

	public GameObject obj;
	public GameObject storyObj;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void AnimeEnd(){
		obj.SetActive(false);
		storyObj.SetActive(false);
		GameController.Instance.PlayMainBgm();
		gameObject.SetActive(false);
	}
}
