using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class SculptingUI : MonoBehaviour {
    
    public GameObject quiver;
    public GameObject xArrow;
    public GameObject yArrow;
    public GameObject zArrow;
    public GameObject[] prefabs = new GameObject [2];
    private List<GameObject> foundObjects = new List<GameObject>();
    public GameObject generatedObjects;
    public GameObject finishButton;
    public Transform spawnPoint;
    public Camera cam;
    Ray ray;
    private bool objectHeld;
    private GameObject selectedObject;
    private GameObject selectedArrow;
    private Vector3 mousePos;
    private int itemsGenerated = 0;
    public int finishButtonNumber;
    private bool finished;
    private Vector3 centerPosition;
    System.Random rando = new System.Random();
    [SerializeField]
    GameObject joiner;

    // Use this for initialization
    void Start () {
        finished = false;
	}

    // Update is called once per frame
    void Update() {
        finishButton.SetActive(itemsGenerated > finishButtonNumber);
        if (!finished)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                RaycastHit hit;
                ray = cam.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, 1000.0f))
                {
                    Debug.Log(ray.direction);
                    if (hit.transform != null && hit.transform.gameObject.tag == "selectable")
                    {
                        selectedObject = hit.transform.gameObject;
                        moveArrows(hit.transform.gameObject);
                    }
                    else if (hit.transform != null && hit.transform.gameObject.tag == "xArrow")
                    {
                        Debug.Log("clicking on arrow");
                        selectedArrow = xArrow;
                        mousePos = Input.mousePosition;
                    }
                    else if (hit.transform != null && hit.transform.gameObject.tag == "yArrow")
                    {
                        Debug.Log("clicking on arrow");
                        selectedArrow = yArrow;
                        mousePos = Input.mousePosition;
                    }
                    else if (hit.transform != null && hit.transform.gameObject.tag == "zArrow")
                    {
                        Debug.Log("clicking on arrow");
                        selectedArrow = zArrow;
                        mousePos = Input.mousePosition;
                    }
                    else
                    {
                        selectedObject = null;
                        moveArrows(quiver);
                    }
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                selectedArrow = null;
            }
            if (Input.GetMouseButton(0) && selectedArrow != null)
            {
                Debug.Log("clicking on arrow");
                //if x, and mousePos.x is different than original, move obj by that much and the arrows too

                Vector3 newTrans = selectedObject.transform.position;
                if (selectedArrow.CompareTag("xArrow"))
                {
                    newTrans.x += (Input.mousePosition.x - mousePos.x);

                }
                else if (selectedArrow.CompareTag("yArrow"))
                {
                    newTrans.y += (Input.mousePosition.y - mousePos.y);
                }
                else if (selectedArrow.CompareTag("zArrow"))
                {
                    newTrans.z += (Input.mousePosition.x - mousePos.x);
                }
                selectedObject.transform.SetPositionAndRotation(newTrans, selectedObject.transform.rotation);
                moveArrows(selectedObject);
                mousePos = Input.mousePosition;

            }
            if (Input.GetMouseButton(1) && selectedArrow != null)
            {
                Debug.Log("clicking on arrow");
                //if x, and mousePos.x is different than original, move obj by that much and the arrows too

                Quaternion newRotation = selectedObject.transform.rotation;
                if (selectedArrow.CompareTag("xArrow"))
                {
                    newRotation.x += (Input.mousePosition.x - mousePos.x) / 10;

                }
                else if (selectedArrow.CompareTag("yArrow"))
                {
                    newRotation.y += (Input.mousePosition.y - mousePos.y) / 10;
                }
                else if (selectedArrow.CompareTag("zArrow"))
                {
                    newRotation.z += (Input.mousePosition.x - mousePos.x) / 10;
                }
                selectedObject.transform.SetPositionAndRotation(selectedObject.transform.position, newRotation);
                moveArrows(selectedObject);
                mousePos = Input.mousePosition;

            }
        }
    }

    
       

    public void moveArrows(GameObject obj)
    {
        xArrow.transform.SetPositionAndRotation(obj.transform.position + new Vector3(25, 50, 0), xArrow.transform.rotation);
        yArrow.transform.SetPositionAndRotation(obj.transform.position + new Vector3(0,100,0), yArrow.transform.rotation);
        zArrow.transform.SetPositionAndRotation(obj.transform.position + new Vector3(0,50, 35), zArrow.transform.rotation);
    }


    public void createObject()
    {
        itemsGenerated++;
        foundObjects.Add(Instantiate(prefabs[rando.Next(0, prefabs.Length)], this.transform.position, this.transform.rotation));
        
    }

    public void finishSculpture()
    {
        
        Debug.Log("IT IS DONE!");
        finished = true;
        for(int x = 0; x < foundObjects.Count;x++)
        {
            centerPosition += foundObjects[x].transform.position;
            
        }
        centerPosition.x /= foundObjects.Count - 1;
        centerPosition.y /= foundObjects.Count - 1;
        centerPosition.z /= foundObjects.Count - 1;
        joiner.GetComponent<ObjectJoiner>().Join("Sculpture", foundObjects.ToArray(), centerPosition);
    }
}
