using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DinoControl : MonoBehaviour {
	
	public float speed;
	public GameObject child;
	public Camera mainCamera;
	public float minX = -14f;
	public float maxY = -2.5f;
	public float minY = -4f;	
	public static bool roaring = false;
	
	public static bool enemyCounting = false;
	
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
	
	float enemyTimer = 0;
	
	public static List<GameObject> followers = new List<GameObject>();
	
	//public AudioClip[] sounds;
	public AudioSource[] sounds;
	public AudioSource[] snapSoundsLeft;
	public AudioSource[] snapSoundsRight;
	
	// public AudioSource[] sounds_Snaps;
	[HideInInspector] public int canTriggerNewSnapSound = 1;
	[HideInInspector] public int numberOfSnapsPlaying;
	
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
			scale.x = Mathf.Sign(dir.x);
			
			transform.localScale = scale;
			
			Vector3 pos = transform.position;
			pos += dir.normalized * Time.deltaTime * speed; //Linear speed
			pos.z = pos.y - 2f;
			Animate(walkAnim.name);
			//pos += dir * Time.deltaTime * speed;
			
			transform.position = pos;
		}
		
		if(Input.GetMouseButtonUp(0))
		{
			canTriggerNewSnapSound = 1;
		}		
		
		if(enemyCounting){
			enemyTimer += Time.deltaTime;
		} else {
			enemyTimer = 0;
		}
		
		Debug.Log(enemyCounting + ", " + enemyTimer);
		
		if(enemyTimer >= 5){
			int num = followers.Count;
			Debug.Log(num);
			if(num >= 1){
				followers[0].SendMessage("FollowOff");
				followers.RemoveAt(0);
				enemyCounting = false;
			}
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
						PlaySound("roar");
						mainCamera.gameObject.SendMessage("Shake", 0.2);
						roaring = true;
					} else {
						AnimateBlend(jawAnim.name);
						roaring = false;
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
					if(mouseTime <= 0.05f){	
						target = transform.position;
						AnimateBlend(frontWristAnim.name);
						if(canTriggerNewSnapSound == 1)
						{
							if(snapSoundsLeft[0].isPlaying == false &&  snapSoundsLeft[1].isPlaying == false && snapSoundsLeft[2].isPlaying == false )
							{
								canTriggerNewSnapSound = 0;
								PlaySound("clawSnapLeft");
							}
						}
					}
					
					break;
				case "arm_back_wrist":
					if(mouseTime <= 0.05f){						
						target = transform.position;
						AnimateBlend(backWristAnim.name);
						if(canTriggerNewSnapSound == 1)
						{							
							if(snapSoundsRight[0].isPlaying == false &&  snapSoundsRight[1].isPlaying == false && snapSoundsRight[2].isPlaying == false )
							{	
								canTriggerNewSnapSound = 0;
								PlaySound("clawSnapRight");
							}
						}
					}
					break;
				case "tail09":
				case "tail07":
				case "tail08":
				case "tail06":
					target = transform.position;
					AnimateBlend(tailAnim.name);
					break;
				case "Animation":
					target = transform.position;
					hit.transform.gameObject.SendMessageUpwards("Activate");
					break;
				default:
					target = ray.origin;
					roaring = false;
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
	
	void OnTriggerEnter (Collider col){
		if(col.gameObject.tag == "ClearTimer"){
			enemyCounting = false;
		}
	}
	
	void turnOffCounter(){
		enemyCounting = false;
	}
	
	public void PlaySound (string evt) {		
		switch (evt){
		case "roar":
			int randomRoar = Random.Range(16, 19);
			sounds[randomRoar].Play();
			break;
		case "step":
			int randomStep = Random.Range(12, 15);
			sounds[randomStep].Play();
			mainCamera.SendMessage("Shake", 0.05);
			break;
		case "breatheIn":
			int randomBreathIn = Random.Range(30, 39);
			sounds[randomBreathIn].Play();
            break;
        case "breatheOut":            
			int randomBreathOut = Random.Range(20, 29);
			sounds[randomBreathOut].Play();
            break;
		case "clawSnapRight":			
			int randomSnapRight = Random.Range(0, snapSoundsRight.Length);
			snapSoundsRight[randomSnapRight].Play();
			break;
		case "clawSnapLeft":
			int randomSnapLeft = Random.Range(0, snapSoundsLeft.Length);
			snapSoundsLeft[randomSnapLeft].Play();
			break;
        }
	}
}