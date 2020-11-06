using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour {
	[SerializeField] Animator _torso = null;
	[SerializeField] Animator _legs = null;

	Rigidbody2D _rb;
	PlayerMovement _movement = null;

	void Start() {
		_rb = GetComponent<Rigidbody2D>();
		_movement = GetComponent<PlayerMovement>();
	}

	void Update() {
		if (_rb.velocity == Vector2.zero) {
			_torso.SetBool("Moving", false);
			_legs.SetBool("Moving", false);
		}
		else {
			_torso.SetBool("Moving", _movement.IsWalking ? false : true);
			_legs.SetBool("Moving", true);
		}
	}
}
