using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour {
	[SerializeField] GameObject _projectile = null;
	[SerializeField] float _variation = 0;
	[SerializeField] float _fireRate = 1;
	[SerializeField] bool _isHold = true;
	float _timer = 0;
	float _deviation = 0;

	void Update() {
		_timer += Time.deltaTime;
		if (Input.GetMouseButton(1)) {
			// show sight.
			Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up * 10), Color.green);
			Vector3 tmp = Vector3.up * 10;
			Vector3 tmp1 = Quaternion.Euler(transform.rotation.eulerAngles + (Vector3.forward * _variation)) * tmp;
			Debug.DrawRay(transform.position, tmp1, Color.magenta);
			tmp1 = Quaternion.Euler(transform.rotation.eulerAngles - (Vector3.forward * _variation)) * tmp;
			Debug.DrawRay(transform.position, tmp1, Color.magenta);

			// shrink variation.

			if (_timer > _fireRate) {
				if (_isHold) {
					if (Input.GetMouseButton(0)) {
						Shoot();
						_timer = 0;
					}
				}
				else {
					if (Input.GetMouseButtonDown(0)) {
						Shoot();
						_timer = 0;
					}
				}
			}
		}
	}

	void Shoot() {
		float variation = Random.Range(-_variation, _variation);
		Quaternion direction = Quaternion.Euler(transform.rotation.eulerAngles + (Vector3.forward * variation));
		Instantiate(_projectile, transform.position, direction);
	}
}
