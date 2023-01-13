using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsConsoleGame
{
    public interface IWorkerServices
    {
        public string Name { get; set; }
        public byte Lvl { get; set; }
        public ushort UpgradeCost { get; set; }

        public void IncreaseService() { }
        private void SetUpgradeCost() { }
    }
}
