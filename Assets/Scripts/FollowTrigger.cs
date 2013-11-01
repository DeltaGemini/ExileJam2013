using UnityEngine;
using System.Collections;

public class FollowTrigger : MonoBehaviour {
	
	public GameObject target;
	
	void Awake(){
	}
	
	void OnTriggerStay (Collider col) {
		Debug.Log(col);
		if(col.gameObject.tag == "Player"){
			target.SendMessage("FollowOn");			
			Destroy(this.gameObject);
		}
	}
}
