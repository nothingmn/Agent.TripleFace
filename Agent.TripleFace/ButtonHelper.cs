using System;
using System.Collections;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;

namespace Agent.TripleFace
{
    public enum Buttons
    {
        TopLeft = 0,
        BottomLeft = 1,
        TopRight = 2,
        MiddleRight = 3,
        BottomRight = 4
    }

    public enum ButtonDirection
    {
        Down = 0,
        Up = 1
    }

    public class ButtonHelper
    {
        private static object _lock = new object();
        private static ButtonHelper current;

        public static ButtonHelper Current
        {
            get
            {
                lock (_lock)
                {
                    if (current == null) current = new ButtonHelper(Buttons.MiddleRight);
                }
                return current;
            }
        }

        public delegate void ButtonPress(Buttons button, InterruptPort port, ButtonDirection direction, DateTime time);

        public event ButtonPress OnButtonPress;

        private ArrayList buttonPorts = new ArrayList();


        private ButtonHelper()
        {
        }

        private ButtonHelper(params Buttons[] buttons)
        {
            buttonPorts = new ArrayList();
            if (buttons != null)
            {
                foreach (Buttons b in buttons)
                {
                    InterruptPort s = new InterruptPort((Cpu.Pin)b, false, Port.ResistorMode.Disabled,
                                                        Port.InterruptMode.InterruptEdgeBoth);
                    buttonPorts.Add(s);
                    s.OnInterrupt += s_OnInterrupt;
                }
            }
        }

        private void s_OnInterrupt(uint data1, uint data2, DateTime time)
        {
            //data1 is the is the number of the pin of the switch
            //data2 is the value if the button is pushed or released; 0 = down, 1 = up
            if (OnButtonPress != null)
            {
                Buttons _button = (Buttons) data1;
                ButtonDirection _direction = (ButtonDirection) data2;
                foreach (InterruptPort b in buttonPorts)
                {
                    if ((uint)b.Id == data1)
                    {
                        OnButtonPress(_button, b, _direction, time);
                        break;
                    }
                }

            }

        }

    }
}