using UnityEngine;
using System;
using System.Collections;
using TreeSharpPlus;

public class ConversationBehaviorTree: MonoBehaviour
{
    public Transform wander1;
    public Transform wander2;
    public Transform wander3;
    public GameObject participant;
    public GameObject participant2;
    public GameObject participant3;

    private BehaviorAgent behaviorAgent;
    // Use this for initialization
    void Start()
    {
        behaviorAgent = new BehaviorAgent(this.BuildTreeRoot());
        BehaviorManager.Instance.Register(behaviorAgent);
        behaviorAgent.StartBehavior();
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected Node ST_ApproachAndWait(Transform target, Transform target2, Transform target3)
    {
        Val<Vector3> position = Val.V(() => target.position);
        Val<Vector3> position2 = Val.V(() => target2.position);
        Val<Vector3> position3 = Val.V(() => target3.position);
        return new Sequence(new SequenceParallel(participant.GetComponent<BehaviorMecanim>().Node_GoTo(position), participant2.GetComponent<BehaviorMecanim>().Node_GoTo(position2), participant3.GetComponent<BehaviorMecanim>().Node_GoTo(position3)), 
                            new SequenceParallel(participant.GetComponent<BehaviorMecanim>().Node_OrientTowards(participant2.transform.position), 
                                                 participant2.GetComponent<BehaviorMecanim>().Node_OrientTowards(participant.transform.position)),
                            participant.GetComponent<BehaviorMecanim>().ST_PlayHandGesture("BASH", 2000), new SequenceParallel(participant2.GetComponent<BehaviorMecanim>().ST_PlayHandGesture("BASH", 2000),
                                                                                                                               participant3.GetComponent<BehaviorMecanim>().ST_PlayHandGesture("CHEER", 2000))
                            );
    }

    protected Node BuildTreeRoot()
    {
        Node roaming = new DecoratorLoop(
                        new SequenceShuffle(
                        this.ST_ApproachAndWait(this.wander1, this.wander2, this.wander3)));
        return roaming;
    }
}
