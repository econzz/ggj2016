using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class PersonMove : MonoBehaviour {

	private float _speed;
	public float speed { get { return _speed; } set { _speed = value; } }

	private string _myWord = "hello";
	public string myWord { get { return _myWord; } set { _myWord = value; } }

	private bool _isClear = false;
	public bool isClear { get { return _isClear; } set { _isClear = value; } }

	private bool _isThrough = false;
	public bool isThrough { get { return _isThrough; } set { _isThrough = value; } }

	private float _queueingTime = 0.0f;
	public float queueingTime { get { return _queueingTime; } set { _queueingTime = value; } }

	public Text baloonChat;

	private Game gameController;

	private GameObject _wordFrezeObj;

	void Start(){
		
	}

	public void SetWordAndSpeed(string word, GameObject wordFrezeObj){
		this.myWord = word;
		if(GameController.Instance.mode != GameController.MODE.NEKO){
//			baloonChat.text = ""+this.myWord;
			baloonChat.text = "";
		}else{
			baloonChat.text = "";
		}

		float tempSpeed = Mathf.Floor (200 - (this.myWord.Length * 20.0f));

		if (tempSpeed <= 50.0f) {
			tempSpeed = 50.0f;
		}

		this.speed = tempSpeed;

//		this.speed = Mathf.Abs(40 + Mathf.Floor(300 - (this.myWord.Length * 20.0f)));

		Debug.Log ("my word is "+word+" my speed is "+this.speed);

		_wordFrezeObj = wordFrezeObj;
	}

	bool isOnceTimeFlag = false;
	public bool isCollided = false;

	void Update () {
		if (GameController.Instance.state == GameController.STATE.PLAY) {
			
			if (isThrough == false) {
				if (this.transform.position.x >= -300 && isCollided == false) {
					transform.Translate (new Vector3 (-1, 0, 0) * speed * Time.deltaTime);
				} else if(this.transform.position.x < -300) {

//					if (Game.queueingPerson == 0) {
//						Vector3 temp = this.transform.position;
//						temp.x = -300;
//						this.transform.position = temp;
//					} else if (Game.queueingPerson == 1) {
//						Vector3 temp = this.transform.position;
//						temp.x = -350;
//						this.transform.position = temp;
//					} else if (Game.queueingPerson == 2) {
//						Vector3 temp = this.transform.position;
//						temp.x = -400;
//						this.transform.position = temp;
//					}
					if (isClear == false) {
						if (isOnceTimeFlag == false) {
							Game.queueingPerson += 1;
							isOnceTimeFlag = true;
						}

						if (queueingTime > 0.0f) {
							Debug.Log ("myqueue time is " + queueingTime);
							queueingTime -= Time.deltaTime;
							if (queueingTime <= 0.0f) {
								Game.queueingPerson -= 1;
								if(Game.queueingPerson <= 0){
									Game.queueingPerson = 0;
								}
								isThrough = true;
							}
						} 
					}

				}
			} else if (isThrough == true) {
				if(_wordFrezeObj != null){
//					foreach(GameObject o in _wordFrezeObj.GetComponentsInChildren<GameObject>()){
//						Destroy(o);
//					}
					Destroy(_wordFrezeObj);
				}
				transform.Translate (new Vector3 (-1, 0, 0) * speed * Time.deltaTime);
			}
//
//
//			if (Game.queueingPerson == 0 && this.transform.position.x >=-300) {
//				transform.Translate(new Vector3(-1, 0, 0) * speed * Time.deltaTime);
//				Game.queueingPerson += 1;
//			}
//			if (Game.queueingPerson == 1 && this.transform.position.x >=-250) {
//				transform.Translate(new Vector3(-1, 0, 0) * speed * Time.deltaTime);
//				Game.queueingPerson += 1;
//			}
//			if (Game.queueingPerson == 2 && this.transform.position.x >=-200) {
//				transform.Translate(new Vector3(-1, 0, 0) * speed * Time.deltaTime);
//				Game.queueingPerson += 1;
//			}
		}
	
	}

	void OnCollisionEnter2D(Collision2D col){
		Debug.Log ("Collision with "+col.gameObject.name);
		//speed = 0;
		if (isOnceTimeFlag == false && isClear == false) {
			Game.queueingPerson += 1;
			isOnceTimeFlag = true;
		}
		isCollided = true;
	}

	void OnCollisionExit2D(Collision2D col){
		isCollided = false;
	}

}
