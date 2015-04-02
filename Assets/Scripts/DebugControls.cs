using UnityEngine;
using System.Collections;

public class DebugControls : MonoBehaviour {

	private Vector3 startPos = new Vector3(41,18,216); //JORDAN'S EDIT DONT CHANGE

	void Update () {
		if (Input.GetKey (KeyCode.LeftShift)){
			Vector3 switchPos = startPos;
			if(startPos.x == 41){
				startPos=new Vector3(111,152,-1828);
			}else{
				startPos=new Vector3(41,18,216);
			}
			transform.position=switchPos;
		}
	}
}
