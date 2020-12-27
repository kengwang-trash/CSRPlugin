# AmazeForm
这是 Kengwang 自己写的一个表单生成类,没别的,就是因为懒

## 生成说明
这边是生成表单的示例,我会采用Component的方式来生成,JSON转换的话准备采用 .NET 自带的转换

### 模态框

模态框为一个对话框,有两个按钮,通常为作是否选择的时候使用

```csharp
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
```

