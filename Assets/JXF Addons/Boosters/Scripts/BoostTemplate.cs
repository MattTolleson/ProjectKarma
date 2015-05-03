// IMPORTANT : uncomment the #define if you HAVE the package
// P.S.> for script editing & compiling only... there is a global define already in place
// leaving it commented out is fine :)
//#define JMFP
//#define JSF

using UnityEngine;
using System.Collections;

namespace JXF.AddOns.Boosters {
	public class BoostTemplate : Booster {

		protected override bool performPower (int x, int y)
		{	
			// x,y is the board's position that was clicked...

			#if JMFP
			if(mgm != null){ // JMFP's code section
				// do something???

				// mgm = the JMFP's GameManager... you can also call JMFUtils.gm
				// refer to the online help document for function calls you may want to use :)
			}
			#endif
			
			#if JSF
			if(sgm != null){ // JSF's code section
				// do something???
				
				// sgm = the JSF's GameManager... you can also call JSFUtils.gm
				// refer to the online help document for function calls you may want to use :)
			}
			#endif

			return true; // power performed...
			// OR return false; if there is a criteria the item needs for the power to execute
			// false would indicate that the item could not be used... the quantity will not be deducted!
		}
	}
}

