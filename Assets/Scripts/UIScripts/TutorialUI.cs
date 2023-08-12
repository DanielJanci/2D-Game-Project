using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour
{
    public GameObject panel1; 
    public GameObject panel2; 

    void Start()
    {
        panel1.SetActive(true);
        panel2.SetActive(false);
    }
    
    
    public void OnPrevButton()
    {
        AudioManager.OnButtonClick?.Invoke();
        panel1.SetActive(true);
        panel2.SetActive(false);
    }
    
    public void OnNextButton()
    {
        AudioManager.OnButtonClick?.Invoke();
        panel1.SetActive(false);
        panel2.SetActive(true);
    }

    public void OnReturnButton()
    {
        AudioManager.OnButtonClick?.Invoke();
    }
}
