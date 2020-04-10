using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour {
	[SerializeField] Firearm _weapon = null;
	float _timer = 0;
	float _deviation = 0;

	void Start() {
		_weapon.Setup(gameObject);
		_deviation = _weapon.DrawDeviation;
	}

	void Update() {
		_timer += Time.deltaTime;
		if (Input.GetMouseButton(1)) {
			// show min/max deviation.
			Vector3 tmp = Vector3.up * 10;
			Debug.DrawRay(transform.position, transform.TransformDirection(tmp), Color.green);
			Vector3 tmp1 = Quaternion.Euler(transform.rotation.eulerAngles + (Vector3.forward * _weapon.MaxDeviation)) * tmp;
			Debug.DrawRay(transform.position, tmp1, Color.magenta);
			tmp1 = Quaternion.Euler(transform.rotation.eulerAngles - (Vector3.forward * _weapon.MaxDeviation)) * tmp;
			Debug.DrawRay(transform.position, tmp1, Color.magenta);
			tmp1 = Quaternion.Euler(transform.rotation.eulerAngles + (Vector3.forward * _weapon.MinDeviation)) * tmp;
			Debug.DrawRay(transform.position, tmp1, Color.cyan);
			tmp1 = Quaternion.Euler(transform.rotation.eulerAngles - (Vector3.forward * _weapon.MinDeviation)) * tmp;
			Debug.DrawRay(transform.position, tmp1, Color.cyan);

			// shrink deviation.
			_deviation -= _weapon.DeviationReductionRate * Time.deltaTime;
			_deviation = _deviation < _weapon.MinDeviation ? _weapon.MinDeviation : _deviation;

			// show sight.
			tmp1 = Quaternion.Euler(transform.rotation.eulerAngles + (Vector3.forward * _deviation)) * tmp;
			Debug.DrawRay(transform.position, tmp1, Color.white);
			tmp1 = Quaternion.Euler(transform.rotation.eulerAngles - (Vector3.forward * _deviation)) * tmp;
			Debug.DrawRay(transform.position, tmp1, Color.white);

			if (_timer > _weapon.FireRate) {
				if (_weapon.IsHold) {
					if (Input.GetMouseButton(0)) {
						Shoot();
						_timer = 0;
						_deviation += _weapon.FireDeviation;
						_deviation = _deviation > _weapon.MaxDeviation ? _weapon.MaxDeviation : _deviation;
					}
				}
				else {
					if (Input.GetMouseButtonDown(0)) {
						Shoot();
						_timer = 0;
						_deviation += _weapon.FireDeviation;
						_deviation = _deviation > _weapon.MaxDeviation ? _weapon.MaxDeviation : _deviation;
					}
				}
			}
		}
		else {
			_deviation = _weapon.DrawDeviation;
		}
	}

	void Shoot() {
		float deviation = Random.Range(-_deviation, _deviation);
		Quaternion direction = Quaternion.Euler(transform.rotation.eulerAngles + (Vector3.forward * deviation));
		_weapon.Fire(gameObject, direction);
	}
}
