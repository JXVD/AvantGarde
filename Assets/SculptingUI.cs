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
    [SerializeField]
    GameObject catapultPrompt;

    // Tuning:
    [SerializeField]
    Vector2 launchThrust = new Vector2(3000, 2000);
    [SerializeField]
    float fieldOfView = 90;
    [SerializeField]
    bool debugEnabled = false;

    // Use this for initialization
    void Start () {
        finished = false;
	}

    // Update is called once per frame
    void Update() {
        if (!finished)
        {
            finishButton.SetActive(itemsGenerated > finishButtonNumber);
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                RaycastHit hit;
                ray = cam.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, 1000.0f))
                {
                    if (debugEnabled) Debug.Log(ray.direction);
                    if (debugEnabled) Debug.LogFormat("Clicked on {0}", hit.collider);
                    if (hit.transform != null && hit.transform.gameObject.tag == "selectable")
                    {
                        selectedObject = hit.transform.gameObject;
                        moveArrows(hit.transform.gameObject);
                    }
                    else if (hit.transform != null && hit.transform.gameObject.tag == "xArrow")
                    {
                        if (debugEnabled) Debug.Log("clicking on X arrow");
                        selectedArrow = xArrow;
                        mousePos = Input.mousePosition;
                    }
                    else if (hit.transform != null && hit.transform.gameObject.tag == "yArrow")
                    {
                        if (debugEnabled) Debug.Log("clicking on Y arrow");
                        selectedArrow = yArrow;
                        mousePos = Input.mousePosition;
                    }
                    else if (hit.transform != null && hit.transform.gameObject.tag == "zArrow")
                    {
                        if (debugEnabled) Debug.Log("clicking on Z arrow");
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
                if (debugEnabled) Debug.Log("clicking on arrow");
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
                    float delta = Input.mousePosition.x > Screen.width / 2 ? - Input.mousePosition.x + mousePos.x : Input.mousePosition.x - mousePos.x;
                    newTrans.z += delta;
                }
                selectedObject.transform.SetPositionAndRotation(newTrans, selectedObject.transform.rotation);
                moveArrows(selectedObject);
                mousePos = Input.mousePosition;

            }
            if (Input.GetMouseButton(1) && selectedArrow != null)
            {
                if (debugEnabled) Debug.Log("clicking on arrow");
                //if x, and mousePos.x is different than original, move obj by that much and the arrows too

                Quaternion newRotation = selectedObject.transform.rotation;
                if (selectedArrow.CompareTag("xArrow"))
                {
                    newRotation.x += (Input.mousePosition.y - mousePos.y) / 10;

                }
                else if (selectedArrow.CompareTag("yArrow"))
                {
                    newRotation.y += (Input.mousePosition.x - mousePos.x) / 10;
                }
                else if (selectedArrow.CompareTag("zArrow"))
                {
                    newRotation.z += (Input.mousePosition.y - mousePos.y) / 10;
                }
                selectedObject.transform.SetPositionAndRotation(selectedObject.transform.position, newRotation);
                moveArrows(selectedObject);
                mousePos = Input.mousePosition;

            }
        }
    }

    
       

    public void moveArrows(GameObject obj)
    {
        xArrow.transform.SetPositionAndRotation(obj.transform.position + new Vector3(50, 50, -50), xArrow.transform.rotation);
        yArrow.transform.SetPositionAndRotation(obj.transform.position + new Vector3(0, 100,-50), yArrow.transform.rotation);
        zArrow.transform.SetPositionAndRotation(obj.transform.position + new Vector3(0, 50, -100), zArrow.transform.rotation);
    }


    public void createObject()
    {
        itemsGenerated++;
        foundObjects.Add(Instantiate(prefabs[rando.Next(0, prefabs.Length)], spawnPoint.transform.position, spawnPoint.transform.rotation));
        
    }

    public void finishSculpture()
    {
        
        if (debugEnabled) Debug.Log("IT IS DONE!");
        finished = true;
        for(int x = 0; x < foundObjects.Count;x++)
        {
            centerPosition += foundObjects[x].transform.position;
            
        }
        centerPosition.x /= foundObjects.Count - 1;
        centerPosition.y /= foundObjects.Count - 1;
        centerPosition.z /= foundObjects.Count - 1;
        GameObject sculpture = joiner.GetComponent<ObjectJoiner>().Join("Sculpture", foundObjects.ToArray(), centerPosition);
        Catapult catapult = sculpture.GetComponent<Catapult>();
        catapult.SetThrust(launchThrust);
        catapult.OnLaunch(() => { 
            updateCameraTarget(sculpture.transform, fieldOfView);
            catapultPrompt.SetActive(false);
        });
    }

    void updateCameraTarget(Transform target, float fieldOfView) {
        CinemachineFreeLook cinemachine = cam.GetComponent<CinemachineFreeLook>();
        cinemachine.LookAt = target;
        cinemachine.Follow = target;
        cinemachine.m_Lens.FieldOfView = fieldOfView;
    }
}
