using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

public class DialogueScript : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    [FormerlySerializedAs("lines")] public string[] NormalTextLines;
    public string[] BlankUseText;
    public string[] OPENText;
    public float textSpeed;
    public int index;
    public bool nextText = false;
    public GameObject RDoor;
    public bool createRDoor = false;
    public GameObject CDoor;
    public bool createCrashDoor = false;
    public GameObject RealDoor;
    public bool createRealDoor = false;
    public TheUnfinishedSpawnScript UnfinishedSpawnScript;
    public bool SpawnInUnfinished = false;
    
    public GameObject Player;
    public GameObject UnfinishedSpawn;
    public CameraController MainCamera;

    public bool PressedO = false;
    public bool PressedP = false;
    public bool PressedE = false;
    public bool PressedN = false;
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
    public float startDialogueTimer17 = 30;
    public float startDialogueTimer18 = 30;
    public float startDialogueTimer19 = 30;
    public float startDialogueTimer20 = 30;
    public float startDialogueTimer21 = 30;
    public float startDialogueTimer22 = 1;
    public float startDialogueTimer23 = 30;
    public float startDialogueTimer24 = 30;

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
            if (textComponent.text == NormalTextLines[index])
            {
                NextLine();
                nextDialogueTimer = 5;
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = NormalTextLines[index];
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
            nextText = false;
            index = 87;
            //startDialogueTimer16 -= Time.deltaTime;
        }
        
        /*
        if (index == 87) //
        {
            nextText = false;
            startDialogueTimer22 -= Time.deltaTime;
        }
        
        if (index == 69) //
        {
            nextText = false;
            startDialogueTimer17 -= Time.deltaTime;
        }
        if (index == 72) //
        {
            nextText = false;
            startDialogueTimer18 -= Time.deltaTime;
        }
        if (index == 76) //
        {
            nextText = false;
            startDialogueTimer19 -= Time.deltaTime;
        }
        if (index == 79) //
        {
            nextText = false;
            startDialogueTimer20 -= Time.deltaTime;
        }
        if (index == 82) //
        {
            nextText = false;
            startDialogueTimer21 -= Time.deltaTime;
        }
        if (index == 85) //
        {
            nextText = false;
            //startDialogueTimer22 -= Time.deltaTime;
        }
        */
        if (index == 95) //
        {
            nextText = false;
            UnfinishedSpawnScript.Spawnin = true;
            UnfinishedSpawnScript.SpawnSound = true;
            startDialogueTimer23 -= Time.deltaTime;
            MainCamera.Target = UnfinishedSpawn;
        }
        if (index == 96) //
        {
            //nextText = false;
            textComponent.fontSize = 60;
            textSpeed = 0.1f;
            nextText = true;
            //NextLine();
        }
        
        if (index == 97) //
        {
            RealDoor.SetActive(true);
            MainCamera.Target = Player;
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

        if (startDialogueTimer17 <= 0)
        {
            NextLine();
            startDialogueTimer17 = 1;
            nextText = true;
        }

        if (startDialogueTimer18 <= 0)
        {
            NextLine();
            startDialogueTimer18 = 1;
            nextText = true;
        }

        if (startDialogueTimer19 <= 0)
        {
            NextLine();
            startDialogueTimer19 = 1;
            nextText = true;
        }

        if (startDialogueTimer20 <= 0)
        {
            NextLine();
            startDialogueTimer20 = 1;
            nextText = true;
        }

        if (startDialogueTimer21 <= 0)
        {
            NextLine();
            startDialogueTimer21 = 1;
            nextText = true;
        }

        if (startDialogueTimer22 <= 0)
        {
            NextLine();
            startDialogueTimer22 = 1;
            nextText = true;
        }
        
        if (startDialogueTimer23 <= 0)
        {
            NextLine();
            startDialogueTimer23 = 1;
            nextText = true;
        }

        if (Input.GetKey(KeyCode.O) & PressedO == false)
        {
            NextLineOpen();
            PressedO = true;
        }
        if (Input.GetKey(KeyCode.P) & PressedO == true)
        {
            NextLineOpen();
            PressedP = true;
        }
        if (Input.GetKey(KeyCode.E) & PressedP == true)
        {
            NextLineOpen();
            PressedE = true;
        }
        if (Input.GetKey(KeyCode.N) & PressedE == true)
        {
            NextLineOpen();
            PressedN = true;
            index = 0;
        }
    }

    void StartDialouge()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        /*string ss = NormalTextLines[index];
        // Type each character 1 by 1
        foreach (char c in ss.ToCharArray())*/
        // Type each character 1 by 1
        foreach (char c in NormalTextLines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
    
    IEnumerator TypeLineOpen()
    {
        // Type each character 1 by 1
        foreach (char c in OPENText[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < NormalTextLines.Length - 1)
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
    
    void NextLineOpen()
    {
        if (index < OPENText.Length - 1)
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
