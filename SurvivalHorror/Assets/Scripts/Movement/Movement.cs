using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
	[SerializeField] MovementBrain _brain = null;
	Rigidbody2D _rb2d = null;
	Vector2 _move = Vector2.zero;

	void Awake() {
		_brain.Setup(gameObject);
		_rb2d = GetComponent<Rigidbody2D>();
	}

	void Update() {
		_move = _brain.GetMovement(transform);
	}

	void FixedUpdate() {
		Vector2 target = (Vector2)transform.position + (_move * _brain.Speed * Time.fixedDeltaTime);
		_rb2d.MovePosition(target);
	}
}
