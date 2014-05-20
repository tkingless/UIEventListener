using UnityEngine;
using System.Collections;

public class nav_traget : MonoBehaviour
{
	public Transform target;
	public GameObject follower;
	private NavMeshAgent agent;
	public Camera mCam;

	void Start(){
		agent = follower.GetComponent<NavMeshAgent>();
	}

	void Update(){
		agent.SetDestination(target.position);
	}

	void Nav_Target_OnTap(){

		SetMoveCursor();
	}

	void SetMoveCursor(){

		Vector3 curScreenPos = Input.mousePosition;
		Ray ray=mCam.ScreenPointToRay(curScreenPos);
		RaycastHit hit;
		if(Physics.Raycast(ray,out hit)){
			if(hit.collider.gameObject.tag=="Ground"){ 
				target.position = new Vector3( hit.point.x,hit.point.y,hit.point.z);  
	}
		}
	}
}