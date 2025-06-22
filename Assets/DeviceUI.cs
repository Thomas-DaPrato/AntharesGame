using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class UIController
{
    public Controller controller;
    public Sprite controllerSprite;
}
public class DeviceUI : MonoBehaviour
{
    [SerializeField]
    private Button LButton;
    [SerializeField]
    private Button RButton;
    [SerializeField]
    private Image imageController;
    [SerializeField]
    private List<UIController> controllers = new List<UIController>();
    private int index = 0;
    [SerializeField]
    private Player player;
    [SerializeField]
    private List<DeviceImageHandler> deviceImageHandlers = new List<DeviceImageHandler>();
    private void Awake()
    {
        foreach (DeviceImageHandler action in deviceImageHandlers)
            action.ChangeImage(Controller.Xbox);

        /*switch (player)
        {
            case Player.P1:
                DeviceManager.Instance.connectedController1Actions.Invoke(Controller.Xbox);
                break;
            case Player.P2:
                DeviceManager.Instance.connectedController2Actions.Invoke(Controller.Xbox);
                break;
    }   */
    }

    public void LeftButtonAction()
    {
        index--;
        if (index < 0)
            index = controllers.Count - 1;
        imageController.sprite = controllers[index].controllerSprite;
        ChangeController();
    }
    public void RightButtonAction()
    {
        index++;
        index = index % controllers.Count;
        imageController.sprite = controllers[index].controllerSprite;
        ChangeController();
    }

    private void ChangeController()
    {
        DeviceManager.Instance.ChangeController(player, controllers[index].controller);
    }
}
