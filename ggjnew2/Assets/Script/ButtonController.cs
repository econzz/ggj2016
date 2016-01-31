using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ButtonController : MonoBehaviour {
	public int _x;
	public int _y;
	public int _newy;
	public string _s;
	public Text text;

	private bool isOn = false;
	private Button btn;
	private Image image;
	private Action<ButtonController, string, bool> _callback;

	void Awake() {
		btn = GetComponent<Button>();
		image = GetComponent<Image>();
	}
	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {

	}

	public void Init(int x, int y, string s, Action<ButtonController, string, bool> callback){
		_x = x;
		_y = _newy = y;
		_s = s;
		text.text = s;
		btn = GetComponent<Button>();
		image = GetComponent<Image>();
		string pref = "Moji_";
		if(GameController.Instance.mode == GameController.MODE.NEKO){
			pref = "MojiN_";
		}
		btn.image.sprite = (Sprite)Resources.Load<Sprite>(pref+s);
		isOn = false;
		_callback = callback;
	}

	public void Click(){
		if(GameController.Instance.state != GameController.STATE.PLAY)
			return;

		Debug.Log("Click");
		isOn = !isOn;
		if(isOn){
			image.color = Color.red;
		}else{
			image.color = Color.white;
		}
		_callback(this, _s, isOn);
	}
}
