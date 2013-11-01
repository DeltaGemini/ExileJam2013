using UnityEngine;
using System.Collections;

public class Sounds_Atmos : MonoBehaviour {
	
	
	public AudioSource[] birdsAudioSources;
	[HideInInspector] public int birdSoundsActive;
	public float birdSpawnTimeMin;
	public float birdSpawnTimeMax;
	public float birdVolMin;
	public float birdVolMax;
	public float birdPitchMin;
	public float birdPitchMax;
	public float birdCutoffFreq;
	
	public AudioSource[] earthquakeAudiosources;
	[HideInInspector] public int earthquakeSoundsActive;
	public float earthquakeSpawnTimeMin;
	public float earthquakeSpawnTimeMax;
	
	

	
	// Use this for initialization
	void Start () 

	{
	
	}
	
	// Update is called once per frame
	void Update () 

	{
		int randomNumber;
		randomNumber = Random.Range(0, 3);
		Debug.Log("random: " + randomNumber);
		
		
		
		if(randomNumber == 2)
		{
			if(birdSoundsActive == 0)
			{
				StartCoroutine( SpawnBirdSoundsCoroutine() );
			}
		}
		
		int randomSpawnEarthQuake;
		randomSpawnEarthQuake = Random.Range(0, 3);
		
		if(randomSpawnEarthQuake == 2)
		{
			if(earthquakeSoundsActive == 0)
			{
				StartCoroutine( SpawnEarthquakeSoundsCoroutine() );
			}
		}
		
		
		

		
	}








IEnumerator SpawnBirdSoundsCoroutine ()
	{
		birdSoundsActive = 1;

		float randomVolume = Random.Range(birdVolMin, birdVolMax);
		float randomPitch = Random.Range(birdPitchMin, birdPitchMax);
		int nextAudioSource = Random.Range(0, birdsAudioSources.Length);
		birdsAudioSources[nextAudioSource].volume = randomVolume;
		birdsAudioSources[nextAudioSource].pitch = randomPitch;
		birdsAudioSources[nextAudioSource].Play();
		Debug.Log("started sound");
		float waitUntilNextSpawn = Random.Range(birdSpawnTimeMin, birdSpawnTimeMax); // wait until next spawn
	
		yield return new WaitForSeconds (birdsAudioSources[nextAudioSource].clip.length);
		
		yield return new WaitForSeconds (waitUntilNextSpawn);
		

		birdSoundsActive = 0;
	}
	
IEnumerator SpawnEarthquakeSoundsCoroutine ()
	{
		earthquakeSoundsActive = 1;

		
		int nextAudioSource = Random.Range(0, earthquakeAudiosources.Length);
		earthquakeAudiosources[nextAudioSource].Play();
		Debug.Log("started quake");
		float waitUntilNextSpawn = Random.Range(earthquakeSpawnTimeMin, earthquakeSpawnTimeMax); // wait until next spawn
	
		yield return new WaitForSeconds (earthquakeAudiosources[nextAudioSource].clip.length);
		
		yield return new WaitForSeconds (waitUntilNextSpawn);
		

		earthquakeSoundsActive = 0;
	}
	
	

}
