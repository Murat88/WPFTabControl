using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OzyaysanBusinessEngine
{
   public class Enumarations
    {
        public enum UserType
        { 
        Owner=1,
        Master=2,
        Painter=3
        }
        public enum OrderStatus
        {
            İmalataHazır = 1,
            Imalatta = 2,
            Boyahanede = 3,
            Boyanıyor=4,
            Hazir=5,
            Gonderildi=6
        }
        public enum State
        {
           System=0	,
	       Pasif=1,
	       Aktif=2,
	       OnayBekliyor=3
        }
    }
}
