using UnityEngine;
public class MenuPrincipal : MonoBehaviour
{
    public void Quiter()
    {
        Debug.Log("QUIT");
        Application.Quit();

    }

    public void PrintDebug() {
        Debug.Log("PLay");
    }

}
