using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Grab : MonoBehaviour
{
	[SerializeField] private Rigidbody _rigidbody;
	public Rigidbody _Rigidbody => this._rigidbody;

	[SerializeField] private Animator _animator;
	public Animator _Animator => this._animator;

	[SerializeField] private string _animationTriggerName = "Left hand  grab";
	public string _AnimationTriggerName => this._animationTriggerName;

	[SerializeField] private InputActionReference _inputActionReference;
	public InputActionReference _InputActionReference => this._inputActionReference;

	private Item _grabbedItem;
	private bool _canGrab;

	private void Update()
	{
		if (this._inputActionReference.action.WasPressedThisFrame())
		{
			this._animator.SetBool(this._animationTriggerName, true);

			this._canGrab = true;
		}
		else if (this._inputActionReference.action.WasReleasedThisFrame())
		{
			this._animator.SetBool(this._animationTriggerName, false);

			if (this._grabbedItem != null)
			{
				this._grabbedItem.Release();

				this._grabbedItem = null;
			}

			this._canGrab = false;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (this._canGrab && this._grabbedItem == null && other.TryGetComponent<Item>(out this._grabbedItem))
		{
			this._grabbedItem.PickUp(this._rigidbody);
		}
	}

	private void OnEnable()
	{
		this._inputActionReference.action.Enable();
	}

	private void OnDisable()
	{
		this._inputActionReference.action.Disable();
	}

#if UNITY_EDITOR
	private void Reset()
	{
		this._rigidbody = this.GetComponent<Rigidbody>();
		this._animator = this.GetComponent<Animator>();
	}
#endif
}
