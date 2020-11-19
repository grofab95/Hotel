using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Environment
{
    public class Config
    {
        public decimal PriceForStay => 50;
        public int FreeRoomHour => 15;

        private Config() { }

        public static Config Get => new Config();
    }
}
