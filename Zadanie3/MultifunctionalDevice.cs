using System;
using System.Collections.Generic;
using System.Text;

namespace Zadanie3 {
    public class MultifunctionalDevice : Copier {
        public Fax Fax { get; private set; }
        public int FaxCounter { get => Fax.Counter; }

        public MultifunctionalDevice() {
            Fax = new Fax();
        }

        public void SendFax(in IDocument document, string to) {
            if(state == IDevice.State.on) {
                Fax.PowerOn();
                Fax.SendFax(document, to);
                Fax.PowerOff();
            }
        }
        

        public void ScanAndFax(string to) {
            IDocument scannedDocument;
            Scan(out scannedDocument);
            SendFax(scannedDocument, to);
        }
    }
}
