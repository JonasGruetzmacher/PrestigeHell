using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DebugCommandBase
{
    private string _commandId;
    private string _commandDescription;
    private string _commandFormat;

    public string commandId { get => _commandId; }
    public string commandDescription { get => _commandDescription; }
    public string commandFormat { get => _commandFormat; }

    public DebugCommandBase(string id, string description, string format)
    {
        _commandId = id;
        _commandDescription = description;
        _commandFormat = format;
    }
}


public class DebugCommand : DebugCommandBase
{
    private Action command;

    public DebugCommand(string id, string description, string format, Action command) : base(id, description, format)
    {
        this.command = command;
    }

    public void Invoke()
    {
        Debug.Log("Invoking command: " + commandId);
        command.Invoke();
    }
}

public class DebugCommand<T1> : DebugCommandBase
{
    private Action<T1> command;

    public DebugCommand(string id, string description, string format, Action<T1> command) : base(id, description, format)
    {
        this.command = command;
    }

    public void Invoke(T1 value)
    {
        Debug.Log("Invoking command: " + commandId);
        command.Invoke(value);
    }
}

public class DebugCommand<T1, T2> : DebugCommandBase
{
    private Action<T1, T2> command;

    public DebugCommand(string id, string description, string format, Action<T1, T2> command) : base(id, description, format)
    {
        this.command = command;
    }

    public void Invoke(T1 value1, T2 value2)
    {
        Debug.Log("Invoking command: " + commandId);
        command.Invoke(value1, value2);
    }
}