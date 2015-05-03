using UnityEngine;
using System.Collections;

/* ==============================================================================================
 * 70:30 CONTROLLER SCRIPT - created by D.Michalke / 70:30 / http://70-30.de / info@70-30.de
 * This shared file is used for all 70:30 scripting templates to create a consistent menu system
 * and static variables that can be called from anywhere in unity.
 * please edit this file only if you need more menu states than the ones existing already.
 * ==============================================================================================
 */

public class Seventythirty : MonoBehaviour {

	//the identifier you will call from anywhere in the game. it sets the current menu state.
	public static string state = "intro";

	//the identifier that connects your menu states with the button-positions. only needed internally to make Shift! work.
	public static int phase = 0;

	//if you plan to have all buttons and objects move in the same speed, check it. if you plan to set all speeds individually, uncheck it.
	public bool useGlobalButtonSpeed;
	public static bool globalButtonSpeedActive;

	//if useGlobalButtonSpeed is set to true, this speed will be used for all buttons and objects you use with Shift!
	public float globalSpeed = 1f;
	public static float speed;

	//you can use these variables to integrate music/sound on/off button in your menu by setting them to 0 or 1
	public static int musicOn;
	public static int soundOn;

	//the menu slots that exist in your game. you will set them anywhere from the game with "state". you can modify them as you like or add additional ones.
	public string state0 = "intro";
	public string state1 = "menu_main";
	public string state2 = "menu_settings";
	public string state3 = "credits";
	public string state4 = "pause";
	public string state5 = "playing";
	public string state6 = "game_over";
	public string state7 = "quit_mainmenu";
	public string state8 = "quit_game";
	public string state9 = "menu_shop";
	public string state10 = "your_state1";
	public string state11 = "your_state2";
	public string state12 = "your_state3";
	public string state13 = "your_state4";
	public string state14 = "your_state5";
	public string state15 = "your_state6";
	public string state16 = "your_state7";
	public string state17 = "your_state8";
	public string state18 = "your_state9";
	public string state19 = "your_state10";
	public string state20 = "your_state11";

	//some internal 70:30 variables
	private int firstStart;

	void Update()
	{
		//here the states are connected to the positions. don't forget to add the lines if you add additional states over 20.
		if (state == state0){phase = 0;}
		if (state == state1){phase = 1;}
		if (state == state2){phase = 2;}
		if (state == state3){phase = 3;}
		if (state == state4){phase = 4;}
		if (state == state5){phase = 5;}
		if (state == state6){phase = 6;}
		if (state == state7){phase = 7;}
		if (state == state8){phase = 8;}
		if (state == state9){phase = 9;}
		if (state == state10){phase = 10;}
		if (state == state11){phase = 11;}
		if (state == state12){phase = 12;}
		if (state == state13){phase = 13;}
		if (state == state14){phase = 14;}
		if (state == state15){phase = 15;}
		if (state == state16){phase = 16;}
		if (state == state17){phase = 17;}
		if (state == state18){phase = 18;}
		if (state == state19){phase = 19;}
		if (state == state20){phase = 20;}
	}

	void Start()
	{
		//this is only used for internal setting of global/individual speed.
		speed = globalSpeed*0.1f;
		if (useGlobalButtonSpeed)
		{
			globalButtonSpeedActive = true;
		}
		else
		{
			globalButtonSpeedActive = false;
		}

		//check if the game is started the first time. if so, set music and sound to "on". for internal use only.
		firstStart = PlayerPrefs.GetInt("7030Firststart");
		if (firstStart == 0)
		{
			FirstStart();
		}
		else
		{
			//if the game has been started before, get the user preferences. for internal use only.
			musicOn = PlayerPrefs.GetInt("7030Music");
			soundOn = PlayerPrefs.GetInt("7030Sound");
		}
	}

	//happens when you start something with 70:30 templates for the first time. can be modified.
	void FirstStart()
	{
		//things that happen when the game is started for the first time
		//=============================================================
		musicOn = 1;
		soundOn = 1;
		PlayerPrefs.SetInt("7030Music",musicOn);
		PlayerPrefs.SetInt("7030Sound",soundOn);
		PlayerPrefs.Save();
		
		//=============================================================
		firstStart = 1;

	}

}
