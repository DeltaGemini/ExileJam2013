using UnityEngine;
using System.Collections;

public class MouseFire : MonoBehaviour {
	
	public GameObject dinosaur;
	public float speed;
	
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
		Debug.Log(target);
		target.y = startLocation.y;
		target.z = startLocation.z;
		
		if(Input.GetMouseButtonDown(0)){
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
	        if (Physics.Raycast(ray, out hit, 100)){
	            print(hit.transform.name);
				hit.transform.SendMessage("Activate", SendMessageOptions.DontRequireReceiver);
				target = ray.origin;
			}			
		}
		
		float dist = Vector2.Distance(transform.position, target);
		if(dist > 2) {
			transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime);
		}
	}
}
