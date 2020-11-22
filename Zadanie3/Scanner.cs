using System;
using System.Collections.Generic;
using System.Text;

namespace Zadanie3 {
    public class Scanner : IScanner {
        public int Counter { get; private set; } = 0;
        protected IDevice.State state = IDevice.State.off;
        public IDevice.State GetState() => state;

        public void PowerOff() {
            state = IDevice.State.off;
        }

        public void PowerOn() {
            state = IDevice.State.on;
        }

        public void Scan(out IDocument document, IDocument.FormatType formatType) {
            if(GetState() == IDevice.State.on) {
                switch(formatType) {
                    case IDocument.FormatType.JPG:
                        document = new ImageDocument($"ImageScan{Counter}.jpg");
                        break;
                    case IDocument.FormatType.TXT:
                        document = new ImageDocument($"TextScan{Counter}.txt");
                        break;
                    case IDocument.FormatType.PDF:
                    default:
                        document = new PDFDocument($"PDFScan{Counter}.pdf");
                        break;
                }
                Console.WriteLine($"{DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")} Scan: {document.GetFileName()}");
                Counter++;
            } else {
                document = null;
            }
        }

        public void Scan(out IDocument document) {
            Scan(out document, IDocument.FormatType.JPG);
        }
    }
}
