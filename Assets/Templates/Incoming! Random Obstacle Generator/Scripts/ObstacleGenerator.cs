using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstacleGenerator : MonoBehaviour {
/* ========================================================================================================
 * 70:30 RANDOM OBSTACLE GENERATOR SCRIPT - created by D.Michalke / 70:30 / http://70-30.de / info@70-30.de
 * edit to your purpose as required!
 * ========================================================================================================
 */

	//put the obstacle generator on or off
	public bool activated = true;
	//internal bool to check if the obstacle generator is currently running
	private bool running;

	//the direction/speed the obstacles will be fired
	public Vector3 castDirectionAndSpeed;
	private Vector3 castDirectionSet;
	//if you want a speed variable to cast the obstacles at random, choose it here
	public enum randomCastSpeed { none = 0, xAxis = 1, yAxis = 2, zAxis = 3 }
	public randomCastSpeed randomSpeed = randomCastSpeed.none;
	//the position where the obstacles will be fired
	public Vector3 castPosition;
	private Vector3 castPositionSet;
	//if you want an axis to cast the obstacles at random, choose it here
	public enum randomCastPosition { none = 0, xAxis = 1, yAxis = 2, zAxis = 3 }
	public randomCastPosition randomPosition = randomCastPosition.none;
	//the rotation in which the obstacles will be fired
	public Vector3 castRotation;
	//if you want a random start rotation of the obstacle in any axis, define it here
	public enum randomCastRotation { none = 0, xAxis = 1, yAxis = 2, zAxis = 3 }
	public randomCastRotation randomRotation = randomCastRotation.none;
	//numbers of obstacles to cast in a wave
	public int numObstacles;
	//time to wait between obstacles
	public float waitBetweenObstacles;
	//the number of obstacle waves. setting it to 0 triggers infinite waves!
	public int numWaves; 
	private int actualWaves;
	//time to wait before the generator starts
	public float waitBeforeWaves;
	//time to wait between waves
	public float waitBetweenWaves;
	//the speed addition each wave for all axes. setting it to 0 disables the speed increase, leaving the speed as it is defined
	public Vector3 speedAdditionEachWave;

	//any amount of game objects can be assigned here as obstacle
	public GameObject[] obstacle;
	//the randomly picked obstacle to cast each shot
	private GameObject obstacleDiced;
	//to avoid having an obstacle cast too many times by chance, here 2 internal variables to settle it
	private int lastObstacleCount;
	private GameObject lastObstacleDiced;


	void Update () {


		//start the generator if it is set to active
		if (activated && !running)
		{
			StartCoroutine("ObstacleGeneration");
			running = true;
		}
		//stop the generator if it is not set to active
		if (!activated && running)
		{
			StopCoroutine("ObstacleGeneration");
			running = false;
		}
		//set wave number to fake infinite if it is 0
		if (numWaves == 0)
		{
			actualWaves = 99999999;
		}
		else
		{
			actualWaves = numWaves;
		}
	}

	//the actual generator
	IEnumerator ObstacleGeneration()
	{
		//wait the time defined before the waves start
		yield return new WaitForSeconds (waitBeforeWaves);
		//start to cast as many obstacles as defined before
		for (int w = 0; w < actualWaves; w++)
		{

			Debug.Log("Wave #"+(w+1));
			for (int i = 0; i < numObstacles; i++)
			{
				//pick a random obstacle of the assigned ones
				if (obstacle != null)
				{
					obstacleDiced = obstacle[Random.Range(0,obstacle.Length) ];
				}
				else
				{
					Debug.Log ("70:30 - No obstacles are assigned! Please assign obstacles in the ObstacleGenerator.");
				}

				//to avoid having one obstacle cast to many times by chance, let the obstacle generator check if the obstacle cast last time is the same as this time
				//if it has been cast 2 times already, it choses another obstacle
				if (lastObstacleDiced == obstacleDiced)
				{
					//set the counter +1
					lastObstacleCount += 1;
				}

				//check if it is bigger than or 2
				if (lastObstacleCount >= 2)
				{
					//choose a new obstacle as long as the last one cast equals the current one
					while (lastObstacleDiced == obstacleDiced)
					{
						//find random obstacle again
						obstacleDiced = obstacle[Random.Range(0,obstacle.Length) ];
						//reset the counter
						lastObstacleCount = 0;
					}
				}
				else
				{
					//reset the counter
					lastObstacleCount = 0;
				}

				//tell the obstacle generator which obstacle will be cast this time for the next cast
				lastObstacleDiced = obstacleDiced;

				//position the obstacle depending on your settings

				//set the position to cast
				if (randomPosition == randomCastPosition.none)
				{
					//if no random is selected, use the entered position values
					castPositionSet = castPosition;
				}
				if (randomPosition == randomCastPosition.xAxis)
				{
					//if random position at x axes is selected, randomize between + and - of the entered x value
					castPositionSet = new Vector3(Random.Range(-castPosition.x,castPosition.x),castPosition.y,castPosition.z);
				}
				if (randomPosition == randomCastPosition.yAxis)
				{
					//if random position at y axes is selected, randomize between + and - of the entered y value
					castPositionSet = new Vector3(castPosition.x,Random.Range(-castPosition.y,castPosition.y),castPosition.z);
				}
				if (randomPosition == randomCastPosition.zAxis)
				{
					//if random position at z axes is selected, randomize between + and - of the entered z value
					castPositionSet = new Vector3(castPosition.x,castPosition.y,Random.Range(-castPosition.z,castPosition.z));
				}

				//set the rotation 
				if (randomRotation == randomCastRotation.none)
				{
					//if no random is selected, use the entered rotation values
					obstacleDiced.transform.eulerAngles = castRotation;
				}
				if (randomRotation == randomCastRotation.xAxis)
				{
					//if random angle at x axes is selected, randomize between + and - of the entered x value
					obstacleDiced.transform.eulerAngles = new Vector3(Random.value*360,castRotation.y,castRotation.z);
				}
				if (randomRotation == randomCastRotation.yAxis)
				{
					//if random angle at y axes is selected, randomize between + and - of the entered y value
					obstacleDiced.transform.eulerAngles = new Vector3(castRotation.x,Random.value*360,castRotation.z);
				}
				if (randomRotation == randomCastRotation.zAxis)
				{
					//if random angle at z axes is selected, randomize between + and - of the entered z value
					obstacleDiced.transform.eulerAngles = new Vector3(castRotation.x,castRotation.y,Random.value*360);
				}

				Quaternion castRotationSet =  obstacleDiced.transform.rotation;

				//set the direction to cast
				if (randomSpeed == randomCastSpeed.none)
				{
					//if no random direction is selected, use the entered direction values
					castDirectionSet = castDirectionAndSpeed;
				}
				if (randomSpeed == randomCastSpeed.xAxis)
				{
					//if random direction at x axes is selected, randomize between + and - of the entered x value
					castDirectionSet = new Vector3(Random.value*castDirectionAndSpeed.x,castDirectionAndSpeed.y,castDirectionAndSpeed.z);
				}
				if (randomSpeed == randomCastSpeed.yAxis)
				{
					//if random direction at y axes is selected, randomize between + and - of the entered y value
					castDirectionSet = new Vector3(castDirectionAndSpeed.x,Random.value*castDirectionAndSpeed.y,castDirectionAndSpeed.z);
				}
				if (randomSpeed == randomCastSpeed.zAxis)
				{
					//if random direction at z axes is selected, randomize between + and - of the entered z value
					castDirectionSet = new Vector3(castDirectionAndSpeed.x,castDirectionAndSpeed.y,Random.value*castDirectionAndSpeed.z);
				}

				//instantiate the obstacle finally with set values
				GameObject obstacleCasted = Instantiate(obstacleDiced, castPositionSet, castRotationSet) as GameObject;


				//assign the obstacle given speed and direction
				obstacleCasted.GetComponent<Rigidbody>().AddForce(castDirectionSet);
				//obstacleDiced.rigidbody.velocity = castDirectionSet;

				//tell the obstacle generator that the coroutine is still running
				running = true;

				//wait until next obstacle will be cast
				yield return new WaitForSeconds (waitBetweenObstacles);
			}
			//wait until next wave will start
		yield return new WaitForSeconds (waitBetweenWaves);
		//add speed if set
		castDirectionAndSpeed += speedAdditionEachWave;
		}
	}

}
