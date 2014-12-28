using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Digital.Chart.Chart
{
    public class ChatHub : Hub
    {
        private readonly ChatTicker ticker;

        public ChatHub()
        {
            ticker = ChatTicker.Instance;
        }

        public void Send(string group, string username)
        {
            //注册到全局
            ticker.GlobalContext.Groups.Add(Context.ConnectionId, group);
            Clients.All.broadcastMessage(group, "user register:" + username);
        }
    }

    public class ChatTicker
    {
        #region 实现一个单例

        private static readonly ChatTicker _instance =
            new ChatTicker(GlobalHost.ConnectionManager.GetHubContext<ChatHub>());

        private readonly IHubContext m_context;

        private ChatTicker(IHubContext context)
        {
            m_context = context;
            //这里不能直接调用Sender，因为Sender是一个不退出的“死循环”，否则这个构造函数将不会退出。
            //其他的流程也将不会再执行下去了。所以要采用异步的方式。
            //Task.Run(() => Sender());
            m_context.Clients.Group("Group1").broadcastMessage1("group is:" + tag, "current count:" + count);
        }

        public IHubContext GlobalContext
        {
            get { return m_context; }
        }

        public static ChatTicker Instance
        {
            get { return _instance; }
        }

        #endregion

        public void Sender()
        {
            int count = 0;
            while (true)
            {
                Thread.Sleep(500);
                int tag = count % 2;
                //动态绑定前端的js函数 broadcaseMessage
                m_context.Clients.Group(tag + "").broadcastMessage("group is:" + tag, "current count:" + count);
                count++;
            }
        }
    }
}