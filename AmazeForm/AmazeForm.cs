using CSR;
using FCPlugin.AmazeForm.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FCPlugin.AmazeForm
{
    public class AmazeForm
    {
        private static MCCSAPI mcapi;
        private static Dictionary<uint, AmazeForm> forms = new Dictionary<uint, AmazeForm>();


        public string json = "";
        public AmazeFormType FormType;
        public string title = "AmazeForm";
        public uint formid;
        public string content = "This is an AmazeForm";
        public List<Button> buttons = new List<Button>();

        public static void InitAmazeForm(MCCSAPI api)
        {
            AmazeForm.mcapi = api;
        }

        public void Send(string uuid)
        {
            Console.WriteLine(json);
            formid = mcapi.sendCustomForm(uuid, json);
            forms.Add(formid, this);
        }

        public static void ProcessFormBack(FormSelectEvent fe)
        {
            if (!forms.ContainsKey((uint)fe.formid)) return;//这个表单可能不是由AmazeForm创建的
            Console.WriteLine("玩家 {0} 选择了表单 id={1} ，selected={2}", fe.playername, fe.formid, fe.selected);
            AmazeForm form = forms[(uint)fe.formid];
            Button btn = form.buttons.Find(x => { return x.selected == fe.selected; });
            if (btn.callBack != null)
            {
                btn.callBack.Invoke(new CsPlayer(mcapi, fe.playerPtr));
            }

            //在做完这一切之后,可以释放此表单
            forms.Remove((uint)fe.formid);
        }
    }

    public enum AmazeFormType
    {
        ModalForm
    }

    public class AmazeFormExpection : ApplicationException
    {
        private string error;
        private Exception innerException;
        public AmazeFormExpection()
        {

        }
        public AmazeFormExpection(string msg) : base(msg)
        {
            this.error = msg;
        }
        public AmazeFormExpection(string msg, Exception innerException) : base(msg)
        {
            this.innerException = innerException;
            this.error = msg;
        }
        public string GetError()
        {
            return error;
        }
    }
}
