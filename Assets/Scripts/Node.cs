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

    [SerializeField] private List<GameObject> forwardNodes;
    [SerializeField] private List<GameObject> backNodes;
    [SerializeField] private List<GameObject> rightNodes;
    [SerializeField] private List<GameObject> leftNodes;

    //private void Update()
    //{
    //    forwardNodes = CreateNodes(Vector3.forward * _distance);
    //    backNodes = CreateNodes(Vector3.back * _distance);
    //    rightNodes = CreateNodes(Vector3.right * _distance);
    //    leftNodes = CreateNodes(Vector3.left * _distance);
    //}

    private void OnCollisionEnter(Collision collision)
    {
        //Destroy(GetComponent<Rigidbody>());

        forwardNodes = CreateNodes(Vector3.forward * _distance);
        backNodes = CreateNodes(Vector3.back * _distance);
        rightNodes = CreateNodes(Vector3.right * _distance);
        leftNodes = CreateNodes(Vector3.left * _distance);

        //Destroy(GetComponent<MeshRenderer>());
    }

    private List<GameObject> CreateNodes(Vector3 newNodePosition)
    {
        List<GameObject> listObjects = new List<GameObject>();
        RaycastHit[] frameHits = Physics.RaycastAll(transform.position + newNodePosition +
                                                    // С высоты на которую можно запрыгнуть
                                                    transform.up * (_jumpHeight * 1.01f - transform.localScale.y / 2),
                                                    -transform.up, _jumpHeight + _depthFall + 0.015f);
        Debug.DrawRay(transform.position + newNodePosition +
                      // С высоты на которую можно запрыгнуть
                      transform.up * (_jumpHeight * 1.01f - transform.localScale.y / 2),
                      -transform.up * (_jumpHeight + _depthFall + 0.015f),
                      Color.red);

        for (int i = 0; i < frameHits.Length; i++)
        {
            if (frameHits[i].collider.tag == "Frame")
            {
                bool isFrameBrake = false;
                Vector3 vectorNewObject = transform.position + newNodePosition +
                                          // Позиция Frame относительно Node
                                          transform.up * (frameHits[i].collider.transform.position.y - transform.position.y) +
                                          // Верхняя часть Frame относительно Node
                                          transform.up * (frameHits[i].collider.transform.localScale.y / 2 + _nodePoint.transform.localScale.y / 2);

                RaycastHit[] checkFrameHits1;
                RaycastHit[] checkFrameHits2;

                if (transform.position.y >= vectorNewObject.y)
                {
                    checkFrameHits1 = Physics.RaycastAll(transform.position, newNodePosition, newNodePosition.x + newNodePosition.z);
                    Debug.DrawRay(transform.position, newNodePosition, Color.green);
                    checkFrameHits2 = Physics.RaycastAll(transform.position + newNodePosition, -transform.up, transform.position.y - vectorNewObject.y);
                    Debug.DrawRay(transform.position + newNodePosition, -transform.up * (transform.position.y - vectorNewObject.y), Color.green);
                }
                else
                {
                    checkFrameHits1 = Physics.RaycastAll(transform.position, transform.up, vectorNewObject.y - transform.position.y);
                    Debug.DrawRay(transform.position, transform.up * (vectorNewObject.y - transform.position.y), Color.green);
                    checkFrameHits2 = Physics.RaycastAll(transform.position + transform.up * (vectorNewObject.y - transform.position.y), newNodePosition, newNodePosition.x + newNodePosition.z);
                    Debug.DrawRay(transform.position + transform.up * (vectorNewObject.y - transform.position.y), newNodePosition, Color.green);
                }

                for (int j = 0; j < checkFrameHits1.Length; j++)
                {
                    if (checkFrameHits1[j].collider.tag == "Frame")
                    {
                        isFrameBrake = true;
                    }
                }

                for (int j = 0; j < checkFrameHits2.Length; j++)
                {
                    if (checkFrameHits2[j].collider.tag == "Frame")
                    {
                        isFrameBrake = true;
                    }
                }

                if (isFrameBrake)
                {
                    continue;
                }

                RaycastHit hit;
                Ray landRay = new Ray(transform.position + newNodePosition +
                              // Позиция Frame относительно Node
                              transform.up * (frameHits[i].collider.transform.position.y - transform.position.y) +
                              // Верхняя часть Frame относительно Node
                              transform.up * (frameHits[i].collider.transform.localScale.y / 2 - 0.01f),
                              transform.up);

                Debug.DrawRay(transform.position + newNodePosition +
                              // Позиция Frame относительно Node
                              transform.up * (frameHits[i].collider.transform.position.y - transform.position.y) +
                              // Верхняя часть Frame относительно Node
                              transform.up * (frameHits[i].collider.transform.localScale.y / 2 - 0.01f),
                              transform.up * _nodePoint.transform.localScale.y,
                              Color.blue);

                // Проверяет присутствует ли сверху Node
                if (Physics.Raycast(landRay, out hit, _nodePoint.transform.localScale.y))
                {
                    if (hit.collider.tag == _nodePoint.tag)
                    {
                        listObjects.Add(hit.collider.gameObject);
                    }
                }
                else
                {
                    listObjects.Add(Instantiate(_nodePoint, transform.position + newNodePosition +
                                              // Позиция Frame относительно Node
                                              transform.up * (frameHits[i].collider.transform.position.y - transform.position.y) +
                                              // Верхняя часть Frame относительно Node
                                              transform.up * (frameHits[i].collider.transform.localScale.y / 2 + _nodePoint.transform.localScale.y / 2),
                                              Quaternion.identity));
                }
            }
        }
        return listObjects;
    }
}
