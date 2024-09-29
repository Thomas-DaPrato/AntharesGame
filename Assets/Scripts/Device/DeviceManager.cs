using UnityEngine;
using System.Collections.Generic;

public class DeviceManager : MonoBehaviour
{
    private Dictionary<int, string> connectedControllers = new Dictionary<int, string>();

    void Update()
    {
        string[] controllers = Input.GetJoystickNames();

        for (int i = 0; i < controllers.Length; i++)
        {
            string controllerName = controllers[i];

            if (!string.IsNullOrEmpty(controllerName) && controllerName.Length > 5)
            {
                if (!connectedControllers.ContainsKey(i))
                {
                    connectedControllers[i] = controllerName;
                    Debug.Log("Controller " + (i + 1) + ": " + controllerName + " connected.");

                    if (controllerName.ToLower().Contains("xbox"))
                    {
                        Debug.Log("Detected Xbox Controller");
                    }
                    else if (controllerName.ToLower().Contains("wireless controller") || controllerName.ToLower().Contains("dualshock") || controllerName.ToLower().Contains("dual sense"))
                    {
                        Debug.Log("Detected PlayStation Controller");
                    }
                    else
                    {
                        Debug.Log("Detected Other Controller: " + controllerName);
                    }
                }
            }
            else
            {
                if (connectedControllers.ContainsKey(i))
                {
                    Debug.Log("Controller " + (i + 1) + ": " + connectedControllers[i] + " disconnected.");
                    connectedControllers.Remove(i);  
                }
            }
        }

        List<int> toRemove = new List<int>();
        foreach (var controller in connectedControllers)
        {
            if (controller.Key >= controllers.Length || string.IsNullOrEmpty(controllers[controller.Key]))
            {
                Debug.Log("Controller " + (controller.Key + 1) + ": " + controller.Value + " disconnected.");
                toRemove.Add(controller.Key);
            }
        }

        foreach (var index in toRemove)
        {
            connectedControllers.Remove(index);
        }
    }
}
