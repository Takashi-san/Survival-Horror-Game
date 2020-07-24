using UnityEngine;
using System;
using System.Collections;

public abstract class Firearm : ScriptableObject {
	// Aim.
	[Header("About the aim and firing")]
	[SerializeField] [Range(0, 90)] protected float _maxDeviation = 0;
	[SerializeField] [Range(0, 90)] protected float _minDeviation = 0;
	[SerializeField] [Range(0, 90)] protected float _drawDeviation = 0;
	[SerializeField] [Min(0)] protected float _fireDeviation = 0;
	[SerializeField] [Min(0)] protected float _deviationReductionRate = 0;
	[SerializeField] [Min(0)] protected float _fireRate = 0;
	[SerializeField] protected bool _isHold = true;
	public float MaxDeviation { get => _maxDeviation; }
	public float MinDeviation { get => _minDeviation; }
	public float DrawDeviation { get => _drawDeviation; }
	public float FireDeviation { get => _fireDeviation; }
	public float DeviationReductionRate { get => _deviationReductionRate; }
	public float FireRate { get => _fireRate; }
	public bool IsHold { get => _isHold; }

	// Reloading and ammo.
	[Header("About the reload and capacity")]
	[SerializeField] [Min(0)] protected float _reloadTime = 0;
	[SerializeField] [Min(1)] protected int _magazineSize = 0;
	int _magazine;
	public int Magazine {
		get => _magazine;
		set {
			if (value < 0) {
				_magazine = 0;
			}
			else if (value > _magazineSize) {
				_magazine = _magazineSize;
			}
			else {
				_magazine = value;
			}
		}
	}
	public int MagazineSize => _magazineSize;
	public bool IsEmpty { get => _magazine == 0; }
	public float ReloadTime => _reloadTime;

	// Shot.
	[Header("About the shot")]
	[SerializeField] [Min(0)] protected int _damage = 0;
	[SerializeField] [Min(1)] protected int _nBullets = 1;
	[SerializeField] [Range(0, 90)] protected float _bulletScatter = 0;

	public abstract void Fire(GameObject owner, Quaternion direction);
	public virtual void Setup(GameObject owner) { }
}
