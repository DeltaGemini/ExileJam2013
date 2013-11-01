using UnityEngine;
using System.Collections;

public class SimpleFollow : MonoBehaviour {

	public GameObject parent;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 temp = transform.position;
		temp.x = parent.transform.position.x;
		transform.position = temp;
	}
}
