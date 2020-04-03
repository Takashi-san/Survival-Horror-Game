using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextUpdateHealth : MonoBehaviour {
	[SerializeField] Health _health = null;
	TextMeshProUGUI _text = null;

	void Awake() {
		_text = gameObject.GetComponent<TextMeshProUGUI>();
		if (_text == null) {
			Debug.LogWarning("no textmeshpro");
		}

		_health.healthUpdate += UpdateText;
	}

	void UpdateText(int health) {
		_text.text = "HP: " + health;
	}
}
