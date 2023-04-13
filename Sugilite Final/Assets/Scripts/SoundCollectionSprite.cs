using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SoundCollectionSprite : MonoBehaviour
{
    public TextMeshProUGUI soundDisplay;
    public Canvas inventory;

    private GameObject currentSoundCheck;

    private bool inventoryUp = false;

    public void Update()
    { 
       
        if (Input.GetMouseButtonDown(0)) //when left click is pressed
        {
            RaycastHit hit; //shoot ray
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //from mouse and camera position
            if (Physics.Raycast(ray, out hit) && hit.collider.tag == "Object") //if ray hits object with object tag
            {
                Debug.Log("Hit" + hit.collider.name); 
                AudioClip audioClip = hit.collider.gameObject.GetComponent<AudioSource>().clip; //audioClip variable will become the audio clip of the object it hit
                Sprite objectSprite = hit.collider.gameObject.GetComponentInChildren<SpriteRenderer>(true).sprite; //name will be name of object hit
                AddSound(objectSprite, audioClip); //will call add sound function with the name and the audio clip as the string and audioclip variable

                Debug.Log("" + objectSprite, audioClip);
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
                    inventory.gameObject.SetActive(true); //show input field
                    inventoryUp = true; //turn bool to true
                    currentSoundCheck = hit.collider.gameObject; //and currentSoundCheck will equal object hit
                    
                    
                }
                else //if sound already assigned then
                {
                    hit.collider.gameObject.GetComponent<AudioSource>().Play(); //audio clip will play
                    Debug.Log("Sound Assigned");
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && inventoryUp == false) //if enter is pressed when textBox bool is true
        {
            inventory.gameObject.SetActive(true);
            inventoryUp = true;
            //RemoveSound(currentSoundCheck); //call Remove sound function with currentSoundCheck object
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && inventoryUp == true)
        {
            inventory.gameObject.SetActive(false);
            inventoryUp = false;
        }
        
        
    }

    private Dictionary<Sprite, AudioClip> soundsCollected = new Dictionary<Sprite, AudioClip>(); //creates new dictionary called soundsCollected
    public Image[] soundSprite; //creates array for image boxes

    public void AddSound(Sprite soundIcon, AudioClip sound) //Function takes a string and audio clip variable
    {
        soundsCollected.Add(soundIcon, sound); //string is added as the key and audio clip as value in dictionary
        DisplaySounds(soundIcon);
    }

    public void ChooseImageBox(Sprite soundIcon)
    {
        
    }

    /*public void RemoveSound(GameObject obj) //function takes a game object
    {
        foreach(KeyValuePair<Sprite, AudioClip> keyValuePair in soundsCollected) //looks through the dictionary, grabbing the keys and corresponding value
        {
            if (soundSprite. == keyValuePair.Key) //if the text in the input field matches a key in the dictionary
            {
                Sprite soundIcon = keyValuePair.Key; //the matching key will become the string for soundName variable
                AudioClip sound = keyValuePair.Value; //the corresponding value to the key will become the audioClip for sound variable
                obj.GetComponent<AudioSource>().clip = sound; //get the gameObject Audio source component and make the Audio Clip in the audio source = the sound variable from the dictionary
                soundsCollected.Remove(soundIcon); //remove sound name from the dictionary which will equal the key assigned to soundName
                inventory.gameObject.SetActive(false); //deactivate Input Field
                
            }

        }
    }*/

    public void DisplaySounds(Sprite soundIcon)
    {
        soundDisplay.text = "Sounds:\n"; //Text will say sound
        
            for (int i = 0; i < soundSprite.Length; i++)
            {
                if (soundSprite[i].sprite == null) //this is where sprite is placed.
                {
                    soundSprite[i].sprite = soundIcon;
                    return;
                }
            }
    }
    

}

