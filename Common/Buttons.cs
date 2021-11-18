using System;
using System.ComponentModel;

namespace ExposureMachine.Common
{
    [Flags]
    public enum Buttons
    {
        [Description("Юстировка ФШ")]
        AlignmentMask = 0b00000001,
        [Description("Фиксация ФШ")]
        FixingMask = 0b00000010,
        [Description("Фиксация рамки ФШ")]
        FixingFrame = 0b00000100,
        [Description("Фиксация подложки")]
        FixingSubstrate = 0b00001000,
        [Description("Столик шаровая опора")]
        BallSupport = 0b00010000,
        [Description("Подъём столика")]
        LiftingTable = 0b00100000,
        [Description("Фиксация столика")]
        FixingTable = 0b01000000,
        [Description("Зазор")]
        Gap = 0b10000000,
        [Description("Экспонирование")]
        Exposing = 0b100000000,
        [Description("Манипулятор")]
        Mouse = 0b1000000000,
    }
}
