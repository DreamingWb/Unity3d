using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 井字棋 : MonoBehaviour {
	private int turn = 1;
	private int[,] board = new int [3, 3];
	private int res = 0;
	// Use this for initialization
	void reset(){
		turn = 1;
		res = 0;
		for (int i = 0; i < 3; ++i)
			for (int j = 0; j < 3; ++j) board [i,j] = 0;
	}

	int check(){
		int count = 0;
		for (int i = 0; i < 3; i++) 
			for (int j = 0; j < 3; j++) if (board [i, j] != 0) count++;
		for (int i=0; i<3; ++i) if (board[i,0]!=0 && board[i,0]==board[i,1] && board[i,1]==board[i,2]) return board[i,0];    			  
		for (int j=0; j<3; ++j) if (board[0,j]!=0 && board[0,j]==board[1,j] && board[1,j]==board[2,j]) return board[0,j];          
		if (board[1,1]!=0 && (board[0,0]==board[1,1] && board[1,1]==board[2,2] || board[0,2]==board[1,1] && board[1,1]==board[2,0])) return board[1,1];    
		if (count == 9) return 3;
		return 0;
	}

	void Start () {
		reset ();
	}
	
	// Update is called once per frame
	void OnGUI(){
		GUIStyle Style = new GUIStyle();  
		Style.normal.background = null;   
		Style.normal.textColor= new Color(1, 0, 0);    
		Style.fontSize = 20;
		GUIStyle Style2 = new GUIStyle ();
		Style2.normal.background = null;   
		Style2.normal.textColor= new Color(1, 0, 0);    
		Style2.fontSize = 50;
		GUIStyle Style3 = new GUIStyle();   
		Style3.normal.textColor= new Color(0, 1, 0);    
		Style3.alignment = TextAnchor.MiddleCenter;
		Style3.fontSize = 40;
		GUIStyle Style4 = new GUIStyle();   
		Style4.normal.textColor= new Color(1, 0, 0);    
		Style4.alignment = TextAnchor.MiddleCenter;
		Style4.fontSize = 40;
		GUIStyle Style5 = new GUIStyle();  
		Style5.normal.background = null;   
		Style5.normal.textColor= new Color(0, 1, 0);    
		Style5.fontSize = 20;
		GUIStyle Style6 = new GUIStyle ();
		Style6.normal.background = null;   
		Style6.normal.textColor= new Color(0, 1, 0);    
		Style6.fontSize = 50;
		GUIStyle Style7 = new GUIStyle();  
		Style7.normal.background = null;   
		Style7.normal.textColor= new Color(0, 0, 0);    
		Style7.fontSize = 20;
		GUI.Label (new Rect (400, 50, 200, 50), "Welcome To TicTacToe",Style7);
		GUI.Label (new Rect (280, 25, 100, 50), "Turns:", Style7);
		GUI.Label (new Rect (280, 55, 100, 50), "Player1", Style5);
		GUI.Label (new Rect (280, 85, 100, 50), "Player2", Style);
		for (int i = 0; i < 3; ++i)
			for (int j = 0; j < 3; ++j) {
				if (board [i,j] == 0)
				if (GUI.Button (new Rect (430+50*i, 100+50*j, 50, 50), "")&&check()==0) {
					board [i, j] = turn;
					turn = 3 - turn;
				}
				if (board [i, j] == 1) GUI.Button (new Rect (430+50*i, 100+50*j, 50, 50), "o",Style3);
				if (board [i, j] == 2) GUI.Button (new Rect (430+50*i, 100+50*j, 50, 50), "x",Style4);
			}
		if (turn == 1) GUI.Label (new Rect (350, 40, 100, 50), "·", Style6);
		else GUI.Label (new Rect (350, 70, 100, 50), "·", Style2);
		if (GUI.Button (new Rect (280, 200, 100, 50), "Reset")) reset ();
		res = check ();
		if(res==1) GUI.Label (new Rect (280, 150, 100, 50), "Player 1 Win",Style7);
		else if(res==2) GUI.Label (new Rect (280, 150, 100, 50), "Player 2 Win",Style7);
		else if(res==3) GUI.Label (new Rect (280, 150, 100, 50), "Tie",Style7);
		else GUI.Label (new Rect (280, 150, 100, 50), "Playing...",Style7);
	}
}
