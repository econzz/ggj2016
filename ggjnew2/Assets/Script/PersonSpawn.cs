using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PersonSpawn : MonoBehaviour {

	public GameObject[] persons;
	public float delayTimer;
	private float timer = 0;
	private int personNo = 0;

	public List<PersonMove> personList = new List<PersonMove>();

	private int counter = 5;

	public GameObject freamRootObj;
	public GameObject wordRootObj;
	public GameObject wordPrefObj;

	void Update () {
		
		timer -= Time.deltaTime;

		if (timer <= 0) {
			Vector3 personPos = new Vector3 (transform.position.x, transform.position.y, transform.position.z);

			personNo = Random.Range(0, persons.Length - 1);
			GameObject person = Instantiate (persons[personNo], personPos, transform.rotation) as GameObject;
			person.transform.parent = GameObject.Find ("GameCanvas").transform;
			timer = delayTimer;

			PersonMove temp = person.GetComponent<PersonMove> ();
			int indexWord = Random.Range(0,Top.listA.Count-1);

			if (counter > 0) {
				do {
					indexWord = Random.Range (0, Top.listA.Count - 1);


				} while(Top.listA [indexWord].word.Length > 4);
			}

			GameObject rootObj = (GameObject)GameObject.Instantiate(wordRootObj);
			rootObj.transform.SetParent(freamRootObj.transform);
			rootObj.transform.SetLocalPosition(-800.0f, 50.0f, 0.0f);
			rootObj.SetActive(true);

			string word = Top.listA [indexWord].word;
			foreach(char c in word){
				GameObject wordObj = (GameObject)GameObject.Instantiate(wordPrefObj);
				string pref = "Moji_";
				if(GameController.Instance.mode == GameController.MODE.NEKO){
					pref = "MojiN_";
				}
				wordObj.GetComponent<Image>().sprite = (Sprite)Resources.Load<Sprite>(pref+c.ToString());
				wordObj.transform.SetParent(rootObj.transform);
				wordObj.SetActive(true);
			}
			rootObj.transform.SetSiblingIndex(0);

			temp.SetWordAndSpeed (Top.listA [indexWord].word, rootObj);

			personList.Add (temp);

			if (counter > 0) {
				counter -= 1;
			}

		}
	
	}
}
