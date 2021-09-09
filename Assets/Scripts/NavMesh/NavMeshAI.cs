using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAI : MonoBehaviour
{
    [SerializeField] private NavMeshSurface surface;

    private NavMeshAgent navMeshAgent;

    [SerializeField] private Transform movePosTransform;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        //movePosTransform = PathEndObj._object;
    }

    private void Start()
    {
        navMeshAgent.destination = movePosTransform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            surface.BuildNavMesh();

            navMeshAgent.destination = movePosTransform.position;
        }

    }
}
