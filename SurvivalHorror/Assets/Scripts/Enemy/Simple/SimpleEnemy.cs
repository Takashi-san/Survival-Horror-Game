using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Pathfind2D;

public class SimpleEnemy : MonoSteeringBody {
	StackFSM _brain;
	SteeringManager _steeringManager;
	[SerializeField] Transform _player = null;
	Vector3[] _path;
	int _pathTargetIndex;

	void Awake() {
		_brain = new StackFSM();
		_steeringManager = new SteeringManager(this);

		_brain.PushState(PathToPlayer);
	}

	void Update() {
		_brain.UpdateState();

		Movement();
	}

	void Movement() {
		transform.position = _steeringManager.Update();
	}

	#region States
	void Waiting() {
		// See player
	}

	void SeekPlayer() {
		// Get to player
		_steeringManager.Seek(_player.position);
		// If close enough attack
		// If far lose player
	}

	void Attack() {
		// Attack
		// after attack go after player
	}

	void PathToPlayer() {
		PathRequestManager.instance.RequestPath(transform.position, _player.position, OnPathFound);
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
