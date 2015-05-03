// IMPORTANT : uncomment the #define if you HAVE the package
// P.S.> for script editing & compiling only... there is a global define already in place
// leaving it commented out is fine :)
//#define JMFP
//#define JSF

using UnityEngine;
using System.Collections;

namespace JXF.AddOns.Boosters {
	public abstract class Booster : MonoBehaviour {
		public Renderer bg; // the Background GUI for this script to change the color according to usage
		public TextMesh txt; // the Text GUI for this script to display the quantity left

		protected int quantity = 0; // the current quantity available for this item
		public int useLimitPerGame = 1; // the limit of use per game ( 0 = unlimited! )
		protected int timesUsed = 0; // How many times this booster has been used during this game
		protected static Booster selected = null; // to keep track of which item was selected

		public int scorePerPiece = 2000; // can be changed in the inspector
		public int numAvailable = 10; // can be changed in the inspector

		#if JMFP
		protected GameManager mgm {get{return JMFUtils.gm;}} // JMFP's GameManager reference
		#endif
		#if JSF
		protected JSFGameManager sgm {get{return JSFUtils.gm;}} // JSF's GameManager reference
		#endif

		void Start(){
			// attaches itself to the appropriate relay script
			#if JMFP
			JMFRelay.dlgOnPanelClick += useBooster; // when the panel is clicked... call useBooster!
			#endif
			#if JSF
			JSFRelay.dlgOnPanelClick += useBooster; // when the panel is clicked... call useBooster!
			#endif
			
			addQty(numAvailable); // a simple call to add the item quantity to be used...
		}

		void OnMouseUpAsButton(){
			#if JMFP
			if(JMFUtils.gm != null && JMFUtils.gm.gameState != GameState.GameActive) return; // no items usable out of gameplay
			#endif
			#if JSF
			if(JSFUtils.gm != null && JSFUtils.gm.gameState != JSFGameState.GameActive) return; // no items usable out of gameplay
			#endif
			if(selected == this){ // deselected this booster
				selected = null; // deselect items
				autoBGColor(this);
				return; // finish deselecting...
			} else if(selected != null){ // selected a different booster
				autoBGColor(selected);
				selected = null; // deselect items
			} 
			if(selected == null && quantity > 0 && 
			   (useLimitPerGame == 0 || timesUsed < useLimitPerGame) ){ // selected a booster
				selected = this; // this item is selected
				if(bg) bg.material.color = Color.green;
			}
		}

		// function to add quantity to the selected booster
		public virtual void addQty(int num){
			quantity += num;
			if(txt) txt.text = quantity.ToString(); // updates the value to the text
		}

		public virtual void useBooster(int x, int y){
			#if JMFP
			if(JMFUtils.gm != null && JMFUtils.gm.gameState != GameState.GameActive) {
				autoBGColor(this);
				selected = null; // resets selected item
				return; // no items usable out of gameplay
			}
			#endif
			#if JSF
			if(JSFUtils.gm != null && JSFUtils.gm.gameState != JSFGameState.GameActive) {
				autoBGColor(this);
				selected = null; // resets selected item
				return; // no items usable out of gameplay
			}
			#endif

			if(selected == this && quantity > 0 && (useLimitPerGame == 0 || timesUsed < useLimitPerGame) ){ // can use this booster?
				if(performPower(x,y)){ // if successfully used power...
					quantity--; // reduce by 1
					timesUsed++; // add by 1
					if(txt) txt.text = quantity.ToString(); // updates the value to the text
				} else{ // announce that the power did not trigger
					#if JMFP
					if(JMFUtils.gm != null) JMFUtils.gm.audioScript.badMoveSoundFx.play();
					#endif
					#if JSF
					if(JSFUtils.gm != null) JSFUtils.gm.audioScript.badMoveSoundFx.play();
					#endif
				}

				autoBGColor(this); // set the bg color
				selected = null; // resets selected item
			}
		}

		// sets the bg to white or red depending on usability
		void autoBGColor(Booster bstr){
			if(bstr.quantity == 0 || (bstr.useLimitPerGame > 0 && bstr.timesUsed >= bstr.useLimitPerGame) ){
				if(bstr.bg) bstr.bg.material.color = Color.red; // cannot be used anymore
			} else {
				if(bstr.bg) bstr.bg.material.color = Color.white; // back to deselected
			}
			selected = null; // resets selected item
		}

		protected abstract bool performPower(int x, int y); // a function each child script needs to define...
	}
}
