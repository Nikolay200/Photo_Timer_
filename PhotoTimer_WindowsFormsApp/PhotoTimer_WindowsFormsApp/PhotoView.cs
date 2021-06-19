﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoTimer_WindowsFormsApp
{   
        public delegate void EventDelegate();

        public class PhotoView

        {

            public event EventDelegate MyEvent = null;

            public void InvokeEvent()

            {
                MyEvent.Invoke();
            }
        }    
}
