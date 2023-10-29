using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Map;
    public void Start()
    {
        Time.timeScale = 0f;
    }
    public void pause()
    {
        Time.timeScale = 0f;
    }
    public void Play()
    {
        Time.timeScale = 1f;
    }
    public void FFWD()
    {
        Time.timeScale = 3f;
    }
    public void Road()
    {
        Map.GetComponent<MapEditor>().buildroad = true;
    }
    public void grass()
    {
        Map.GetComponent<MapEditor>().buildroad = false;
    }
}
