using UnityEngine;
using System.Collections;

public class EnemyAnimationCue : MonoBehaviour {
	
	public GameObject parent;
	
	// Use this for initialization
	void RoarSound(){
		parent.SendMessage("PlaySound");
	}
}
