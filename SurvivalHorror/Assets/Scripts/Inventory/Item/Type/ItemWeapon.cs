using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Weapon", menuName = "ScriptableObjects/Item/Weapon")]
public class ItemWeapon : ItemType {
	[Header("Item weapon info")]
	[SerializeField] Firearm _weapon = null;

	public override bool Use() {
		GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Shooter>().ChangeWeapon(_weapon);
		return true;
	}
}
