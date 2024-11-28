using System.Windows.Input;
using UnityEngine;

public class ConvertController
{
    public string StateMachineName { get; }
    private readonly IController controller;
    private readonly bool enable;

    public ConvertController(string stateMachineName, IController controller, bool enable)
    {
        StateMachineName = stateMachineName;
        this.controller = controller;
        this.enable = enable;
    }

    public void Execute()
    {
        controller.canRun = enable;
        Debug.Log($"[ConvertController] {StateMachineName} …Ë÷√ {controller.GetType().Name}.enabled = {enable}");
    }
}