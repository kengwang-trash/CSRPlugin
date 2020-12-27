using FCPlugin.AmazeForm.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace FCPlugin.AmazeForm.Forms
{
    class ModalForm : AmazeForm
    {
        public ModalForm()
        {
            FormType = AmazeFormType.ModalForm;
        }

        public void Serialize()
        {
            ModalFormSend send = new ModalFormSend()
            {
                title = title,
                content = content
            };
            if (buttons.Count != 2) throw new AmazeFormExpection("模态表单按钮数错误,传入了 " + buttons.Count + " 个但应当是 2 个");
            send.button1 = buttons[0].name;
            buttons[0].selected = "true";
            send.button2 = buttons[1].name;
            buttons[1].selected = "false";
            JavaScriptSerializer jsons = new JavaScriptSerializer();
            json = jsons.Serialize(send);
        }
    }

    class ModalFormSend
    {
        public string title;
        public readonly string type = "modal";
        public string content;
        public string button1;
        public string button2;
    }
}
