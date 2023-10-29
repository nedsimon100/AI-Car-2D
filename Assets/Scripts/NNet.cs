using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NNet : MonoBehaviour
{
    CarController control;
    public float[] inputLayer = new float[5];
    public float[,] hiddenLayers = new float[2, 10];
    public float[,] weights = new float[3,100];
    public float[] outputLayer = new float[2];
    //--------------------------------------------------------------------------------------------------------------------
    //--------------------------------------------------------------------------------------------------------------------

    public void runNetwork()
    {
        for (int y = 0; y < 10; y ++)
        {
            for (int x = 0; x < 2; x++)
            {
                hiddenLayers[x,y] = 0;
            }
        }
        outputLayer[0] = 0;
        outputLayer[1] = 0;
        for (int y = 0; y < 50; y+=5)
        {
            for (int x = 0; x < 5; x++)
            {
                hiddenLayers[0, y/5] += (inputLayer[x]) * weights[0, x+y];
            }
        }
        for (int y = 0; y < 100; y += 10)
        {
            for (int x = 0; x < 10; x++)
            {
                hiddenLayers[1, y/10] += (hiddenLayers[0,x]) * weights[1, x + y];
            }
        }
        for (int y = 0; y < 100; y += 10)
        {
            for (int x = 0; x < 2; x++)
            {
                outputLayer[x] += (hiddenLayers[1, x]) * weights[1, x + y];
            }
        }
        this.GetComponent<CarController>().useOutputs(outputLayer[0], outputLayer[1]);
    }
    //--------------------------------------------------------------------------------------------------------------------


}
