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
        Controller tmpController = Controller.Xbox;
        int controller = 0;
        if (player == Player.P1)
            controller = PlayerPrefs.GetInt("P1Controller");
        else
            controller = PlayerPrefs.GetInt("P2Controller");
        switch (controller)
        {
            case 0:
                tmpController = Controller.Xbox;
                break;
            case 1:
                tmpController = Controller.PlayStation;
                break;
            case 2:
                tmpController = Controller.Switch;
                break;
            case 3:
                tmpController = Controller.PC;
                break;
        }
        foreach (DeviceImageHandler action in deviceImageHandlers)
            action.ChangeImage(tmpController);
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
