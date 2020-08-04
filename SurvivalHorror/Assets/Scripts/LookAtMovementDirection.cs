using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMovementDirection : MonoBehaviour {
	[SerializeField] Movement _movement = null;
	Vector2 _direction;

	void Update() {
		if (_movement.Direction != Vector2.zero) {
			_direction = _movement.Direction;
		}
	}

	// The up vector is the one who points towards target.
	void FixedUpdate() {
		Vector3 target = _direction;
		transform.LookAt(transform.position + Vector3.forward, target);
	}
}
