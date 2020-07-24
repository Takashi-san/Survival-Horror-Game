using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Shooter : MonoBehaviour {
	[SerializeField] Firearm _weapon = null;
	float _timer = 0;
	float _deviation = 0;
	Coroutine _reloadCorroutine = null;

	public Action<int> ammoUpdate;

	void Start() {
		_weapon.Setup(gameObject);
		_deviation = _weapon.DrawDeviation;
		if (ammoUpdate != null)
			ammoUpdate(_weapon.Magazine);
	}

	void Update() {
		_timer += Time.deltaTime;

		// Reload command.
		if (Input.GetKeyDown(KeyCode.R)) {
			Reload();
		}

		// Aim command if not reloading.
		if (Input.GetMouseButton(1) && (_reloadCorroutine == null)) {
			#region Aim Draw
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
			#endregion

			if (!_weapon.IsEmpty) {
				if (_timer > _weapon.FireRate) {
					if (_weapon.IsHold) {
						if (Input.GetMouseButton(0))
							Shoot();
					}
					else {
						if (Input.GetMouseButtonDown(0))
							Shoot();
					}
				}
			}
			else {
				Debug.Log("Out of bullets!");
			}
		}
		else {
			_deviation = _weapon.DrawDeviation;
		}
	}

	void Shoot() {
		// Firing.
		float deviation = UnityEngine.Random.Range(-_deviation, _deviation);
		Quaternion direction = Quaternion.Euler(transform.rotation.eulerAngles + (Vector3.forward * deviation));
		_weapon.Fire(gameObject, direction);

		// Deviation, ammo and timer update.
		_timer = 0;
		_deviation += _weapon.FireDeviation;
		_deviation = _deviation > _weapon.MaxDeviation ? _weapon.MaxDeviation : _deviation;
		_weapon.Magazine--;
		if (ammoUpdate != null)
			ammoUpdate(_weapon.Magazine);
	}

	void Reload() {
		if (_reloadCorroutine == null) {
			_reloadCorroutine = StartCoroutine(Reloading());
		}
	}

	IEnumerator Reloading() {
		yield return new WaitForSeconds(_weapon.ReloadTime);
		_weapon.Magazine = _weapon.MagazineSize; // Full reload for test.
		_reloadCorroutine = null;
		if (ammoUpdate != null)
			ammoUpdate(_weapon.Magazine);
	}

	public void ChangeWeapon(Firearm weapon) {
		if (weapon != null) {
			_weapon = weapon;
			if (_reloadCorroutine != null) {
				StopCoroutine(_reloadCorroutine);
				_reloadCorroutine = null;
			}
			if (ammoUpdate != null)
				ammoUpdate(_weapon.Magazine);
		}
	}
}
