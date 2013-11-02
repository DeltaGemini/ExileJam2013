using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour {
	
	public GameObject player;
	bool triggerActive = false;
	public int dir = -1;
	public GameObject child;
	public AudioClip[] roarEffect;
	
	AnimationState anim01;
	
	// Use this for initialization
	void Start () {
		Vector3 scale = transform.localScale;
		scale.x = Mathf.Sign(dir);		
		transform.localScale = scale;
		
		anim01 = child.animation["EnemyEmerge"];
	}
	
	void Update() {
		float dist = transform.position.x - player.transform.position.x;
		if(dist <= 85){
			if(DinoControl.roaring && triggerActive){
				Deactivated ();
			}
		}
		if(!child.animation.isPlaying){
			Debug.Log ("Ready to shout");
		} else {
			Debug.Log("Shouting");
		}
	}
	
	void Activate() {
		if(!triggerActive){
			child.animation.Play(anim01.name);
			triggerActive = true;
		}
	}
	
	void Deactivated(){
		anim01.speed = -1;
		child.animation.Play(anim01.name);
		player.SendMessage("turnOffCounter");
	}
	
	void PlaySound(){
		AudioClip roarOnce = roarEffect[Random.Range(0,3)];
		audio.PlayOneShot(roarOnce);
	}
}