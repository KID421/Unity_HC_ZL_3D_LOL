using UnityEngine;
using System;

public class NewBehaviourScript : MonoBehaviour
{
    public void GetInput(string getInput)
    {
        int number = Convert.ToInt32(getInput);

        print(number);
    }
}
