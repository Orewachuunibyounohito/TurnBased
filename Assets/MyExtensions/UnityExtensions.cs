using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UnityExtensions
{
    public static Vector3 AsVector3(this Vector2 source) => (Vector3)source;
    public static Vector2 AsVector2(this Vector3 source) => (Vector2)source;

    public static IDictionary<object, TValue> ValueCast<TValue>(this IDictionary source){
        var cast = new Dictionary<object, TValue>();
        foreach(var key in source.Keys ){
            var value = (TValue)source[key];
            cast.Add(key, value);
        }
        return cast;
    }

    // public static T Cast<T>(this object source){
    //     return (T)source;
    // }
}
