using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading.Tasks;
using System.Linq;

namespace ExposureMachine.Classes
{
    public interface ICOM
    {
        public string[] AvailablePorts { get; init; }
        public string ConnectedPort { get; }
        public bool EstablishConnection(string comPort);
        public void WriteByte(byte data);
        public event EventHandler<decimal> GetData;
    }

   

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
                PortName = comPort,
                BaudRate = 9600,
                Parity = Parity.Even,
                WriteTimeout = 1000,
                ReadTimeout = 1000
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

        ~ValveSet()
        {
            _serialPort.Dispose();
        }
    }
}




