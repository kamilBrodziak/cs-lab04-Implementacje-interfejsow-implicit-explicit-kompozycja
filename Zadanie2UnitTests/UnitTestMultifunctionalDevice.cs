using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using Zadanie2;

namespace ver1UnitTests {

    public class ConsoleRedirectionToStringWriter : IDisposable {
        private StringWriter stringWriter;
        private TextWriter originalOutput;

        public ConsoleRedirectionToStringWriter() {
            stringWriter = new StringWriter();
            originalOutput = Console.Out;
            Console.SetOut(stringWriter);
        }

        public string GetOutput() {
            return stringWriter.ToString();
        }

        public void Dispose() {
            Console.SetOut(originalOutput);
            stringWriter.Dispose();
        }
    }


    [TestClass]
    public class UnitTestMultifunctionalDevice {
        [TestMethod]
        public void MultifunctionalDevice_GetState_StateOff() {
            var copier = new MultifunctionalDevice();
            copier.PowerOff();

            Assert.AreEqual(IDevice.State.off, copier.GetState());
        }

        [TestMethod]
        public void MultifunctionalDevice_GetState_StateOn() {
            var copier = new MultifunctionalDevice();
            copier.PowerOn();

            Assert.AreEqual(IDevice.State.on, copier.GetState());
        }


