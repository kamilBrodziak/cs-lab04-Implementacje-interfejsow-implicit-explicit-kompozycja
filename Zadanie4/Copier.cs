using System;

namespace Zadanie4 {
    public class Copier : IPrinter, IScanner {
        public int PrintCounter { get; private set; } = 0;
        public int ScanCounter { get; private set; } = 0;
        public int Counter { get; private set; } = 0;
        private IPrinter printer;
        private IScanner scanner;

        private IDevice.State printerState = IDevice.State.off;
        private IDevice.State scannerState = IDevice.State.off;
        public int PrintInRowCounter { get; private set; } = 0;
        public int ScanInRowCounter { get; private set; } = 0;

        public Copier() {
            printer = this;
            scanner = this;
        }
        public IDevice.State GetState() {
            if(scanner.GetState() == printer.GetState()) {
                return scanner.GetState();
            }
            return IDevice.State.on;
        }

        IDevice.State IPrinter.GetState() {
            return printerState;
        }
        IDevice.State IScanner.GetState() {
            return scannerState;
        }
        void IPrinter.SetState(IDevice.State state) {
            printerState = state;
        }

        void IScanner.SetState(IDevice.State state) {
            scannerState = state;
        }
        void IDevice.SetState(IDevice.State state) {
            printer.SetState(state);
            scanner.SetState(state);
        }

        public void CopierPowerOn() {
            if(GetState() == IDevice.State.off) {
                Counter++;
                ((IDevice)this).PowerOn();
            }
        }
        public void CopierPowerOff() {
            if(GetState() != IDevice.State.off) {
                ((IDevice)this).PowerOff();
            }
        }
        public void CopierStandbyOn() {
            if(GetState() != IDevice.State.standby) {
                ((IDevice)this).StandbyOn();
            }
        }
        public void CopierStandbyOff() {
            if(GetState() == IDevice.State.standby) {
                ((IDevice)this).StandbyOff();
            }
        }

        public void Print(in IDocument document) {
            if(GetState() != IDevice.State.off) {
                if(scanner.GetState() == IDevice.State.on) scanner.StandbyOn();
                if(printer.GetState() != IDevice.State.on) {
                    printer.StandbyOff();
                } else if(PrintInRowCounter%3 == 0) {
                    printer.StandbyOn();
                    Console.WriteLine("Printer cooling down, wait...");
                    PrintInRowCounter = 0;
                    printer.StandbyOff();
                }
                PrintInRowCounter++;
                Console.WriteLine($"{DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")} Print: {document.GetFileName()}");
                PrintCounter++;
            }
        }

        public void Scan(out IDocument document, IDocument.FormatType formatType) {
            if(GetState() != IDevice.State.off) {
                if(printer.GetState() == IDevice.State.on) printer.StandbyOn();
                if(scanner.GetState() != IDevice.State.on) {
                    scanner.StandbyOff();
                } else if(ScanInRowCounter%2 == 0) {
                    scanner.StandbyOn();
                    Console.WriteLine("Scanner cooling down, wait...");
                    ScanInRowCounter = 0;
                    scanner.StandbyOff();
                }
                ScanInRowCounter++;
                switch(formatType) {
                    case IDocument.FormatType.JPG:
                        document = new ImageDocument($"ImageScan{ScanCounter}.jpg");
                        break;
                    case IDocument.FormatType.TXT:
                        document = new ImageDocument($"TextScan{ScanCounter}.txt");
                        break;
                    case IDocument.FormatType.PDF:
                    default:
                        document = new PDFDocument($"PDFScan{ScanCounter}.pdf");
                        break;
                }
                Console.WriteLine($"{DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")} Scan: {document.GetFileName()}");
                ScanCounter++;
            } else {
                document = null;
            }
        }

        public void Scan(out IDocument document) {
            Scan(out document, IDocument.FormatType.JPG);
        }

        public void ScanAndPrint() {
            IDocument scannedDocument;
            Scan(out scannedDocument);
            Print(scannedDocument);
        }
    }
}
