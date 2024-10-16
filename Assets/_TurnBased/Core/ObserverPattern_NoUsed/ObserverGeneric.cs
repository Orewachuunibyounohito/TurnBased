using System;
using UnityEngine;
using UnityEngine.Events;

public class Observer<T>
{
    [SerializeField]
    private T _value;

    [SerializeField]
    private UnityEvent<T> _onValueChanged;

    public T Value {
        get => _value;
        set => Set(value);
    }

    public static implicit operator T(Observer<T> observer) => observer._value;

    public Observer(T value, UnityAction<T> callback = null){
        _value          = value;
        _onValueChanged = new UnityEvent<T>();
        if(callback != null){ _onValueChanged.AddListener(callback); }
    }

    private void Set(T value){
        if(Equals(_value, value)){ return ; }
        _value = value;
        Invoke();
    }

    private void Invoke(){
        Debug.Log($"Invoking {_onValueChanged.GetPersistentEventCount()} listeners.");
        _onValueChanged.Invoke(_value);
    }

    public void AddListener(UnityAction<T> callback){
        if(callback == null){ return ; }
        if(_onValueChanged == null){ _onValueChanged = new UnityEvent<T>(); }
        
        _onValueChanged.AddListener(callback);
    }

    public void RemoveListener(UnityAction<T> callback){
        if(callback == null){ return ; }
        if(_onValueChanged == null){ return ; }

        _onValueChanged.RemoveListener(callback);
    }

    public void RemoveAllListeners(){
        if(_onValueChanged == null){ return ; }

        _onValueChanged.RemoveAllListeners();
    }

    public void Dispose(){
        RemoveAllListeners();
        _onValueChanged = null;
        _value          = default;
    }
}


public class HealthForObserver
{
    public Observer<int> Health = new Observer<int>(100);

    public HealthForObserver(){
        int example1 = Health.Value;
        int example2 = Health;          // public static implicit operator
    }
}