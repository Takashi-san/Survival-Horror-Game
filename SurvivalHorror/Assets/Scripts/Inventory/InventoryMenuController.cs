using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMenuController : MonoBehaviour {
	[SerializeField] GameObject _InventoryCanvas = null;
	CameraBasePosition _cameraBase = null;

	void Start() {
		_InventoryCanvas.SetActive(false);
		_cameraBase = FindObjectOfType<CameraBasePosition>();
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.I)) {
			if (_InventoryCanvas.activeInHierarchy) {
				_InventoryCanvas.SetActive(false);
				_cameraBase.unlockFromPlayer();
			}
			else {
				_InventoryCanvas.SetActive(true);
				_cameraBase.lockToPlayer();
			}

		}
	}
}
