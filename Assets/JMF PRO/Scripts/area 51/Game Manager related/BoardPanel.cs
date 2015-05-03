using UnityEngine;
using System.Collections;
using PathologicalGames;

/// <summary> ##################################
/// 
/// NOTICE :
/// This script is the Panel class used by the "Board" script.
/// 
/// DO NOT TOUCH UNLESS REQUIRED
/// 
/// </summary> ##################################


public class BoardPanel {
	
	string panelPoolName {get{return JMFUtils.panelPoolName;}}
	public int durability = -1; // for panels that can be destroyed
	public Board master; // the origin of the panel - aka who this panel belongs too
	public PanelDefinition pnd;
	public GameObject backPanel; // for visuals
	public GameObject frontPanel; // for visuals
	public GameObject defaultPanel; // for visuals - the default panel at the back
	
	public BoardPanel(PanelDefinition newDefinition, int strength, Board myMaster){
		master = myMaster; // set the master script
		
		// set the type - DO NOT USE setType() as we do not want to initPanels()~!
		pnd = newDefinition;
		durability = strength;
	}
	
	// ##############################
	// EXTERNAL SCRIPTS
	// ##############################
	
	// for external scripts to set the current panel type 
	// REMEMBER : durability 0 means 1 hit to destroy!
	public void setStrength(int strength){
		durability = strength;
		createPanels();
	}
	
	public void setType(PanelDefinition newDefinition, int strength){
		if(pnd != null){
			onPanelDestroy(); // the destroy call if there is a panel type
		}
		pnd = newDefinition;
		durability = strength;
		initPanels();
	}
	
	
	// ##############################
	// INTERNAL SCRIPTS
	// ##############################
	
	// panel definition init function
	public void initPanels(){
		if(!pnd.hasStartingPiece && master.isFilled){
			master.piece.removePiece();
		}
		createPanels(); // the actual creation of the GameObject
		onPanelCreate(); // the onCreate function for the panel (if any)
		master.gm.notifyBoardHasChanged();
	}
	
	// just a simple function to call all related functions
	public void destroyPanels(){
		if(JMFUtils.isPooling){
			// ******** POOL MANAGER version *********
			// give back backPanel to the pool
			if( backPanel != null){
				PoolManager.Pools[panelPoolName].Despawn(backPanel.transform);
				backPanel = null;
			}
			// give back frontPanel to the pool
			if( frontPanel != null){
				PoolManager.Pools[panelPoolName].Despawn(frontPanel.transform);
				frontPanel = null;
			}
			// give back defaultPanel to the pool
			if( defaultPanel != null){
				PoolManager.Pools[panelPoolName].Despawn(defaultPanel.transform);
				defaultPanel = null;
			}
		} else {
			// ******** NON POOL MANAGER version ********
			Object.Destroy(backPanel); // destroy previous leftover panel (if any)
			Object.Destroy(frontPanel); // destroy previous leftover panel (if any)
			Object.Destroy(defaultPanel); // destroy previous default panel (if any)
		}
		
	}
	
	// just a simple function to call all related functions
	public void createPanels(){
		destroyPanels(); // remove old panels first
		createFrontPanel(); // the front panel as the foreground on top of the game piece
		createBackPanel(); // the back panel as the background
		attachPanelScript(); // attaches the panel script
	}
	
