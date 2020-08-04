using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour {
	[SerializeField] Movement _movement = null;
	[SerializeField] Animator _torso = null;
	[SerializeField] Animator _legs = null;

	void Update() {
		if (_movement.Direction == Vector2.zero) {
			_torso.SetBool("Moving", false);
			_legs.SetBool("Moving", false);
		}
		else {
			_torso.SetBool("Moving", true);
			_legs.SetBool("Moving", true);
		}
	}
}
