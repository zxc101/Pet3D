using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField] private GameObject _nodePoint;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _jumpDistance;
    [SerializeField] private float _distance;
    [SerializeField] private float _depthFall;
    [SerializeField] private float _depthDistance;

    [SerializeField] private GameObject forwardNode;
    [SerializeField] private GameObject backNode;
    [SerializeField] private GameObject rightNode;
    [SerializeField] private GameObject leftNode;

    [SerializeField] private GameObject upForwardNode;
    [SerializeField] private GameObject upBackNode;
    [SerializeField] private GameObject upRightNode;
    [SerializeField] private GameObject upLeftNode;

    [SerializeField] private GameObject downForwardNode;
    [SerializeField] private GameObject downBackNode;
    [SerializeField] private GameObject downRightNode;
    [SerializeField] private GameObject downLeftNode;


    private void Start()
    {
        //forwardNode = CreateNode(Vector3.forward);
        //backNode = CreateNode(Vector3.back);
        //rightNode = CreateNode(Vector3.right);
        //leftNode = CreateNode(Vector3.left);
        //// UP //

        //upForwardNode = CreateNode(Vector3.up * _jumpHeight +
        //                           Vector3.forward * _distance);

        //upBackNode = CreateNode(Vector3.up * _jumpHeight +
        //                        Vector3.back * _distance);

        //upRightNode = CreateNode(Vector3.up * _jumpHeight +
        //                         Vector3.right * _distance);

        //upLeftNode = CreateNode(Vector3.up * _jumpHeight +
        //                        Vector3.left * _distance);

        //// DOWN //

        //downForwardNode = CreateNode(Vector3.down * _depthFall +
        //                             Vector3.forward * _distance);

        //downBackNode = CreateNode(Vector3.down * _depthFall +
        //                          Vector3.back * _distance);

        //downRightNode = CreateNode(Vector3.down * _depthFall +
        //                           Vector3.right * _distance);

        //downLeftNode = CreateNode(Vector3.down * _depthFall +
        //                          Vector3.left * _distance);
    }

    //private void Update()
    //{
    //    CreateNode(Vector3.forward * _distance);
    //    CreateNode(Vector3.back * _distance);
        //    //Ray ray = new Ray(transform.position, Vector3.down * transform.localScale.y / 2);
        //    //RaycastHit hit;
        //    //if(Physics.Raycast(ray, out hit, _dist))
        //    //{
        //    //    transform.Translate(Vector3.down);
        //    //}
    //}

    private void OnCollisionEnter(Collision collision)
    {
        //Destroy(GetComponent<Rigidbody>());

        forwardNode = CreateNode(Vector3.forward * _distance);
        backNode = CreateNode(Vector3.back * _distance);
        rightNode = CreateNode(Vector3.right * _distance);
        leftNode = CreateNode(Vector3.left * _distance);

        // UP //

        upForwardNode = CreateNode(Vector3.up * _jumpHeight +
                                   Vector3.forward * _jumpDistance);

        upBackNode = CreateNode(Vector3.up * _jumpHeight +
                                Vector3.back * _jumpDistance);

        upRightNode = CreateNode(Vector3.up * _jumpHeight +
                                 Vector3.right * _jumpDistance);

        upLeftNode = CreateNode(Vector3.up * _jumpHeight +
                                Vector3.left * _jumpDistance);

        // DOWN //

        downForwardNode = CreateNode(Vector3.down * _depthFall +
                                     Vector3.forward * _distance);

        downBackNode = CreateNode(Vector3.down * _depthFall +
                                  Vector3.back * _distance);

        downRightNode = CreateNode(Vector3.down * _depthFall +
                                   Vector3.right * _distance);

        downLeftNode = CreateNode(Vector3.down * _depthFall +
                                  Vector3.left * _distance);

        //Destroy(GetComponent<MeshRenderer>());
    }

    //private IEnumerator CoroutineCreateNodes()
    //{
    //    while (forwardNode == null ||
    //           backNode == null ||
    //           rightNode == null ||
    //           leftNode == null ||
    //           upForwardNode == null ||
    //           upBackNode == null ||
    //           upRightNode == null ||
    //           upLeftNode == null)
    //    {
    //        forwardNode = CreateNode(Vector3.forward * _distance);
    //        backNode = CreateNode(Vector3.back * _distance);
    //        rightNode = CreateNode(Vector3.right * _distance);
    //        leftNode = CreateNode(Vector3.left * _distance);
    //        yield return new WaitForFixedUpdate();
    //    }
    //}

    private GameObject CreateNode(Vector3 newNodePosition)
    {
        RaycastHit[] downHits = Physics.RaycastAll(transform.position + newNodePosition + transform.up * transform.localScale.y/2 * 1.01f, -transform.up, transform.localScale.y + 0.01f);

        for (int i = 0; i < downHits.Length; i++)
        {
            //Debug.Log(downHits[i].collider.name);
            if (downHits[i].collider.tag == "Node")
            {
                return downHits[i].collider.gameObject;
            }
        }

        if(downHits.Length > 0)
        {
            return Instantiate(_nodePoint, transform.position + newNodePosition, Quaternion.identity);
        }
        else
        {
            return null;
        }
    }
}
