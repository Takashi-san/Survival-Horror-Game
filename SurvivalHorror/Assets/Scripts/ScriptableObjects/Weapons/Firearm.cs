using UnityEngine;

public abstract class Firearm : ScriptableObject {
	[SerializeField] [Range(0, 90)] protected float _maxDeviation = 0;
	[SerializeField] [Range(0, 90)] protected float _minDeviation = 0;
	[SerializeField] [Range(0, 90)] protected float _drawDeviation = 0;
	[SerializeField] [Min(0)] protected float _fireDeviation = 0;
	[SerializeField] [Min(0)] protected float _deviationReductionRate = 0;
	[SerializeField] [Min(0)] protected float _fireRate = 0;
	[SerializeField] protected bool _isHold = true;

	[SerializeField] [Min(0)] protected int _damage = 0;
	[SerializeField] [Min(1)] protected int _nBullets = 1;
	[SerializeField] [Range(0, 90)] protected float _bulletScatter = 0;

	public float MaxDeviation { get => _maxDeviation; }
	public float MinDeviation { get => _minDeviation; }
	public float DrawDeviation { get => _drawDeviation; }
	public float FireDeviation { get => _fireDeviation; }
	public float DeviationReductionRate { get => _deviationReductionRate; }
	public float FireRate { get => _fireRate; }
	public bool IsHold { get => _isHold; }

	public abstract void Fire(GameObject owner, Quaternion direction);
	public virtual void Setup(GameObject owner) { }
}
