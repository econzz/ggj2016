using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class Game : MonoBehaviour {

	enum STR{
		A,
		B,
		C,
		D,
		E,
		F,
		G,
		H,
		I,
		J,
		K,
		L,
		M,
		N,
		O,
		P,
		Q,
		R,
		S,
		T,
		U,
		V,
		W,
		X,
		Y,
		Z,
	}

	enum STRN{
		A,
		B,
		C,
		D,
		E,
		F,
		G,
		H,
		I,
		J,
	}

	//
	public GameObject pinkObj;
	public GameObject pantsObj;
	public GameObject staffObj;
	public ParticleSystem particleEffect;
	//

	private List<ButtonController> clearBtns;
	public List<ButtonController> btns;
	public int maxX = 13;
	public int maxY = 8;
	public float high = 80.0f;
	public float width = 100.0f;
	public float bufx = 0.0f;
	public float buff = 0.0f;

	//
	public Image imagePref;
	public GameObject playerInputRoot;
	public List<GameObject> playerInputList;
	public GameObject enemyInputRoot;


	private int timeIndex;

	public GameObject btnPref;
	public GameObject rootObj;
	public Text text;
	public Text temp;

	//無敵
	private bool invincible = false;

	//Timer
	private float currentLife;
//	private float timer;
//	public float finishTime = 60.0f;
	public float maxLife = 100.0f;

	public float bonus = 5.0f;
	public Image gageImage;
	public Text timerText;

    //===========asano_changed===================
//	private GameObject bg;
    private GameObject cam;
    private int RandNum=1;
    private int StatePosX;
    //==============================

	private Text daynight;

	//Nelson
	private int _score;
	public int score { get { return _score; } set { _score = value; } }

	public Text textScore;

	private PersonSpawn spawnObj;

	void Awake() {
//		Debug.Log("Awake Game");
	}

	// Use this for initialization
	void Start () {
		text.text = "";

		//Reset Score
		GameController.Instance.score = 0;

		playerInputList = new List<GameObject>();
		clearBtns = new List<ButtonController>();
		btns = new List<ButtonController>();

		for(int y = 0; y < maxY; y++){
			for(int x = 0; x < maxX; x++){
				GameObject obj = (GameObject)GameObject.Instantiate(btnPref);
				obj.transform.SetParent(rootObj.transform);
				float setx = (float)x * width + bufx;
				float sety = (float)y * high + buff;
				obj.transform.SetLocalPosition(setx, sety, 0.0f);
				obj.SetActive(true);
				ButtonController ctr = obj.GetComponent<ButtonController>();
				ctr.Init(x, y, EnumCommon.Random<STR>().ToString(), this.SetList);

				btns.Add(ctr);
			}
		}

		timeIndex = 0;
		daynight = GameObject.Find ("DayNight").GetComponent<Text>();
		daynight.text = "Morning";

		currentLife = maxLife;

		//bg = GameObject.Find ("Background");
        cam = GameObject.Find("BG_Camera");

		if(GameController.Instance.state == GameController.STATE.START){
			GameController.Instance.state = GameController.STATE.PLAY;
		}

		spawnObj = GameObject.Find ("PersonSpawner").GetComponent<PersonSpawn>();
	}
	
	// Update is called once per frame
	void Update () {
		if(GameController.Instance.state != GameController.STATE.PLAY)
			return;

//		timer -= Time.deltaTime;
//		gageImage.fillAmount = (timer / finishTime);

//		if(timer <= 0.0f){
		if(currentLife <= 0.0f){
			Debug.Log("GameController.Instance.state:"+GameController.Instance.state);
			currentLife = 0.0f;
			GameController.Instance.state = GameController.STATE.GAMEOVER;
			//Debug.Log("Result Load");
			SceneManager.LoadScene("Result", LoadSceneMode.Additive);
		}
//		timerText.text = string.Format("{0}:{1:00}", Mathf.Floor(timer % 60f), ((timer % 1 * 100) >= 99.0f)? 99.0f: (timer % 1 * 100));
		timerText.text = ""+currentLife;

		timeIndex = ((int)(Time.time / 10)+1)%2;

//		if(timeIndex == 0)
//			daynight.text = "Morning";
//		else
//			daynight.text = "Evening";

		if (queueingPerson > 0) {
			counter -= Time.deltaTime;

			if (counter <= 0.0f) {
				counter = 1.0f;

				if(!invincible){
					this.currentLife -= damage * queueingPerson;
				}
				gageImage.fillAmount = (this.currentLife / maxLife);
			}

			if (queueingPerson >= 3) {

				if(!invincible){
					this.currentLife -= damage * 15;
					gageImage.fillAmount = (this.currentLife / maxLife);
				}
				queueingPerson = 0;
				foreach (PersonMove tempPerson in spawnObj.personList) {
					tempPerson.isThrough = true;
				}
			}
		}

        //===========asano_changed===================
/*
		bg.GetComponent<Transform> ().AddPositionX(-Time.deltaTime*200);
		if (bg.GetComponent<Transform> ().position.x < -1600)
			bg.GetComponent<Transform> ().SetPositionX(0);
*/
        cam.GetComponent<Transform>().AddPositionX(Time.deltaTime * 2);
        switch (RandNum)
        {
            case 1:
//                Debug.Log(RandNum);
                if (cam.GetComponent<Transform>().position.x > 40)
                {
                    RandNum = Random.Range(1, 3);
                    if (RandNum == 1)
                        cam.GetComponent<Transform>().SetPositionX(0);
                    else
                        cam.GetComponent<Transform>().SetPositionX(80);
                }
                break;
            case 2:

//                Debug.Log(RandNum);
                if (cam.GetComponent<Transform>().position.x > 120||cam.GetComponent<Transform>().position.x<80)
                {
                    RandNum = Random.Range(1, 3);
                    if (RandNum == 1)
                        cam.GetComponent<Transform>().SetPositionX(0);
                    else 
                        cam.GetComponent<Transform>().SetPositionX(80);
                }
                break;
        }
        //==============================

		if (spawnObj.personList.Count > 0) {
			foreach (PersonMove obj in spawnObj.personList) {
				obj.gameObject.transform.GetChild (2).gameObject.SetActive (true);
				break;
			}
		}

    }

	public void SetList(ButtonController _ctr, string s, bool isActive){

		if(GameController.Instance.mode == GameController.MODE.NEKO){
			int seNo = clearBtns.Count;
			if(clearBtns.Count >= 3){
				seNo = clearBtns.Count % 3;
			}
			Debug.Log("seNo:"+seNo);
			GameController.Instance.PlayCatClickSe(seNo);
		}else{
			int seNo = clearBtns.Count;
			if(clearBtns.Count >= 7){
				seNo = clearBtns.Count % 7;
			}
			Debug.Log("seNo:"+seNo);
			GameController.Instance.PlayClickSe(seNo);
		}

		foreach(GameObject obj in playerInputList){
			Destroy(obj);
		}
		playerInputList.Clear();

		if(isActive){
			clearBtns.Add(_ctr);
		}else{
			clearBtns.Remove(_ctr);
		}
		string m = "";
		foreach(ButtonController ctr in clearBtns){
			m += ctr._s;

			GameObject obj = (GameObject)GameObject.Instantiate(imagePref.gameObject);
			string pref = "Moji_";
			if(GameController.Instance.mode == GameController.MODE.NEKO){
				pref = "MojiN_";
			}
			obj.GetComponent<Image>().sprite = (Sprite)Resources.Load<Sprite>(pref+ctr._s);
			obj.transform.SetParent(playerInputRoot.transform);
			obj.SetActive(true);

			playerInputList.Add(obj);
		}
//		text.text = m;
	}

	public float damage = 1;

	public static int queueingPerson = 0;

	private float counter = 1.0f;


	public void BackSpace(){
		GameController.Instance.PlayBackspaceKeySe();

		if(clearBtns.Count > 0){
			int no = clearBtns.Count -1;
			clearBtns[no].Click();
		}
	}

	public void Enter(){
		GameController.Instance.PlayEnterKeySe();

		bool isGoodWords = false;

		Debug.Log("Enter");

		//選択オブジェクトから、文字列チェック
		string m = "";
		foreach(ButtonController ctr in clearBtns){
			m += ctr._s;
		}
		foreach(GameObject obj in playerInputList){
			Destroy(obj);
		}
		playerInputList.Clear();


		bool isValidWords = false;
		foreach (Greeting g in Top.listA) {
			//Debug.Log (g.word);
//			if (m.Equals (g.word) && (g.time==1 || (timeIndex+2) == g.time)) {
			if (m.Equals (g.word) && (g.time==1 || (timeIndex+2) == g.time)) {
				isValidWords = true;
				GameController.Instance.PlaySuccessSe();
			}
		}

		switch(m){
		case "STAFF":
			staffObj.SetActive(true);
			break;
		case "NEKO":
			foreach(ButtonController ctr in btns){
				Destroy(ctr.gameObject);
			}
			btns.Clear();
			GameController.Instance.mode = GameController.MODE.NEKO;
			for(int y = 0; y < maxY; y++){
				for(int x = 0; x < maxX; x++){
					GameObject obj = (GameObject)GameObject.Instantiate(btnPref);
					obj.transform.SetParent(rootObj.transform);
					float setx = (float)x * width + bufx;
					float sety = (float)y * high + buff;
					obj.transform.SetLocalPosition(setx, sety, 0.0f);
					obj.SetActive(true);
					ButtonController ctr = obj.GetComponent<ButtonController>();
					ctr.Init(x, y, EnumCommon.Random<STRN>().ToString(), this.SetList);
					btns.Add(ctr);
				}
			}
			var reader = new System.IO.StreamReader(System.IO.File.OpenRead(Application.streamingAssetsPath + "/greetingn.csv"));
			Top.listA = new List<Greeting>();
			while (!reader.EndOfStream)
			{
				var line = reader.ReadLine();
				var values = line.Split(',');
				Top.listA.Add(new Greeting(values[0].ToString(),int.Parse(values[1].ToString())));
			}
			break;
		case "PANTS":
			pantsObj.SetActive(true);
			break;
		case "DEBUG":
			Debug.Log("Debug!!!!!!!!!!!!");
			currentLife = maxLife;
			invincible = !invincible;
			break;
		case "exclamation":
			Debug.Log("exclamation!!!!!!!!!!!!");
			break;
		case "facefaceface":
			Debug.Log("face!!!!!!!!!!!!");
			break;
		case "PINK":
			pinkObj.SetActive(true);
			Debug.Log("PINK!!!!!!!!!!!!");
			break;
		}



		if (isValidWords == false) {
//			foreach (PersonMove person in spawnObj.personList) {
//				if (person.isThrough == false && person.isClear == false) {
//					person.isThrough = true;
//					queueingPerson -= 1;
//					if(queueingPerson <= 0){
//						queueingPerson = 0;
//					}
//
//					//damage
//					if(!invincible){
//						this.currentLife -= damage;
//					}
//					gageImage.fillAmount = (this.currentLife / maxLife);
//					person.isCollided = false;
//					break;
//				}
//			}
		} else if (isValidWords == true) {
			PersonMove toDestroy = null;
			foreach (PersonMove person in spawnObj.personList) {
				Debug.Log ("my word is "+person.myWord+" typed word is "+m);
				if (person.isClear == false) {
					person.isCollided = false;
					person.isClear = true;
					person.isThrough = true;
					if (m.Equals (person.myWord.ToUpper())) {
						Debug.Log ("OK");

						isGoodWords = true;
						queueingPerson -= 1;
						if(queueingPerson <= 0){
							queueingPerson = 0;
						}
						this.currentLife += 5;
						if (this.currentLife >= maxLife) {
							this.currentLife = maxLife;
						}
						gageImage.fillAmount = (this.currentLife / maxLife);
						toDestroy = person;
						toDestroy.transform.GetChild (2).gameObject.SetActive (false);
						particleEffect.gameObject.SetActive (false);
						particleEffect.gameObject.SetActive (true);
//						particleEffect.time = 0;
//						particleEffect.Play();
//						this.particleEffect.Play ();
//						this.particleEffect.Emit (100);
//						this.particleEffect.GetComponent<MeshParticleEmitter> ().enabled = true;
						break;
					} else {
						//not the same as what he/she says but has dictionary
						//calculate
						queueingPerson -= 1;
						if(queueingPerson <= 0){
							queueingPerson = 0;
						}

						Debug.Log ("current queue "+queueingPerson);
						float tempDiff = person.myWord.Count() - m.Count();

						if(tempDiff >= 0.0f){
							person.queueingTime = 0.5f * tempDiff;

						}
						toDestroy = person;
						toDestroy.transform.GetChild (2).gameObject.SetActive (false);
						break;
					}


				}

			}

			spawnObj.personList.Remove (toDestroy);

			int multiplier = 1;
			if (isGoodWords) {
				multiplier = 10;
			}
				

			this.AddScore ((m.Count() * (10 * multiplier)));
		}
			
//		foreach (Greeting g in Top.listA) {
////			Debug.Log (g.word);
//			if (m.Equals (g.word) && (g.time==1 || (timeIndex+2) == g.time)) {
//				isGoodWords = true;
//				temp.text = m;
////				timer += m.Length;
////				if (timer > finishTime)
////					timer = finishTime;
//			}
//		}

		//選択オブジェクト削除
		Dictionary<int, int> delCount = new Dictionary<int, int>();
		foreach(ButtonController ctr in clearBtns){
			if(btns.Contains(ctr)){
				if(delCount.ContainsKey(ctr._x)){
					delCount[ctr._x]++;
				}else{
					delCount.Add(ctr._x, 1);
				}
				btns.Remove(ctr);
				Destroy(ctr.gameObject);
			}
		}
		clearBtns.Clear();

		//新規ボタン追加作成
		foreach(int key in delCount.Keys){
			int newCount = delCount[key];
			for(int n = 0; n < newCount; n++){
				Debug.Log("n:"+n);
				GameObject obj = (GameObject)GameObject.Instantiate(btnPref);
				obj.transform.SetParent(rootObj.transform);
				float setx = (float)key * width + bufx;
				float sety = (float)n * high + buff + 640.0f;
				obj.transform.SetLocalPosition(setx, sety, 0.0f);
				obj.SetActive(true);
				ButtonController ctr = obj.GetComponent<ButtonController>();
				if(GameController.Instance.mode == GameController.MODE.NEKO){
					ctr.Init(key, maxY + n, EnumCommon.Random<STRN>().ToString(), this.SetList);
				}else{
					ctr.Init(key, maxY + n, EnumCommon.Random<STR>().ToString(), this.SetList);
				}
				btns.Add(ctr);
			}
		}

		//新規座標入れ直し
		foreach(int key in delCount.Keys){
			List<ButtonController> picks = btns.Where( t => t._x == key).OrderBy( s => s._y).ToList();
			int n = 0;
			foreach(ButtonController ctr in picks){
//				Debug.Log("old:"+ctr._y+" n:"+n);
				ctr._newy = n;
				n++;
			}
		}

		foreach(ButtonController ctr in btns){
			if(ctr._y != ctr._newy){
				float posy = (float)ctr._newy * high + buff;
				iTween.MoveTo(ctr.gameObject, iTween.Hash("islocal", true, "y", posy, "time", 1.0f, "easeType", iTween.EaseType.easeOutBounce));
				ctr._y = ctr._newy;
			}
		}
		text.text = "";



	}

	private void AddScore(int value){
//		this.score += value;
		GameController.Instance.score += value;

		this.textScore.text = ""+GameController.Instance.score;
	}
}