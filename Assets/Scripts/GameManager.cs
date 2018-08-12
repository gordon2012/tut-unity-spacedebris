using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AudioSource))]

public class GameManager : MonoBehaviour {

	public GameObject[] debris; 
	public float minTime;
	public float maxtime;
	public int totalItemsOnScreen; 
	public GameObject screenDebris;
	private float randomTime; 
	private float curTime;

	// Use this for initialization
	/**
	 * Get a random time at which to spawn the first Debris
	 */
	void Start () {
		randomTime = Random.Range (minTime, maxtime);
	}
	
	// Update is called once per frame
	/**
	 * Spawn Debris, when time interval is bigger than randomTime and there is less Debris than totalItemsOnScreen.
	 * If there is more than one model in debris[], the game will use this to randomize how the Debris looks.
	 */
	void Update () {
		curTime += Time.deltaTime; 
		if (curTime > randomTime && screenDebris.transform.childCount < totalItemsOnScreen) {
			Vector3 spawnPoint = screenDebris.transform.position;
			int index = (int) Random.Range(0, debris.Length); 
			GameObject debrisItem = Instantiate(debris[index], spawnPoint, Quaternion.identity) as GameObject;
			Vector3 newPosition = debrisItem.transform.position; 
			debrisItem.transform.position = newPosition; 
			debrisItem.transform.parent = screenDebris.transform; 
			newPosition.z = 0;
			curTime = 0; 
		}
	}
}
