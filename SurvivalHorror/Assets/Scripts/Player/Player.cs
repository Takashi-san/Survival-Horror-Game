using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {
	public static Player instance;

	[SerializeField] GameObject _deadPrefab = null;

	PlayerControls _controls;
	PlayerShooter _shooter;
	PlayerMovement _movement;
	PlayerInteraction _interaction;
	PlayerAnimatorController _animator;
	Vector2 _moveInput;
	CameraBasePosition _camera;
	UIInventoryMenu _inventoryMenu;
	LookAtMouse _lookAtMouse;
	ScreenShake _screenShake;

	void Awake() {
		if (instance == null) {
			instance = this;
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

		_controls.UI.Inventory.performed += Inventory;
		_controls.UI.Pause.performed += ScreenShake;
		_controls.UI.Enable();

		_movement = GetComponent<PlayerMovement>();
		_shooter = GetComponentInChildren<PlayerShooter>();
		_interaction = GetComponent<PlayerInteraction>();
		_animator = GetComponent<PlayerAnimatorController>();
		_camera = FindObjectOfType<CameraBasePosition>();
		_inventoryMenu = FindObjectOfType<UIInventoryMenu>();
		_inventoryMenu.Deactivate();
		_lookAtMouse = GetComponentInChildren<LookAtMouse>();
		GetComponent<Health>().healthUpdate += HealthUpdate;
		_screenShake = FindObjectOfType<ScreenShake>();
	}

	void Update() {
		_movement.UpdateDirection(_controls.Player.Move.ReadValue<Vector2>());
	}

	void Aiming(InputAction.CallbackContext p_context) {
		if (!_inventoryMenu.IsActive) {
			_movement.IsAiming = true;
			_shooter.IsAiming = true;
			_camera.IsAiming = true;
			_animator.SetAiming(true);
		}
	}

	void StopAiming(InputAction.CallbackContext p_context) {
		_movement.IsAiming = false;
		_shooter.IsAiming = false;
		_camera.IsAiming = false;
		_animator.SetAiming(false);
	}

	void Shooting(InputAction.CallbackContext p_context) {
		if (!_inventoryMenu.IsActive) {
			_shooter.IsShooting = true;
		}
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
		if (!_inventoryMenu.IsActive) {
			_shooter.Reload();
		}
	}

	void Interacted(InputAction.CallbackContext p_context) {
		if (!_inventoryMenu.IsActive) {
			_interaction.Interact();
		}
	}

	void Inventory(InputAction.CallbackContext p_context) {
		if (_inventoryMenu.IsActive) {
			_inventoryMenu.Deactivate();
			_camera.inInventory = false;
			_movement.canWalk = true;
			_lookAtMouse.isActive = true;
		}
		else {
			_inventoryMenu.Activate();
			_camera.inInventory = true;
			_movement.canWalk = false;
			_movement.UpdateDirection(Vector2.zero);
			_lookAtMouse.isActive = false;
			StopAiming(p_context);
			StopShooting(p_context);
		}
	}

	void HealthUpdate(int p_health) {
		if (p_health == 0) {
			print("Player died");
			if (_deadPrefab != null) {
				Instantiate(_deadPrefab, transform.position, Quaternion.identity);
			}
			Destroy(gameObject);
		}
	}

	public void EquipWeapon(Firearm p_weapon) {
		Debug.Log("Equiped weapon: " + p_weapon);
		_animator.SetShotgun(true);
		_shooter.ChangeWeapon(p_weapon);
	}

	void ScreenShake(InputAction.CallbackContext p_context) {
		_screenShake.AddTrauma(0.2f);
	}
}
