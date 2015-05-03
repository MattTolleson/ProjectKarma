// IMPORTANT : uncomment the #define if you HAVE the package
// P.S.> for script editing & compiling only... there is a global define already in place
// leaving it commented out is fine :)
//#define JMFP
//#define JSF

using UnityEngine;
using System.Collections;

namespace JXF.AddOns.Boosters {
	public class Special5Boost : Booster {

		protected override bool performPower (int x, int y)
		{
			#if JMFP
			// additional condition check for special5 specifics
			if(mgm != null && (!mgm.board[x,y].isFilled || mgm.board[x,y].pd.isSpecial) ){
				return false; // cannot proceed with power... conditions not met...
			}
			#endif
			#if JSF
			// additional condition check for special5 specifics
			if(sgm != null && (!sgm.board[x,y].isFilled || sgm.board[x,y].pd.isSpecial) ){
				return false; // cannot proceed with power... conditions not met...
			}
			#endif
			StartCoroutine( specialFiveBooster(x,y) );

			return true; // power performed...
		}

		// custom defined function
		IEnumerator specialFiveBooster(int x, int y){
			// this is a revised copy of the Special5 power

			#if JMFP
			if(mgm != null){
				// Does the power merge colored. match 5 type power ( destroys specified param color )
				mgm.audioScript.matchFiveSoundFx.play(); // play this sound fx
				mgm.animScript.doAnim(animType.RAINBOW,x,y); // visual fx animation
				mgm.destroyInTimeMarked(x,y, 2f, scorePerPiece); // locks this piece & destroys after x seconds
				//		thisBd.isFalling = true; // do not let it fall :)
				
				float delayPerPiece = 0.00f;
				yield return new WaitForSeconds(2f);
				// destroys the selected color in each board
				int mColor = mgm.board[x,y].piece.slotNum;
				foreach(Board board in mgm.board){
					if(board.isFilled && !board.pd.isSpecial && board.piece.slotNum == mColor){
						mgm.destroyInTime(board, delayPerPiece, scorePerPiece);
					}
				}
			}
			#endif

			#if JSF
			if(sgm != null){
				// Does the power merge colored. match 5 type power ( destroys specified param color )
				sgm.audioScript.matchFiveSoundFx.play(); // play this sound fx
				sgm.animScript.doAnim(JSFanimType.Special5,x,y); // visual fx animation
				sgm.destroyInTimeMarked(x,y, 2f, scorePerPiece); // locks this piece & destroys after x seconds
				//		thisBd.isFalling = true; // do not let it fall :)
				
				float delayPerPiecej = 0.00f;
				yield return new WaitForSeconds(2f);
				// destroys the selected color in each board
				int sColor = sgm.board[x,y].piece.slotNum;
				foreach(JSFBoard board in sgm.board){
					if(board.isFilled && !board.pd.isSpecial && board.piece.slotNum == sColor){
						sgm.destroyInTime(board, delayPerPiecej, scorePerPiece);
					}
				}
			}
			#endif

			yield return new WaitForSeconds(0f); // to return something for compile error :D
		}
	}
}

