using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public abstract class GhostState
    {
        protected GhostController ghostController;

        public GhostState(GhostController controller)
        {
            ghostController = controller;
        }

        public virtual void OnStateEnter() { }
        public abstract void UpdateState();
    }

