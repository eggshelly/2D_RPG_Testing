using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour {

    [Tooltip("Image of the dialog box")]
    [SerializeField] private Image dialogBox;
    [Tooltip("The text box where the text is inputted")]
    [SerializeField] private Text dialogTextDisplay;
    [Tooltip(".txt file of what to display")]
    [SerializeField] private TextAsset dialogText;
    [Tooltip("where the sprite should be displayed")]
    [SerializeField] private Image displaySprite;     
    
    private string spritePath = "Sprite/FaceSprite/";    
    private string[,] textArray;                            // { { [displaySprite] , [displaySpriteMode] , text} , {...}}
    private int currentTextPos = 0;                         //

    //private bool playerInrange;  

	// Use this for initialization
	void Start () {
        dialogTextDisplay.text = "";        //initialize text to nothing (so displays nothing)
        textArray = parseDialog(dialogText);
	}
	
	// Update is called once per frame
	void Update ()
    {
        //updateDialog();
    }

    /*private void updateDialog() //old update dialog which uses more private var
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerInrange)  //if player is here and presses key
        {
            if (dialogBox.activeInHierarchy) 
            {
                if (currentTextPos + 1 < textArray.Length)
                {
                    dialogTextDisplay.text = textArray[++currentTextPos];       //updates dialog to next (and inceases dialog pos)
                }
                else
                {
                    dialogBox.SetActive(false);                 //reached the end of dialog, close dialog box and return pos to 0
                    currentTextPos = 0;
                }

            }
            else
            {
                dialogBox.SetActive(true);                  //dialogbox isn't active we activate it and set dialogtext = to something
                dialogTextDisplay.text = textArray[currentTextPos];
            }
        }
    }*/

    /*private void OnTriggerEnter2D(Collider2D other)       //used to use this to set var but use OnTriggerStay2D is so much easier
    {
        if (other.CompareTag("Player"))
        {
            playerInrange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInrange = false;
            
        }
    }*/

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                updateDialogBox(other);
            }

        }
    }

    private void updateDialogBox(Collider2D other)  //basically same thing as commented out dialog box but adding if player can move/not move
    {
        if (dialogBox.enabled)
        {
            if (currentTextPos + 1 < textArray.GetLength(0))        //C# 2d array.Length returns row*col to get row or col only use .GetLength(int) where int is the dimention (ex,  0 is the row, 1 is the col) 
            {
                dialogTextDisplay.text = textArray[++currentTextPos, 2];
                displaySprite.sprite = loadSprite(textArray[currentTextPos, 0], textArray[currentTextPos, 1]);
            }
            else
            {
                other.GetComponent<Player_Controller>().flipCanMove();
                //dialogBox.SetActive(false);     //if dialogBox was set as type GameObject
                dialogBox.enabled = false;
                displaySprite.enabled = false;  //disables image
                displaySprite.sprite = null;
                dialogTextDisplay.text = "";
                currentTextPos = 0;
            }

        }
        else
        {
            //dialogBox.SetActive(true); //if dialogBox was set as type GameObject
            dialogBox.enabled = true;
            displaySprite.enabled = true;
            displaySprite.sprite = loadSprite(textArray[currentTextPos, 0], textArray[currentTextPos, 1]);
            dialogTextDisplay.text = textArray[currentTextPos,2];
            other.GetComponent<Player_Controller>().flipCanMove();
        }
        if (GetComponent<NPC_Movement>() != null)       //TODO: later on if this is tacted on to npc stop them from moving, TODO: seperate showing dialog box and npc data
            GetComponent<NPC_Movement>().flipCanMove();
    }

    private Sprite loadSprite(string spriteName, string spriteMode)
    {
        // Load all sprites in atlas
        Sprite[] abilityIconsAtlas = Resources.LoadAll<Sprite>(spritePath+ spriteName);
        // Get specific sprite
        foreach(Sprite s in abilityIconsAtlas)
        {
            if (s.name.Equals(spriteName + "_" + spriteMode))
                return s;
        }
        return null;
;
    }


        //TODO: easier way to do this would prob be regex
    private string[,] parseDialog(TextAsset dialog)     // parses the text file so returns a 2d array of {[which file to display],[white emote to display], <text>}
    {
        string [] temp= dialog.text.Split('\n');
        string[,] rtn = new string[temp.Length,3];
        for(int i = 0; i<temp.Length; ++i)
        {
            int firstIndex = temp[i].IndexOf(']');
            int secondIndex = temp[i].IndexOf(']', firstIndex+1);
            
            if(firstIndex == -1 || secondIndex == -1)
            {
                rtn[i,0] = "null";
                rtn[i,1] = "null";
                rtn[i,2] = "null";
            }
            else
            {
                rtn[i, 0] = temp[i].Substring(1,firstIndex-1);
                rtn[i, 1] = temp[i].Substring(firstIndex+2, secondIndex-firstIndex-2);
                rtn[i, 2] = temp[i].Substring(secondIndex+1);

            }
                
        }
        return rtn;
    }
}   //TODO: try/catch/error prone this lol


