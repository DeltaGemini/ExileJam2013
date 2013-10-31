using UnityEngine;
using System.Collections;

public class MouseFire : MonoBehaviour {
	
	public float speed;
	public float minX = -14f;
	public float maxY = 10f;
	public float minY = -4f;
	
	Vector3 target;
	Vector3 startLocation;
	
	// Use this for initialization
	void Start () {
		target = transform.position;
		startLocation.y = transform.position.y;
		startLocation.z = transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {
		//target.y = startLocation.y;
		target.z = startLocation.z;
		
		if(Input.GetMouseButtonDown(0)){
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
	        if (Physics.Raycast(ray, out hit, 100)){
				hit.transform.SendMessage("Activate", SendMessageOptions.DontRequireReceiver);
				target = ray.origin;
				Debug.Log(ray.origin);
			}
		}
		
		//Don't go too far off to the left
		if(target.x < minX){
			target.x = minX;
		}
		
		if(target.y > maxY)
			target.y = maxY;
		
		if(target.y < minY)
			target.y = minY;
		
		float dist = Vector2.Distance(transform.position, target);
		
		if(dist > 2) 
		{
			Vector3 dir = target - transform.position;
			
			Vector3 scale = transform.localScale;
			scale.x = Mathf.Sign(dir.x);
			transform.localScale = scale;
			
			Vector3 pos = transform.position;
			pos += dir.normalized * Time.deltaTime * speed; //Linear speed
			
			//pos += dir * Time.deltaTime * speed;
			
			transform.position = pos;
		}
	}
}
