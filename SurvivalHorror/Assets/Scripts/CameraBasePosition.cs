using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraBasePosition : MonoBehaviour {
	[SerializeField] Transform _player = null;
	/*
	[SerializeField] [Range(0, 1)] float _mouseRatio = 0;
	[SerializeField] [Min(0)] float _maxDistanceFromPlayer = 0;
	[SerializeField] [Min(1)] float _positionAproachRatio = 1;
	*/
	[SerializeField] [Min(1)] float _playerAproachRatio = 1;

	bool _lockToPlayer = false;

	public Action lockToPlayer;
	public Action unlockFromPlayer;

	void Awake() {
		lockToPlayer += LockToPlayer;
		unlockFromPlayer += UnlockFromPlayer;
	}

	void LateUpdate() {
		//if (_lockToPlayer) {
		if (true) {
			Vector3 diff = (_player.position - transform.position) / _playerAproachRatio;
			diff.z = 0;

			transform.position = transform.position + diff;
		}
		else {
			/*
			Vector3 camera = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
			Vector3 diff = camera - _player.position;
			float magnitude = diff.magnitude * _mouseRatio;

			if (magnitude > _maxDistanceFromPlayer) {
				transform.position = new Vector3(_player.position.x + diff.normalized.x * _maxDistanceFromPlayer, _player.position.y + diff.normalized.y * _maxDistanceFromPlayer, -10);
			}
			else {
				transform.position = new Vector3(_player.position.x + diff.normalized.x * magnitude, _player.position.y + diff.normalized.y * magnitude, -10);
			}
			*/
		}
	}

	void LockToPlayer() {
		_lockToPlayer = true;
	}

	void UnlockFromPlayer() {
		_lockToPlayer = false;
	}
}
