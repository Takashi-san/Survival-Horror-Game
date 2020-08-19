using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBasePosition : MonoBehaviour {
	[SerializeField] Transform _player = null;
	[SerializeField] [Range(0, 1)] float _cameraRatio = 0;
	[SerializeField] [Min(0)] float _maxDistanceFromPlayer = 0;

	void LateUpdate() {
		Vector3 camera = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
		Vector3 diff = camera - _player.position;
		float magnitude = diff.magnitude * _cameraRatio;

		if (magnitude > _maxDistanceFromPlayer) {
			transform.position = new Vector3(_player.position.x + diff.normalized.x * _maxDistanceFromPlayer, _player.position.y + diff.normalized.y * _maxDistanceFromPlayer, -10);
		}
		else {
			transform.position = new Vector3(_player.position.x + diff.normalized.x * magnitude, _player.position.y + diff.normalized.y * magnitude, -10);
		}
	}
}
