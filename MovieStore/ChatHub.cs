using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace MovieStore
{
    public class ChatHub : Hub
    {
        public override Task OnConnected()
        {
            Clients.All.user(Context.User.Identity.Name);
            return base.OnConnected();
        }

        public void send(string message)
        {
            var you = "you";
            var other = Context.User.Identity.Name;
            Clients.Caller.message(you, message);
            Clients.Others.message(other, message);
        }
    }
}