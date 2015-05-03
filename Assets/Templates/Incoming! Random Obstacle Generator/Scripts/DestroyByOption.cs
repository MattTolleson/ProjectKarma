using UnityEngine;
using System.Collections;

public class DestroyByOption : MonoBehaviour {
/* ========================================================================================================
 * 70:30 INCOMING / DESTROY BY OPTION SCRIPT - created by D.Michalke / 70:30 / http://70-30.de / info@70-30.de
 * edit to your purpose as required!
 * ========================================================================================================
 */
	//options
	//flag to destroy obstacle if invisible
	public bool destroyOutOfScreen;
	//flag to destroy obstacle if colliding with something
	public bool destroyByCollision;
	//flag if destruction of obstacle should be triggerd after an amount of time. if time is 0, it is disabled
	public float destroyByTime;
	//destroy obstacle by an existing boundary object. enter the name of the object and the obstacle will be destroyed if it leaves the boundary
	public string nameOfDestroyBoundary;

	//destroy by time
	void Start () 
	{
		if (destroyByTime > 0)
		{
			DestroyByTime();
		}
	}

	//destroy by out-of-screen
	void Update () 
	{
		bool visible = GetComponent<Renderer>().isVisible;
		if (!visible && destroyOutOfScreen)
		{
			Destroy (gameObject);
		}
	}

	//destroy by collision
	void OnTriggerEnter(Collider other)
	{
		if (destroyByCollision)
		{
			Destroy (gameObject);
		}
	}

	//destroy by boundary exit
	void OnTriggerExit(Collider other)
	{
		if (nameOfDestroyBoundary != "" && other.gameObject.name == nameOfDestroyBoundary)
		{
			Destroy (gameObject);
		}
	}

	//destroy by time function
	void DestroyByTime()
	{
		Destroy (gameObject,destroyByTime);
	}
}
