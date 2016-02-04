using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Test : MonoBehaviour {
	void Update() {
		Debug.Log("Update");
		if (Controller.GetLastButton() != KeyCode.None) {
			GetComponent<TextMesh>().text = Controller.GetLastButton().ToString();
			Debug.Log(Controller.GetLastButton().ToString());
		} else {
			GetComponent<TextMesh>().text = "Press Any Key...";
		}
	}
}