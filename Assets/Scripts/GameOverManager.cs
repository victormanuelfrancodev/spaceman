using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{

    public static GameOverManager sharedInstance;
    public Canvas menuCanvas;
    // Start is called before the first frame update

    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
    }

    public void ShowGameOverCanvas(){
        menuCanvas.enabled = true;
    }

    public void HideGameOverCanvas(){
        menuCanvas.enabled = false;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
