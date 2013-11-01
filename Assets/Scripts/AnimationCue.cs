using UnityEngine;
using System.Collections;

public class AnimationCue : MonoBehaviour {
	
	public GameObject parent;
	
	// Use this for initialization
	void RunAnimation(){
		parent.SendMessage("PlaySound", "step");
	}
}
