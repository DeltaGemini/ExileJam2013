using UnityEngine;
using System.Collections;

public class PetBehaviour : MonoBehaviour {
	
	public GameObject parent;
	Vector3 target;
	public float speed = 5;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		target = parent.transform.position;
		
		Vector3 dir = target - transform.position;			
		Vector3 scale = transform.localScale;
		
		if(dir.x > 10 || dir.x < -10){
			
			scale.x = Mathf.Sign(dir.x);
			
			transform.localScale = scale;
			
			Vector3 pos = transform.position;
			pos += dir.normalized * Time.deltaTime * speed; //Linear speed
			pos.z = pos.y - 2f;
			//pos += dir * Time.deltaTime * speed;
			
			transform.position = pos;
		}
		
		Debug.Log (dir.x);
	}
}
