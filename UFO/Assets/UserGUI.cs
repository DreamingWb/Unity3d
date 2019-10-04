using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour
{
	private IUserAction action;
	public int life = 6;
	GUIStyle bold_style = new GUIStyle();
	GUIStyle text_style = new GUIStyle();     
	GUIStyle text_style2 = new GUIStyle();     

	void Start ()
	{
		action = SSDirector.GetInstance().CurrentSceneControllor as IUserAction;
	}

	void OnGUI ()
	{
		text_style.normal.textColor = new Color(1,1,1, 1);
		text_style.fontSize = 16;
		text_style2.normal.textColor = new Color(1,1,1, 1);
		text_style2.fontSize = 100;
		if (action.isCounting ()) {
			GUI.Label(new Rect(Screen.width / 2 - 40, Screen.width / 2 - 300, 50, 50), action.getEmitTime().ToString(), text_style2);
		} else {
			if (Input.GetButtonDown("Fire1"))
			{
				Vector3 pos = Input.mousePosition;
				action.Hit(pos);
			}
			GUI.Label(new Rect(10, 5, 200, 50), "score:", text_style);
			GUI.Label(new Rect(55, 5, 200, 50), action.GetScore().ToString(), text_style);
			GUI.Label(new Rect(10, 30, 50, 50), "hp:", text_style);
			for (int i = 0; i < life; i++)
			{
				GUI.Label(new Rect(40 + 10 * i, 30, 50, 50), "X", text_style);
			}
			if (life == 0)
			{
				GUI.Label(new Rect(Screen.width / 2 - 50, Screen.width / 2 - 300, 100, 100), "GameOver!", text_style);
				if (GUI.Button(new Rect(Screen.width / 2 - 60, Screen.width / 2 - 250, 100, 50), "Restart"))
				{
					action.Restart();
					return;
				}
				action.GameOver();
			}	
		}
	}
	public void ReduceBlood()
	{
		if(life > 0)
			life--;
	}
}