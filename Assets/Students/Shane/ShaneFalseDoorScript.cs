using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaneFalseDoorScript : MonoBehaviour
{
    public GameObject player;
    public GameObject wall;
    public GameObject hiddenWall;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(activateTrap());
    }

    IEnumerator activateTrap()
    {
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        while (wall.transform.position.y >= -10)
        {
            Vector2 wallPos = wall.transform.position;
            wallPos.y -= .005f;
            wall.transform.position = wallPos;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(1);
        hiddenWall.SetActive(true);
        while (hiddenWall.transform.position.x <= 159)
        {
            Vector2 wallPos = hiddenWall.transform.position;
            wallPos.x += .005f;
            hiddenWall.transform.position = wallPos;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForEndOfFrame();
    }
}
