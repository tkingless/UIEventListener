using UnityEngine;
using System.Collections;

public class SmoothPanning : MonoBehaviour {

	//This is camera script

	// The target we are following
	public Transform mTarget;
	// The distance in the x-z plane to the target
	public float mDistance = 10.0f;
	// the height we want the camera to be above the target
	public float mHeight = 5.0f;
	public float mHeightDamping = 2.0f;
	public float mRotationDamping = 3.0f;

	void LateUpdate () {
		// Early out if we don't have a target
		if (!mTarget)
						return;

		float wantedHeight = mTarget.position.y + mHeight;
		float currentHeight = transform.position.y;
		// Damp the height
		currentHeight = Mathf.Lerp (currentHeight, wantedHeight, mHeightDamping * Time.deltaTime);
		// Set the position of the camera on the x-z plane to:
		// distance meters behind the target
		transform.position = mTarget.position;
		transform.position -= Vector3.forward*mDistance;

		AxisValuePair aTest = new AxisValuePair (Axis.y, currentHeight);

#if true
		// Set the height of the camera
		Vector3 temp = transform.position;
		transform.position = new Vector3 (temp.x, currentHeight, temp.z);
#else
		transform.position = SetTransformPos();
#endif
		// Always look at the target
		transform.LookAt (mTarget);
	}

	//The below may be considered to be untensil, unfinished yet
	enum Axis {x,y,z};

	struct AxisValuePair {
		Axis mAxisToChange;
		float mValueToSet;

		public AxisValuePair(Axis initAxis, float initVal){

			mAxisToChange = initAxis;
			mValueToSet = initVal;
		}
	}

	Vector3 SetTransformPos (Transform aTrans,AxisValuePair[] aAVP){

		/*for (int i=0; i<aAVP.Length; i++) {

			switch(aAVP[i]
				}*/
		return Vector3.zero;
	}

}
