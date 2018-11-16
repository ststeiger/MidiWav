using System;
using System.Collections.Generic;
using System.Text;

namespace Metis.Modules
{
    public enum OscillatorType
    {
        Sin,
        Square,
        Saw,
        Pulse, 
        WhiteNoise,  // float between 1 und -1
        DigitalNoise // 1 or 0
    }

    [ModuleType("Source")]
    public class Oscillator : Module
    {
        private OscillatorType type;
        private Random randomProvider;
        private double frequency = 1.0;
        private double phaseOffset = 0.0;
        private double outValue = 0.0;
        

        #region Properties
        
        public double PhaseOffset
        {
            get { return phaseOffset; }
            set { phaseOffset = value; }
        }

        public double Frequency
        {
            get { return frequency; }
            set { frequency = value; }
        }

        public OscillatorType Type
        {
            get { return type; }
            set 
            { 
                type = value;
        //        Name = type.ToString();
            }
        }
        #endregion

        public Oscillator(OscillatorType oscType)
        {
            Name = "Oscillator";
            Type = oscType;

            randomProvider = new Random();
            
            InputPort = new Port[2];
            InputPort[0] = new Port(this, 0, "f", PortType.Input);
            InputPort[1] = new Port(this, 1, "t0", PortType.Input);

            OutputPort = new Port[1];
            OutputPort[0] = new Port(this, 0, "Out", PortType.Output);
        }

        public Oscillator() : this(OscillatorType.Sin) { }
        
        
        public override void Update(double t)
        {
            // Get input port values 
            if (InputPort[0].TargetPort != null)
            {
                frequency = InputPort[0].SingleValue;
            }
         
            if (InputPort[0].TargetPort != null)
            {
                phaseOffset = InputPort[1].SingleValue;
            }
            

            switch (type)
            {
                case OscillatorType.Sin:
                    outValue = Math.Sin(2 * Math.PI * frequency * t + phaseOffset);
                    break;
                case OscillatorType.Saw:
                    double ta = t * frequency + phaseOffset;
                    outValue = 1 - 4 * Math.Abs(Math.Round(ta) - ta);
                    break;
                case OscillatorType.Square:
                    outValue = Math.Sign(Math.Sin(2 * Math.PI * frequency * t + phaseOffset));
                    break;
                case OscillatorType.Pulse:
                    outValue = (Math.Abs(Math.Sin(2 * Math.PI * frequency * t + phaseOffset)) < 1.0 - 10E-3) ? (0) : (1);
                    break;
                case OscillatorType.WhiteNoise:
                    outValue = 2 *(double)randomProvider.Next(int.MaxValue) / int.MaxValue - 1.0;
                    break;
                case OscillatorType.DigitalNoise:
                    outValue = randomProvider.Next(2);
                    break;
            }

            // Set output port values
            OutputPort[0].Value[0] = outValue;
            
        }
            
    }
}
