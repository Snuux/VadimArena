using UnityEngine;

namespace Assets._Project.Develop.Runtime.Infrastructure.Gameplay
{
    public class InputSequenceHandler
    {
        public string InputSymbols { get; private set; }

        public void ProcessInputKeys()
        {
            if (Input.inputString.Length > 0 && Input.anyKeyDown)
            {
                foreach (char character in Input.inputString)
                    InputSymbols += character;

                Debug.Log(InputSymbols);
            }
        }

        public void Clear() => InputSymbols = "";
    }
}