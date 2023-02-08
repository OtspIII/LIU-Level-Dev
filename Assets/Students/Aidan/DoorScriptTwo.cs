using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorScriptTwo : MonoBehaviour
{
    // Start is called before the first frame update
    public List<ButtonScript> Buttons;
    public List<ButtonScript> UnpressedButtons;

    void Start()
    {
        UnpressedButtons.AddRange(Buttons);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonPress(ButtonScript button)
    {
        bool correctButton = button == UnpressedButtons[0];
        
        if (!correctButton) return;
        
        UnpressedButtons.Remove(button);
        if (UnpressedButtons.Count == 0)
        {
            Destroy(gameObject);
        }
        Destroy(button.gameObject);
        // 
    }
}
