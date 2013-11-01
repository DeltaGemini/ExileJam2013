using UnityEngine;
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
	
	public AudioClip[] sounds;
	
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
			scale.x = Mathf.Sign(dir.x);
			
			transform.localScale = scale;
			
			Vector3 pos = transform.position;
			pos += dir.normalized * Time.deltaTime * speed; //Linear speed
			pos.z = pos.y - 2f;
			Animate(walkAnim.name);
			//pos += dir * Time.deltaTime * speed;
			
			transform.position = pos;
		}
		
		if(Input.GetMouseButton(0)){
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
	        if (Physics.Raycast(ray, out hit, 1000)){
				hit.transform.SendMessage("Activate", SendMessageOptions.DontRequireReceiver);
				Debug.Log(hit.transform.name);
				switch(hit.transform.name){
				case "neck":
					target = transform.position;
					Animate(roarAnim.name);
					PlaySound("roar");
					mainCamera.gameObject.SendMessage("Shake", 0.2);
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
					Animate(frontWristAnim.name);
					break;
				case "arm_back_wrist":
					target = transform.position;
					Animate(backWristAnim.name);
					break;
				case "tail09":
				case "tail07":
				case "tail08":
				case "tail06":
					target = transform.position;
					Animate (tailAnim.name);
					break;
				default:
					target = ray.origin;
					break;
				}					
			}
		}
		
		//Don't go too far off to the left
		if(target.x < minX)
			target.x = minX;		
		
		if(target.y > maxY)
			target.y = maxY;
		
		if(target.y < minY)
			target.y = minY;		
		
		if(child.animation["Walk"].time == 0.21 || child.animation["Walk"].time == 2){
			Debug.Log("Step");
			PlaySound("step");
		}		
	}
	
	void Animate(string name){
		child.animation.CrossFade(name);
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
			mainCamera.SendMessage("Shake", 0.05);
			break;
		}
	}
}
