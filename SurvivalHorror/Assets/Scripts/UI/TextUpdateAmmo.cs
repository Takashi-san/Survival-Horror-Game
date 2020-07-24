using UnityEngine;
using TMPro;

public class TextUpdateAmmo : MonoBehaviour {
	[SerializeField] Shooter _shooter = null;
	TextMeshProUGUI _text = null;

	void Awake() {
		_text = gameObject.GetComponent<TextMeshProUGUI>();
		if (_text == null) {
			Debug.LogWarning("no textmeshpro");
		}

		_shooter.ammoUpdate += UpdateText;
	}

	void UpdateText(int magazine) {
		_text.text = "Ammo: " + magazine;
	}
}
