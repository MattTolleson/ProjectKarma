using UnityEngine;
using System.Collections;

/* =========================================================================================================
 * SHOW MENU STATE SCRIPT, ONLY FOR DEMO - created by D.Michalke / 70:30 / http://70-30.de / info@70-30.de
 * =========================================================================================================
 */

public class ShowMenuState : MonoBehaviour {

	void Update () 
	{
		gameObject.GetComponent<GUIText>().text = "Menu State: "+Seventythirty.state+" (State "+Seventythirty.phase+")";
	}
}
