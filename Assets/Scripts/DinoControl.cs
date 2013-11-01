﻿using UnityEngine;
using System.Collections;

public class DinoControl : MonoBehaviour {
	
	public float speed;
	public GameObject child;
	public Camera mainCamera;
	public float minX = -14f;
	public float maxY = 10f;
	public float minY = -4f;
	
	Vector3 target;
	Vector3 startLocation;
	AnimationState roarAnim;
	AnimationState jumpAnim;
	AnimationState walkAnim;
	AnimationState idleAnim;
	AnimationState frontWristAnim;
	AnimationState backWristAnim;
	AnimationState tailAnim;
	AnimationState jawAnim;
	
	public AudioClip[] sounds;
	
	public Transform[] pets;
	
	//Hold down timer
	float mouseTime = 0;
	
	// Use this for initialization
	void Start () {
		target = transform.position;
		startLocation.y = transform.position.y;
		startLocation.z = transform.position.z;
		
		roarAnim = child.animation["Roar"];
		roarAnim.layer = 8;		
		jumpAnim = child.animation["Jump"];
		jumpAnim.layer = 5;
		walkAnim = child.animation["Walk"];
		walkAnim.layer = 3;
		idleAnim = child.animation["Idle"];
		idleAnim.layer = 3;
		frontWristAnim = child.animation["ShakeHandFront"];
		frontWristAnim.layer = 6;		
		backWristAnim = child.animation["ShakeHandBack"];
		backWristAnim.layer = 6;	
		tailAnim = child.animation["WriggleTail"];
		tailAnim.layer = 4;
		jawAnim = child.animation["ClapJaws"];
		jawAnim.layer = 7;		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float dist = Vector2.Distance(transform.position, target);
		
		if(dist <= 3) {			
			target = transform.position;
			Animate(idleAnim.name);
			
		} else {			
			Vector3 dir = target - transform.position;
			
			Vector3 scale = transform.localScale;
			scale.x = Mathf.Sign(dir.x + 5);
			
			transform.localScale = scale;
			
			Vector3 pos = transform.position;
			pos += dir.normalized * Time.deltaTime * speed; //Linear speed
			pos.z = pos.y - 2f;
			Animate(walkAnim.name);
			//pos += dir * Time.deltaTime * speed;
			
			transform.position = pos;
		}
		
		if(Input.GetMouseButton(0)){
						
			mouseTime += Time.deltaTime;
			
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
	        if (Physics.Raycast(ray, out hit, 1000)){
				hit.transform.SendMessage("Activate", SendMessageOptions.DontRequireReceiver);
				switch(hit.transform.name){
				case "neck":
					if(mouseTime >= 0.5f){
						AnimateBlend(roarAnim.name);
						foreach(Transform pet in pets){
							pet.gameObject.SendMessage("AnimateBlend", "scared");
						}
						PlaySound("roar");
						mainCamera.gameObject.SendMessage("Shake", 0.2);					
						mouseTime = 0;
					} else if(mouseTime > 3) {
						mouseTime = 0;
					} else {
						AnimateBlend(jawAnim.name);
					}
					break;
				case "foot_back":
					target = transform.position;
					Animate(jumpAnim.name);
					break;
				case "foot_front":
					target = transform.position;
					Animate(jumpAnim.name);
					break;
				case "arm_front_wrist":
					target = transform.position;
					AnimateBlend(frontWristAnim.name);
					break;
				case "arm_back_wrist":
					target = transform.position;
					AnimateBlend(backWristAnim.name);
					break;
				case "tail09":
				case "tail07":
				case "tail08":
				case "tail06":
					target = transform.position;
					AnimateBlend(tailAnim.name);
					break;
				default:
					target = ray.origin;
					break;
				}					
			}
		} else {
			mouseTime = 0;	
		}		
		
		//Don't go too far off to the left
		if(target.x < minX)
			target.x = minX;		
		
		if(target.y > maxY)
			target.y = maxY;
		
		if(target.y < minY)
			target.y = minY;		
		
		if(child.animation["Walk"].time == 0.21 || child.animation["Walk"].time == 2){
			PlaySound("step");
		}
	}
	
	void Animate(string name){
		child.animation.CrossFade(name);
	}
	
	void AnimateBlend (string name){
		child.animation.Blend(name, 0.5f);
	}
	
	public void PlaySound (string evt) {
		switch (evt){
		case "roar":
			AudioClip roarOnce = sounds[Random.Range(16,19)];
			audio.PlayOneShot(roarOnce);
			break;
		case "step":
			AudioClip footStep = sounds[Random.Range(12,15)];
			audio.PlayOneShot(footStep);
			mainCamera.SendMessage("Shake", 0.1);
			break;
		}
	}
}