	// to create the background visual... 
	void createBackPanel() {
		if(pnd.hasDefaultPanel){
			createDefaultPanel(); // creates the default panel when specified
		}
		
		if(pnd.isInFront || pnd.hasNoSkin){
			return; // already created a front panel, do not make this back panel
		}
		
		if(pnd.skin.Length > 0) { // if the prefab exists
			if(JMFUtils.isPooling){
				// POOL MANAGER Version ~~~~~~~~~
				backPanel = PoolManager.Pools[panelPoolName].Spawn(
					pnd.skin[Mathf.Min( pnd.skin.Length-1,Mathf.Abs(durability))].transform).gameObject;
			} else {
				// NON POOL MANAGER Version ~~~~~~~~~
				backPanel = (GameObject) Object.Instantiate(pnd.skin[Mathf.Min(
					pnd.skin.Length-1,Mathf.Abs(durability))]);
			}
			
			
			//----------
		} else {
			Debug.Log("No panel skin available. Have you forgotten to skin the panel script?");
		}
		
		if(backPanel != null){
			// re-parent the object to the gameManager panel
			backPanel.transform.parent = master.gm.gameObject.transform;
			backPanel.transform.localPosition = master.localPos;
			
			switch(JMFUtils.gm.boardType){
			case BoardType.Square :
				JMFUtils.autoScale(backPanel); // auto scaling feature
				break;
			case BoardType.Hexagon :
				JMFUtils.autoScaleHexagon(backPanel); // auto scaling feature
				break;
			}
			
			// positioning code
			backPanel.transform.localPosition +=
				new Vector3(0,0,2*master.gm.size*backPanel.transform.localScale.z);
		}
	}
	
	// to create the foreground visual...
	void createFrontPanel() {
		if(!pnd.isInFront || pnd.hasNoSkin){
			return; // not a front panel... no need to proceed
		}
		
		if(pnd.skin.Length > 0) { // if the prefab exists
			
			if(JMFUtils.isPooling){
				// POOL MANAGER Version ~~~~~~~~~
				frontPanel = PoolManager.Pools[panelPoolName].Spawn(
					pnd.skin[Mathf.Min( pnd.skin.Length-1,Mathf.Abs(durability))].transform).gameObject;
			} else {
				// NON POOL MANAGER Version ~~~~~~~~~
				frontPanel = (GameObject) Object.Instantiate(pnd.skin[Mathf.Min(
					pnd.skin.Length-1,Mathf.Abs(durability))]);
			}
			
		}else {
			Debug.Log("No panel skin available. Have you forgotten to skin the panel script?");
		}
		
		if(frontPanel != null){
			// re-parent the object to the gameManager panel
			frontPanel.transform.parent = master.gm.gameObject.transform;
			frontPanel.transform.localPosition = master.localPos;
			
			switch(JMFUtils.gm.boardType){
			case BoardType.Square :
				JMFUtils.autoScale(frontPanel); // auto scaling feature
				break;
			case BoardType.Hexagon :
				JMFUtils.autoScaleHexagon(frontPanel); // auto scaling feature
				break;
			}
			
			
			// minor code just to arrange the Z order to always be at the front
			frontPanel.transform.localPosition +=
				new Vector3(0,0,-2*master.gm.size*frontPanel.transform.localScale.z);
		}
	}
	
	// function to create the default panel - in case of tranparency backPanels
	protected void createDefaultPanel(){
		GameObject prefab = null;
		switch(JMFUtils.gm.boardType){
		case BoardType.Square :
			if((master.arrayRef[0] + master.arrayRef[1]) % 2 == 0){
				if(JMFUtils.vm.defaultSquareBackPanel != null) { // if the prefab exists
					prefab = JMFUtils.vm.defaultSquareBackPanel;
				} else {
					Debug.Log("whoops? have you forgotten to provide a default panel prefab?");
					return; // do not continue...
				}
			} else {
				if(JMFUtils.vm.defaultAltSquareBackPanel != null) { // if the prefab exists
					prefab = JMFUtils.vm.defaultAltSquareBackPanel;
				} else if(JMFUtils.vm.defaultSquareBackPanel != null) { // if the prefab exists
					prefab = JMFUtils.vm.defaultSquareBackPanel;
				} else {
					Debug.Log("whoops? have you forgotten to provide a default panel prefab?");
					return; // do not continue...
				}
			}
			break;
		case BoardType.Hexagon :
			if(JMFUtils.vm.defaultHexBackPanel != null) { // if the prefab exists
				prefab = JMFUtils.vm.defaultHexBackPanel;
			} else {
				Debug.Log("whoops? have you forgotten to provide a default panel prefab?");
				return; // do not continue...
			}
			break;
		}
		
		if(JMFUtils.isPooling){
			// POOL MANAGER Version ~~~~~~~~~
			defaultPanel = PoolManager.Pools[panelPoolName].Spawn(prefab.transform).gameObject;
		} else {
			// NON POOL MANAGER Version ~~~~~~~~
			defaultPanel = (GameObject) Object.Instantiate(prefab);
		}
		
		// re-parent the object to the gameManager panel
		defaultPanel.transform.parent = master.gm.gameObject.transform;
		defaultPanel.transform.localPosition = master.localPos;
		
		switch(JMFUtils.gm.boardType){
		case BoardType.Square :
			JMFUtils.autoScale(defaultPanel); // auto scaling feature
			break;
		case BoardType.Hexagon :
			JMFUtils.autoScaleHexagon(defaultPanel); // auto scaling feature
			break;
		}
		
		// minor code just to arrange the Z order to always be at the back
		defaultPanel.transform.localPosition +=
			new Vector3(0,0,4*master.gm.size*defaultPanel.transform.localScale.z);
	}
	
