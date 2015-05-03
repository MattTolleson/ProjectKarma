// IMPORTANT : uncomment the #define if you HAVE the package
// P.S.> for script editing & compiling only... there is a global define already in place
// leaving it commented out is fine :)
//#define JMFP
//#define JSF

using UnityEngine;
using System.Collections;

namespace JXF.AddOns.Boosters {
	public class HBoost : Booster {

		protected override bool performPower (int x, int y)
		{
			// custom defined function
			// this is a revised copy of the Horizontal power

			float delay = 0f;
			float delayIncreament = 0.1f; // the delay of each piece being destroyed.

			#if JMFP
			if(mgm != null){
				delay = 0f;
				delayIncreament = 0.1f; // the delay of each piece being destroyed.

				mgm.destroyInTime(x,y,0f,scorePerPiece); // destroys the clicked piece

				switch(mgm.boardType){ // check board type
				case BoardType.Square: // square type horizontal power
					mgm.animScript.doAnim(animType.ARROWH,x,y); // perform anim
					mgm.audioScript.arrowSoundFx.play(); // play arrow sound fx
					
					// the left of this board...
					foreach(Board _board in mgm.board[x,y].getAllBoardInDirection(BoardDirection.Left) ){
						mgm.destroyInTime(_board.arrayRef,delay,scorePerPiece);
						delay += delayIncreament;
					}
					delay = 0f; // reset the delay
					// the right of this board...
					foreach(Board _board in mgm.board[x,y].getAllBoardInDirection(BoardDirection.Right) ){
						mgm.destroyInTime(_board.arrayRef,delay,scorePerPiece);
						delay += delayIncreament;
					}
					break;
				case BoardType.Hexagon:// hex type horizontal power
					mgm.animScript.doAnim(animType.ARROWTLBR,x,y); // perform anim
					mgm.animScript.doAnim(animType.ARROWTRBL,x,y); // perform anim
					mgm.audioScript.arrowSoundFx.play(); // play arrow sound fx
					
					// the TopLeft of this board...
					foreach(Board _board in mgm.board[x,y].getAllBoardInDirection(BoardDirection.TopLeft) ){
						mgm.destroyInTime(_board.arrayRef,delay,scorePerPiece);
						delay += delayIncreament;
					}
					delay = 0f; // reset the delay
					// the TopRight of this board...
					foreach(Board _board in mgm.board[x,y].getAllBoardInDirection(BoardDirection.TopRight) ){
						mgm.destroyInTime(_board.arrayRef,delay,scorePerPiece);
						delay += delayIncreament;
					}
					delay = 0f; // reset the delay
					// the BottomLeft of this board...
					foreach(Board _board in mgm.board[x,y].getAllBoardInDirection(BoardDirection.BottomLeft) ){
						mgm.destroyInTime(_board.arrayRef,delay,scorePerPiece);
						delay += delayIncreament;
					}
					delay = 0f; // reset the delay
					// the BottomRight of this board...
					foreach(Board _board in mgm.board[x,y].getAllBoardInDirection(BoardDirection.BottomRight) ){
						mgm.destroyInTime(_board.arrayRef,delay,scorePerPiece);
						delay += delayIncreament;
					}
					break;
				}
			}
			#endif

			#if JSF
			if(sgm != null){
				delay = 0f;
				delayIncreament = 0.1f; // the delay of each piece being destroyed.

				sgm.destroyInTime(x,y,0f,scorePerPiece); // destroys the clicked piece

				switch(sgm.boardType){ // check board type
				case JSFBoardType.Square: // square type horizontal power
					sgm.animScript.doAnim(JSFanimType.ARROWH,x,y); // perform anim
					sgm.audioScript.arrowSoundFx.play(); // play arrow sound fx
					
					// the left of this board...
					foreach(JSFBoard _board in sgm.board[x,y].getAllBoardInDirection(JSFBoardDirection.Left) ){
						sgm.destroyInTime(_board.arrayRef,delay,scorePerPiece);
						delay += delayIncreament;
					}
					delay = 0f; // reset the delay
					// the right of this board...
					foreach(JSFBoard _board in sgm.board[x,y].getAllBoardInDirection(JSFBoardDirection.Right) ){
						sgm.destroyInTime(_board.arrayRef,delay,scorePerPiece);
						delay += delayIncreament;
					}
					break;
				case JSFBoardType.Hexagon:// hex type horizontal power
					sgm.animScript.doAnim(JSFanimType.ARROWTLBR,x,y); // perform anim
					sgm.animScript.doAnim(JSFanimType.ARROWTRBL,x,y); // perform anim
					sgm.audioScript.arrowSoundFx.play(); // play arrow sound fx
					
					// the TopLeft of this board...
					foreach(JSFBoard _board in sgm.board[x,y].getAllBoardInDirection(JSFBoardDirection.TopLeft) ){
						sgm.destroyInTime(_board.arrayRef,delay,scorePerPiece);
						delay += delayIncreament;
					}
					delay = 0f; // reset the delay
					// the TopRight of this board...
					foreach(JSFBoard _board in sgm.board[x,y].getAllBoardInDirection(JSFBoardDirection.TopRight) ){
						sgm.destroyInTime(_board.arrayRef,delay,scorePerPiece);
						delay += delayIncreament;
					}
					delay = 0f; // reset the delay
					// the BottomLeft of this board...
					foreach(JSFBoard _board in sgm.board[x,y].getAllBoardInDirection(JSFBoardDirection.BottomLeft) ){
						sgm.destroyInTime(_board.arrayRef,delay,scorePerPiece);
						delay += delayIncreament;
					}
					delay = 0f; // reset the delay
					// the BottomRight of this board...
					foreach(JSFBoard _board in sgm.board[x,y].getAllBoardInDirection(JSFBoardDirection.BottomRight) ){
						sgm.destroyInTime(_board.arrayRef,delay,scorePerPiece);
						delay += delayIncreament;
					}
					break;
				}
			}
			#endif

			return true; // power performed...
		}

	}
}

