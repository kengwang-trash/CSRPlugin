using CSR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FCPlugin.AmazeForm.Components
{
    public class Button
    {
        public delegate void CallBack(CsPlayer player);
        public string name="按钮";
        public string image ="";
        public bool hasimage { get { return string.IsNullOrEmpty(image); } }
        public bool imagetype { get { return image.StartsWith("http"); } }
        public CallBack callBack;
        public string selected = "";
    }
}
