using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {
	
	private Vector3 originPosition;
	private Quaternion originRotation;
	public float shake_decay;
	public float shake_intensity;
	public GameObject player;
 
	void Update (){
      	if (shake_intensity > 0){
			originPosition.x = player.transform.position.x;
			
         	transform.position = originPosition + Random.insideUnitSphere * shake_intensity;
         	transform.rotation = new Quaternion(
         	originRotation.x + Random.Range (-shake_intensity,shake_intensity) * .2f,
         	originRotation.y + Random.Range (-shake_intensity,shake_intensity) * .2f,
         	originRotation.z + Random.Range (-shake_intensity,shake_intensity) * .2f,
         	originRotation.w + Random.Range (-shake_intensity,shake_intensity) * .2f);
			shake_intensity -= shake_decay;
		}
	}
 
	public void Shake(float intensity){
		
		if(shake_intensity <= 0){
			/*Vector3 temp = transform.position;
			temp.z = player.transform.position.z;
			*/
			originPosition = transform.position;
			originRotation = transform.rotation;
			shake_intensity = intensity;
			shake_decay = 0.003f;
		}
	}
}