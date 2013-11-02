using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour {
	
	public GameObject triggerBox;
	bool triggerActive = false;
	
	// Use this for initialization
	void Start () {
		int dir = -1;
		Vector3 scale = transform.localScale;
		scale.x = Mathf.Sign(dir);		
		transform.localScale = scale;
	}
	
	void Activated(){
		Destroy(this.gameObject);
	}
}
