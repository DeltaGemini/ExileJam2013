﻿using UnityEngine;
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
	
	// Use this for initialization
	void Start () {
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
			Move ();
			if(DinoControl.roaring){
				AnimateBlend(scaredAnim.name);
			}
		} else {
			target = transform.position;
		}
	}
	
	void FollowOn(){
		follow = true;
		DinoControl.followers.Add(this.gameObject);	
	}
	
	void FollowOff(){
		follow = false;
	}
	
	void Move(){
		target = parent.transform.position;
		
		Vector3 dir = target - transform.position;			
		Vector3 scale = transform.localScale;
		
		if(dir.x >= dist || dir.x <= -dist){
			
			scale.x = Mathf.Sign(dir.x);
			
			transform.localScale = scale;
			
			//parentY = parent.transform.position.y - parent.gameObject.collider.bounds.size.y/2;
			Vector3 pos = transform.position;
			pos += dir.normalized * Time.deltaTime * (speed + Random.Range(1f,2f)); //Linear speed			
			if(transform.position.y < 0 && transform.position.y > -6){
				pos.y += Random.Range(-0.5f,2)*Time.deltaTime;
			}
			pos.z = pos.y + 2f;
			transform.position = pos;
			
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