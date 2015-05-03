using UnityEngine;
using System.Collections;

/// <summary> ##################################
/// 
/// NOTICE :
/// This script is a simple level loader.
/// 
/// DO NOT TOUCH UNLESS REQUIRED
/// 
/// </summary> ##################################

public class JXFLoadThisLevel : MonoBehaviour {
	public string sceneName = "name!"; // changable in the inspector
	
	void OnMouseUpAsButton(){
		Application.LoadLevel(sceneName); // loads the specified level when clicked
	}
}
