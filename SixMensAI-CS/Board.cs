using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixMensAI_CS
{
    class Board
    {
        Network network;
        AI ai;

        public void StartAIvsAI()
        {
            network = new Network();
            ai = new AI();
        }

        public void Host()
        {
            network.Host();
            GameLoop("host");
        }

        public void Connect(String input)
        {
            network.Connect(input);
            GameLoop("opponent");
        }

        public void GameLoop(String first)
        {
            int start = -1;
            int destination = -1;
            int side = -1;

            if(first == "host")
            {
                while (true) {
                    ai.MoveAI(ref start, ref destination);
                    side = ai.SearchPositionSide(destination);
                    if(side != -1)
                    {
                        
                    }
                    network.Send(ref start, ref destination);
                    network.Receive(ref start, ref destination);
                    ai.MoveOpp(ref start, ref destination);
                }

            } else if (first == "opponent")
            {
                while (true)
                {
                    network.Receive(ref start, ref destination);
                    ai.MoveOpp(ref start, ref destination);
                    ai.MoveAI(ref start, ref destination);
                    network.Send(ref start, ref destination);
                }
            }
        }
    }
}
