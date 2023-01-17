using System;
using System.IO.Ports;
using System.Linq;

namespace ExposureMachine.Classes;

public class ValveSet : ICOM
{
    public ValveSet(string com = "")
    {           
        AvailablePorts = SerialPort.GetPortNames();
        if (AvailablePorts.Length == 0)
        {
            throw new MyComException("Отсутствует COM-порт");
        }
        _ = AvailablePorts.Contains(com) ? EstablishConnection(com) : EstablishConnection(AvailablePorts[0]);
    }

    event EventHandler<decimal> ICOM.GetData
    {
        add
        {
            throw new NotImplementedException();
        }

        remove
        {
            throw new NotImplementedException();
        }
    }
    
    private SerialPort _serialPort;
    public bool EstablishConnection(string comPort)
    {
        
        _serialPort = new SerialPort
        {
            //PortName = comPort,
            //BaudRate = 9600,
            //Parity = Parity.Even,
            //WriteTimeout = 1000,
            //ReadTimeout = 1000

            PortName = comPort,
            BaudRate = 9600,
            Parity = Parity.None,
            DataBits = 8,
            StopBits = StopBits.One,
            WriteTimeout = 100,
            ReadTimeout = 100,
            ReceivedBytesThreshold = 1
        };
        _serialPort.Open();
        _serialPort.DiscardNull = true;
        return _serialPort.IsOpen;
    }
    public string[] AvailablePorts { get; init; }

    public string ConnectedPort { 
        get
        {
            return _serialPort?.PortName;
        } 
    }

    void ICOM.WriteByte(byte data)
    {   
        _serialPort.Write(new byte[] { data }, 0, 1);               
    }

    public void WriteLine(string line)
    {
        _serialPort.WriteLine(line);
    }

    ~ValveSet()
    {
        _serialPort.Dispose();
    }
}




