using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {
	
	public GameObject target;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float dist = target.transform.position.x - transform.position.x;
		
		Vector3 pos = transform.position;
		pos.x += dist;
		transform.position = pos;
	}
}
