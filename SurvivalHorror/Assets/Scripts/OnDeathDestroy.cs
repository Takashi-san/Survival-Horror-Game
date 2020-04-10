using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDeathDestroy : MonoBehaviour {
	void Awake() {
		GetComponent<Health>().healthUpdate += Check;
	}

	void Check(int health) {
		if (health == 0) {
			Destroy(gameObject);
		}
	}
}
