using System;

namespace ExposureMachine.Classes;

public interface ICOM
{
    public string[] AvailablePorts { get; init; }
    public string ConnectedPort { get; }
    public bool EstablishConnection(string comPort);
    public void WriteByte(byte data);
    public void WriteLine(string line);
    public event EventHandler<decimal> GetData;
}




