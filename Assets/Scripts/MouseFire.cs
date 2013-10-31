using UnityEngine;
using System.Collections;

public class MouseFire : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
	        if (Physics.Raycast(ray, out hit, 1000))
	            print(hit.transform.name);
				hit.transform.SendMessage("Activate", SendMessageOptions.DontRequireReceiver);
		}
	}
}
