using UnityEngine;
using System.Collections;

public class PetBehaviour : MonoBehaviour {
	
	public GameObject parent;
	public GameObject child;
	string find;
	Vector3 target;
	public float speed = 5;
	public float dist = 8;
	
	float parentY;
	AnimationState idleAnim;
	AnimationState walkAnim;
	AnimationState scaredAnim;
	
	bool follow = false;
	bool pickedUpBefore = false;
	
	Vector3 originPosition;
	
	// Use this for initialization
	void Start () {
		
		originPosition = transform.position;
		
		idleAnim = child.animation["idle"];
		idleAnim.layer = 8;
		walkAnim = child.animation["walk"];
		walkAnim.layer = 3;
		scaredAnim = child.animation["scared"];
		scaredAnim.layer = 5;
	}
	
	// Update is called once per frame
	void Update () {
		if(follow) {
			target = parent.transform.position;
			Move ();
			if(DinoControl.roaring){
				AnimateBlend(scaredAnim.name);
			}
		} else if(pickedUpBefore){
			target = originPosition;
			speed = 8;
			Move ();
		}
	}
	
	void FollowOn(){
		follow = true;
		pickedUpBefore = true;
		DinoControl.followers.Add(this.gameObject);	
	}
	
	void FollowOff(){
		follow = false;
	}
	
	void Move(){		
		Vector3 dir = target - transform.position;			
		Vector3 scale = transform.localScale;
		
		if(dir.x >= dist || dir.x <= -dist){
			
			scale.x = Mathf.Sign(dir.x * dist);
			
			transform.localScale = scale;
			
			//parentY = parent.transform.position.y - parent.gameObject.collider.bounds.size.y/2;
			Vector3 pos = transform.position;
			pos += dir.normalized * Time.deltaTime * (speed + Random.Range(1f,2f)); //Linear speed			
			/*if(transform.position.y < 0 && transform.position.y > -5){
				pos.y += Random.Range(0,1)*Time.deltaTime;
			}*/
			pos.z = pos.y + 2f;
			transform.position = new Vector3(pos.x, transform.position.y, pos.z);
			
			Animate(walkAnim.name);
		}
	}
	
	void Animate(string name){
		child.animation.CrossFade(name);
	}
	
	void AnimateBlend(string name){
		child.animation.Blend(name, 1.0f);
	}
}