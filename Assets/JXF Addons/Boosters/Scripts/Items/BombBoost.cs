// IMPORTANT : uncomment the #define if you HAVE the package
// P.S.> for script editing & compiling only... there is a global define already in place
// leaving it commented out is fine :)
//#define JMFP
//#define JSF

using UnityEngine;
using System.Collections;

namespace JXF.AddOns.Boosters {
	public class BombBoost : Booster {

		protected override bool performPower (int x, int y)
		{
			// this is a direct copy of the bomb power
			
			#if JMFP
			if(mgm != null){
				mgm.animScript.doAnim(animType.BOMB,x,y); // perform anim
				mgm.audioScript.bombSoundFx.play(); // play arrow sound fx

				mgm.destroyInTime(x,y,0f,scorePerPiece); // destroys the clicked piece

				// all the surrounding neighbour boards...
				foreach(Board _board in mgm.getBoardsFromDistance(mgm.board[x,y].arrayRef,1) ){
					mgm.destroyInTime(_board.arrayRef,0.1f,scorePerPiece);
				}
			}
			#endif
			
			#if JSF
			if(sgm != null){
				sgm.animScript.doAnim(JSFanimType.BOMB,x,y); // perform anim
				sgm.audioScript.bombSoundFx.play(); // play arrow sound fx

				sgm.destroyInTime(x,y,0f,scorePerPiece); // destroys the clicked piece

				// all the surrounding neighbour boards...
				foreach(JSFBoard _board in sgm.getBoardsFromDistance(sgm.board[x,y].arrayRef,1) ){
					sgm.destroyInTime(_board.arrayRef,0.1f,scorePerPiece);
				}
			}
			#endif

			return true; // power performed...
		}
	}
}

