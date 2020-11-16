using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Consumable", menuName = "ScriptableObjects/Item/Consumable")]
public class ItemConsumable : ItemType {
	[Header("Item consumable info")]
	[SerializeField] [Min(0)] int _heal = 0;

	public override bool Use() {
		Health playerHealth = Player.instance.GetComponent<Health>();
		if (!playerHealth.IsFull) {
			playerHealth.HealDamage(_heal);
			return true;
		}
		return false;
	}
}
