using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RodAndFrame { 

public class TrialDictionary : MonoBehaviour {

        public int repetitions;
        public bool randomizeRodValues;
        public List<float> rodValues;
        public List<float> frameValues;


        void Awake()
        {
            if (randomizeRodValues) {
                for (int i = 0; i < rodValues.Count; i++)
                    rodValues[i] = Random.Range(-45, 45);
            }
        }
    }
}
