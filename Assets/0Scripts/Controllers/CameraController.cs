using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LateUpdate()
    {
        if (Target == null)
        {
            return;
        }

        //TODO
        transform.position = new Vector3(Target.transform.position.x, Target.transform.position.y, -10);
    }
}
