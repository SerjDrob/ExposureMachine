using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Exposure_Machine.Model;
using System.Diagnostics;
using System.ComponentModel;
using ExposureMachine.Classes;
namespace ExposureMachine.ViewModel
{
    public enum Buttons
    {
        [Description("Юстировка ФШ")]
        AlignmentMask,
        [Description("Фиксация ФШ")]
        FixingMask,
        [Description("Фиксация рамки ФШ")]
        FixingFrame,
        [Description("Фиксация подложки")]
        FixingSubstrate,
        [Description("Столик шаровая опора")]
        BallSupport,
        [Description("Подъём столика")]
        LiftingTable
    }
       
    class MainViewModel
    {
        public ICommand PushCmd { get; set; }
        internal MainViewModel()
        {
            PushCmd = new Command(args => PushTheButton(args));
            ((Command)PushCmd).CanExecuteDelegate = StopExec;
            _comPort = new ValveSet("COM9");
        }
        private ICOM _comPort;
        private void PushTheButton(object parameter)
        {
            switch (parameter)
            {
                case Buttons button:
                    switch (button)
                    {
                        case Buttons.AlignmentMask:
                            Trace.WriteLine($"{button.GetDescription()}");
                            break;
                        case Buttons.FixingMask:
                            _comPort.WriteByte(0x1);
                            Trace.WriteLine($"{button.GetDescription()}");
                            break;
                        case Buttons.FixingFrame:
                            Trace.WriteLine($"{button.GetDescription()}");
                            break;
                        case Buttons.FixingSubstrate:
                            Trace.WriteLine($"{button.GetDescription()}");
                            break;
                        case Buttons.BallSupport:
                            Trace.WriteLine($"{button.GetDescription()}");
                            break;
                        case Buttons.LiftingTable:
                            Trace.WriteLine($"{button.GetDescription()}");
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
        }
        private bool StopExec(object parameter)
        {
            switch (parameter)
            {
                case Buttons button:
                    switch (button)
                    {
                        case Buttons.AlignmentMask:
                            return false;
                            break;
                        case Buttons.FixingMask:
                            break;
                        case Buttons.FixingFrame:
                            break;
                        case Buttons.FixingSubstrate:
                            break;
                        case Buttons.BallSupport:
                            break;
                        case Buttons.LiftingTable:
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
            return true;
        }
    }
}
