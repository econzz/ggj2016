using UnityEngine;
using System.Collections;

public class PersonDestroy : MonoBehaviour {

	private PersonSpawn spawner;

	void Start(){
		spawner = GameObject.Find ("PersonSpawner").GetComponent<PersonSpawn>();
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Person")
		{
			Destroy(col.gameObject);
			spawner.personList.Remove (col.gameObject.GetComponent<PersonMove> ());
		}
	}
}
