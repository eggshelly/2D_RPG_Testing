using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Depth : MonoBehaviour {

    SpriteRenderer render;
    //float timer = 3;
    [SerializeField] private bool isPlayer = false;
    [SerializeField] private bool hideable = false;
    [SerializeField] private bool interactable = false;
    private Color transparent = new Color(1, 1, 1, 0.5f);
    private Color full = new Color(1, 1, 1, 1f);

	// Use this for initialization
	void Start () {
        render = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        //render.sortingOrder = (int)Camera.main.WorldToScreenPoint(this.transform.position).y * -1;


	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<Depth>() != null && other.GetComponent<Depth>().enabled && hideable && !isPlayer && other.GetComponent<Depth>().isPlayer)
        {
            render.color = transparent;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Depth>() != null && other.GetComponent<Depth>().enabled && hideable && !isPlayer && other.GetComponent<Depth>().isPlayer)
        {
            render.color = full;
        }
    }
}
