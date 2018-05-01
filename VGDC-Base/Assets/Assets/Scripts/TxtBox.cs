using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TxtBox : MonoBehaviour
{

    public bool loop;
    public bool lookAt;
	bool pressed = false;
    public string[] conversation;
    public float damping = 3.5f;
	public Camera cam;

    private int convoIndex = 0;
    private string currentTalk = "";
    private bool playerIn = false;
    // Use this for initialization
    private GUIText pGUI;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay(Collider col)
    {

        if (lookAt)
        {
            smoothLookAtIgnoreHeight(col.transform);
        }
			

        if (col.gameObject.tag == "Player")
        {

            currentTalk = conversation[convoIndex];

            if (Input.GetButtonDown("Activate"))
            {
                pressed = true;
            }

            if (Input.GetButtonUp("Activate"))
            {
                if (pressed == true)
                {
                    convoIndex++;
                    pressed = false;
                    if (convoIndex >= conversation.Length)
                    {
                        if (loop) convoIndex = 0;
                        else convoIndex = conversation.Length - 1;
                    }
                }
            }
        }
    }

    void OnTriggerExit(Collider col)
    {

        if (col.gameObject.tag == "Player")
        {
            currentTalk = "";
            playerIn = false;
        }
    }

    void OnGUI()
    {
		Vector3 points = cam.WorldToScreenPoint (transform.position);
		GUI.Label(new Rect(points.x,points.y, 500, 200), currentTalk);
    }

    void LookAtIgnoreHeight(Transform target)
    {

        Vector3 lookAtPos = target.position;
        //Set y of LookAt target to be my height.
        lookAtPos.y = transform.position.y;
        transform.LookAt(lookAtPos);
    }

    void smoothLookAtIgnoreHeight(Transform target)
    {
        Vector3 targetPos = target.position;

        targetPos.y = transform.position.y;

        Quaternion rotation = Quaternion.LookRotation(targetPos - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
    }

}
