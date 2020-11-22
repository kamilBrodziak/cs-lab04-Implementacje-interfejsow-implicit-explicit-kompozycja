using System;

namespace Zadanie3 {
    public class Copier : BaseDevice {

        public Printer Printer { get; private set; }
        public Scanner Scanner { get; private set; }
        public int PrintCounter { get => Printer.Counter; }
        public int ScanCounter { get => Scanner.Counter; }
        public Copier() {
            Printer = new Printer();
            Scanner = new Scanner();
        }

        public void Scan(out IDocument document, IDocument.FormatType formatType) {
            if(state == IDevice.State.on) {
                Scanner.PowerOn();
                Scanner.Scan(out document, formatType);
                Scanner.PowerOff();
            } else {
                document = null;
            }
        }

        public void Scan(out IDocument document) {
            Scan(out document, IDocument.FormatType.JPG);
        }

        public void Print(in IDocument document) {
            if(state == IDevice.State.on) {
                Printer.PowerOn();
                Printer.Print(document);
                Printer.PowerOff();
            }
        }

        public void ScanAndPrint() {
            IDocument scannedDocument;
            Scan(out scannedDocument);
            Print(scannedDocument);
        }
    }
}
