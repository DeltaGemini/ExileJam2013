using UnityEngine;
using System.Collections;

public class MouseFire : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// Shoot the ray on mouse click
	    if (Input.GetButtonUp("Fire1"))
	    {
	        RaycastHit hit;
	 
	        // Cast a ray forward from our position for the specified distance
	        if (Physics.Raycast(transform.position, transform.forward, hit, distance))
	        {
				
			}	    
	    }
	}
}
