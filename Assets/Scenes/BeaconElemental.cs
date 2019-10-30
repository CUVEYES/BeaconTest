using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class BeaconElemental : MonoBehaviour {

	//以下、取得したデータを表示する箇所
	public GameObject UUIDObj;

	public GameObject MajorObj;

	public GameObject MinorObj;

	public GameObject RSSIObj;

	public GameObject TxPowerObj;

	public GameObject DistanceObj;

	public GameObject TimeObj;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void GetBeaconParameter(string text){

		if (text == "") {
			return;
		}

		string[] splitJson = text.Split (',');


		UUIDObj.GetComponent<Text> ().text = splitJson [0];
		Debug.Log ("こここ1 " + splitJson[0]);
		MajorObj.GetComponent<Text> ().text = splitJson [1];
		Debug.Log ("こここ2 " + splitJson[1]);

		MinorObj.GetComponent<Text> ().text = splitJson [2];
		Debug.Log ("こここ3 " + splitJson[2]);

		RSSIObj.GetComponent<Text> ().text = splitJson [3];
		Debug.Log ("こここ4 " + splitJson[3]);

		TxPowerObj.GetComponent<Text> ().text = splitJson [4];
		Debug.Log ("こここ5 " + splitJson[4]);

		DistanceObj.GetComponent<Text> ().text = splitJson [5];
		Debug.Log ("こここ6 " + splitJson[5]);

		DateTime time = DateTime.Now;

		TimeObj.GetComponent<Text> ().text = time.ToString();
		Debug.Log ("こここ7 " + time.ToString());



	}

	public void Destroy(){
		Destroy (this);
	}
}
