using UnityEngine;
using System;
using System.Collections;

public abstract class SingletonMonoBehaviourFast<T> : MonoBehaviour where T : SingletonMonoBehaviourFast<T> {
	protected static readonly string[] findTags = {
		"GameController",
		"GameManager",
	};
	protected static T instance;

	public static T Instance {
		get {
			if(instance == null) {
				
				Type type = typeof(T);
				
				foreach(var tag in findTags) {
					GameObject[] objs = GameObject.FindGameObjectsWithTag(tag);
					
					for(int j = 0; j < objs.Length; j++) {
						instance = (T)objs[j].GetComponent(type);
						if(instance != null) return instance;
					}
				}
				
				Debug.LogWarning(string.Format("{0} is not found", type.Name));
			}
			
			return instance;
		}
	}

	virtual protected void Awake() {
		CheckInstance();
	}

	protected bool CheckInstance() {
		if(instance == null) {
			Debug.Log("Inst is null:" + this.name);
			instance = (T)this;
			return true;
		} else if(Instance == this) {
			Debug.Log("Inst is this:" + this.name);
			return true;
		}
		Debug.Log("Inst is destroy:" + this.name);
		
		Destroy(this);
		return false;
	}
}