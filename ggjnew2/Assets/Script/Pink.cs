using UnityEngine;
using System.Collections;

public class Pink : MonoBehaviour {

	private float timer = 5.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if(timer <= 0.0f){
			gameObject.SetActive(false);
		}
	}
}
