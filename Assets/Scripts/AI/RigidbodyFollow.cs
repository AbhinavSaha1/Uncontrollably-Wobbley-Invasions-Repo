using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyFollow : MonoBehaviour
{
	[SerializeField] private Transform _target;
	public Transform _Target => this._target;

	private Rigidbody _rigidbody;

	private void FixedUpdate()
	{
		////this._rigidbody.velocity = Vector3.zero;

		////if ((this.transform.position - this._target.position).sqrMagnitude > 1.0f)
		////	this._rigidbody.AddForce(force: (this.transform.position - this._target.position).normalized * 200); // This won't work with AI, it needs precision in target desination and how to get to it correctly.

		this._rigidbody.MovePosition(position: this._target.position);
		this._rigidbody.MoveRotation(rot: this._target.rotation);
	}

	private void Awake()
	{
		this._rigidbody = this.GetComponent<Rigidbody>();
	}
}