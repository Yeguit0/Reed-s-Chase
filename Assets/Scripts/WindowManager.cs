using UnityEngine;

public class WindowManager : MonoBehaviour
{
    public GameObject ventana;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Close()
    {
        ventana.SetActive(false);
    }

}