using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerShooter : MonoBehaviour {
	public bool IsAiming {
		get => _isAiming;
		set => _isAiming = value;
	}

	public bool IsShooting {
		get => _isShooting;
		set {
			_isShooting = value;
			if (value == false) {
				_isNewShotCommand = true;
			}
		}
	}

	public Action<int> ammoUpdate;
	public Action<Firearm> changedWeapon;

	[SerializeField] Firearm _weapon = null;
	[SerializeField] AudioSource _audioSource = null;
	float _timer = 0;
	float _deviation = 0;
	Coroutine _reloadCorroutine = null;
	bool _isAiming;
	bool _isShooting;
	bool _isNewShotCommand;


	void Start() {
		if (_weapon != null) {
			_weapon.Setup(gameObject);
			_deviation = _weapon.DrawDeviation;
			if (ammoUpdate != null) {
				ammoUpdate(_weapon.Magazine);
			}
		}
		else {
			if (ammoUpdate != null) {
				ammoUpdate(0);
			}
		}
		if (changedWeapon != null) {
			changedWeapon(_weapon);
		}
	}

	void Update() {
		_timer += Time.deltaTime;

		// No weapon.
		if (_weapon == null) {
			return;
		}

		// Aim command if not reloading.
		if (_isAiming && _reloadCorroutine == null) {
			// shrink deviation.
			_deviation -= _weapon.DeviationReductionRate * Time.deltaTime;
			_deviation = _deviation < _weapon.MinDeviation ? _weapon.MinDeviation : _deviation;

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

			// show sight.
			tmp1 = Quaternion.Euler(transform.rotation.eulerAngles + (Vector3.forward * _deviation)) * tmp;
			Debug.DrawRay(transform.position, tmp1, Color.white);
			tmp1 = Quaternion.Euler(transform.rotation.eulerAngles - (Vector3.forward * _deviation)) * tmp;
			Debug.DrawRay(transform.position, tmp1, Color.white);
			#endregion

			// Fire command.
			if (_timer > _weapon.FireRate) {
				if (_weapon.IsHold) {
					if (_isShooting) {
						if (!_weapon.IsEmpty) {
							Shoot();
						}
						else {
							Debug.Log("Out of bullets!");
						}
					}
				}
				else {
					if (_isShooting && _isNewShotCommand) {
						_isNewShotCommand = false;
						if (!_weapon.IsEmpty) {
							Shoot();
						}
						else {
							Debug.Log("Out of bullets!");
						}
					}
				}
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
		_audioSource.clip = _weapon.FireSound;
		_audioSource.Play();

		// Deviation, ammo and timer update.
		_timer = 0;
		_deviation += _weapon.FireDeviation;
		_deviation = _deviation > _weapon.MaxDeviation ? _weapon.MaxDeviation : _deviation;
		_weapon.Magazine--;
		if (ammoUpdate != null) {
			ammoUpdate(_weapon.Magazine);
		}
	}

	public void Reload() {
		if (_reloadCorroutine == null) {
			if (_weapon != null) {
				if (!_weapon.IsFull) {
					_reloadCorroutine = StartCoroutine(Reloading());
				}
			}
		}
	}

	IEnumerator Reloading() {
		int __reload = _weapon.MagazineSize - _weapon.Magazine;
		int __notReloaded = InventorySystem.instance.GetAmmo(_weapon.AmmoType, __reload);
		if (__notReloaded == __reload) {
			_reloadCorroutine = null;
			yield break;
		}

		_audioSource.clip = _weapon.ReloadSound;
		_audioSource.Play();
		yield return new WaitForSeconds(_weapon.ReloadTime);
		_weapon.Magazine = _weapon.MagazineSize - __notReloaded;

		_reloadCorroutine = null;
		if (ammoUpdate != null) {
			ammoUpdate(_weapon.Magazine);
		}
	}

	public void ChangeWeapon(Firearm p_weapon) {
		if (p_weapon == _weapon) {
			return;
		}

		_weapon = p_weapon;
		if (p_weapon != null) {
			_weapon.Setup(gameObject);
			_deviation = _weapon.DrawDeviation;
		}
		if (_reloadCorroutine != null) {
			StopCoroutine(_reloadCorroutine);
			_reloadCorroutine = null;
		}

		if (ammoUpdate != null) {
			if (p_weapon != null) {
				ammoUpdate(_weapon.Magazine);
			}
			else {
				ammoUpdate(0);
			}
		}
		if (changedWeapon != null) {
			changedWeapon(_weapon);
		}
	}
}
