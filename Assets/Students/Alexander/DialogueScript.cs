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
    //Timers
    public bool startDialogue = false;
    public float startDialogueTimer = 3;
    public float startDialogueTimer2 = 30;
    public float startDialogueTimer3 = 30;
    public float startDialogueTimer4 = 30;
    public float startDialogueTimer5 = 1;
    public float startDialogueTimer6 = 30;
    public float startDialogueTimer7 = 30;
    public float startDialogueTimer8 = 30;
    
    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = ""; //string.empty
        StartDialouge();
        nextText = false;

        startDialogue = true; //DELETE
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && nextText == true)
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
        
        if (index == 3) // DELETE
        {
            nextText = false;
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
            //Instantiate()
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
        if (index == 41) // Spawns Second Real Door
        {
            
            nextText = false;
            startDialogueTimer8 -= Time.deltaTime;
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
