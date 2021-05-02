using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveDisplay : MonoBehaviour
{
    public Text moveName1;
    public Text moveName2;
    public Text moveName3;
    public Text moveName4;
    public Text moveName5;

    public Text Attribute1;
    public Text Attribute2;
    public Text Attribute3;
    public Text Attribute4;
    public Text Attribute5;

    public Text owner1;
    public Text owner2;
    public Text owner3;
    public Text owner4;
    public Text owner5;

    public Slider moveSlider;

    public void setMoveDisplay(Move[] moveArray)
    {
        Debug.Log("In set move display");
        moveName1.text = moveArray[0].moveName;
        moveName2.text = moveArray[1].moveName;
        moveName3.text = moveArray[2].moveName;
        moveName4.text = moveArray[3].moveName;
        moveName5.text = moveArray[3].moveName;

        Attribute1.text = moveArray[0].attribute;
        Attribute2.text = moveArray[1].attribute;
        Attribute3.text = moveArray[2].attribute;
        Attribute4.text = moveArray[3].attribute;
        Attribute5.text = moveArray[4].attribute;

        owner1.text = moveArray[0].moveOwner;
        owner2.text = moveArray[1].moveOwner;
        owner3.text = moveArray[2].moveOwner;
        owner4.text = moveArray[3].moveOwner;
        owner5.text = moveArray[4].moveOwner;

    }

    public void moveBarSetup(float weightCap, float currentWeight)
    {
        moveSlider.maxValue = weightCap;
        moveSlider.value = currentWeight;
    }

    public void moveBarSet(float currentWeight)
    {
        moveSlider.value = currentWeight;
    }
}
