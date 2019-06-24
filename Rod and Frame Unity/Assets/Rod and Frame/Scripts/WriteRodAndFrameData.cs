﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RodAndFrame {
    public class WriteRodAndFrameData : MonoBehaviour {

        private static WriteRodAndFrameData instance = null;
        public string participantID;

        public static WriteRodAndFrameData Instance {
            get { return instance; }
        }

        //This allows the start function to be called only once.
        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
            else
                instance = this;

            DontDestroyOnLoad(this.gameObject);
        }

        // Use this for initialization
        private void Start() {
            WriteToFile("subject ID", "trial", "head x", "head y", "head z", "rod origin", "frame origin", "selection");
        }


        public void WriteToFile(string a, string b, string c, string d, string e, string f, string g, string h) {

            string stringLine = a + "," + b + "," + c + "," + d + "," + e + "," + f + "," + g + "," + h;

        System.IO.StreamWriter file = new System.IO.StreamWriter("./Logs/" + participantID + "_log.csv", true);
        file.WriteLine(stringLine);
        file.Close();
    }
}
}
