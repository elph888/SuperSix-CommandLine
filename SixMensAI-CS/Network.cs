using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixMensAI_CS
{

    class Network
    {

        String IP;

        public void Host()
        {

        }

        public void Connect(String inputIP)
        {
            IP = inputIP;
        }

        internal void Send(ref int start, ref int destination)
        {
            throw new NotImplementedException();
        }

        internal void Receive(ref int start, ref int destination)
        {
            throw new NotImplementedException();
        }
    }
}
