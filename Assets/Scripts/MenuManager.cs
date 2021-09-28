using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject ResumeButton;
    [SerializeField] private Canvas Menu;
    [SerializeField] private GameObject SaveButton;
    [SerializeField] private GameObject LoadButton;

#if UNITY_WEBGL
    private void Start()
    {
        SaveButton.SetActive(false);
        LoadButton.SetActive(false);
    }
#endif

    internal void ToggleResume(bool val)
    {
        ResumeButton.SetActive(val);
    }
    internal void ToggleMenu(bool val)
    {
        Menu.gameObject.SetActive(val);

    }
}
