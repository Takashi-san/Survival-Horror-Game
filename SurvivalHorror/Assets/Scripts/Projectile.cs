using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
	[SerializeField] float _speed = 0;
	[SerializeField] float _timeToDestroy = 1;
	Rigidbody2D _rb = null;
	float _timer = 0;

	void Start() {
		_rb = GetComponent<Rigidbody2D>();
		_rb.AddRelativeForce(Vector2.up * _speed, ForceMode2D.Impulse);
	}

	void Update() {
		_timer += Time.deltaTime;
		if (_timer > _timeToDestroy) {
			Destroy(gameObject);
		}
	}
}