	void attachPanelScript(){
		if(pnd.hasNoSkin && !pnd.hasDefaultPanel){
			return; // no panels created... empty panel perhaps?
		}
		
		if(pnd.hasNoSkin && pnd.hasDefaultPanel){ // no skin but has default panel?
			if(defaultPanel.GetComponent<PanelTracker>() == null) 
				defaultPanel.AddComponent<PanelTracker>();
			defaultPanel.GetComponent<PanelTracker>().arrayRef = master.arrayRef;
		}
		else if(pnd.isInFront){
			if(frontPanel.GetComponent<PanelTracker>() == null)
				frontPanel.AddComponent<PanelTracker>();
			frontPanel.GetComponent<PanelTracker>().arrayRef = master.arrayRef;
		} else {
			if(backPanel.GetComponent<PanelTracker>() == null) 
				backPanel.AddComponent<PanelTracker>();
			backPanel.GetComponent<PanelTracker>().arrayRef = master.arrayRef;
		}
		// NOTES :- the box collider for this to work is already defined in PanelTracker.cs script itself.
	}
	
	// ###########################
	// ACCESS FUNCTIONS FOR PANEL-DEFINITION
	// relays information to PanelDefinition for easy access from GameManager
	// ###########################
	
	// for external scripts to call, will indicate that the panel got hit
	public bool gotHit(){
		bool registeredHit = pnd.gotHit(this);
		if(registeredHit){
			if(durability < 0 && !(pnd == master.gm.panelTypes[0]) ){
				setType( master.gm.panelTypes[0], 0 ); // change back to basic panel
			} else {
				createPanels(); // refresh the panel gameObjects
			}
		}
		return registeredHit;
	}
	
	// for external scripts to call, if splash damage hits correct panel type, perform the hit
	public bool splashDamage() {
		bool registeredHit = pnd.splashDamage(this);
		if(registeredHit){
			if(durability < 0 && !(pnd == master.gm.panelTypes[0]) ){
				setType( master.gm.panelTypes[0], 0 ); // change back to basic panel
			} else {
				createPanels(); // refresh the panel gameObjects
			}
		}
		return registeredHit;
	}

	// on destroy call
	public void onPanelDestroy() {
		pnd.onPanelDestroy(this);
	}
	// on create call
	public void onPanelCreate() {
		pnd.onPanelCreate(this);
	}

	// function to check if pieces can fall into this board box
	public bool allowsGravity() {
		return pnd.allowsGravity(this);
	}

	// if the piece here can be used to form a match
	public bool isMatchable() {
		return pnd.isMatchable(this);
	}
	
	// if the piece here can be switched around
	public bool isSwitchable() {
		return pnd.isSwitchable(this);
	}
	
	// if the piece here (if any) can be destroyed
	public bool isDestructible() {
		return pnd.isDestructible(this);
	}
	
	// function to check if pieces can be stolen from this box by gravity
	public bool isStealable() {
		return pnd.isStealable(this);
	}
	
	// function to check if this board needs to be filled by gravity
	public bool isFillable() {
		return pnd.isFillable(this);
	}
	
	// function to check if this board is a solid panel that gravity cannot pass through
	public bool isSolid() {
		return pnd.isSolid(this);
	}
}
