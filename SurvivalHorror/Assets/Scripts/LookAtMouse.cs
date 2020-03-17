using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouse : MonoBehaviour {
	// The up vector is the one who points towards target.
	void FixedUpdate() {
		Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
		target.z = 0;
		transform.LookAt(transform.position + Vector3.forward, target);
	}
}
