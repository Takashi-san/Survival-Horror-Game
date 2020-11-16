using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouse : MonoBehaviour {
	public bool isActive = true;
	PlayerControls _controls;

	void Awake() {
		_controls = new PlayerControls();
		_controls.Player.MousePosition.Enable();
	}

	// The up vector is the one who points towards target.
	void FixedUpdate() {
		if (isActive) {
			Vector3 target = Camera.main.ScreenToWorldPoint(_controls.Player.MousePosition.ReadValue<Vector2>()) - transform.position;
			target.z = 0;
			transform.LookAt(transform.position + Vector3.forward, target);
		}
	}
}
