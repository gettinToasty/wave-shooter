using UnityEngine;
using System.Collections;

using UnityStandardAssets.CrossPlatformInput;

public class Camera_Rotate_CS : MonoBehaviour {

	// This script must be attached to "Camera_Pivot".

	Transform thisTransform ;
	Vector2 previousMousePos ;
	float angY ;
	float angZ ;

	int myID ;
	bool isCurrentID ;

	void Start () {
		thisTransform = transform ;
		angY = thisTransform.eulerAngles.y ;
		angZ = thisTransform.eulerAngles.z ;
	}

	void Update () {
		if ( isCurrentID ) {
			#if UNITY_ANDROID || UNITY_IPHONE
			angY += CrossPlatformInputManager.GetAxis ( "Camera_X" ) * 2.0f ;
			angZ -= CrossPlatformInputManager.GetAxis ( "Camera_Y" ) ;
			#else
			if ( Input.GetMouseButtonDown ( 1 ) ) {
				previousMousePos = Input.mousePosition ;
			}
			if ( Input.GetMouseButton ( 1 ) ) {
				float horizontal = ( Input.mousePosition.x - previousMousePos.x ) * 0.1f ;
				float vertical = ( Input.mousePosition.y - previousMousePos.y ) * 0.1f ;
				previousMousePos = Input.mousePosition ;
				angY += horizontal * 3.0f ;
				angZ -= vertical * 2.0f ;
			}
			#endif
			thisTransform.rotation = Quaternion.Euler ( 0.0f , angY , angZ ) ;
		}
	}

	void Get_ID ( int idNum ) {
		myID = idNum ;
	}
	
	void Get_Current_ID ( int idNum ) {
		if ( idNum == myID ) {
			isCurrentID = true ;
		} else {
			isCurrentID = false ;
		}
	}
}
