using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GOAPState
{
        public string name;
        public bool val;

        public GOAPState(string name)
        {
            this.name = name;
        }

        public GOAPState(string name, bool value)
        {
            this.name = name;
            val = value;
        }
}
