using System;

namespace AX9.MetaTool.Models
{
    public class InterruptModel
    {
        public InterruptModel() { }
        public InterruptModel(ushort val1)
        {
            this[0] = val1;
        }
        public InterruptModel(ushort val1, ushort val2): this(val1)
        {
            this[1] = val2;
        }


        public ushort this[int index]
        {
            get
            {
                if (index > 2 || index < 0) throw new IndexOutOfRangeException();
                return interruptNumber[index];
            }
            set
            {
                if (value >= 1024) throw new ArgumentException("Specify EnableInterrupts for a 10-bit unsigned integer.");
                interruptNumber[index] = value;
            }
        }


        private readonly ushort[] interruptNumber = { 1023, 1023 };
        private readonly KcFlagModel capability = new KcFlagModel { EntryNumber = 11u };


        public uint CalcFlag()
        {
            capability.FieldValue = (uint)(interruptNumber[0] | interruptNumber[1] << 10);
            return capability.Flag;
        }
    }
}
