using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using CSR;
using FCPlugin.AmazeForm;
using System.Web.Script.Serialization;
using FCPlugin.AmazeForm.Components;

namespace FCPlugin
{
    class Program
    {
        private static MCCSAPI mcapi = null;

        // 测试用的临时表单ID
        //private static uint tformid;

        public static void init(MCCSAPI api)
        {
            mcapi = api;
            Console.OutputEncoding = Encoding.UTF8;
            AmazeForm.AmazeForm.InitAmazeForm(api);

            api.addBeforeActListener(EventKey.onAttack, x =>
            {
                try
                {
                    AmazeForm.Forms.ModalForm modalForm = new AmazeForm.Forms.ModalForm();
                    modalForm.title = "这是新标题";
                    modalForm.content = "请选择!";
                    Button btn = new Button();
                    btn.name = "真按钮";
                    btn.callBack = (player) => { Console.WriteLine("玩家" + player.getName() + "选择了真!"); };
                    modalForm.buttons.Add(btn);
                    btn = new Button();
                    btn.name = "假按钮";
                    btn.callBack = (player) => { Console.WriteLine("玩家" + player.getName() + "选择了假!"); };
                    modalForm.buttons.Add(btn);
                    CsPlayer p = new CsPlayer(api, (BaseEvent.getFrom(x) as AttackEvent).playerPtr);
                    modalForm.Serialize();
                    modalForm.Send(p.Uuid);
                    return true;
                }catch(Exception e)
                {
                    Console.WriteLine(e.Message+e.StackTrace+e.Source);
                    return true;
                }

            });

            api.addAfterActListener(EventKey.onFormSelect, x =>
            {
                FormSelectEvent fse = BaseEvent.getFrom(x) as FormSelectEvent;
                AmazeForm.AmazeForm.ProcessFormBack(fse);
                return true;
            });
        }

    }
}

namespace CSR
{
    partial class Plugin
    {
        /// <summary>
        /// 通用调用接口，需用户自行实现
        /// </summary>
        /// <param name="api">MC相关调用方法</param>
        public static void onStart(MCCSAPI api)
        {
            // TODO 此接口为必要实现
            FCPlugin.Program.init(api);
            Console.WriteLine("[Demo] CSR测试插件已装载。");
        }
    }
}