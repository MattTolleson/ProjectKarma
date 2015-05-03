// IMPORTANT : uncomment the #define if you HAVE the package
// P.S.> for script editing & compiling only... there is a global define already in place
// leaving it commented out is fine :)
//#define JMFP
//#define JSF

using UnityEngine;
using System.Collections;

namespace JXF.AddOns.Boosters {
	public class Special6Boost : Booster {

		protected override bool performPower (int x, int y)
		{
			// this is a direct copy of the special6 power
			
			#if JMFP
			if(mgm != null){
				mgm.audioScript.bombSoundFx.play(); // play this sound fx
				
				float delayPerPiece = 0.05f;
				mgm.animScript.doAnim(animType.BOMB,x,y); // visual fx animation
				
				for(int x1 = 0; x1 < mgm.boardWidth;x1++){
					for(int y1 = 0; y1 < mgm.boardHeight;y1++)
					{
						// code below fans out the destruction with the bomb being the epicentre
						if( (x-x1) >= 0 && (y-y1) >=0 ){
							mgm.destroyInTime(x-x1,y-y1, delayPerPiece*(x1+y1), scorePerPiece);
						}
						if( (x+x1) < mgm.boardWidth && (y+y1) < mgm.boardHeight ){
							mgm.destroyInTime(x+x1,y+y1, delayPerPiece*(x1+y1), scorePerPiece);
						}
						if( (x-x1) >= 0 && (y+y1) < mgm.boardHeight ){
							mgm.destroyInTime(x-x1,y+y1, delayPerPiece*(x1+y1), scorePerPiece);
						}
						if( (x+x1) < mgm.boardWidth && (y-y1) >=0 ){
							mgm.destroyInTime(x+x1,y-y1, delayPerPiece*(x1+y1), scorePerPiece);
						}
					}
				}
			}
			#endif
			
			#if JSF
			if(sgm != null){
				sgm.audioScript.bombSoundFx.play(); // play this sound fx
				
				float delayPerPiece = 0.05f;
				sgm.animScript.doAnim(JSFanimType.Special6,x,y); // visual fx animation
				
				for(int x2 = 0; x2 < sgm.boardWidth;x2++){
					for(int y2 = 0; y2 < sgm.boardHeight;y2++)
					{
						// code below fans out the destruction with the bomb being the epicentre
						if( (x-x2) >= 0 && (y-y2) >=0 ){
							sgm.destroyInTime(x-x2,y-y2, delayPerPiece*(x2+y2), scorePerPiece);
						}
						if( (x+x2) < sgm.boardWidth && (y+y2) < sgm.boardHeight ){
							sgm.destroyInTime(x+x2,y+y2, delayPerPiece*(x2+y2), scorePerPiece);
						}
						if( (x-x2) >= 0 && (y+y2) < sgm.boardHeight ){
							sgm.destroyInTime(x-x2,y+y2, delayPerPiece*(x2+y2), scorePerPiece);
						}
						if( (x+x2) < sgm.boardWidth && (y-y2) >=0 ){
							sgm.destroyInTime(x+x2,y-y2, delayPerPiece*(x2+y2), scorePerPiece);
						}
					}
				}
			}
			#endif

			return true; // power performed...
		}

	}
}

