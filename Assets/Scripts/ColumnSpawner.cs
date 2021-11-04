using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnSpawner : MonoBehaviour
{
    public float minY,maxY;
    public GameObject columnPrefab;
    float timer ;
    public float maxTime;

    // Start is called before the first frame update
    void Start()
    {
        SpawnCoumn();
    }

    // Update is called once per frame
    void Update()
    {
timer += Time.deltaTime;
if( timer >= maxTime)
{
    SpawnCoumn();
    timer=0;
}
    }


    void SpawnCoumn ()
    {    float randYPos = Random.Range(minY,maxY);
        GameObject newColumn = Instantiate(columnPrefab);
        newColumn.transform.position = new Vector2(
            transform.position.x,
            randYPos
        );
    }
}
