using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionActions : MonoBehaviour
{
    public GameObject RoadControllerObj;
    public GameObject RoadTile;
    Vector3 newTilePosition; // = new Vector3(0f, 6.74f, 1f);

    private void Awake()
    {
        newTilePosition = GameObject.FindGameObjectWithTag("TopTile").transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
        Instantiate(RoadTile, newTilePosition, Quaternion.identity, RoadControllerObj.transform);
    }
}
