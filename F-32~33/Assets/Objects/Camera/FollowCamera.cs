using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField]
    private Transform followObject;
    [SerializeField]
    private float minY;
    [SerializeField]
    private float maxY;
    [SerializeField]
    private float cameraSpeed;

    private float delayTime = 1f;

    private Vector3 TargetPos;

    private void Update()
    {
        if(delayTime > 0)
        {
            delayTime -= Time.deltaTime;
            return;
        }

        maxY = Mathf.Max(maxY, minY);
        minY = Mathf.Min(maxY, minY);

        TargetPos = followObject.position;

        if (TargetPos.y < minY)
            TargetPos = new Vector3(TargetPos.x, minY, TargetPos.z);
        if (TargetPos.y > maxY)
            TargetPos = new Vector3(TargetPos.x, maxY, TargetPos.z);

        float speed = cameraSpeed;

        if (Mathf.Abs(transform.position.y - TargetPos.y) > 6)
            speed *= 4;
        if (Mathf.Abs(transform.position.y - TargetPos.y) > 10)
            speed *= 4;

        if (transform.position.y < TargetPos.y)
        {
            if (Mathf.Abs(transform.position.y - TargetPos.y) < speed * Time.deltaTime)
                transform.position = new Vector3(transform.position.x, TargetPos.y, transform.position.z);
            else
                transform.position += new Vector3(0, speed * Time.deltaTime, 0);
        }
        else if (transform.position.y > TargetPos.y)
        {
            if (Mathf.Abs(transform.position.y - TargetPos.y) < speed * Time.deltaTime)
                transform.position = new Vector3(transform.position.x, TargetPos.y, transform.position.z);
            else
                transform.position -= new Vector3(0, speed * Time.deltaTime, 0);
        }

    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(-100, minY, 0), new Vector3(+100, minY, 0));
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector3(-100, maxY, 0), new Vector3(+100, maxY, 0));
    }

}
