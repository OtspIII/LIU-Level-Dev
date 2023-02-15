using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEditor.UI;
using UnityEngine;

public class EnemyProjCode : MonoBehaviour
{
    public Vector3 movePosition;
    public GameObject BlankParticleEffect;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + new Vector3(0 * Time.deltaTime, -1 * Time.deltaTime, 0);
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (BlankParticleEffect != null)
        {
            Destroy(gameObject);
        }
    }
}
