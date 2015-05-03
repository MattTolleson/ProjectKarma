using UnityEngine;
using System.Collections;

public class incomingGUI : MonoBehaviour {
/* ========================================================================================================
 * 70:30 DEMO GUI SCRIPT - created by D.Michalke / 70:30 / http://70-30.de / info@70-30.de
 * created to show an example of Incoming!
 * ========================================================================================================
 */

	float sw = Screen.width;
	float sh = Screen.height;
	Rect button1;
	Rect button2;
	Rect button3;
	Rect button4;
	Rect button5;
	Rect button6;
	Rect label3;
	Rect label4;
	Rect label5;
	Rect label6;
	//get the obstacle generator
	GameObject ObstacleGenerator;
	ObstacleGenerator obstacleGenerator; 
	//some temporary variables for changing the generator settings
	private float speed;
	private float position;
	private float rotation;
	private float interval;
	
	void Start () 
	{
		//definition of the button rects
		button1 = new Rect(sw*0.1f,sh*0.1f,sw*0.2f,sh*0.1f);
		button2 = new Rect(sw*0.1f,sh*0.25f,sw*0.2f,sh*0.1f);

		button3 = new Rect(sw*0.1f,sh*0.45f,sw*0.2f,sh*0.1f);
		button4 = new Rect(sw*0.1f,sh*0.60f,sw*0.2f,sh*0.1f);
		button5 = new Rect(sw*0.1f,sh*0.75f,sw*0.2f,sh*0.1f);
		button6 = new Rect(sw*0.1f,sh*0.90f,sw*0.2f,sh*0.1f);
		label3 = new Rect(sw*0.1f,sh*0.4f,sw*0.2f,sh*0.1f);
		label4 = new Rect(sw*0.1f,sh*0.55f,sw*0.2f,sh*0.1f);
		label5 = new Rect(sw*0.1f,sh*0.70f,sw*0.2f,sh*0.1f);
		label6 = new Rect(sw*0.1f,sh*0.85f,sw*0.2f,sh*0.1f);

		//intro state is not needed here. let's just advance to the main menu
		if (Seventythirty.state == "intro")
		{
			Seventythirty.state = "menu_main";
		}

		//get the obstacle generator script
		ObstacleGenerator = GameObject.Find ("ObstacleGenerator");
		obstacleGenerator = ObstacleGenerator.GetComponent<ObstacleGenerator>();
		//lets turn the generator off first
		obstacleGenerator.activated = false;

		//setting the default values of the adjustment sliders
		speed = -100;
		position = 4;
		rotation = 0;
		interval = 1;
	}


	//the demo GUI
	void OnGUI () 
	{
		//main menu
		if (Seventythirty.state == "menu_main"){
			if (GUI.Button(button1,"Activate Generator"))
			{
				Seventythirty.state = "playing";

			}
			if (GUI.Button(button2,"Pause Generator"))
			{
				Seventythirty.state = "pause";

			}
		}

		//active
		if (Seventythirty.state == "playing"){
			if (GUI.Button(button1,"Deactivate Generator"))
			{
				Seventythirty.state = "menu_main";
				
			}
			if (GUI.Button(button2,"Pause Generator"))
			{
				Seventythirty.state = "pause";
				
			}
		}

		//pause
		if (Seventythirty.state == "pause"){
			if (obstacleGenerator.activated)
			{
				if (GUI.Button(button1,"Deactivate Generator"))
				{
					Seventythirty.state = "menu_main";
				}
			}
			else
			{
				if (GUI.Button(button1,"Activate Generator"))
				{
					Seventythirty.state = "playing";
				}
			}
			if (GUI.Button(button2,"Unpause Generator"))
			{
				Time.timeScale = 1;
				Seventythirty.state = "playing";
			}
		}

		//settings menu
		if (Seventythirty.state == "menu_main" || Seventythirty.state == "pause" || Seventythirty.state == "playing")
		{
			GUI.Label (label3,"Speed adjust");
			speed = GUI.HorizontalSlider(button3,speed,-50,-500);
			obstacleGenerator.castDirectionAndSpeed = new Vector3(obstacleGenerator.castDirectionAndSpeed.x,speed,obstacleGenerator.castDirectionAndSpeed.z);

			GUI.Label (label4,"Interval adjust");
			interval = GUI.HorizontalSlider(button4,interval,0.1f,5);
			obstacleGenerator.waitBetweenObstacles = interval;

			GUI.Label (label5,"Position adjust");
			position = GUI.HorizontalSlider(button5,position,1,6);
			obstacleGenerator.castPosition = new Vector3(position,obstacleGenerator.castPosition.y,obstacleGenerator.castPosition.z);

			GUI.Label (label6,"Rotation adjust");
			rotation = GUI.HorizontalSlider(button6,rotation,0,359);
			obstacleGenerator.castRotation = new Vector3(obstacleGenerator.castRotation.x,obstacleGenerator.castRotation.y,rotation);
		}


	}


	void Update()
	{
		//configuring some GUI settings for internal use
		if (Seventythirty.state == "playing" && !obstacleGenerator.activated)
		{
			obstacleGenerator.activated = true;
			Time.timeScale = 1;
		}
		if (Seventythirty.state == "menu_main" && obstacleGenerator.activated)
		{
			obstacleGenerator.activated = false;
			Time.timeScale = 1;
		}
		if (Seventythirty.state == "pause")
		{
			Time.timeScale = 0;
		}
	}
}
