using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {
	[SerializeField] GameObject _menu = null;
	[SerializeField] KeyCode _input = KeyCode.Escape;

	void Start() {
		if (!_menu) {
			Debug.LogWarning("No Menu specified to control");
		}
		else {
			_menu.SetActive(false);
		}
	}

	void Update() {
		if (Input.GetKeyDown(_input)) {
			if (_menu.activeInHierarchy) {
				Deactivate();
			}
			else {
				Activate();
			}
		}
	}

	void OnDestroy() {
		Time.timeScale = 1;
	}

	public void Activate() {
		_menu.SetActive(true);
		Time.timeScale = 0;
	}

	public void Deactivate() {
		_menu.SetActive(false);
		Time.timeScale = 1;
	}
}
