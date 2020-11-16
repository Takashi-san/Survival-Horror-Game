using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	public bool IsWalking {
		get => _isWalking;
		set => _isWalking = value;
	}

	public bool IsAiming {
		get => _isAiming;
		set => _isAiming = value;
	}

	public bool canWalk = true;

	[SerializeField] [Min(0)] float _maxVelocity = 0;
	[SerializeField] [Range(0, 1)] float _walkingModifier = 0;
	[SerializeField] [Range(0, 1)] float _aimingModifier = 0;
	[SerializeField] [Min(0)] float _maxSteering = 0;
	bool _isWalking = false;
	bool _isAiming = false;

	Rigidbody2D _rb;
	Vector2 _targetDirection;

	void Start() {
		_rb = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate() {
		Vector2 __steering;
		if (_isAiming) {
			__steering = (_targetDirection * _maxVelocity * _aimingModifier) - _rb.velocity;
		}
		else if (_isWalking) {
			__steering = (_targetDirection * _maxVelocity * _walkingModifier) - _rb.velocity;
		}
		else {
			__steering = (_targetDirection * _maxVelocity) - _rb.velocity;
		}
		__steering = Vector2.ClampMagnitude(__steering, _maxSteering);
		__steering = __steering / _rb.mass;

		if (!canWalk) {
			__steering = Vector2.zero;
		}
		_rb.velocity += __steering;
	}

	public void UpdateDirection(Vector2 p_direction) {
		_targetDirection = p_direction;
	}
}
