using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {
	public static Transform instance;

	PlayerControls _controls;
	PlayerShooter _shooter;
	PlayerMovement _movement;
	PlayerInteraction _interaction;
	Vector2 _moveInput;
	CameraBasePosition _camera;

	void Awake() {
		if (instance == null) {
			instance = this.transform;
		}
		else {
			Debug.Log("More than one player!");
		}

		_controls = new PlayerControls();
		_controls.Player.Aim.started += Aiming;
		_controls.Player.Aim.canceled += StopAiming;
		_controls.Player.Shoot.started += Shooting;
		_controls.Player.Shoot.canceled += StopShooting;
		_controls.Player.Walk.started += Walking;
		_controls.Player.Walk.canceled += StopWalking;
		_controls.Player.Interact.started += Interacted;
		_controls.Player.Reload.started += Reload;
		_controls.Player.Enable();

		_movement = GetComponent<PlayerMovement>();
		_shooter = GetComponentInChildren<PlayerShooter>();
		_interaction = GetComponent<PlayerInteraction>();
		_camera = FindObjectOfType<CameraBasePosition>();
		GetComponent<Health>().healthUpdate += HealthUpdate;
	}

	void Update() {
		_movement.UpdateDirection(_controls.Player.Move.ReadValue<Vector2>());
	}

	void Aiming(InputAction.CallbackContext p_context) {
		_movement.IsAiming = true;
		_shooter.IsAiming = true;
		_camera.IsAiming = true;
	}

	void StopAiming(InputAction.CallbackContext p_context) {
		_movement.IsAiming = false;
		_shooter.IsAiming = false;
		_camera.IsAiming = false;
	}

	void Shooting(InputAction.CallbackContext p_context) {
		_shooter.IsShooting = true;
	}

	void StopShooting(InputAction.CallbackContext p_context) {
		_shooter.IsShooting = false;
	}

	void Walking(InputAction.CallbackContext p_context) {
		_movement.IsWalking = true;
	}

	void StopWalking(InputAction.CallbackContext p_context) {
		_movement.IsWalking = false;
	}

	void Reload(InputAction.CallbackContext p_context) {
		_shooter.Reload();
	}

	void Interacted(InputAction.CallbackContext p_context) {
		_interaction.Interact();
	}

	void HealthUpdate(int p_health) {
		if (p_health == 0) {
			print("Player died");
		}
	}
}
