using UnityEngine;
using System.Collections;

public class AnimationCue : MonoBehaviour {
	
	public GameObject parent;
	
	// Use this for initialization
	void RunAnimation(){
		parent.SendMessage("PlaySound", "step");
	}
	
	void BreatheIn(){
		parent.SendMessage("PlaySound", "breatheIn");
	}
	
	void BreatheOut(){
		parent.SendMessage("PlaySound", "breatheOut");
	}
}
