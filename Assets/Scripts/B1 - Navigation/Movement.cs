using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour {

    private Animator anim;
    private NavMeshAgent navMeshAgent;
    private Renderer matRender;
    private bool isSelected;
    private bool isWalking;
    private bool isRunning;
    private bool isJumping;
    public Director director;
	public Camera camera;

    // Use this for initialization
    void Awake()
    {
        anim = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        matRender = GetComponent<Renderer>();
        isSelected = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSelected)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (director.beginBrakes == true)
                {
                    director.beginBrakes = false;
                    director.stoppedAgents.Clear();
                }
                navMeshAgent.Resume();
                RaycastHit hit;
				Ray ray = camera.ScreenPointToRay(Input.mousePosition);
				if (Physics.Raycast(ray, out hit))
                {
                    navMeshAgent.destination = hit.point;
                    isWalking = true;
                    navMeshAgent.updatePosition = true;
                    navMeshAgent.updateRotation = true;
                    navMeshAgent.nextPosition = transform.position;
                }
            }
            else if (Vector3.Distance(transform.position, navMeshAgent.destination) < 1.0f)
            {
                if (director.beginBrakes != true)
                {
                    navMeshAgent.Stop();
                    director.beginBrakes = true;
                    director.stoppedAgents = new Hashtable();
                    director.stoppedAgents.Add(gameObject.name, gameObject);
                }
            }
        }
    }

    void OnMouseDown()
    {
        if (!isSelected)
        {
            isSelected = true;
        }
        else
        {
            isSelected = false;
        }
    }
}
