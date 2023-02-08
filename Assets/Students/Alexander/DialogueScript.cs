using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueScript : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    public int index;
    public bool nextText = false;
    public GameObject RDoor;
    public bool createRDoor = false;
    public GameObject CDoor;
    public bool createCrashDoor = false;
    public GameObject RealDoor;
    public bool createRealDoor = false;
    public GameObject UnfinishedSpawnIn;
    public bool SpawnInUnfinished = false;
    
    public GameObject player;
    //Timers
    public bool nextDialogue = false;
    public float nextDialogueTimer = 5;
    public bool startDialogue = false;
    public float startDialogueTimer = 3;
    public float startDialogueTimer2 = 30;
    public float startDialogueTimer3 = 30;
    public float startDialogueTimer4 = 30;
    public float startDialogueTimer5 = 1;
    public float startDialogueTimer6 = 30;
    public float startDialogueTimer7 = 30;
    public float startDialogueTimer8 = 1;
    public float startDialogueTimer9 = 30;
    public float startDialogueTimer10 = 30;
    public float startDialogueTimer11 = 30;
    public float startDialogueTimer12 = 1;
    public float startDialogueTimer13 = 30;
    public float startDialogueTimer14 = 30;
    public float startDialogueTimer15 = 30;
    public float startDialogueTimer16 = 30;

    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = ""; //string.empty
        StartDialouge();
        nextText = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && nextText == true || nextDialogue == true)
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
                nextDialogueTimer = 5;
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
        if (nextDialogueTimer <= 0)
        {
            NextLine();
        }

        // Stop Dialogue
        if (index == 9) //Will do for the entire of that line
        {
            nextText = false;
            startDialogueTimer2 -= Time.deltaTime;
        }
        if (index == 12) //
        {
            nextText = false;
            startDialogueTimer3 -= Time.deltaTime;
        }
        if (index == 20) //
        {
            nextText = false;
            startDialogueTimer4 -= Time.deltaTime;
        }
        if (index == 27) // Spawns the Rick door
        {
            if (!createRDoor) // Does once
            {
                RDoor.SetActive(true);
                //Instantiate(RDoor, new Vector3(0, 0, 0), Quaternion.identity);
                createRDoor = true;
            }
            nextText = false;
            startDialogueTimer5 -= Time.deltaTime;
        }
        if (index == 32) //
        {
            nextText = false;
            startDialogueTimer6 -= Time.deltaTime;
        }
        if (index == 37) //
        {
            nextText = false;
            startDialogueTimer7 -= Time.deltaTime;
        }
        if (index == 39) // Delete Rick Door
        {
            RDoor.SetActive(false);
            //DestroyImmediate(RDoor, true);
        }
        if (index == 41) // Spawns Second Real Door
        {
            if (!createCrashDoor) // Does once
            {
                CDoor.SetActive(true);
                //Instantiate(CDoor, new Vector3(0, 0, 0), Quaternion.identity);
                createCrashDoor = true;
            }
            nextText = false;
            startDialogueTimer8 -= Time.deltaTime;
        }
        if (index == 43) //
        {
            nextText = false;
            startDialogueTimer9 -= Time.deltaTime;
        }
        if (index == 45) //
        {
            nextText = false;
            startDialogueTimer10 -= Time.deltaTime;
        }
        if (index == 47) //
        {
            nextText = false;
            startDialogueTimer11 -= Time.deltaTime;
        }
        if (index == 50) //
        {
            CDoor.SetActive(false);
            nextText = false;
            startDialogueTimer12 -= Time.deltaTime;
        }
        if (index == 55) //
        {
            nextText = false;
            startDialogueTimer13 -= Time.deltaTime;
        }
        if (index == 57) //
        {
            nextText = false;
            startDialogueTimer14 -= Time.deltaTime;
        }
        if (index == 60) //
        {
            nextText = false;
            startDialogueTimer15 -= Time.deltaTime;
        }
        if (index == 65) //
        {
            RealDoor.SetActive(true);
            nextText = false;
            startDialogueTimer16 -= Time.deltaTime;
            
        }
        
        
        // Start Dialogue Timers
        if (startDialogue == true)
        {
            startDialogueTimer -= Time.deltaTime;
        }
        
        if (startDialogueTimer <= 0)
        {
            StartDialouge();
            NextLine();
            startDialogueTimer = 1;
            startDialogue = false;
            nextText = true;
        }

        if (startDialogueTimer2 <= 0)
        {
            NextLine();
            startDialogueTimer2 = 1;
            nextText = true;
        }
        
        if (startDialogueTimer3 <= 0)
        {
            NextLine();
            startDialogueTimer3 = 1;
            nextText = true;
        }
        
        if (startDialogueTimer4 <= 0)
        {
            NextLine();
            startDialogueTimer4 = 1;
            nextText = true;
        }
        if (startDialogueTimer5 <= 0) // Spawns the Rick door
        {
            NextLine();
            startDialogueTimer5 = 1;
            nextText = true;
        }
        
        if (startDialogueTimer6 <= 0)
        {
            NextLine();
            startDialogueTimer6 = 1;
            nextText = true;
        }
        
        if (startDialogueTimer7 <= 0)
        {
            NextLine();
            startDialogueTimer7 = 1;
            nextText = true;
        }
        
        if (startDialogueTimer8 <= 0) // Spawns Second Real Door
        {
            NextLine();
            startDialogueTimer8 = 1;
            nextText = true;
        }
        
        if (startDialogueTimer9 <= 0)
        {
            NextLine();
            startDialogueTimer9 = 1;
            nextText = true;
        }

        if (startDialogueTimer10 <= 0)
        {
            NextLine();
            startDialogueTimer10 = 1;
            nextText = true;
        }

        if (startDialogueTimer11 <= 0)
        {
            NextLine();
            startDialogueTimer11 = 1;
            nextText = true;
        }

        if (startDialogueTimer12 <= 0)
        {
            NextLine();
            startDialogueTimer12 = 1;
            nextText = true;
        }
        
        if (startDialogueTimer13 <= 0)
        {
            NextLine();
            startDialogueTimer13 = 1;
            nextText = true;
        }
        
        if (startDialogueTimer14 <= 0)
        {
            NextLine();
            startDialogueTimer14 = 1;
            nextText = true;
        }
        
        if (startDialogueTimer15 <= 0)
        {
            NextLine();
            startDialogueTimer15 = 1;
            nextText = true;
        }
        
        if (startDialogueTimer16 <= 0)
        {
            NextLine();
            startDialogueTimer16 = 1;
            nextText = true;
        }

    }

    void StartDialouge()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        // Type each character 1 by 1
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
            IndexTrigger();
            textSpeed = 0.1f;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    void IndexTrigger() //Do something at a specific text
    {
        if (index == 9)
        {
            nextText = false;
        }
    }
}
