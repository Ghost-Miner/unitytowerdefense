using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathEndObj : MonoBehaviour
{
    [SerializeField]
    public static Transform _object;

    // Start is called before the first frame update
    void Start()
    {
        _object.position = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
