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
	
	
	// Use this for initialization
	void Start () {
		target = transform.position;
		startLocation.y = transform.position.y;
		startLocation.z = transform.position.z;
		roarAnim = child.animation["Roar"];
		roarAnim.layer = 2;		
		jumpAnim = child.animation["Jump"];
		jumpAnim.layer = 1;
	}
	
	// Update is called once per frame
	void Update () {
		
		float dist = Vector2.Distance(transform.position, target);
		
		if(dist <= 3) {			
			Debug.Log ("Stopped");
			target = transform.position;
			Animate("Idle");
			
		} else {			
			Debug.Log ("Walking");
			Vector3 dir = target - transform.position;
			
			Vector3 scale = transform.localScale;
			scale.x = Mathf.Sign(dir.x);
			
			transform.localScale = scale;
			
			Vector3 pos = transform.position;
			pos += dir.normalized * Time.deltaTime * speed; //Linear speed
			pos.z = pos.y - 2f;
			Animate("Walk");
			//pos += dir * Time.deltaTime * speed;
			
			transform.position = pos;
		}
		
		if(Input.GetMouseButtonDown(0)){
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
	        if (Physics.Raycast(ray, out hit, 100)){
				hit.transform.SendMessage("Activate", SendMessageOptions.DontRequireReceiver);
				
				switch(hit.transform.name){
				case "neck":
			target = transform.position;
					Animate(roarAnim.name);
					mainCamera.gameObject.SendMessage("Shake");
					break;
				case "foot_back":
			target = transform.position;
					Animate(jumpAnim.name);
					break;
				case "foot_front":
			target = transform.position;
					Animate(jumpAnim.name);
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
		
	}
	
	void Animate(string name){
		child.animation.CrossFade(name);
	}
	
	void PlaySound () {
		
	}
}
