using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Weapon", menuName = "ScriptableObjects/Item/Weapon")]
public class ItemWeapon : ItemType {
	[Header("Item weapon info")]
	public Firearm weapon = null;

	public override bool Use() {
		Player.instance.EquipWeapon(weapon);
		return true;
	}
}
