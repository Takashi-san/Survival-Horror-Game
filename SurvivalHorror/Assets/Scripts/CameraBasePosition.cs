using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraBasePosition : MonoBehaviour {
	public bool IsAiming {
		get => _isAiming;
		set {
			_isAiming = value;
		}
	}

	[SerializeField] [Min(0)] float _maxOffset = 0;
	[SerializeField] [Min(0)] float _mouseInfluenceLimitDistance = 0;
	PlayerControls _controls;
	bool _isAiming = false;
	Vector3 _offset = Vector3.zero;
	float _timer = 0;

	void Awake() {
		_controls = new PlayerControls();
		_controls.Player.MousePosition.Enable();
	}

	void LateUpdate() {
		Vector3 __target = Player.instance.position;
		__target.z = 0;

		_timer += Time.deltaTime;
		Vector3 __camera = Camera.main.ScreenToWorldPoint(_controls.Player.MousePosition.ReadValue<Vector2>());
		__camera.z = _mouseInfluenceLimitDistance / 3;
		Vector3 __direction = __camera - __target;
		_offset = __direction.normalized;
		_offset.z = 0;
		_offset *= _maxOffset;

		__target.z = -10;
		transform.position = __target + _offset;
	}
}
