using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextUpdateInventoryAmmo : MonoBehaviour {
	[SerializeField] Shooter _shooter = null;
	TextMeshProUGUI _text = null;
	Firearm _weapon;

	void Awake() {
		_text = gameObject.GetComponent<TextMeshProUGUI>();
		if (_text == null) {
			Debug.LogWarning("no textmeshpro");
		}

		_shooter.reloaded += UpdateText;
		_shooter.changedWeapon += ChangedWeapon;
	}

	void Start() {
		InventorySystem.instance.addedItem += UpdateText;
	}

	void UpdateText() {
		if (_weapon == null) {
			_text.text = "Inventory Ammo: -";
		}
		else {
			_text.text = "Inventory Ammo: " + InventorySystem.instance.CheckAmmo(_weapon.AmmoType);
		}
	}

	void ChangedWeapon(Firearm weapon) {
		_weapon = weapon;
		UpdateText();
	}
}
