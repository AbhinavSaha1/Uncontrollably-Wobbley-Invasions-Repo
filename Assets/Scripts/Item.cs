using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
	public FixedJoint _FixedJoint { get; private set; }

	[SerializeField] private int _layer;
	public int _Layer => this._layer;

	private int _defaultLayer;
	[SerializeField] private int _breakForce;

	public void PickUp(Rigidbody rigidbody)
	{
		if (this._FixedJoint == null)
		{
			this._FixedJoint = this.gameObject.AddComponent<FixedJoint>();
			this._FixedJoint.breakForce = _breakForce;
			this._FixedJoint.connectedBody = rigidbody;

			this.gameObject.layer = this._layer;

			Debug.Log("Grabbed" + this.name);
		}
	}

	public void Release()
	{
		Object.Destroy(this._FixedJoint);

		this.gameObject.layer = this._defaultLayer;
	}

	private void Awake()
	{
		this._defaultLayer = this.gameObject.layer;
	}
}
