using System;
using System.Collections.Generic;
using System.Text;

namespace Zadanie2 {
    public class MultifunctionalDevice : Copier, IFax {
        public int FaxCounter { get; private set; } = 0;

        public void SendFax(in IDocument document, String to) {
            if(GetState() == IDevice.State.on) {
                Console.WriteLine($"{DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")} Fax: {document.GetFileName()} to: {to}");
                FaxCounter++;
            }
        }

        public void ScanAndFax(String to) {
            IDocument scannedDocument;
            Scan(out scannedDocument);
            SendFax(scannedDocument, to);
        }
    }
}
