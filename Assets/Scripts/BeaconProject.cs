using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// BeaconTest-release.aar
/// </summary>

public class BeaconProject : MonoBehaviour {

	//固定UUID入出チェック
	public GameObject BeaconCheckObj;

	//	
	public GameObject BeaconLayoutObj;

	public GameObject BeaconGroup;

	private float time = 0.0f;

	private BeaconElemental beaconElemental = new BeaconElemental ();

	private int objCount = 0;

	private int objNumMax = 0;


	void Start () {
		CheckPermission ();

		AndroidJavaClass adjClass = new AndroidJavaClass("com.example.beaconlibrary.BeaconTestLibrary");
		adjClass.CallStatic("UpdateBeacon");

	}
	
		void Update () {
		timeset ();
	}

	public void OnClick(){

	}

	public void OnCheckLocation(string LocationBool){
		Debug.Log("ここロケーション " + LocationBool);
	}

	public void UpdateBeacon(string text){
		BeaconCheckObj.GetComponent<Text> ().text = text;

		if (text == "退場しました") {

			Debug.Log ("ここかも退場しました ");
			Debug.Log ("ここかもobjNumMax " + objNumMax);

			for (int j = 0; j < objNumMax; j++) {
					Debug.Log ("ここかも null ");

			if (BeaconGroup.transform.Find ("BeaconElement" + j.ToString ()) != null) {
				
				GameObject allBeacons = BeaconGroup.transform.Find ("BeaconElement" + j.ToString ()).gameObject;
					Destroy (allBeacons);
					objCount--; 
				}

		}
	}
	}


	/// <summary>
	/// aarファイルから返されるビーコン情報を受け取る関数
	/// </summary>
	/// <param name="json">Json.</param>
	public void BeaconAllData(string json){

		string jsonBeacon = json;

		if (jsonBeacon == "") {
			return;
		}


		//改行コードでビーコンの数を分ける
		string[] splitJson = jsonBeacon.Split ('\n');


		if(splitJson.Length == 0){
			//ビーコンの接続が切れた場合

			for (int j = 0; j < objCount; j++) {
				string[] splitJsoncomma = splitJson [0].Split (',');

				Debug.Log ("ここかもsplitJsoncomma " + splitJsoncomma);

				GameObject allBeacons = BeaconGroup.transform.Find ("BeaconElement" + j.ToString ()).gameObject;
				GameObject elementalBeacon = allBeacons.transform.Find ("UUIDGroup/UUIDText").gameObject;

				Debug.Log ("ここかもBeaconElement " + j);


				if (elementalBeacon.GetComponent<Text> ().text != splitJsoncomma [0]) {
					Destroy (allBeacons);
					objCount--;


				}

			}


		}
		//分けたビーコンの数、ループを回す
		for(int i = 0;i < splitJson.Length-1;i++){
			Debug.Log ("ここかも1 " + i.ToString() + " " + splitJson[i]);
			Debug.Log ("ここかも2 " + splitJson.Length);

			Debug.Log ("objCount " + objCount);

			if (objCount == splitJson.Length - 1) {
				for (int j = 0; j < objCount; j++) {
					Debug.Log ("ここかもj " + j);

					//jsonがカンマ区切りで情報を持っているので細分化
					string[] splitJsoncomma = splitJson [j].Split (',');

					GameObject allBeacons = BeaconGroup.transform.Find ("BeaconElement" + i.ToString ()).gameObject;
					GameObject elementalBeacon = allBeacons.transform.Find ("UUIDGroup/UUIDText").gameObject;

					//同じUUIDのビーコンは情報が更新されるたびに上書きする。
					if (elementalBeacon.GetComponent<Text> ().text == splitJsoncomma [0]) {
						
						BeaconElemental beaconEleIn = allBeacons.GetComponent<BeaconElemental> ();
						beaconEleIn.GetBeaconParameter (splitJson [j]);


					}
				}
			}else if(objCount > splitJson.Length - 1){
				//ビーコンの接続が切れた場合
					int objNum = objCount;
					for (int j = 0; j < objNum; j++) {
					string[] splitJsoncomma = splitJson [i].Split (',');

					Debug.Log ("ここかもsplitJsoncomma " + splitJsoncomma);

					GameObject allBeacons = BeaconGroup.transform.Find ("BeaconElement" + j.ToString ()).gameObject;
					GameObject elementalBeacon = allBeacons.transform.Find ("UUIDGroup/UUIDText").gameObject;

					Debug.Log ("ここかもBeaconElement " + j);


					if (elementalBeacon.GetComponent<Text> ().text != splitJsoncomma [0]) {
						Destroy (allBeacons);
						objCount--;

						Debug.Log ("ここかもsplitJson [i] " + splitJson [j]);

					}

				}


			} else {
				//ビーコンが追加された場合
					GameObject Beacons = GameObject.Instantiate (BeaconLayoutObj, new Vector3 (0.0f, 0.0f, 0.0f), Quaternion.identity) as GameObject;
					Beacons.transform.parent = BeaconGroup.transform;
					Beacons.name = "BeaconElement" + objCount.ToString ();
				Debug.Log ("ここかもBeacons.name " + Beacons.name);

					Beacons.transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);

					BeaconElemental beaconEle = Beacons.GetComponent<BeaconElemental> ();


					beaconEle.GetBeaconParameter (splitJson [i]);

					objCount++;
					Debug.Log ("objCount " + objCount);
				objNumMax++;

			}

		}


	}

	public void CheckPermission(){
		AndroidJavaClass adjClass = new AndroidJavaClass("com.example.beaconlibrary.BeaconTestLibrary");
		adjClass.CallStatic("_cl_checkPermission");

	}

	public void timeset(){

		if (time >= 60.0f) {

			AndroidJavaClass adjClass = new AndroidJavaClass("com.example.beaconlibrary.BeaconTestLibrary");
			adjClass.CallStatic("UpdateBeacon");


			time = 0.0f;
		} else {
			time += Time.time;
		}
	}
}
