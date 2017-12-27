using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Maya
{
    public class ExempleHhub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }

        public void Send(string name, string message)
        {
            // Call the addNewMessageToPage method to update clients.
            Clients.All.addNewMessageToPage(name, message);
        }

        public void SendVideo()
        {
            // Call the addNewMessageToPage method to update clients.
            Clients.All.getVideo();
        }

        public void PlayVideo()
        {
            // Call the addNewMessageToPage method to update clients.
            Clients.All.play();
        }
    }
}