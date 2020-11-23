namespace Zadanie4 {
    public interface IDevice {
        enum State { on, off, standby };
        public void PowerOn() { SetState(State.on); }
        public void PowerOff() { SetState(State.off); }
        public void StandbyOn() { SetState(State.standby); }
        public void StandbyOff() { SetState(State.on); }
        State GetState();
        abstract protected void SetState(State state);
        int Counter { get; }  // zwraca liczbę charakteryzującą eksploatację urządzenia,
                              // np. liczbę uruchomień, liczbę wydrukow, liczbę skanów, ...
    }

    public interface IPrinter : IDevice {
        /// <summary>
        /// Dokument jest drukowany, jeśli urządzenie włączone. W przeciwnym przypadku nic się nie wykonuje
        /// </summary>
        /// <param name="document">obiekt typu IDocument, różny od `null`</param>
        public void Print(in IDocument document);
        public new void PowerOn() { SetState(State.on); }
        public new void PowerOff() { SetState(State.off); }
        public new void StandbyOn() { SetState(State.standby); }
        public new void StandbyOff() { SetState(State.on); }
        new State GetState();
         new void SetState(State state);
    }

    public interface IScanner : IDevice {
        // dokument jest skanowany, jeśli urządzenie włączone
        // w przeciwnym przypadku nic się dzieje
        public void Scan(out IDocument document, IDocument.FormatType formatType);
        public new void PowerOn() { SetState(State.on); }
        public new void PowerOff() { SetState(State.off); }
        public new void StandbyOn() { SetState(State.standby); }
        public new void StandbyOff() { SetState(State.on); }
        new State GetState();
         new void SetState(State state);

    }

}
