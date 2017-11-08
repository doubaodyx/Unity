using UnityEngine;
using System;
using System.Collections;
using TreeSharpPlus;

public class BruteBehavior : MonoBehaviour {

	// Use this for initialization
	public Transform wander1;
	public Transform wander2;
	public Transform wander3;
	public GameObject Brute;
	public GameObject Monster;
	//initial the setting 
	private bool B_Killing = false;
	private bool B_Dead = false;

	private BehaviorAgent behaviorAgent;
	// Use this for initialization
	void Start ()
	{
		behaviorAgent = new BehaviorAgent (this.BuildTreeRoot ());
		BehaviorManager.Instance.Register (behaviorAgent);
		behaviorAgent.StartBehavior ();
	}

	// Update is called once per frame
	void Update ()
	{

	}

	bool checkForCriminals () {
		if (Vector3.Distance (Brute.transform.position, Monster.transform.position) < 4.0f) {
			print ("see the monster");
			return true;
		} else {
			return false;
		}
	}

	protected Node ST_ApproachAndWait(Transform target)
	{
		Val<Vector3> position = Val.V (() => target.position);
		return new Sequence( Brute.GetComponent<BehaviorMecanim>().Node_GoTo(position), new LeafWait(1000));
	}

	protected Node ST_CatchAndKill(Transform target)
	{
		Val<Vector3> position = Val.V (() => target.position);
		return new Sequence (Brute.GetComponent<BehaviorMecanim> ().Node_GoTo (position), Brute.GetComponent<BehaviorMecanim> ().ST_PlayBodyGesture ("Standing Melee Attack Horizontal", 2000), Monster.GetComponent<BehaviorMecanim>().ST_PlayBodyGesture("DYING",200));
	}
		

	protected Node BuildTreeRoot()
	{
		Val<bool> criminalInRange = Val.V (() => this.checkForCriminals ());
		Func<bool> act1 = () => (criminalInRange.Value == false);
		Node lookoutNode = new DecoratorLoop (new LeafAssert (act1));
		Node patrolNode = new DecoratorLoop (
			                  new SequenceShuffle (
				this.ST_ApproachAndWait(this.wander3)
			                  )
		                  );
		Node phasePatrol = new DecoratorLoop (new DecoratorForceStatus (RunStatus.Success, new SequenceParallel (lookoutNode, patrolNode)));
		Val<UnityEngine.Transform> criminalPosition = Val.V (()=>this.Monster.transform);

		Func<bool> act2 = () => (criminalInRange.Value == true);
		Node chaseNode = new DecoratorLoop (new LeafAssert (act2));
		Node followCriminal = new DecoratorLoop (new Sequence (this.ST_ApproachAndWait (criminalPosition.Value)));
		Node phaseChase = new DecoratorLoop (new DecoratorForceStatus (RunStatus.Success, new SequenceParallel (chaseNode, followCriminal)));

		Node root = new DecoratorLoop (new SelectorParallel (phasePatrol, phaseChase));

		return root;
	}
}
