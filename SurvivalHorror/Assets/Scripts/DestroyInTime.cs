using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyInTime : MonoBehaviour {
	[SerializeField] float _destroyTime = 0;

	float _timer = 0;

	void Update() {
		_timer += Time.deltaTime;
		if (_timer >= _destroyTime) {
			Destroy(gameObject);
		}
	}
}
