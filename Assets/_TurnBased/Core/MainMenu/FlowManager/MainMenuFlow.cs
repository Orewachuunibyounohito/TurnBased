using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TurnBasedPractice.MainMenu.Commands;
using TurnBasedPractice.MainMenu.Commands.Factories;
using UnityEngine;
using UnityEngine.InputSystem;

public partial class MainMenuFlow : MonoBehaviour
{
    private const float SECOND_PER_FRAME = 0.1f;
    public bool IsScriptableVersion = false;
    
    [ShowIf("IsScriptableVersion")]
    [Header("-- Scriptable Version --")]
    [SerializeField, HideLabel]
    private ScriptableVersionFields scriptableVersionFields;
    [ReadOnly, SerializeField]
    private AnimateCommandFields peekCurrentCommand;

    private void Start()
    {
        peekCurrentCommand = default;
        if(IsScriptableVersion){
            UseScriptableVersion();
            return ;
        }
    }

    private void Update(){
        if(Mouse.current.leftButton.wasPressedThisFrame){
            peekCurrentCommand?.Skip();
        }else if(Keyboard.current.anyKey.wasPressedThisFrame){
            peekCurrentCommand?.Skip();
        }
    }

    private void UseScriptableVersion(){
        ObjectIntoCommandQueue();
        if(scriptableVersionFields.steps == default) return ;
        ExecuteCommands();
    }

    private void ObjectIntoCommandQueue(){
        scriptableVersionFields.steps = new Queue<AnimateCommandFields>();
        foreach (var step in scriptableVersionFields.stepObjects){
            step.Command.Target = step.Target;

            IAnimateCommand command = CommandFactories.Generate(step) as IAnimateCommand;
            var commandFields = new AnimateCommandFields(){
                AnimateCommand = command,
                PlayImmediately = step.PlayImmediately,
                CustomEndTime = new CustomEndTime{
                    Enable = step.Command.EndTimeContent.Enable,
                    EndTime = step.Command.EndTimeContent.EndTime,
                    Timer = 0
                }
            };
            scriptableVersionFields.steps.Enqueue(commandFields);
        }
    }

    public void ExecuteCommands() => StartCoroutine(CommandTask());

    private IEnumerator CommandTask(){
        Queue<AnimateCommandFields> stepsClone = new Queue<AnimateCommandFields>(scriptableVersionFields.steps);
        AnimateCommandFields currentCommand = ExecuteNextCommand(stepsClone);

        while(HasNext(stepsClone))
        {
            yield return new WaitForSeconds(SECOND_PER_FRAME);
            currentCommand.CustomEndTime.Timer += SECOND_PER_FRAME;
            if (IsRunning(currentCommand)) { continue; }
            currentCommand.CustomEndTime.Timer = 0;
            currentCommand = ExecuteNextCommand(stepsClone);
            peekCurrentCommand = currentCommand;
            if (NeedResetQueue(stepsClone)){
                stepsClone = new Queue<AnimateCommandFields>(scriptableVersionFields.steps);
            }
        }
    }
    private AnimateCommandFields ExecuteNextCommand(Queue<AnimateCommandFields> commands)
    {
        AnimateCommandFields commandFields = commands.Dequeue();
        commandFields.Execute();
        if(commandFields.PlayImmediately){
            commandFields.Finish();
        }
        return commandFields;
    }

    private bool HasNext(Queue<AnimateCommandFields> commands) => commands.Count > 0;
    private bool IsRunning(AnimateCommandFields command){
        if(!command.CustomEndTime.Enable){ return command.IsRunning; }

        return command.CustomEndTime.Timer < command.CustomEndTime.EndTime? true : false;
    } 
    private bool NeedResetQueue(Queue<AnimateCommandFields> commands) => commands.Count == 0 && scriptableVersionFields.Loop;
}

