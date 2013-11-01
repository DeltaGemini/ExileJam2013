using UnityEngine;
using System.Collections;

public class PetBehaviour : MonoBehaviour {
	
	public GameObject parent;
	public GameObject child;
	Vector3 target;
	public float speed = 5;
	public float dist = 8;
	
	float parentY;
	AnimationState walkAnim;
	
	// Use this for initialization
	void Start () {		
		walkAnim = child.animation["walk"];
		walkAnim.layer = 3;	
	}
	
	// Update is called once per frame
	void Update () {
		target = parent.transform.position;
		
		Vector3 dir = target - transform.position;			
		Vector3 scale = transform.localScale;
		
		if(dir.x > dist || dir.x < -dist){
			
			scale.x = Mathf.Sign(dir.x);
			
			transform.localScale = scale;
			
			Vector3 pos = transform.position;
			pos += dir.normalized * Time.deltaTime * speed; //Linear speed
			pos.z = parent.transform.position.z + 0.2f;
			//pos += dir * Time.deltaTime * speed;
			
			transform.position = pos;
			Animate(walkAnim.name);
		}
		
		parentY = parent.transform.position.y - parent.gameObject.collider.bounds.size.y/2;
		
		Vector3 newPos = transform.position;
		newPos.y = parentY + gameObject.collider.bounds.size.y/2 + 7.5f;
		transform.position = newPos;
		
		Debug.Log(transform.position.y + ", " + parent.transform.position.y);
	}	
	
	void Animate(string name){
		child.animation.CrossFade(name);
	}
}
