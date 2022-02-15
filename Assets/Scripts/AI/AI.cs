using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
	//[SerializeField] private float _activationRadius = 5.0f;
	//public float _ActivationRadius => this._activationRadius;

	[SerializeField] private Collider _collider;
	public Collider _Collider => this._collider;

	[SerializeField] private LayerMask _activationLayerMask;
	public LayerMask _ActivationLayerMask => this._activationLayerMask;

	[SerializeField] private NavMeshAgent _navMeshAgent;
	public NavMeshAgent _NavMeshAgent => this._navMeshAgent;

	[Tooltip("It's AI unit reaction time to changes. Lower number means more frequent update of AI behaviour.")]
	[Min(0.05f)]
	[SerializeField] private float _behaviourTickDeltaTime = 0.25f;
	public float _BehaviourTickDeltaTime => this._behaviourTickDeltaTime;

	private Transform _target;

	private IEnumerator BehaviourTickProcess()
	{
		WaitForSeconds waitForSeconds = new WaitForSeconds(seconds: this._behaviourTickDeltaTime);

		while (this._target != null)
		{
			this._navMeshAgent.SetDestination(target: this._target.position);

			yield return waitForSeconds;
		}
	}

	private Coroutine _behaviourTickCorountine;

	private void OnTriggerEnter(Collider other)
	{
		Debug.LogError(other + $" - {this._activationLayerMask.Contains(collider: other)}");
		if (this._activationLayerMask.Contains(collider: other)) // Vector3.Distance(other.transform.position, this.transform.position) < this._activationRadius && 
		{
			this._target = other.transform;
			this._behaviourTickCorountine = this.StartCoroutine(routine: this.BehaviourTickProcess());
		}
	}

#if UNITY_EDITOR
	private void Reset()
	{
		this._navMeshAgent = this.GetComponent<NavMeshAgent>();
		this._collider = this.GetComponent<Collider>();
	}

	//private void OnDrawGizmosSelected()
	//{
	//	Gizmos.color = Color.white;
	//	Gizmos.DrawWireSphere(
	//		center: this.transform.position,
	//		radius: this._activationRadius
	//	);
	//}
#endif
}