namespace Zadanie3 {
    class Program {
        static void Main() {
            var xerox = new MultifunctionalDevice();
            xerox.PowerOn();
            IDocument doc1 = new PDFDocument("aaa.pdf");
            xerox.Print(in doc1);

            IDocument doc2;
            xerox.Scan(out doc2);

            xerox.ScanAndPrint();

            xerox.SendFax(doc1, "1234");
            xerox.ScanAndFax("1234");

            System.Console.WriteLine(xerox.Counter);
            System.Console.WriteLine(xerox.PrintCounter);
            System.Console.WriteLine(xerox.ScanCounter);
            System.Console.WriteLine(xerox.FaxCounter);
        }
    }
}
