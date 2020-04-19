using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance { get; private set; }

        public bool Halted { get; private set; }

        void Start()
        {
            Instance = this;
        }

        private void Update()
        {
            
        }

        public void Ate(GameObject thing)
        {
            Halted = true;
            GetComponent<ContainersController>().Remove(thing);
            GetComponent<HealthController>().AddHealth(1);
        }

        public void GoodToGo()
        {
            Halted = false;
        }
    }
}
