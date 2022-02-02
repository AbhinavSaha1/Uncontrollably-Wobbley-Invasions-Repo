using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	public float speed;
	public float strafeSpeed;
	public float jumpForce;
	public Animator animator;

	public Rigidbody hips;
	public bool isGrounded;

	private void Update()
	{
		if (this._inputActions.Player.MoveMain.inProgress)
			this._moveMainDirection = this._inputActions.Player.MoveMain.ReadValue<Vector2>();

		if (this._inputActions.Player.Jump.WasPressedThisFrame() && this.isGrounded)
		{
			this.hips.AddForce(new Vector3(0, this.jumpForce, 0));
			this.isGrounded = false;
		}
	}

	private void FixedUpdate()
	{
		//if (Input.GetKey(KeyCode.W))
		//{
		//	this.animator.SetBool("isRunning", true);
		//	this.hips.AddForce(this.hips.transform.forward * this.speed);
		//}
		//else
		//{
		//	this.animator.SetBool("isRunning", false);
		//}

		//if (Input.GetKey(KeyCode.A))
		//{
		//	this.animator.SetBool("isSlideLeft", true);
		//	this.hips.AddForce(-this.hips.transform.right * this.speed);
		//}
		//else
		//{
		//	this.animator.SetBool("isSlideLeft", false);
		//}

		//if (Input.GetKey(KeyCode.S))
		//{
		//	this.animator.SetBool("isRunning", true);
		//	this.hips.AddForce(-this.hips.transform.forward * this.speed);
		//}
		//else if (!Input.GetKey(KeyCode.W))
		//{
		//	this.animator.SetBool("isRunning", false);
		//}

		//if (Input.GetKey(KeyCode.D))
		//{
		//	this.animator.SetBool("isSlideRight", true);
		//	this.hips.AddForce(this.hips.transform.right * this.speed);
		//}
		//else
		//{
		//	this.animator.SetBool("isSlideRight", false);
		//}

		if (this._inputActions.Player.MoveMain.inProgress)
		{
			Vector3 right = this.hips.transform.right * this._moveMainDirection.x;
			Vector3 forward = this.hips.transform.forward * this._moveMainDirection.y;

			this.hips.AddForce(
				force: new Vector3(
					x: right.x + forward.x,
					y: 0.0f,
					z: right.z + forward.z
				).normalized * this.speed
			);
		}
	}

	private Vector2 _moveMainDirection;

	private void MoveMainPerformed(InputAction.CallbackContext callbackContext)
	{
	}

	private void MoveMainStarted(InputAction.CallbackContext callbackContext)
	{
		this.animator.SetBool("isRunning", true);
	}

	private void MoveMainCanceled(InputAction.CallbackContext callbackContext)
	{
		this.animator.SetBool("isRunning", false);

		this._moveMainDirection = Vector2.zero;
	}

	private void MoveSecondary()
	{
	}

	private void PerformMainAction()
	{
	}

	private MainControls _inputActions;

	private void Awake()
	{
		this._inputActions = new MainControls();
	}

	private void Start()
	{
		this.hips = this.GetComponent<Rigidbody>();
	}

	private void OnEnable()
	{
		this._inputActions.Player.MoveMain.performed += this.MoveMainPerformed;
		this._inputActions.Player.MoveMain.started += this.MoveMainStarted;
		this._inputActions.Player.MoveMain.canceled += this.MoveMainCanceled;

		this._inputActions.Enable();
	}

	private void OnDisable()
	{
		this._inputActions.Player.MoveMain.performed -= this.MoveMainPerformed;
		this._inputActions.Player.MoveMain.started -= this.MoveMainStarted;
		this._inputActions.Player.MoveMain.canceled -= this.MoveMainCanceled;

		this._inputActions.Disable();
	}
}
