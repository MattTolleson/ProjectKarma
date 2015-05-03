using UnityEngine;
using System.Collections;


/// <summary> ##################################
/// 
/// NOTICE :
/// This script is just a simple delegate to announce to GameManager
/// on which piece is being dragged and dragged towards which piece.
/// 
/// DO NOT TOUCH UNLESS REQUIRED
/// 
/// </summary> ##################################

public class PanelTracker : MonoBehaviour {
	
	[HideInInspector] public GameManager gm {get{return JMFUtils.gm;}}
	[HideInInspector] public int[] arrayRef = new int[2]; // a tracker to keep note on which board this piece belongs too..
	int x {get{return arrayRef[0];}} // easy reference for arrayRef[0]
	int y {get{return arrayRef[1];}} // easy reference for arrayRef[1]

	void OnEnable(){
		BoxCollider bc = gameObject.AddComponent<BoxCollider>();

		switch(gm.boardType){ // to autoscale the box collider... depending on board type
		case BoardType.Square : default :
			JMFUtils.autoScale(gameObject);
			break;
		case BoardType.Hexagon :
			JMFUtils.autoScaleHexagon(gameObject);
			break;
		}

		bc.center = new Vector3(0,0,10*gm.size); // send the collider to the back, so it wont interfere with PieceTracker
	}

	void OnMouseUpAsButton(){
		JMFRelay.onPanelClick(x,y);
	}
}
