using System.Text;
using TurnBasedPractice.Character;
using TurnBasedPractice.Character.Controller;


namespace TurnBasedPractice.Extension
{
    public static class TurnBasedExtensions
    {   
        public static string ToStringEx(this Hero[] heroes){
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("[");
            foreach(var item in heroes){
                stringBuilder.Append(item.name + ", ");
            }
            stringBuilder.Remove(stringBuilder.Length - 2, 2);
            stringBuilder.Append("]");
            return stringBuilder.ToString();
        }
    }
}