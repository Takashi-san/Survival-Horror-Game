using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour {
	[SerializeField] ItemType _item = null;
	[SerializeField] [Min(1)] int _quantity = 1;

	void Awake() {
		GetComponent<SpriteRenderer>().sprite = _item.SpriteGame;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			_quantity = InventorySystem.instance.AddItem(_item, _quantity);
			if (_quantity == 0) {
				Destroy(gameObject);
			}
		}
	}
}
