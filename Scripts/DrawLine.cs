using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    LineRenderer line;

    float counter;

    float dist;

    [SerializeField]
    Transform origin;

    [SerializeField]
    Transform destination;

    [SerializeField]
    float drawSpeed = 0.1f;

    [SerializeField]
    float thicc = .25f;

    float prevDist;

    [SerializeField]
    float moveSens = 1f;

    bool moved = false;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.SetWidth (thicc, thicc);
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(origin.position, destination.position);
        moved = false;
        if (Mathf.Abs(dist - prevDist) > moveSens)
        {
            moved = true;
        }

        if (dist > 15f)
        {
            destination
                .gameObject
                .GetComponent<Rigidbody>()
                .AddForce((origin.position - destination.position) * 5);
        }
        if (counter < dist)
        {
            counter += .1f / drawSpeed;
            float x = Mathf.Lerp(0, dist, counter);
            Vector3 p1 = origin.position;
            Vector3 p2 = destination.position;

            Vector3 point = x * Vector3.Normalize(p2 - p1) + p1;
            line.SetPosition(1, point);
        }
        else if (moved)
        {
            counter = 0;
        }
        line.SetPosition(0, origin.position);

        prevDist = dist;
    }
}
