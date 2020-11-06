using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMovementDirection : MonoBehaviour {
	[SerializeField] Rigidbody2D _rb = null;

	// The up vector is the one who points towards target.
	void FixedUpdate() {
		transform.LookAt(transform.position + Vector3.forward, _rb.velocity.normalized);
	}
}
