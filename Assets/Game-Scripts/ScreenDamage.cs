using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenDamage : MonoBehaviour
{
    public Death death;
    private Image screen;
    Color alphaColor;
    // Start is called before the first frame update
    void Start()
    {
        screen = GetComponent<Image>();
        alphaColor = screen.color;  
    }

    // Update is called once per frame
 

    public void VisualDamage()
    {
        if (death.health >= 50)
        {
            alphaColor.a += 0.02f;
            screen.color = alphaColor;
        }

        else if (death.health < 50 && death.health >= 20)
        {
            alphaColor.a += 0.05f;
            screen.color = alphaColor;
        }

        else if (death.health <20)
        {
            alphaColor.a += 0.1f;
            screen.color = alphaColor;
        }
            

    }

    public void ResetVisualDamage()
    {
        alphaColor.a = 0.0f;
        screen.color = alphaColor;
    }
}

//https://www.youtube.com/watch?v=kKnspC7-Yg4