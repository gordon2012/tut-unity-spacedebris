using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	/**
	 * Remove Bullet when it leaves the screen
	 */
	void OnBecameInvisible() {
		Destroy (gameObject);
	}

}
