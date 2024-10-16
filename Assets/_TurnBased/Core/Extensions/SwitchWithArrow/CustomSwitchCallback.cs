using System;

namespace ChuuniExtension
{
    [Serializable]
    public class CustomSwitchCallback
    {
        public readonly int PreviousIndex;
        public readonly int Index;
        public readonly string Value;
        public readonly bool IsFirst;
        public readonly bool IsLast;

        public CustomSwitchCallback(int previousIndex, int index, string value, bool isFirst, bool isLast){
            PreviousIndex = previousIndex;
            Index = index;
            Value = value;
            IsFirst = isFirst;
            IsLast = isLast;
        }
    }
}