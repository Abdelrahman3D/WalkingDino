using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace WalkingDino
{
    public class Action
    {
        public string Name;
        public Keys Key;
        public bool IsPressed;
        public bool IsReleased;
        public float PressTime;

        public Action(string name, Keys key)
        {
            this.Name = name;
            this.Key = key;
        }
    }

    class Controller
    {
        private KeyboardState keyState;             
        private KeyboardState previousKeyState;
        private PlayerIndex playerNumber;
        private List<Action> actionList;

        public Controller(PlayerIndex playerNumber, List<Action> actionList)
        {
            this.playerNumber = playerNumber;
            this.actionList = actionList;
        }

        // Check if the action activated
        public bool IsActionPressed(string name)
        {
            Action currentAction =new Action(" ", Keys.Escape);
            foreach (Action action in actionList)
            {
                if (action.Name == name)
                    currentAction = action;
            }
            return currentAction.IsPressed;
        }

        // Check if the action released
        public bool IsActionRelesed(string name)
        {
            Action currentAction = new Action(" ", Keys.Escape);
            foreach (Action action in actionList)
            {
                if (action.Name == name)
                    currentAction = action;
            }
            return currentAction.IsPressed;
        }

        // Get the press time form an action
        public float ActionTime(string name)
        {
            Action currentAction = new Action(" ", Keys.Escape);
            foreach (Action action in actionList)
            {
                if (action.Name == name)
                    currentAction = action;
            }
            return currentAction.PressTime;
        }

        public void Update()
        {
            // Get current key state
            keyState = Keyboard.GetState();


            // Loop around actions list and check if it's pressed or not
            foreach (Action action in actionList)
            {
                if(keyState.IsKeyDown(action.Key))
                {
                    action.IsPressed = true;
                    action.PressTime += 1;
                }
                else if (keyState.IsKeyUp(action.Key) && previousKeyState.IsKeyDown(action.Key))
                {
                    action.IsPressed = false;
                    action.PressTime = 0;
                }
            }

            // Last Key pressed
            previousKeyState = keyState;
        }
    }
}
