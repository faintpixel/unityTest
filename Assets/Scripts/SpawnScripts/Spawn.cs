using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {

    public Transform spawnPoint;
    public float spawnTime = 1.5f;

    public GameObject spawnItem;   

	// Use this for initialization
	void Start () {
        InvokeRepeating("SpawnItem", spawnTime, 0);        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void SpawnItem()
    {                      
        Instantiate(spawnItem, spawnPoint.position, spawnPoint.rotation);
    }
}
