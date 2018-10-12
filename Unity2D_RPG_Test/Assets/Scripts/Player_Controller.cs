using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour {

    [SerializeField] private float move_speed;
    private bool canMove = true;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

       

    }

    private void FixedUpdate()
    {
        if (canMove && (Input.GetAxisRaw("Horizontal") != 0f || Input.GetAxisRaw("Vertical") != 0f))
        {
            transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * move_speed * Time.deltaTime, Input.GetAxisRaw("Vertical") * move_speed * Time.deltaTime, 0f));
        }
   
    }

    public bool getCanMove() { return canMove;  }
    public void flipCanMove() { canMove = !canMove; }
}
