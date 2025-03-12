using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHold : MonoBehaviour
{
    Card holdCard; // Start is called before the first frame update
    public bool isHold = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector3.forward, 15.0f);

            if (hit == false) return;
            if (hit.transform.gameObject.GetComponent<Card>())
            {
                holdCard = hit.transform.gameObject.GetComponent<Card>();
                holdCard.isHold = true;
                this.isHold = true;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (holdCard == null) return;
            holdCard.isHold = false;
            holdCard = null;
            this.isHold = false;
        }

        if (holdCard != null)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            holdCard.transform.position = new Vector3(mousePos.x, mousePos.y, holdCard.transform.position.z);
        }

    }

}