        // weryfikacja, czy po wywołaniu metody `Print` i włączonej kopiarce w napisie pojawia się słowo `Print`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_Print_DeviceOn() {
            var copier = new MultifunctionalDevice();
            copier.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using(var consoleOutput = new ConsoleRedirectionToStringWriter()) {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                copier.Print(in doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `Print` i wyłączonej kopiarce w napisie NIE pojawia się słowo `Print`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_Print_DeviceOff() {
            var copier = new MultifunctionalDevice();
            copier.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using(var consoleOutput = new ConsoleRedirectionToStringWriter()) {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                copier.Print(in doc1);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `Scan` i wyłączonej kopiarce w napisie NIE pojawia się słowo `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_Scan_DeviceOff() {
            var copier = new MultifunctionalDevice();
            copier.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using(var consoleOutput = new ConsoleRedirectionToStringWriter()) {
                IDocument doc1;
                copier.Scan(out doc1);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `Scan` i wyłączonej kopiarce w napisie pojawia się słowo `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_Scan_DeviceOn() {
            var copier = new MultifunctionalDevice();
            copier.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using(var consoleOutput = new ConsoleRedirectionToStringWriter()) {
                IDocument doc1;
                copier.Scan(out doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultifunctionalDevice_Fax_DeviceOn() {
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using(var consoleOutput = new ConsoleRedirectionToStringWriter()) {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                string to = "1234";
                multifunctionalDevice.SendFax(in doc1, to);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Fax"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultifunctionalDevice_Fax_DeviceOff() {
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using(var consoleOutput = new ConsoleRedirectionToStringWriter()) {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                string to = "1234";
                multifunctionalDevice.SendFax(in doc1, to);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy wywołanie metody `Scan` z parametrem określającym format dokumentu
        // zawiera odpowiednie rozszerzenie (`.jpg`, `.txt`, `.pdf`)
        [TestMethod]
        public void MultifunctionalDevice_Scan_FormatTypeDocument() {
            var copier = new MultifunctionalDevice();
            copier.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using(var consoleOutput = new ConsoleRedirectionToStringWriter()) {
                IDocument doc1;
                copier.Scan(out doc1, formatType: IDocument.FormatType.JPG);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".jpg"));

                copier.Scan(out doc1, formatType: IDocument.FormatType.TXT);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".txt"));

                copier.Scan(out doc1, formatType: IDocument.FormatType.PDF);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".pdf"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }


        // weryfikacja, czy po wywołaniu metody `ScanAndPrint` i wyłączonej kopiarce w napisie pojawiają się słowa `Print`
        // oraz `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_ScanAndPrint_DeviceOn() {
            var copier = new MultifunctionalDevice();
            copier.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using(var consoleOutput = new ConsoleRedirectionToStringWriter()) {
                copier.ScanAndPrint();
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `ScanAndPrint` i wyłączonej kopiarce w napisie NIE pojawia się słowo `Print`
        // ani słowo `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_ScanAndPrint_DeviceOff() {
            var copier = new MultifunctionalDevice();
            copier.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using(var consoleOutput = new ConsoleRedirectionToStringWriter()) {
                copier.ScanAndPrint();
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultifunctionalDevice_ScanAndFax_DeviceOff() {
            var copier = new MultifunctionalDevice();
            copier.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using(var consoleOutput = new ConsoleRedirectionToStringWriter()) {
                string to = "1234";
                copier.ScanAndFax(to);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Fax"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultifunctionalDevice_ScanAndFax_DeviceOn() {
            var copier = new MultifunctionalDevice();
            copier.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using(var consoleOutput = new ConsoleRedirectionToStringWriter()) {
                string to = "1234";
                copier.ScanAndFax(to);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Fax"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }


        [TestMethod]
        public void MultifunctionalDevice_PrintCounter() {
            var copier = new MultifunctionalDevice();
            copier.PowerOn();

            IDocument doc1 = new PDFDocument("aaa.pdf");
            copier.Print(in doc1);
            IDocument doc2 = new TextDocument("aaa.txt");
            copier.Print(in doc2);
            IDocument doc3 = new ImageDocument("aaa.jpg");
            copier.Print(in doc3);

            copier.PowerOff();
            copier.Print(in doc3);
            copier.Scan(out doc1);
            copier.PowerOn();

            copier.ScanAndPrint();
            copier.ScanAndPrint();

            // 5 wydruków, gdy urządzenie włączone
            Assert.AreEqual(5, copier.PrintCounter);
        }

        [TestMethod]
        public void MultifunctionalDevice_ScanCounter() {
            var copier = new MultifunctionalDevice();
            copier.PowerOn();

            IDocument doc1;
            copier.Scan(out doc1);
            IDocument doc2;
            copier.Scan(out doc2);

            IDocument doc3 = new ImageDocument("aaa.jpg");
            copier.Print(in doc3);

            copier.PowerOff();
            copier.Print(in doc3);
            copier.Scan(out doc1);
            copier.PowerOn();

            copier.ScanAndPrint();
            copier.ScanAndPrint();

            // 4 skany, gdy urządzenie włączone
            Assert.AreEqual(4, copier.ScanCounter);
        }

        [TestMethod]
        public void MultifunctionalDevice_FaxCounter() {
            var copier = new MultifunctionalDevice();
            copier.PowerOn();
            string to = "1234";
            IDocument doc1 = new PDFDocument("aaa.pdf");
            copier.SendFax(in doc1, to);
            IDocument doc2 = new TextDocument("aaa.txt");
            copier.SendFax(in doc2, to);
            IDocument doc3 = new ImageDocument("aaa.jpg");
            copier.SendFax(in doc3, to);

            copier.PowerOff();
            copier.SendFax(in doc3, to);
            copier.Scan(out doc1);
            copier.PowerOn();

            copier.ScanAndFax(to);
            copier.ScanAndFax(to);

            // 5 wydruków, gdy urządzenie włączone
            Assert.AreEqual(5, copier.FaxCounter);
        }

        [TestMethod]
        public void MultifunctionalDevice_PowerOnCounter() {
            var copier = new MultifunctionalDevice();
            copier.PowerOn();
            copier.PowerOn();
            copier.PowerOn();

            IDocument doc1;
            copier.Scan(out doc1);
            IDocument doc2;
            copier.Scan(out doc2);

            copier.PowerOff();
            copier.PowerOff();
            copier.PowerOff();
            copier.PowerOn();

            IDocument doc3 = new ImageDocument("aaa.jpg");
            copier.Print(in doc3);

            copier.PowerOff();
            copier.Print(in doc3);
            copier.Scan(out doc1);
            copier.PowerOn();

            copier.ScanAndPrint();
            copier.ScanAndPrint();

            // 3 włączenia
            Assert.AreEqual(3, copier.Counter);
        }
    }
}
