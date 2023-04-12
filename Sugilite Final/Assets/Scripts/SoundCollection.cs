using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SoundCollection : MonoBehaviour
{
    public TextMeshProUGUI soundDisplay;
    public TMP_InputField soundCheck;

    private GameObject currentSoundCheck;

    private bool textBox = false;

    public void Update()
    {
        DisplaySounds(); //will update the display to show sounds collected
        if (Input.GetMouseButtonDown(0)) //when left click is pressed
        {
            RaycastHit hit; //shoot ray
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //from mouse and camera position
            if (Physics.Raycast(ray, out hit) && hit.collider.tag == "Object") //if ray hits object with object tag
            {
                Debug.Log("Hit" + hit.collider.name); 
                AudioClip audioClip = hit.collider.gameObject.GetComponent<AudioSource>().clip; //audioClip variable will become the audio clip of the object it hit
                string objectName = hit.collider.gameObject.name; //name will be name of object hit
                AddSound(objectName, audioClip); //will call add sound function with the name and the audio clip as the string and audioclip variable

                Debug.Log("" + objectName, audioClip);
            }
            
        }
        if (Input.GetKeyDown(KeyCode.E)) //when E is pressed
        {
            RaycastHit hit; 
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit) && hit.collider.tag == "Sound Check")
            {
                if (hit.collider.gameObject.GetComponent<AudioSource>().clip == null) //if there is no audio clip
                {
                    soundCheck.gameObject.SetActive(true); //show input field
                    textBox = true; //turn bool to true
                    currentSoundCheck = hit.collider.gameObject; //and currentSoundCheck will equal object hit
                    
                    
                }
                else //if sound already assigned then
                {
                    hit.collider.gameObject.GetComponent<AudioSource>().Play(); //audio clip will play
                    Debug.Log("Sound Assigned");
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Return) && textBox == true) //if enter is pressed when textBox bool is true
        {
            RemoveSound(currentSoundCheck); //call Remove sound function with currentSoundCheck object
        }
        
        
    }

    private Dictionary<string, AudioClip> soundsCollected = new Dictionary<string, AudioClip>(); //creates new dictionary called soundsCollected

    public void AddSound(string soundName, AudioClip sound) //Function takes a string and audio clip variable
    {
        soundsCollected.Add(soundName, sound); //string is added as the key and audio clip as value in dictionary
    }

    public void RemoveSound(GameObject obj) //function takes a game object
    {
        foreach(KeyValuePair<string, AudioClip> keyValuePair in soundsCollected) //looks through the dictionary, grabbing the keys and corresponding value
        {
            if (soundCheck.text.ToUpper() == keyValuePair.Key.ToUpper()) //if the text in the input field matches a key in the dictionary
            {
                string soundName = keyValuePair.Key; //the matching key will become the string for soundName variable
                AudioClip sound = keyValuePair.Value; //the corresponding value to the key will become the audioClip for sound variable
                obj.GetComponent<AudioSource>().clip = sound; //get the gameObject Audio source component and make the Audio Clip in the audio source = the sound variable from the dictionary
                soundsCollected.Remove(soundName); //remove sound name from the dictionary which will equal the key assigned to soundName
                soundCheck.gameObject.SetActive(false); //deactivate Input Field
                
            }

        }
    }

    public void DisplaySounds()
    {
        soundDisplay.text = "Sounds:\n"; //Text will say sound
        
        foreach(var keyValuePair in soundsCollected) //will go through dictionary
        {
            soundDisplay.text += "\n" + keyValuePair.Key; //will add the dictionary key name in the text box
        }
    }
    

}
