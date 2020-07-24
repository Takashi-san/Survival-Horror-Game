using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
	[SerializeField] MovementBrain _brain = null;
	[SerializeField] float _speed = 0;
	Rigidbody2D _rb2d = null;
	Vector2 _move = Vector2.zero;

	void Start() {
		if (!_brain) {
			Debug.LogWarning("No brain attached!");
		}
		_rb2d = GetComponent<Rigidbody2D>();
	}

	void Update() {
		_move = _brain.GetMovement(transform);
	}

	void FixedUpdate() {
		Vector2 target = (Vector2)transform.position + (_move * _speed * Time.fixedDeltaTime);
		_rb2d.MovePosition(target);
	}
}
