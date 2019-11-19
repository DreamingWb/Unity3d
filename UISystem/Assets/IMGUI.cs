using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IMGUI : MonoBehaviour {
	public float health;
	private float res;
	public Slider healthSlider;
	void Start(){
		health = 1.0f;
		res = 1.0f;
	}
	void OnGUI(){
		if (GUI.Button (new Rect(250, 20, 40, 20), "+")) {
			if (res + 0.2f > 1.0f) res = 1.0f;
			else res = res + 0.2f;
		}
			if (GUI.Button (new Rect(250, 50, 40, 20), "-")) {
			if (res - 0.2f < 0.0f) res = 0.0f;
			else res = res - 0.2f;
		}
		health = Mathf.Lerp(health, res, 0.05f);
		GUI.color = Color.red;
		GUI.HorizontalScrollbar(new Rect(20, 20, 200, 20), 0.0f, health, 0.0f, 1.0f);
		healthSlider.value = health*100;
	}
}
