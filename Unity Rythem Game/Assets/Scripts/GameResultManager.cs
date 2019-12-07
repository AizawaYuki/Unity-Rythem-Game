using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameResultManager : MonoBehaviour
{
    public Text musicTitleUI;
    public Text scoreUI;
    public Text maxComboUI;
    // Start is called before the first frame update
    void Start()
    {
        musicTitleUI.text = PlayerInformation.musicTitle;
        scoreUI.text = "" + PlayerInformation.score;
        maxComboUI.text = ""+PlayerInformation.maxCombo;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
