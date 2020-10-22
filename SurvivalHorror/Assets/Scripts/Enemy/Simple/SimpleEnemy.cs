using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Pathfind2D;
using TMPro;

public class SimpleEnemy : MonoSteeringBody {
	[SerializeField] Transform _player = null;
	[SerializeField] TextMeshProUGUI _stateText = null;

	StackFSM _brain;
	SteeringManager _steeringManager;
	Rigidbody2D _rb2d;

	Vector3[] _path;
	int _pathTargetIndex;
	bool _pathRequested = false;

	void Awake() {
		_brain = new StackFSM();
		_steeringManager = new SteeringManager(this);
		_rb2d = GetComponent<Rigidbody2D>();
		GetComponentInChildren<EnemyFieldOfView>().sawPlayer += SawPlayer;

		_brain.PushState(PathPlayer);
	}

	void FixedUpdate() {
		_brain.UpdateState();

		Movement();
	}

	void Movement() {
		//transform.position = _steeringManager.Update();
		_rb2d.MovePosition(_steeringManager.Update());
		transform.LookAt(transform.position + Vector3.forward, GetVelocity().normalized);
		Debug.DrawLine(transform.position, transform.position + (Vector3)GetVelocity().normalized, Color.yellow);
	}

	#region States
	void Idle() {
		_stateText.text = "idle";
		// See player
	}

	void SeekPlayer() {
		_stateText.text = "seek";

		// Get to player
		_steeringManager.Seek(_player.position);
		// If close enough attack
		// If far lose player
	}

	void PathPlayer() {
		_stateText.text = "path";

		if (_path == null) {
			if (!_pathRequested) {
				PathRequestManager.instance.RequestPath(transform.position, _player.position, OnPathFound);
				_pathRequested = true;
			}
		}
		else {
			if (_pathTargetIndex < _path.Length) {
				_steeringManager.Seek(_path[_pathTargetIndex]);

				float __diff = (transform.position - _path[_pathTargetIndex]).magnitude;
				if (__diff < 0.3) {
					_pathTargetIndex++;
				}
			}
			else {
				_path = null;
			}
		}

		// Conditions.
	}

	void Attack() {
		_stateText.text = "attack";
		// Attack
		// after attack go after player
	}
	#endregion

	public void OnPathFound(Vector3[] p_path, bool p_success) {
		if (p_success) {
			_path = p_path;
			_pathTargetIndex = 0;
		}
		else {
			_path = null;
		}
		_pathRequested = false;
	}

	void SawPlayer(Vector3 p_position) {
		_brain.PopState();
		_brain.PushState(SeekPlayer);
	}

	public void OnDrawGizmos() {
		if (_path != null) {
			for (int i = _pathTargetIndex; i < _path.Length; i++) {
				Gizmos.color = Color.blue;
				Gizmos.DrawCube(_path[i], Vector3.one * 0.5f);

				if (i == _pathTargetIndex) {
					Gizmos.DrawLine(transform.position, _path[i]);
				}
				else {
					Gizmos.DrawLine(_path[i - 1], _path[i]);
				}
			}
		}
	}
}
