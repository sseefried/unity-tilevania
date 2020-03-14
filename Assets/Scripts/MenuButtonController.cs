using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonController : MonoBehaviour
{

    public int index;
    [SerializeField] bool keyDown;
    [SerializeField] int maxIndex;

    // Update is called once per frame
    void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        if (vertical != 0)
        {
            int val = Mathf.RoundToInt(-Mathf.Sign(vertical));
            if (!keyDown)
            {
                index += val;
                index = Mathf.Clamp(index, 0, maxIndex);
                keyDown = true;
            }
        } else
        {
            keyDown = false;
        }
    }
}
