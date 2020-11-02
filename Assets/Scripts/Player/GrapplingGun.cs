using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class GrapplingGun : MonoBehaviour
{
    private LineRenderer lr;
    private Vector3 grapplePoint;
    public LayerMask hookableLayer;
    public Transform gunTip, player;
    private float maxDistance = 50f;
    private SpringJoint joint;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            StartGrapple();
        }
        else if (Input.GetMouseButtonUp(1))
        {
            StopGrapple();
        }
    }

    //Called after Update
    void LateUpdate()
    {
        DrawRope();
    }

    /// <summary>
    /// Call whenever we want to start a grapple
    /// </summary>
    void StartGrapple()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, maxDistance, hookableLayer))
        {
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);
            if (hit.rigidbody != null)
            {
                joint.connectedBody = hit.rigidbody;
                //The distance grapple will try to keep from grapple point. 
                joint.maxDistance = .5f;
                joint.minDistance = .05f;
                //Adjust these values to fit your game.
                joint.damper = 20f;
                joint.spring = 50f;
                joint.massScale = 4.5f;
            }
            else 
            {
                //The distance grapple will try to keep from grapple point. 
                joint.maxDistance = distanceFromPoint * 0.8f;
                joint.minDistance = distanceFromPoint * 0.25f;

                //Adjust these values to fit your game.
                joint.damper = 7f;
                joint.spring = 20f;
                joint.massScale = 4.5f;
            }
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;




            lr.positionCount = 2;
            currentGrapplePosition = gunTip.position;
        }
    }


    /// <summary>
    /// Call whenever we want to stop a grapple
    /// </summary>
    void StopGrapple()
    {
        lr.positionCount = 0;
        Destroy(joint);
    }

    private Vector3 currentGrapplePosition;

    void DrawRope()
    {
        //If not grappling, don't draw rope
        if (!joint) return;

        if (joint.connectedBody == null)
        {
            currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 8f);

            lr.SetPosition(0, gunTip.position);
            lr.SetPosition(1, currentGrapplePosition);
        }
        else
        {
            lr.SetPosition(0, gunTip.position);
            lr.SetPosition(1, joint.connectedBody.position);
        }

    }

    public bool IsGrappling()
    {
        return joint != null;
    }

    public Vector3 GetGrapplePoint()
    {
        return grapplePoint;
    }
}
