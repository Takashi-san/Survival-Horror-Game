using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringManagerRB2D : MonoBehaviour {
	[SerializeField] [Min(0)] float _maxVelocity = 0;
	[SerializeField] [Min(0)] float _maxSteering = 0;

	Vector2 _steering;
	Rigidbody2D _rb2d;

	void Awake() {
		_rb2d = GetComponent<Rigidbody2D>();
	}

	public void SetMaxVelocity(float p_maxVelocity) {
		_maxVelocity = p_maxVelocity;
	}

	public void MovementUpdate() {
		_steering = Vector2.ClampMagnitude(_steering, _maxSteering);
		_steering = _steering / _rb2d.mass;

		_rb2d.velocity += _steering;

		_steering = Vector2.zero;
	}

	public void SeekPosition(Vector2 p_target, float p_slowRadius = 0) {
		_steering += DoSeekPosition(p_target, p_slowRadius);
	}

	Vector2 DoSeekPosition(Vector2 p_target, float p_slowRadius) {
		Vector2 __desired = p_target - (Vector2)transform.position;
		float __distance = __desired.magnitude;

		if (__distance < p_slowRadius) {
			__desired = __desired.normalized * _maxVelocity * (__distance / p_slowRadius);
		}
		else {
			__desired = __desired.normalized * _maxVelocity;
		}

		return __desired - _rb2d.velocity;
	}

	public void SeekDirection(Vector2 p_target) {
		_steering += DoSeekDirection(p_target);
	}

	Vector2 DoSeekDirection(Vector2 p_target) {
		return p_target.normalized * _maxVelocity - _rb2d.velocity;
	}

	public void FleePosition(Vector2 p_target, float p_distance, float p_slowDistanceRatio = 1) {
		_steering += DoFleePosition(p_target, p_distance, p_slowDistanceRatio);
	}

	Vector2 DoFleePosition(Vector2 p_target, float p_distance, float p_slowDistanceRatio) {
		Vector2 __desired = (Vector2)transform.position - p_target;
		float __distance = __desired.magnitude;

		if (__distance > p_distance * p_slowDistanceRatio) {
			__desired = __desired.normalized * _maxVelocity * (1 - ((__distance - p_distance * p_slowDistanceRatio) / (p_distance - p_distance * p_slowDistanceRatio)));
		}
		else {
			__desired = __desired.normalized * _maxVelocity;
		}

		return __desired - _rb2d.velocity;
	}

	public void FleeDirection(Vector2 p_target) {
		_steering += DoFleeDirection(p_target);
	}

	Vector2 DoFleeDirection(Vector2 p_target) {
		return -p_target.normalized * _maxVelocity - _rb2d.velocity;
	}
}
