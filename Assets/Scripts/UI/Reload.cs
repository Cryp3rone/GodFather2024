using UnityEngine;

public class Reload : MonoBehaviour
{
    public void ReloadApp()
    {
        Application.LoadLevel("GameManager");
    }
}
