// IMPORTANT : uncomment the #define if you HAVE the package
// P.S.> for script editing & compiling only... there is a global define already in place
// leaving it commented out is fine :)
//#define JMFP
//#define JSF

using UnityEngine;
using System.Collections;

namespace JXF.AddOns.Boosters {
	public class VBoost : Booster {

		protected override bool performPower (int x, int y)
		{
			// custom defined function
			// this is a revised copy of the Vertical power

			float delay = 0f;
			float delayIncreament = 0.1f; // the delay of each piece being destroyed.

			#if JMFP
			if(mgm != null){
				delay = 0f;
				delayIncreament = 0.1f; // the delay of each piece being destroyed.
				
				mgm.destroyInTime(x,y,0f,scorePerPiece); // destroys the clicked piece

				mgm.animScript.doAnim(animType.ARROWV,x,y); // perform anim
				mgm.audioScript.arrowSoundFx.play(); // play arrow sound fx
				
				// the top of this board...
				foreach(Board _board in mgm.board[x,y].getAllBoardInDirection(BoardDirection.Top) ){
					mgm.destroyInTime(_board.arrayRef,delay,scorePerPiece);
					delay += delayIncreament;
				}
				delay = 0f; // reset the delay
				// the bottom of this board...
				foreach(Board _board in mgm.board[x,y].getAllBoardInDirection(BoardDirection.Bottom) ){
					mgm.destroyInTime(_board.arrayRef,delay,scorePerPiece);
					delay += delayIncreament;
				}
			}
			#endif
			
			#if JSF
			if(sgm != null){
				delay = 0f;
				delayIncreament = 0.1f; // the delay of each piece being destroyed.
				
				sgm.destroyInTime(x,y,0f,scorePerPiece); // destroys the clicked piece

				sgm.animScript.doAnim(JSFanimType.ARROWV,x,y); // perform anim
				sgm.audioScript.arrowSoundFx.play(); // play arrow sound fx
				
				// the top of this board...
				foreach(JSFBoard _board in sgm.board[x,y].getAllBoardInDirection(JSFBoardDirection.Top) ){
					sgm.destroyInTime(_board.arrayRef,delay,scorePerPiece);
					delay += delayIncreament;
				}
				delay = 0f; // reset the delay
				// the bottom of this board...
				foreach(JSFBoard _board in sgm.board[x,y].getAllBoardInDirection(JSFBoardDirection.Bottom) ){
					sgm.destroyInTime(_board.arrayRef,delay,scorePerPiece);
					delay += delayIncreament;
				}
			}
			#endif

			return true; // power performed...
		}

	}
}

