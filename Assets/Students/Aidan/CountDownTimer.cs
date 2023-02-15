using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static UnityEngine.Quaternion;

public class CountDownTimer : MonoBehaviour
{
    public float CurrentTime = 0f;
    [SerializeField] private TextMeshPro TimerDown;
    public float StartingTime = 22f;
    public Vector3 StartingPosition;
    public PlayerController Player;
    
    // Start is called before the first frame update
    void Start()
    {
        StartingPosition = transform.position;
        CurrentTime = StartingTime;
    }

    // Update is called once per frame
    void Update()
    {
        CurrentTime -= 1 * Time.deltaTime;
        TimerDown.text = CurrentTime.ToString("0");
        if (CurrentTime <= 0)
        {
            Player.Die(null);
           // gameObject.GetComponent<PlayerController>("Player").transform.position;

        }
        
    }
}