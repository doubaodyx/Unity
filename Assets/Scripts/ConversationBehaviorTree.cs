using UnityEngine;
using System;
using System.Collections;
using TreeSharpPlus;

public class ConversationBehaviorTree: MonoBehaviour
{
    public Transform wander1;
    public Transform wander2;
    public GameObject participant;
    public GameObject participant2;

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

    protected Node ST_ApproachAndWait(Transform target, Transform target2)
    {
        Val<Vector3> position = Val.V(() => target.position);
        Val<Vector3> position2 = Val.V(() => target2.position);
        return new Sequence(new SequenceParallel(participant.GetComponent<BehaviorMecanim>().Node_GoTo(position), participant2.GetComponent<BehaviorMecanim>().Node_GoTo(position2)), 
                            new SequenceParallel(participant.GetComponent<BehaviorMecanim>().Node_OrientTowards(participant2.transform.position), 
                                                 participant2.GetComponent<BehaviorMecanim>().Node_OrientTowards(participant.transform.position)),
                            participant.GetComponent<BehaviorMecanim>().ST_PlayFaceGesture("ROAR", 2000), participant2.GetComponent<BehaviorMecanim>().ST_PlayFaceGesture("SAD", 2000)
                            );
    }

    protected Node BuildTreeRoot()
    {
        Node roaming = new DecoratorLoop(
                        new SequenceShuffle(
                        this.ST_ApproachAndWait(this.wander1, this.wander2)));
        return roaming;
    }
}
