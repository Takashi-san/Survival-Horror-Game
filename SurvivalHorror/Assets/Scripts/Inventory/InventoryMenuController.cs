using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMenuController : MonoBehaviour {
	[SerializeField] GameObject _InventoryCanvas = null;

	void Start() {
		_InventoryCanvas.SetActive(false);
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.I)) {
			_InventoryCanvas.SetActive(!_InventoryCanvas.activeInHierarchy);
		}
	}
}
