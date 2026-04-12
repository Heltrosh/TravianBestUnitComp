using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace TravianBestUnitComp
{
    internal class Animals
    {
        static Animals _instance = new Animals();
        public static Animals Instance { get { return _instance; } }
        public int[] animals { get; set; }

        private Animals()
        {
            animals = new int[10]
            {
                0,0,0,0,0,0,0,0,0,0
            };
        }
    }
}
