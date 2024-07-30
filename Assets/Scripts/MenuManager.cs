using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject QuitButton;
    [SerializeField] private GameObject ResumeButton;
    [SerializeField] private Canvas Menu;
    [SerializeField] private GameObject SaveButton;
    [SerializeField] private GameObject LoadButton;

    private void Start()
    {
#if UNITY_WEBGL
        SaveButton.SetActive(false);
        LoadButton.SetActive(false);
#endif
#if UNITY_IOS || UNITY_ANDROID 
        QuitButton.SetActive(false);
#endif
    }

    internal void ToggleResume(bool val)
    {
        ResumeButton.SetActive(val);
    }
    internal void ToggleMenu(bool val)
    {
        Menu.gameObject.SetActive(val);

    }
}
