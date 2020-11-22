using System;
using System.Collections.Generic;
using System.Text;

namespace Zadanie3 {
    public class Fax : IFax {
        public int Counter { get; private set; } = 0;
        protected IDevice.State state = IDevice.State.off;
        public IDevice.State GetState() => state;

        public void PowerOff() {
            state = IDevice.State.off;
        }

        public void PowerOn() {
            state = IDevice.State.on;
        }

        public void SendFax(in IDocument document, String to) {
            if(GetState() == IDevice.State.on) {
                Console.WriteLine($"{DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")} Fax: {document.GetFileName()} to: {to}");
                Counter++;
            }
        }
    }
}
