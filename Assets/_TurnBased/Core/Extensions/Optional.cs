using System;

namespace TurnBasedPractice.Extension
{
    public class Optional<T>
    {
        private T _value;

        public bool HasValue{ get; private set; }
        public T Put(){
            if (HasValue){ return _value; }
            else{ throw new NullReferenceException(); }
        }

        public Optional(T value){
            HasValue = value != null;
            _value   = value;
        }
    }
}