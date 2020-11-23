using System;

namespace Zadanie4 {
    class Program {
        static void Main() {
            var copier = new Copier();
            var printer = (IPrinter)copier;
            var scanner = (IScanner)copier;
            copier.CopierPowerOn();
            Console.WriteLine($"Device power {copier.GetState()}, Printer power {printer.GetState()}, Scanner power {scanner.GetState()}");
            copier.CopierStandbyOn();
            Console.WriteLine($"Device power {copier.GetState()}, Printer power {printer.GetState()}, Scanner power {scanner.GetState()}");
            IDocument doc1 = new PDFDocument("aaa.pdf");
            copier.Print(in doc1);
            Console.WriteLine($"Device power {copier.GetState()}, Printer power {printer.GetState()}, Scanner power {scanner.GetState()}");
            copier.Print(in doc1);
            copier.Print(in doc1);
            copier.Print(in doc1);
            IDocument doc2;
            copier.Scan(out doc2);
            Console.WriteLine($"Device power {copier.GetState()}, Printer power {printer.GetState()}, Scanner power {scanner.GetState()}");
            copier.Scan(out doc2);
            copier.ScanAndPrint();
            Console.WriteLine(copier.Counter);
            Console.WriteLine(copier.PrintCounter);
            Console.WriteLine(copier.ScanCounter);
            copier.CopierStandbyOn();
            Console.WriteLine($"Device power {copier.GetState()}, Printer power {printer.GetState()}, Scanner power {scanner.GetState()}");
            copier.CopierStandbyOff();
            Console.WriteLine($"Device power {copier.GetState()}, Printer power {printer.GetState()}, Scanner power {scanner.GetState()}");
            copier.CopierPowerOff();
            Console.WriteLine($"Device power {copier.GetState()}, Printer power {printer.GetState()}, Scanner power {scanner.GetState()}");

        }
    }
}
