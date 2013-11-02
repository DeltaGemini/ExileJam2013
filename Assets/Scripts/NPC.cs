using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour {
	
	public GameObject triggerBox;
	bool triggerActive = false;
	public int dir = -1;
	public GameObject child;
	
	AnimationState anim01;
	
	// Use this for initialization
	void Start () {
		Vector3 scale = transform.localScale;
		scale.x = Mathf.Sign(dir);		
		transform.localScale = scale;
		
		anim01 = child.animation["Emerge"];
	}
	
	void Activated(){
		if(triggerActive == false){
			triggerActive = true;
			child.animation.CrossFade(anim01.name);
		}
	}
	
	void Deactivated(){
		child.animation[anim01.name].speed = -1.0f;
		child.animation.CrossFade(anim01.name);
	}
}
