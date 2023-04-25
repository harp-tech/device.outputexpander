using Bonsai;
using Bonsai.Harp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Xml.Serialization;

namespace Harp.OutputExpander
{
    /// <summary>
    /// Generates events and processes commands for the OutputExpander device connected
    /// at the specified serial port.
    /// </summary>
    [Combinator(MethodName = nameof(Generate))]
    [WorkflowElementCategory(ElementCategory.Source)]
    [Description("Generates events and processes commands for the OutputExpander device.")]
    public partial class Device : Bonsai.Harp.Device, INamedElement
    {
        /// <summary>
        /// Represents the unique identity class of the <see cref="OutputExpander"/> device.
        /// This field is constant.
        /// </summary>
        public const int WhoAmI = 1108;

        /// <summary>
        /// Initializes a new instance of the <see cref="Device"/> class.
        /// </summary>
        public Device() : base(WhoAmI) { }

        string INamedElement.Name => nameof(OutputExpander);

        /// <summary>
        /// Gets a read-only mapping from address to register type.
        /// </summary>
        public static new IReadOnlyDictionary<int, Type> RegisterMap { get; } = new Dictionary<int, Type>
            (Bonsai.Harp.Device.RegisterMap.ToDictionary(entry => entry.Key, entry => entry.Value))
        {
            { 32, typeof(AuxInPort) },
            { 33, typeof(AuxInEnableRisingEdge) },
            { 34, typeof(AuxInEnableFallingEdge) },
            { 35, typeof(OutputSet) },
            { 36, typeof(OutputClear) },
            { 37, typeof(OutputToggle) },
            { 38, typeof(OutputState) },
            { 39, typeof(PwmAndStimEnable) },
            { 40, typeof(PwmAndStimDisable) },
            { 41, typeof(PwmAndStimWrite) },
            { 42, typeof(Pwm0Frequency) },
            { 43, typeof(Pwm0DutyCycle) },
            { 44, typeof(Pwm0PulseCount) },
            { 45, typeof(Pwm0RealFrequency) },
            { 46, typeof(Pwm0RealDutyCycle) },
            { 47, typeof(Pwm0AcquisitionMode) },
            { 48, typeof(Pwm0TriggerSource) },
            { 49, typeof(Pwm0EventEnable) },
            { 50, typeof(Pwm1Frequency) },
            { 51, typeof(Pwm1DutyCycle) },
            { 52, typeof(Pwm1PulseCount) },
            { 53, typeof(Pwm1RealFrequency) },
            { 54, typeof(Pwm1RealDutyCycle) },
            { 55, typeof(Pwm1AcquisitionMode) },
            { 56, typeof(Pwm1TriggerSource) },
            { 57, typeof(Pwm1EventEnable) },
            { 58, typeof(Pwm2Frequency) },
            { 59, typeof(Pwm2DutyCycle) },
            { 60, typeof(Pwm2PulseCount) },
            { 61, typeof(Pwm2RealFrequency) },
            { 62, typeof(Pwm2RealDutyCycle) },
            { 63, typeof(Pwm2AcquisitionMode) },
            { 64, typeof(Pwm2TriggerSource) },
            { 65, typeof(Pwm2EventEnable) },
            { 66, typeof(PwmStart) },
            { 67, typeof(PwmStop) },
            { 68, typeof(PwmRiseEventEnable) },
            { 69, typeof(Stim0PulseOnTime) },
            { 70, typeof(Stim0PulseOffTime) },
            { 71, typeof(Stim0PulseCount) },
            { 72, typeof(Stim0AcquisitionMode) },
            { 73, typeof(Stim0TriggerSource) },
            { 74, typeof(StimStart) },
            { 75, typeof(StimStop) },
            { 76, typeof(OutputPulse) },
            { 77, typeof(Out0PulseWidth) },
            { 78, typeof(Out1PulseWidth) },
            { 79, typeof(Out2PulseWidth) },
            { 80, typeof(Out3PulseWidth) },
            { 81, typeof(Out4PulseWidth) },
            { 82, typeof(Out5PulseWidth) },
            { 83, typeof(Out6PulseWidth) },
            { 84, typeof(Out7PulseWidth) },
            { 85, typeof(Out8PulseWidth) },
            { 86, typeof(Out9PulseWidth) },
            { 87, typeof(ExpansionBoard) },
            { 90, typeof(Encoder) },
            { 91, typeof(EncoderSampling) },
            { 94, typeof(ServoPeriod) },
            { 95, typeof(Servo0PulseWidth) },
            { 96, typeof(Servo1PulseWidth) },
            { 97, typeof(Servo2PulseWidth) },
            { 100, typeof(OpticalFlow) }
        };
    }

    /// <summary>
    /// Represents an operator that groups the sequence of <see cref="OutputExpander"/>" messages by register type.
    /// </summary>
    [Description("Groups the sequence of OutputExpander messages by register type.")]
    public partial class GroupByRegister : Combinator<HarpMessage, IGroupedObservable<Type, HarpMessage>>
    {
        /// <summary>
        /// Groups an observable sequence of <see cref="OutputExpander"/> messages
        /// by register type.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of observable groups, each of which corresponds to a unique
        /// <see cref="OutputExpander"/> register.
        /// </returns>
        public override IObservable<IGroupedObservable<Type, HarpMessage>> Process(IObservable<HarpMessage> source)
        {
            return source.GroupBy(message => Device.RegisterMap[message.Address]);
        }
    }

    /// <summary>
    /// Represents an operator that filters register-specific messages
    /// reported by the <see cref="OutputExpander"/> device.
    /// </summary>
    /// <seealso cref="AuxInPort"/>
    /// <seealso cref="AuxInEnableRisingEdge"/>
    /// <seealso cref="AuxInEnableFallingEdge"/>
    /// <seealso cref="OutputSet"/>
    /// <seealso cref="OutputClear"/>
    /// <seealso cref="OutputToggle"/>
    /// <seealso cref="OutputState"/>
    /// <seealso cref="PwmAndStimEnable"/>
    /// <seealso cref="PwmAndStimDisable"/>
    /// <seealso cref="PwmAndStimWrite"/>
    /// <seealso cref="Pwm0Frequency"/>
    /// <seealso cref="Pwm0DutyCycle"/>
    /// <seealso cref="Pwm0PulseCount"/>
    /// <seealso cref="Pwm0RealFrequency"/>
    /// <seealso cref="Pwm0RealDutyCycle"/>
    /// <seealso cref="Pwm0AcquisitionMode"/>
    /// <seealso cref="Pwm0TriggerSource"/>
    /// <seealso cref="Pwm0EventEnable"/>
    /// <seealso cref="Pwm1Frequency"/>
    /// <seealso cref="Pwm1DutyCycle"/>
    /// <seealso cref="Pwm1PulseCount"/>
    /// <seealso cref="Pwm1RealFrequency"/>
    /// <seealso cref="Pwm1RealDutyCycle"/>
    /// <seealso cref="Pwm1AcquisitionMode"/>
    /// <seealso cref="Pwm1TriggerSource"/>
    /// <seealso cref="Pwm1EventEnable"/>
    /// <seealso cref="Pwm2Frequency"/>
    /// <seealso cref="Pwm2DutyCycle"/>
    /// <seealso cref="Pwm2PulseCount"/>
    /// <seealso cref="Pwm2RealFrequency"/>
    /// <seealso cref="Pwm2RealDutyCycle"/>
    /// <seealso cref="Pwm2AcquisitionMode"/>
    /// <seealso cref="Pwm2TriggerSource"/>
    /// <seealso cref="Pwm2EventEnable"/>
    /// <seealso cref="PwmStart"/>
    /// <seealso cref="PwmStop"/>
    /// <seealso cref="PwmRiseEventEnable"/>
    /// <seealso cref="Stim0PulseOnTime"/>
    /// <seealso cref="Stim0PulseOffTime"/>
    /// <seealso cref="Stim0PulseCount"/>
    /// <seealso cref="Stim0AcquisitionMode"/>
    /// <seealso cref="Stim0TriggerSource"/>
    /// <seealso cref="StimStart"/>
    /// <seealso cref="StimStop"/>
    /// <seealso cref="OutputPulse"/>
    /// <seealso cref="Out0PulseWidth"/>
    /// <seealso cref="Out1PulseWidth"/>
    /// <seealso cref="Out2PulseWidth"/>
    /// <seealso cref="Out3PulseWidth"/>
    /// <seealso cref="Out4PulseWidth"/>
    /// <seealso cref="Out5PulseWidth"/>
    /// <seealso cref="Out6PulseWidth"/>
    /// <seealso cref="Out7PulseWidth"/>
    /// <seealso cref="Out8PulseWidth"/>
    /// <seealso cref="Out9PulseWidth"/>
    /// <seealso cref="ExpansionBoard"/>
    /// <seealso cref="Encoder"/>
    /// <seealso cref="EncoderSampling"/>
    /// <seealso cref="ServoPeriod"/>
    /// <seealso cref="Servo0PulseWidth"/>
    /// <seealso cref="Servo1PulseWidth"/>
    /// <seealso cref="Servo2PulseWidth"/>
    /// <seealso cref="OpticalFlow"/>
    [XmlInclude(typeof(AuxInPort))]
    [XmlInclude(typeof(AuxInEnableRisingEdge))]
    [XmlInclude(typeof(AuxInEnableFallingEdge))]
    [XmlInclude(typeof(OutputSet))]
    [XmlInclude(typeof(OutputClear))]
    [XmlInclude(typeof(OutputToggle))]
    [XmlInclude(typeof(OutputState))]
    [XmlInclude(typeof(PwmAndStimEnable))]
    [XmlInclude(typeof(PwmAndStimDisable))]
    [XmlInclude(typeof(PwmAndStimWrite))]
    [XmlInclude(typeof(Pwm0Frequency))]
    [XmlInclude(typeof(Pwm0DutyCycle))]
    [XmlInclude(typeof(Pwm0PulseCount))]
    [XmlInclude(typeof(Pwm0RealFrequency))]
    [XmlInclude(typeof(Pwm0RealDutyCycle))]
    [XmlInclude(typeof(Pwm0AcquisitionMode))]
    [XmlInclude(typeof(Pwm0TriggerSource))]
    [XmlInclude(typeof(Pwm0EventEnable))]
    [XmlInclude(typeof(Pwm1Frequency))]
    [XmlInclude(typeof(Pwm1DutyCycle))]
    [XmlInclude(typeof(Pwm1PulseCount))]
    [XmlInclude(typeof(Pwm1RealFrequency))]
    [XmlInclude(typeof(Pwm1RealDutyCycle))]
    [XmlInclude(typeof(Pwm1AcquisitionMode))]
    [XmlInclude(typeof(Pwm1TriggerSource))]
    [XmlInclude(typeof(Pwm1EventEnable))]
    [XmlInclude(typeof(Pwm2Frequency))]
    [XmlInclude(typeof(Pwm2DutyCycle))]
    [XmlInclude(typeof(Pwm2PulseCount))]
    [XmlInclude(typeof(Pwm2RealFrequency))]
    [XmlInclude(typeof(Pwm2RealDutyCycle))]
    [XmlInclude(typeof(Pwm2AcquisitionMode))]
    [XmlInclude(typeof(Pwm2TriggerSource))]
    [XmlInclude(typeof(Pwm2EventEnable))]
    [XmlInclude(typeof(PwmStart))]
    [XmlInclude(typeof(PwmStop))]
    [XmlInclude(typeof(PwmRiseEventEnable))]
    [XmlInclude(typeof(Stim0PulseOnTime))]
    [XmlInclude(typeof(Stim0PulseOffTime))]
    [XmlInclude(typeof(Stim0PulseCount))]
    [XmlInclude(typeof(Stim0AcquisitionMode))]
    [XmlInclude(typeof(Stim0TriggerSource))]
    [XmlInclude(typeof(StimStart))]
    [XmlInclude(typeof(StimStop))]
    [XmlInclude(typeof(OutputPulse))]
    [XmlInclude(typeof(Out0PulseWidth))]
    [XmlInclude(typeof(Out1PulseWidth))]
    [XmlInclude(typeof(Out2PulseWidth))]
    [XmlInclude(typeof(Out3PulseWidth))]
    [XmlInclude(typeof(Out4PulseWidth))]
    [XmlInclude(typeof(Out5PulseWidth))]
    [XmlInclude(typeof(Out6PulseWidth))]
    [XmlInclude(typeof(Out7PulseWidth))]
    [XmlInclude(typeof(Out8PulseWidth))]
    [XmlInclude(typeof(Out9PulseWidth))]
    [XmlInclude(typeof(ExpansionBoard))]
    [XmlInclude(typeof(Encoder))]
    [XmlInclude(typeof(EncoderSampling))]
    [XmlInclude(typeof(ServoPeriod))]
    [XmlInclude(typeof(Servo0PulseWidth))]
    [XmlInclude(typeof(Servo1PulseWidth))]
    [XmlInclude(typeof(Servo2PulseWidth))]
    [XmlInclude(typeof(OpticalFlow))]
    [Description("Filters register-specific messages reported by the OutputExpander device.")]
    public class FilterMessage : FilterMessageBuilder, INamedElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilterMessage"/> class.
        /// </summary>
        public FilterMessage()
        {
            Register = new AuxInPort();
        }

        string INamedElement.Name
        {
            get => $"{nameof(OutputExpander)}.{GetElementDisplayName(Register)}";
        }
    }

    /// <summary>
    /// Represents an operator which filters and selects specific messages
    /// reported by the OutputExpander device.
    /// </summary>
    /// <seealso cref="AuxInPort"/>
    /// <seealso cref="AuxInEnableRisingEdge"/>
    /// <seealso cref="AuxInEnableFallingEdge"/>
    /// <seealso cref="OutputSet"/>
    /// <seealso cref="OutputClear"/>
    /// <seealso cref="OutputToggle"/>
    /// <seealso cref="OutputState"/>
    /// <seealso cref="PwmAndStimEnable"/>
    /// <seealso cref="PwmAndStimDisable"/>
    /// <seealso cref="PwmAndStimWrite"/>
    /// <seealso cref="Pwm0Frequency"/>
    /// <seealso cref="Pwm0DutyCycle"/>
    /// <seealso cref="Pwm0PulseCount"/>
    /// <seealso cref="Pwm0RealFrequency"/>
    /// <seealso cref="Pwm0RealDutyCycle"/>
    /// <seealso cref="Pwm0AcquisitionMode"/>
    /// <seealso cref="Pwm0TriggerSource"/>
    /// <seealso cref="Pwm0EventEnable"/>
    /// <seealso cref="Pwm1Frequency"/>
    /// <seealso cref="Pwm1DutyCycle"/>
    /// <seealso cref="Pwm1PulseCount"/>
    /// <seealso cref="Pwm1RealFrequency"/>
    /// <seealso cref="Pwm1RealDutyCycle"/>
    /// <seealso cref="Pwm1AcquisitionMode"/>
    /// <seealso cref="Pwm1TriggerSource"/>
    /// <seealso cref="Pwm1EventEnable"/>
    /// <seealso cref="Pwm2Frequency"/>
    /// <seealso cref="Pwm2DutyCycle"/>
    /// <seealso cref="Pwm2PulseCount"/>
    /// <seealso cref="Pwm2RealFrequency"/>
    /// <seealso cref="Pwm2RealDutyCycle"/>
    /// <seealso cref="Pwm2AcquisitionMode"/>
    /// <seealso cref="Pwm2TriggerSource"/>
    /// <seealso cref="Pwm2EventEnable"/>
    /// <seealso cref="PwmStart"/>
    /// <seealso cref="PwmStop"/>
    /// <seealso cref="PwmRiseEventEnable"/>
    /// <seealso cref="Stim0PulseOnTime"/>
    /// <seealso cref="Stim0PulseOffTime"/>
    /// <seealso cref="Stim0PulseCount"/>
    /// <seealso cref="Stim0AcquisitionMode"/>
    /// <seealso cref="Stim0TriggerSource"/>
    /// <seealso cref="StimStart"/>
    /// <seealso cref="StimStop"/>
    /// <seealso cref="OutputPulse"/>
    /// <seealso cref="Out0PulseWidth"/>
    /// <seealso cref="Out1PulseWidth"/>
    /// <seealso cref="Out2PulseWidth"/>
    /// <seealso cref="Out3PulseWidth"/>
    /// <seealso cref="Out4PulseWidth"/>
    /// <seealso cref="Out5PulseWidth"/>
    /// <seealso cref="Out6PulseWidth"/>
    /// <seealso cref="Out7PulseWidth"/>
    /// <seealso cref="Out8PulseWidth"/>
    /// <seealso cref="Out9PulseWidth"/>
    /// <seealso cref="ExpansionBoard"/>
    /// <seealso cref="Encoder"/>
    /// <seealso cref="EncoderSampling"/>
    /// <seealso cref="ServoPeriod"/>
    /// <seealso cref="Servo0PulseWidth"/>
    /// <seealso cref="Servo1PulseWidth"/>
    /// <seealso cref="Servo2PulseWidth"/>
    /// <seealso cref="OpticalFlow"/>
    [XmlInclude(typeof(AuxInPort))]
    [XmlInclude(typeof(AuxInEnableRisingEdge))]
    [XmlInclude(typeof(AuxInEnableFallingEdge))]
    [XmlInclude(typeof(OutputSet))]
    [XmlInclude(typeof(OutputClear))]
    [XmlInclude(typeof(OutputToggle))]
    [XmlInclude(typeof(OutputState))]
    [XmlInclude(typeof(PwmAndStimEnable))]
    [XmlInclude(typeof(PwmAndStimDisable))]
    [XmlInclude(typeof(PwmAndStimWrite))]
    [XmlInclude(typeof(Pwm0Frequency))]
    [XmlInclude(typeof(Pwm0DutyCycle))]
    [XmlInclude(typeof(Pwm0PulseCount))]
    [XmlInclude(typeof(Pwm0RealFrequency))]
    [XmlInclude(typeof(Pwm0RealDutyCycle))]
    [XmlInclude(typeof(Pwm0AcquisitionMode))]
    [XmlInclude(typeof(Pwm0TriggerSource))]
    [XmlInclude(typeof(Pwm0EventEnable))]
    [XmlInclude(typeof(Pwm1Frequency))]
    [XmlInclude(typeof(Pwm1DutyCycle))]
    [XmlInclude(typeof(Pwm1PulseCount))]
    [XmlInclude(typeof(Pwm1RealFrequency))]
    [XmlInclude(typeof(Pwm1RealDutyCycle))]
    [XmlInclude(typeof(Pwm1AcquisitionMode))]
    [XmlInclude(typeof(Pwm1TriggerSource))]
    [XmlInclude(typeof(Pwm1EventEnable))]
    [XmlInclude(typeof(Pwm2Frequency))]
    [XmlInclude(typeof(Pwm2DutyCycle))]
    [XmlInclude(typeof(Pwm2PulseCount))]
    [XmlInclude(typeof(Pwm2RealFrequency))]
    [XmlInclude(typeof(Pwm2RealDutyCycle))]
    [XmlInclude(typeof(Pwm2AcquisitionMode))]
    [XmlInclude(typeof(Pwm2TriggerSource))]
    [XmlInclude(typeof(Pwm2EventEnable))]
    [XmlInclude(typeof(PwmStart))]
    [XmlInclude(typeof(PwmStop))]
    [XmlInclude(typeof(PwmRiseEventEnable))]
    [XmlInclude(typeof(Stim0PulseOnTime))]
    [XmlInclude(typeof(Stim0PulseOffTime))]
    [XmlInclude(typeof(Stim0PulseCount))]
    [XmlInclude(typeof(Stim0AcquisitionMode))]
    [XmlInclude(typeof(Stim0TriggerSource))]
    [XmlInclude(typeof(StimStart))]
    [XmlInclude(typeof(StimStop))]
    [XmlInclude(typeof(OutputPulse))]
    [XmlInclude(typeof(Out0PulseWidth))]
    [XmlInclude(typeof(Out1PulseWidth))]
    [XmlInclude(typeof(Out2PulseWidth))]
    [XmlInclude(typeof(Out3PulseWidth))]
    [XmlInclude(typeof(Out4PulseWidth))]
    [XmlInclude(typeof(Out5PulseWidth))]
    [XmlInclude(typeof(Out6PulseWidth))]
    [XmlInclude(typeof(Out7PulseWidth))]
    [XmlInclude(typeof(Out8PulseWidth))]
    [XmlInclude(typeof(Out9PulseWidth))]
    [XmlInclude(typeof(ExpansionBoard))]
    [XmlInclude(typeof(Encoder))]
    [XmlInclude(typeof(EncoderSampling))]
    [XmlInclude(typeof(ServoPeriod))]
    [XmlInclude(typeof(Servo0PulseWidth))]
    [XmlInclude(typeof(Servo1PulseWidth))]
    [XmlInclude(typeof(Servo2PulseWidth))]
    [XmlInclude(typeof(OpticalFlow))]
    [XmlInclude(typeof(TimestampedAuxInPort))]
    [XmlInclude(typeof(TimestampedAuxInEnableRisingEdge))]
    [XmlInclude(typeof(TimestampedAuxInEnableFallingEdge))]
    [XmlInclude(typeof(TimestampedOutputSet))]
    [XmlInclude(typeof(TimestampedOutputClear))]
    [XmlInclude(typeof(TimestampedOutputToggle))]
    [XmlInclude(typeof(TimestampedOutputState))]
    [XmlInclude(typeof(TimestampedPwmAndStimEnable))]
    [XmlInclude(typeof(TimestampedPwmAndStimDisable))]
    [XmlInclude(typeof(TimestampedPwmAndStimWrite))]
    [XmlInclude(typeof(TimestampedPwm0Frequency))]
    [XmlInclude(typeof(TimestampedPwm0DutyCycle))]
    [XmlInclude(typeof(TimestampedPwm0PulseCount))]
    [XmlInclude(typeof(TimestampedPwm0RealFrequency))]
    [XmlInclude(typeof(TimestampedPwm0RealDutyCycle))]
    [XmlInclude(typeof(TimestampedPwm0AcquisitionMode))]
    [XmlInclude(typeof(TimestampedPwm0TriggerSource))]
    [XmlInclude(typeof(TimestampedPwm0EventEnable))]
    [XmlInclude(typeof(TimestampedPwm1Frequency))]
    [XmlInclude(typeof(TimestampedPwm1DutyCycle))]
    [XmlInclude(typeof(TimestampedPwm1PulseCount))]
    [XmlInclude(typeof(TimestampedPwm1RealFrequency))]
    [XmlInclude(typeof(TimestampedPwm1RealDutyCycle))]
    [XmlInclude(typeof(TimestampedPwm1AcquisitionMode))]
    [XmlInclude(typeof(TimestampedPwm1TriggerSource))]
    [XmlInclude(typeof(TimestampedPwm1EventEnable))]
    [XmlInclude(typeof(TimestampedPwm2Frequency))]
    [XmlInclude(typeof(TimestampedPwm2DutyCycle))]
    [XmlInclude(typeof(TimestampedPwm2PulseCount))]
    [XmlInclude(typeof(TimestampedPwm2RealFrequency))]
    [XmlInclude(typeof(TimestampedPwm2RealDutyCycle))]
    [XmlInclude(typeof(TimestampedPwm2AcquisitionMode))]
    [XmlInclude(typeof(TimestampedPwm2TriggerSource))]
    [XmlInclude(typeof(TimestampedPwm2EventEnable))]
    [XmlInclude(typeof(TimestampedPwmStart))]
    [XmlInclude(typeof(TimestampedPwmStop))]
    [XmlInclude(typeof(TimestampedPwmRiseEventEnable))]
    [XmlInclude(typeof(TimestampedStim0PulseOnTime))]
    [XmlInclude(typeof(TimestampedStim0PulseOffTime))]
    [XmlInclude(typeof(TimestampedStim0PulseCount))]
    [XmlInclude(typeof(TimestampedStim0AcquisitionMode))]
    [XmlInclude(typeof(TimestampedStim0TriggerSource))]
    [XmlInclude(typeof(TimestampedStimStart))]
    [XmlInclude(typeof(TimestampedStimStop))]
    [XmlInclude(typeof(TimestampedOutputPulse))]
    [XmlInclude(typeof(TimestampedOut0PulseWidth))]
    [XmlInclude(typeof(TimestampedOut1PulseWidth))]
    [XmlInclude(typeof(TimestampedOut2PulseWidth))]
    [XmlInclude(typeof(TimestampedOut3PulseWidth))]
    [XmlInclude(typeof(TimestampedOut4PulseWidth))]
    [XmlInclude(typeof(TimestampedOut5PulseWidth))]
    [XmlInclude(typeof(TimestampedOut6PulseWidth))]
    [XmlInclude(typeof(TimestampedOut7PulseWidth))]
    [XmlInclude(typeof(TimestampedOut8PulseWidth))]
    [XmlInclude(typeof(TimestampedOut9PulseWidth))]
    [XmlInclude(typeof(TimestampedExpansionBoard))]
    [XmlInclude(typeof(TimestampedEncoder))]
    [XmlInclude(typeof(TimestampedEncoderSampling))]
    [XmlInclude(typeof(TimestampedServoPeriod))]
    [XmlInclude(typeof(TimestampedServo0PulseWidth))]
    [XmlInclude(typeof(TimestampedServo1PulseWidth))]
    [XmlInclude(typeof(TimestampedServo2PulseWidth))]
    [XmlInclude(typeof(TimestampedOpticalFlow))]
    [Description("Filters and selects specific messages reported by the OutputExpander device.")]
    public partial class Parse : ParseBuilder, INamedElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Parse"/> class.
        /// </summary>
        public Parse()
        {
            Register = new AuxInPort();
        }

        string INamedElement.Name => $"{nameof(OutputExpander)}.{GetElementDisplayName(Register)}";
    }

    /// <summary>
    /// Represents an operator which formats a sequence of values as specific
    /// OutputExpander register messages.
    /// </summary>
    /// <seealso cref="AuxInPort"/>
    /// <seealso cref="AuxInEnableRisingEdge"/>
    /// <seealso cref="AuxInEnableFallingEdge"/>
    /// <seealso cref="OutputSet"/>
    /// <seealso cref="OutputClear"/>
    /// <seealso cref="OutputToggle"/>
    /// <seealso cref="OutputState"/>
    /// <seealso cref="PwmAndStimEnable"/>
    /// <seealso cref="PwmAndStimDisable"/>
    /// <seealso cref="PwmAndStimWrite"/>
    /// <seealso cref="Pwm0Frequency"/>
    /// <seealso cref="Pwm0DutyCycle"/>
    /// <seealso cref="Pwm0PulseCount"/>
    /// <seealso cref="Pwm0RealFrequency"/>
    /// <seealso cref="Pwm0RealDutyCycle"/>
    /// <seealso cref="Pwm0AcquisitionMode"/>
    /// <seealso cref="Pwm0TriggerSource"/>
    /// <seealso cref="Pwm0EventEnable"/>
    /// <seealso cref="Pwm1Frequency"/>
    /// <seealso cref="Pwm1DutyCycle"/>
    /// <seealso cref="Pwm1PulseCount"/>
    /// <seealso cref="Pwm1RealFrequency"/>
    /// <seealso cref="Pwm1RealDutyCycle"/>
    /// <seealso cref="Pwm1AcquisitionMode"/>
    /// <seealso cref="Pwm1TriggerSource"/>
    /// <seealso cref="Pwm1EventEnable"/>
    /// <seealso cref="Pwm2Frequency"/>
    /// <seealso cref="Pwm2DutyCycle"/>
    /// <seealso cref="Pwm2PulseCount"/>
    /// <seealso cref="Pwm2RealFrequency"/>
    /// <seealso cref="Pwm2RealDutyCycle"/>
    /// <seealso cref="Pwm2AcquisitionMode"/>
    /// <seealso cref="Pwm2TriggerSource"/>
    /// <seealso cref="Pwm2EventEnable"/>
    /// <seealso cref="PwmStart"/>
    /// <seealso cref="PwmStop"/>
    /// <seealso cref="PwmRiseEventEnable"/>
    /// <seealso cref="Stim0PulseOnTime"/>
    /// <seealso cref="Stim0PulseOffTime"/>
    /// <seealso cref="Stim0PulseCount"/>
    /// <seealso cref="Stim0AcquisitionMode"/>
    /// <seealso cref="Stim0TriggerSource"/>
    /// <seealso cref="StimStart"/>
    /// <seealso cref="StimStop"/>
    /// <seealso cref="OutputPulse"/>
    /// <seealso cref="Out0PulseWidth"/>
    /// <seealso cref="Out1PulseWidth"/>
    /// <seealso cref="Out2PulseWidth"/>
    /// <seealso cref="Out3PulseWidth"/>
    /// <seealso cref="Out4PulseWidth"/>
    /// <seealso cref="Out5PulseWidth"/>
    /// <seealso cref="Out6PulseWidth"/>
    /// <seealso cref="Out7PulseWidth"/>
    /// <seealso cref="Out8PulseWidth"/>
    /// <seealso cref="Out9PulseWidth"/>
    /// <seealso cref="ExpansionBoard"/>
    /// <seealso cref="Encoder"/>
    /// <seealso cref="EncoderSampling"/>
    /// <seealso cref="ServoPeriod"/>
    /// <seealso cref="Servo0PulseWidth"/>
    /// <seealso cref="Servo1PulseWidth"/>
    /// <seealso cref="Servo2PulseWidth"/>
    /// <seealso cref="OpticalFlow"/>
    [XmlInclude(typeof(AuxInPort))]
    [XmlInclude(typeof(AuxInEnableRisingEdge))]
    [XmlInclude(typeof(AuxInEnableFallingEdge))]
    [XmlInclude(typeof(OutputSet))]
    [XmlInclude(typeof(OutputClear))]
    [XmlInclude(typeof(OutputToggle))]
    [XmlInclude(typeof(OutputState))]
    [XmlInclude(typeof(PwmAndStimEnable))]
    [XmlInclude(typeof(PwmAndStimDisable))]
    [XmlInclude(typeof(PwmAndStimWrite))]
    [XmlInclude(typeof(Pwm0Frequency))]
    [XmlInclude(typeof(Pwm0DutyCycle))]
    [XmlInclude(typeof(Pwm0PulseCount))]
    [XmlInclude(typeof(Pwm0RealFrequency))]
    [XmlInclude(typeof(Pwm0RealDutyCycle))]
    [XmlInclude(typeof(Pwm0AcquisitionMode))]
    [XmlInclude(typeof(Pwm0TriggerSource))]
    [XmlInclude(typeof(Pwm0EventEnable))]
    [XmlInclude(typeof(Pwm1Frequency))]
    [XmlInclude(typeof(Pwm1DutyCycle))]
    [XmlInclude(typeof(Pwm1PulseCount))]
    [XmlInclude(typeof(Pwm1RealFrequency))]
    [XmlInclude(typeof(Pwm1RealDutyCycle))]
    [XmlInclude(typeof(Pwm1AcquisitionMode))]
    [XmlInclude(typeof(Pwm1TriggerSource))]
    [XmlInclude(typeof(Pwm1EventEnable))]
    [XmlInclude(typeof(Pwm2Frequency))]
    [XmlInclude(typeof(Pwm2DutyCycle))]
    [XmlInclude(typeof(Pwm2PulseCount))]
    [XmlInclude(typeof(Pwm2RealFrequency))]
    [XmlInclude(typeof(Pwm2RealDutyCycle))]
    [XmlInclude(typeof(Pwm2AcquisitionMode))]
    [XmlInclude(typeof(Pwm2TriggerSource))]
    [XmlInclude(typeof(Pwm2EventEnable))]
    [XmlInclude(typeof(PwmStart))]
    [XmlInclude(typeof(PwmStop))]
    [XmlInclude(typeof(PwmRiseEventEnable))]
    [XmlInclude(typeof(Stim0PulseOnTime))]
    [XmlInclude(typeof(Stim0PulseOffTime))]
    [XmlInclude(typeof(Stim0PulseCount))]
    [XmlInclude(typeof(Stim0AcquisitionMode))]
    [XmlInclude(typeof(Stim0TriggerSource))]
    [XmlInclude(typeof(StimStart))]
    [XmlInclude(typeof(StimStop))]
    [XmlInclude(typeof(OutputPulse))]
    [XmlInclude(typeof(Out0PulseWidth))]
    [XmlInclude(typeof(Out1PulseWidth))]
    [XmlInclude(typeof(Out2PulseWidth))]
    [XmlInclude(typeof(Out3PulseWidth))]
    [XmlInclude(typeof(Out4PulseWidth))]
    [XmlInclude(typeof(Out5PulseWidth))]
    [XmlInclude(typeof(Out6PulseWidth))]
    [XmlInclude(typeof(Out7PulseWidth))]
    [XmlInclude(typeof(Out8PulseWidth))]
    [XmlInclude(typeof(Out9PulseWidth))]
    [XmlInclude(typeof(ExpansionBoard))]
    [XmlInclude(typeof(Encoder))]
    [XmlInclude(typeof(EncoderSampling))]
    [XmlInclude(typeof(ServoPeriod))]
    [XmlInclude(typeof(Servo0PulseWidth))]
    [XmlInclude(typeof(Servo1PulseWidth))]
    [XmlInclude(typeof(Servo2PulseWidth))]
    [XmlInclude(typeof(OpticalFlow))]
    [Description("Formats a sequence of values as specific OutputExpander register messages.")]
    public partial class Format : FormatBuilder, INamedElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Format"/> class.
        /// </summary>
        public Format()
        {
            Register = new AuxInPort();
        }

        string INamedElement.Name => $"{nameof(OutputExpander)}.{GetElementDisplayName(Register)}";
    }

    /// <summary>
    /// Represents a register that reports the state of the auxiliary inputs.
    /// </summary>
    [Description("Reports the state of the auxiliary inputs.")]
    public partial class AuxInPort
    {
        /// <summary>
        /// Represents the address of the <see cref="AuxInPort"/> register. This field is constant.
        /// </summary>
        public const int Address = 32;

        /// <summary>
        /// Represents the payload type of the <see cref="AuxInPort"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="AuxInPort"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="AuxInPort"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static AuxiliaryInput GetPayload(HarpMessage message)
        {
            return (AuxiliaryInput)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="AuxInPort"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<AuxiliaryInput> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((AuxiliaryInput)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="AuxInPort"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="AuxInPort"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, AuxiliaryInput value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="AuxInPort"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="AuxInPort"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, AuxiliaryInput value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// AuxInPort register.
    /// </summary>
    /// <seealso cref="AuxInPort"/>
    [Description("Filters and selects timestamped messages from the AuxInPort register.")]
    public partial class TimestampedAuxInPort
    {
        /// <summary>
        /// Represents the address of the <see cref="AuxInPort"/> register. This field is constant.
        /// </summary>
        public const int Address = AuxInPort.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="AuxInPort"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<AuxiliaryInput> GetPayload(HarpMessage message)
        {
            return AuxInPort.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that enables rising edge detection on the auxiliary inputs.
    /// </summary>
    [Description("Enables rising edge detection on the auxiliary inputs.")]
    public partial class AuxInEnableRisingEdge
    {
        /// <summary>
        /// Represents the address of the <see cref="AuxInEnableRisingEdge"/> register. This field is constant.
        /// </summary>
        public const int Address = 33;

        /// <summary>
        /// Represents the payload type of the <see cref="AuxInEnableRisingEdge"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="AuxInEnableRisingEdge"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="AuxInEnableRisingEdge"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static AuxiliaryInput GetPayload(HarpMessage message)
        {
            return (AuxiliaryInput)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="AuxInEnableRisingEdge"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<AuxiliaryInput> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((AuxiliaryInput)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="AuxInEnableRisingEdge"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="AuxInEnableRisingEdge"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, AuxiliaryInput value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="AuxInEnableRisingEdge"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="AuxInEnableRisingEdge"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, AuxiliaryInput value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// AuxInEnableRisingEdge register.
    /// </summary>
    /// <seealso cref="AuxInEnableRisingEdge"/>
    [Description("Filters and selects timestamped messages from the AuxInEnableRisingEdge register.")]
    public partial class TimestampedAuxInEnableRisingEdge
    {
        /// <summary>
        /// Represents the address of the <see cref="AuxInEnableRisingEdge"/> register. This field is constant.
        /// </summary>
        public const int Address = AuxInEnableRisingEdge.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="AuxInEnableRisingEdge"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<AuxiliaryInput> GetPayload(HarpMessage message)
        {
            return AuxInEnableRisingEdge.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that enables falling edge detection on the auxiliary input port.
    /// </summary>
    [Description("Enables falling edge detection on the auxiliary input port.")]
    public partial class AuxInEnableFallingEdge
    {
        /// <summary>
        /// Represents the address of the <see cref="AuxInEnableFallingEdge"/> register. This field is constant.
        /// </summary>
        public const int Address = 34;

        /// <summary>
        /// Represents the payload type of the <see cref="AuxInEnableFallingEdge"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="AuxInEnableFallingEdge"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="AuxInEnableFallingEdge"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static AuxiliaryInput GetPayload(HarpMessage message)
        {
            return (AuxiliaryInput)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="AuxInEnableFallingEdge"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<AuxiliaryInput> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((AuxiliaryInput)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="AuxInEnableFallingEdge"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="AuxInEnableFallingEdge"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, AuxiliaryInput value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="AuxInEnableFallingEdge"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="AuxInEnableFallingEdge"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, AuxiliaryInput value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// AuxInEnableFallingEdge register.
    /// </summary>
    /// <seealso cref="AuxInEnableFallingEdge"/>
    [Description("Filters and selects timestamped messages from the AuxInEnableFallingEdge register.")]
    public partial class TimestampedAuxInEnableFallingEdge
    {
        /// <summary>
        /// Represents the address of the <see cref="AuxInEnableFallingEdge"/> register. This field is constant.
        /// </summary>
        public const int Address = AuxInEnableFallingEdge.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="AuxInEnableFallingEdge"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<AuxiliaryInput> GetPayload(HarpMessage message)
        {
            return AuxInEnableFallingEdge.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that set the specified digital output lines.
    /// </summary>
    [Description("Set the specified digital output lines.")]
    public partial class OutputSet
    {
        /// <summary>
        /// Represents the address of the <see cref="OutputSet"/> register. This field is constant.
        /// </summary>
        public const int Address = 35;

        /// <summary>
        /// Represents the payload type of the <see cref="OutputSet"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="OutputSet"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="OutputSet"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static DigitalOutputs GetPayload(HarpMessage message)
        {
            return (DigitalOutputs)message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="OutputSet"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalOutputs> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadUInt16();
            return Timestamped.Create((DigitalOutputs)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="OutputSet"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="OutputSet"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, DigitalOutputs value)
        {
            return HarpMessage.FromUInt16(Address, messageType, (ushort)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="OutputSet"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="OutputSet"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, DigitalOutputs value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, (ushort)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// OutputSet register.
    /// </summary>
    /// <seealso cref="OutputSet"/>
    [Description("Filters and selects timestamped messages from the OutputSet register.")]
    public partial class TimestampedOutputSet
    {
        /// <summary>
        /// Represents the address of the <see cref="OutputSet"/> register. This field is constant.
        /// </summary>
        public const int Address = OutputSet.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="OutputSet"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalOutputs> GetPayload(HarpMessage message)
        {
            return OutputSet.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that clear the specified digital output lines.
    /// </summary>
    [Description("Clear the specified digital output lines")]
    public partial class OutputClear
    {
        /// <summary>
        /// Represents the address of the <see cref="OutputClear"/> register. This field is constant.
        /// </summary>
        public const int Address = 36;

        /// <summary>
        /// Represents the payload type of the <see cref="OutputClear"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="OutputClear"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="OutputClear"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static DigitalOutputs GetPayload(HarpMessage message)
        {
            return (DigitalOutputs)message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="OutputClear"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalOutputs> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadUInt16();
            return Timestamped.Create((DigitalOutputs)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="OutputClear"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="OutputClear"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, DigitalOutputs value)
        {
            return HarpMessage.FromUInt16(Address, messageType, (ushort)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="OutputClear"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="OutputClear"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, DigitalOutputs value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, (ushort)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// OutputClear register.
    /// </summary>
    /// <seealso cref="OutputClear"/>
    [Description("Filters and selects timestamped messages from the OutputClear register.")]
    public partial class TimestampedOutputClear
    {
        /// <summary>
        /// Represents the address of the <see cref="OutputClear"/> register. This field is constant.
        /// </summary>
        public const int Address = OutputClear.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="OutputClear"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalOutputs> GetPayload(HarpMessage message)
        {
            return OutputClear.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that toggle the specified digital output lines.
    /// </summary>
    [Description("Toggle the specified digital output lines")]
    public partial class OutputToggle
    {
        /// <summary>
        /// Represents the address of the <see cref="OutputToggle"/> register. This field is constant.
        /// </summary>
        public const int Address = 37;

        /// <summary>
        /// Represents the payload type of the <see cref="OutputToggle"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="OutputToggle"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="OutputToggle"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static DigitalOutputs GetPayload(HarpMessage message)
        {
            return (DigitalOutputs)message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="OutputToggle"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalOutputs> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadUInt16();
            return Timestamped.Create((DigitalOutputs)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="OutputToggle"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="OutputToggle"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, DigitalOutputs value)
        {
            return HarpMessage.FromUInt16(Address, messageType, (ushort)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="OutputToggle"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="OutputToggle"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, DigitalOutputs value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, (ushort)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// OutputToggle register.
    /// </summary>
    /// <seealso cref="OutputToggle"/>
    [Description("Filters and selects timestamped messages from the OutputToggle register.")]
    public partial class TimestampedOutputToggle
    {
        /// <summary>
        /// Represents the address of the <see cref="OutputToggle"/> register. This field is constant.
        /// </summary>
        public const int Address = OutputToggle.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="OutputToggle"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalOutputs> GetPayload(HarpMessage message)
        {
            return OutputToggle.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that write the state of all digital output lines.
    /// </summary>
    [Description("Write the state of all digital output lines")]
    public partial class OutputState
    {
        /// <summary>
        /// Represents the address of the <see cref="OutputState"/> register. This field is constant.
        /// </summary>
        public const int Address = 38;

        /// <summary>
        /// Represents the payload type of the <see cref="OutputState"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="OutputState"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="OutputState"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static DigitalOutputs GetPayload(HarpMessage message)
        {
            return (DigitalOutputs)message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="OutputState"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalOutputs> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadUInt16();
            return Timestamped.Create((DigitalOutputs)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="OutputState"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="OutputState"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, DigitalOutputs value)
        {
            return HarpMessage.FromUInt16(Address, messageType, (ushort)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="OutputState"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="OutputState"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, DigitalOutputs value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, (ushort)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// OutputState register.
    /// </summary>
    /// <seealso cref="OutputState"/>
    [Description("Filters and selects timestamped messages from the OutputState register.")]
    public partial class TimestampedOutputState
    {
        /// <summary>
        /// Represents the address of the <see cref="OutputState"/> register. This field is constant.
        /// </summary>
        public const int Address = OutputState.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="OutputState"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalOutputs> GetPayload(HarpMessage message)
        {
            return OutputState.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that enables PWM and stimulation on the specified digital output lines.
    /// </summary>
    [Description("Enables PWM and stimulation on the specified digital output lines.")]
    public partial class PwmAndStimEnable
    {
        /// <summary>
        /// Represents the address of the <see cref="PwmAndStimEnable"/> register. This field is constant.
        /// </summary>
        public const int Address = 39;

        /// <summary>
        /// Represents the payload type of the <see cref="PwmAndStimEnable"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="PwmAndStimEnable"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="PwmAndStimEnable"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static PwmAndStimMapping GetPayload(HarpMessage message)
        {
            return (PwmAndStimMapping)message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="PwmAndStimEnable"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<PwmAndStimMapping> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadUInt16();
            return Timestamped.Create((PwmAndStimMapping)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="PwmAndStimEnable"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PwmAndStimEnable"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, PwmAndStimMapping value)
        {
            return HarpMessage.FromUInt16(Address, messageType, (ushort)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="PwmAndStimEnable"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PwmAndStimEnable"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, PwmAndStimMapping value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, (ushort)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// PwmAndStimEnable register.
    /// </summary>
    /// <seealso cref="PwmAndStimEnable"/>
    [Description("Filters and selects timestamped messages from the PwmAndStimEnable register.")]
    public partial class TimestampedPwmAndStimEnable
    {
        /// <summary>
        /// Represents the address of the <see cref="PwmAndStimEnable"/> register. This field is constant.
        /// </summary>
        public const int Address = PwmAndStimEnable.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="PwmAndStimEnable"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<PwmAndStimMapping> GetPayload(HarpMessage message)
        {
            return PwmAndStimEnable.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that disables PWM and stimulation on the specified digital output lines.
    /// </summary>
    [Description("Disables PWM and stimulation on the specified digital output lines.")]
    public partial class PwmAndStimDisable
    {
        /// <summary>
        /// Represents the address of the <see cref="PwmAndStimDisable"/> register. This field is constant.
        /// </summary>
        public const int Address = 40;

        /// <summary>
        /// Represents the payload type of the <see cref="PwmAndStimDisable"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="PwmAndStimDisable"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="PwmAndStimDisable"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static PwmAndStimMapping GetPayload(HarpMessage message)
        {
            return (PwmAndStimMapping)message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="PwmAndStimDisable"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<PwmAndStimMapping> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadUInt16();
            return Timestamped.Create((PwmAndStimMapping)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="PwmAndStimDisable"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PwmAndStimDisable"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, PwmAndStimMapping value)
        {
            return HarpMessage.FromUInt16(Address, messageType, (ushort)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="PwmAndStimDisable"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PwmAndStimDisable"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, PwmAndStimMapping value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, (ushort)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// PwmAndStimDisable register.
    /// </summary>
    /// <seealso cref="PwmAndStimDisable"/>
    [Description("Filters and selects timestamped messages from the PwmAndStimDisable register.")]
    public partial class TimestampedPwmAndStimDisable
    {
        /// <summary>
        /// Represents the address of the <see cref="PwmAndStimDisable"/> register. This field is constant.
        /// </summary>
        public const int Address = PwmAndStimDisable.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="PwmAndStimDisable"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<PwmAndStimMapping> GetPayload(HarpMessage message)
        {
            return PwmAndStimDisable.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that writes the mapping between PWM/stimulation and the specified digital output lines in a single command.
    /// </summary>
    [Description("Writes the mapping between PWM/stimulation and the specified digital output lines in a single command.")]
    public partial class PwmAndStimWrite
    {
        /// <summary>
        /// Represents the address of the <see cref="PwmAndStimWrite"/> register. This field is constant.
        /// </summary>
        public const int Address = 41;

        /// <summary>
        /// Represents the payload type of the <see cref="PwmAndStimWrite"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="PwmAndStimWrite"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="PwmAndStimWrite"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static PwmAndStimMapping GetPayload(HarpMessage message)
        {
            return (PwmAndStimMapping)message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="PwmAndStimWrite"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<PwmAndStimMapping> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadUInt16();
            return Timestamped.Create((PwmAndStimMapping)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="PwmAndStimWrite"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PwmAndStimWrite"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, PwmAndStimMapping value)
        {
            return HarpMessage.FromUInt16(Address, messageType, (ushort)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="PwmAndStimWrite"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PwmAndStimWrite"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, PwmAndStimMapping value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, (ushort)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// PwmAndStimWrite register.
    /// </summary>
    /// <seealso cref="PwmAndStimWrite"/>
    [Description("Filters and selects timestamped messages from the PwmAndStimWrite register.")]
    public partial class TimestampedPwmAndStimWrite
    {
        /// <summary>
        /// Represents the address of the <see cref="PwmAndStimWrite"/> register. This field is constant.
        /// </summary>
        public const int Address = PwmAndStimWrite.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="PwmAndStimWrite"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<PwmAndStimMapping> GetPayload(HarpMessage message)
        {
            return PwmAndStimWrite.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the frequency of PWM0 (Hz).
    /// </summary>
    [Description("Sets the frequency of PWM0 (Hz).")]
    public partial class Pwm0Frequency
    {
        /// <summary>
        /// Represents the address of the <see cref="Pwm0Frequency"/> register. This field is constant.
        /// </summary>
        public const int Address = 42;

        /// <summary>
        /// Represents the payload type of the <see cref="Pwm0Frequency"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.Float;

        /// <summary>
        /// Represents the length of the <see cref="Pwm0Frequency"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Pwm0Frequency"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static float GetPayload(HarpMessage message)
        {
            return message.GetPayloadSingle();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Pwm0Frequency"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<float> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadSingle();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Pwm0Frequency"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Pwm0Frequency"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, float value)
        {
            return HarpMessage.FromSingle(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Pwm0Frequency"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Pwm0Frequency"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, float value)
        {
            return HarpMessage.FromSingle(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Pwm0Frequency register.
    /// </summary>
    /// <seealso cref="Pwm0Frequency"/>
    [Description("Filters and selects timestamped messages from the Pwm0Frequency register.")]
    public partial class TimestampedPwm0Frequency
    {
        /// <summary>
        /// Represents the address of the <see cref="Pwm0Frequency"/> register. This field is constant.
        /// </summary>
        public const int Address = Pwm0Frequency.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Pwm0Frequency"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<float> GetPayload(HarpMessage message)
        {
            return Pwm0Frequency.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the duty cycle of PWM0 (%).
    /// </summary>
    [Description("Sets the duty cycle of PWM0 (%).")]
    public partial class Pwm0DutyCycle
    {
        /// <summary>
        /// Represents the address of the <see cref="Pwm0DutyCycle"/> register. This field is constant.
        /// </summary>
        public const int Address = 43;

        /// <summary>
        /// Represents the payload type of the <see cref="Pwm0DutyCycle"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.Float;

        /// <summary>
        /// Represents the length of the <see cref="Pwm0DutyCycle"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Pwm0DutyCycle"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static float GetPayload(HarpMessage message)
        {
            return message.GetPayloadSingle();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Pwm0DutyCycle"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<float> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadSingle();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Pwm0DutyCycle"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Pwm0DutyCycle"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, float value)
        {
            return HarpMessage.FromSingle(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Pwm0DutyCycle"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Pwm0DutyCycle"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, float value)
        {
            return HarpMessage.FromSingle(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Pwm0DutyCycle register.
    /// </summary>
    /// <seealso cref="Pwm0DutyCycle"/>
    [Description("Filters and selects timestamped messages from the Pwm0DutyCycle register.")]
    public partial class TimestampedPwm0DutyCycle
    {
        /// <summary>
        /// Represents the address of the <see cref="Pwm0DutyCycle"/> register. This field is constant.
        /// </summary>
        public const int Address = Pwm0DutyCycle.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Pwm0DutyCycle"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<float> GetPayload(HarpMessage message)
        {
            return Pwm0DutyCycle.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the number of pulses to generate for PWM0.
    /// </summary>
    [Description("Sets the number of pulses to generate for PWM0.")]
    public partial class Pwm0PulseCount
    {
        /// <summary>
        /// Represents the address of the <see cref="Pwm0PulseCount"/> register. This field is constant.
        /// </summary>
        public const int Address = 44;

        /// <summary>
        /// Represents the payload type of the <see cref="Pwm0PulseCount"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="Pwm0PulseCount"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Pwm0PulseCount"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Pwm0PulseCount"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Pwm0PulseCount"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Pwm0PulseCount"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Pwm0PulseCount"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Pwm0PulseCount"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Pwm0PulseCount register.
    /// </summary>
    /// <seealso cref="Pwm0PulseCount"/>
    [Description("Filters and selects timestamped messages from the Pwm0PulseCount register.")]
    public partial class TimestampedPwm0PulseCount
    {
        /// <summary>
        /// Represents the address of the <see cref="Pwm0PulseCount"/> register. This field is constant.
        /// </summary>
        public const int Address = Pwm0PulseCount.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Pwm0PulseCount"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return Pwm0PulseCount.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that reports the actual frequency to be generated from PWM0 (Hz).
    /// </summary>
    [Description("Reports the actual frequency to be generated from PWM0 (Hz).")]
    public partial class Pwm0RealFrequency
    {
        /// <summary>
        /// Represents the address of the <see cref="Pwm0RealFrequency"/> register. This field is constant.
        /// </summary>
        public const int Address = 45;

        /// <summary>
        /// Represents the payload type of the <see cref="Pwm0RealFrequency"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.Float;

        /// <summary>
        /// Represents the length of the <see cref="Pwm0RealFrequency"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Pwm0RealFrequency"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static float GetPayload(HarpMessage message)
        {
            return message.GetPayloadSingle();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Pwm0RealFrequency"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<float> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadSingle();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Pwm0RealFrequency"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Pwm0RealFrequency"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, float value)
        {
            return HarpMessage.FromSingle(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Pwm0RealFrequency"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Pwm0RealFrequency"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, float value)
        {
            return HarpMessage.FromSingle(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Pwm0RealFrequency register.
    /// </summary>
    /// <seealso cref="Pwm0RealFrequency"/>
    [Description("Filters and selects timestamped messages from the Pwm0RealFrequency register.")]
    public partial class TimestampedPwm0RealFrequency
    {
        /// <summary>
        /// Represents the address of the <see cref="Pwm0RealFrequency"/> register. This field is constant.
        /// </summary>
        public const int Address = Pwm0RealFrequency.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Pwm0RealFrequency"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<float> GetPayload(HarpMessage message)
        {
            return Pwm0RealFrequency.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that reports the actual duty cycle to be generated from PWM0 (%).
    /// </summary>
    [Description("Reports the actual duty cycle to be generated from PWM0 (%).")]
    public partial class Pwm0RealDutyCycle
    {
        /// <summary>
        /// Represents the address of the <see cref="Pwm0RealDutyCycle"/> register. This field is constant.
        /// </summary>
        public const int Address = 46;

        /// <summary>
        /// Represents the payload type of the <see cref="Pwm0RealDutyCycle"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.Float;

        /// <summary>
        /// Represents the length of the <see cref="Pwm0RealDutyCycle"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Pwm0RealDutyCycle"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static float GetPayload(HarpMessage message)
        {
            return message.GetPayloadSingle();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Pwm0RealDutyCycle"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<float> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadSingle();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Pwm0RealDutyCycle"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Pwm0RealDutyCycle"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, float value)
        {
            return HarpMessage.FromSingle(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Pwm0RealDutyCycle"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Pwm0RealDutyCycle"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, float value)
        {
            return HarpMessage.FromSingle(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Pwm0RealDutyCycle register.
    /// </summary>
    /// <seealso cref="Pwm0RealDutyCycle"/>
    [Description("Filters and selects timestamped messages from the Pwm0RealDutyCycle register.")]
    public partial class TimestampedPwm0RealDutyCycle
    {
        /// <summary>
        /// Represents the address of the <see cref="Pwm0RealDutyCycle"/> register. This field is constant.
        /// </summary>
        public const int Address = Pwm0RealDutyCycle.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Pwm0RealDutyCycle"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<float> GetPayload(HarpMessage message)
        {
            return Pwm0RealDutyCycle.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the acquisition mode of PWM0.
    /// </summary>
    [Description("Sets the acquisition mode of PWM0.")]
    public partial class Pwm0AcquisitionMode
    {
        /// <summary>
        /// Represents the address of the <see cref="Pwm0AcquisitionMode"/> register. This field is constant.
        /// </summary>
        public const int Address = 47;

        /// <summary>
        /// Represents the payload type of the <see cref="Pwm0AcquisitionMode"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Pwm0AcquisitionMode"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Pwm0AcquisitionMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static AcquisitionMode GetPayload(HarpMessage message)
        {
            return (AcquisitionMode)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Pwm0AcquisitionMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<AcquisitionMode> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((AcquisitionMode)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Pwm0AcquisitionMode"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Pwm0AcquisitionMode"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, AcquisitionMode value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Pwm0AcquisitionMode"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Pwm0AcquisitionMode"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, AcquisitionMode value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Pwm0AcquisitionMode register.
    /// </summary>
    /// <seealso cref="Pwm0AcquisitionMode"/>
    [Description("Filters and selects timestamped messages from the Pwm0AcquisitionMode register.")]
    public partial class TimestampedPwm0AcquisitionMode
    {
        /// <summary>
        /// Represents the address of the <see cref="Pwm0AcquisitionMode"/> register. This field is constant.
        /// </summary>
        public const int Address = Pwm0AcquisitionMode.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Pwm0AcquisitionMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<AcquisitionMode> GetPayload(HarpMessage message)
        {
            return Pwm0AcquisitionMode.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the trigger source of PWM0.
    /// </summary>
    [Description("Sets the trigger source of PWM0.")]
    public partial class Pwm0TriggerSource
    {
        /// <summary>
        /// Represents the address of the <see cref="Pwm0TriggerSource"/> register. This field is constant.
        /// </summary>
        public const int Address = 48;

        /// <summary>
        /// Represents the payload type of the <see cref="Pwm0TriggerSource"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Pwm0TriggerSource"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Pwm0TriggerSource"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static TriggerSources GetPayload(HarpMessage message)
        {
            return (TriggerSources)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Pwm0TriggerSource"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<TriggerSources> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((TriggerSources)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Pwm0TriggerSource"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Pwm0TriggerSource"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, TriggerSources value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Pwm0TriggerSource"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Pwm0TriggerSource"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, TriggerSources value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Pwm0TriggerSource register.
    /// </summary>
    /// <seealso cref="Pwm0TriggerSource"/>
    [Description("Filters and selects timestamped messages from the Pwm0TriggerSource register.")]
    public partial class TimestampedPwm0TriggerSource
    {
        /// <summary>
        /// Represents the address of the <see cref="Pwm0TriggerSource"/> register. This field is constant.
        /// </summary>
        public const int Address = Pwm0TriggerSource.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Pwm0TriggerSource"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<TriggerSources> GetPayload(HarpMessage message)
        {
            return Pwm0TriggerSource.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that enables the generation of events for PWM0.
    /// </summary>
    [Description("Enables the generation of events for PWM0.")]
    public partial class Pwm0EventEnable
    {
        /// <summary>
        /// Represents the address of the <see cref="Pwm0EventEnable"/> register. This field is constant.
        /// </summary>
        public const int Address = 49;

        /// <summary>
        /// Represents the payload type of the <see cref="Pwm0EventEnable"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Pwm0EventEnable"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Pwm0EventEnable"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static EnableFlag GetPayload(HarpMessage message)
        {
            return (EnableFlag)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Pwm0EventEnable"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<EnableFlag> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((EnableFlag)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Pwm0EventEnable"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Pwm0EventEnable"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, EnableFlag value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Pwm0EventEnable"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Pwm0EventEnable"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, EnableFlag value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Pwm0EventEnable register.
    /// </summary>
    /// <seealso cref="Pwm0EventEnable"/>
    [Description("Filters and selects timestamped messages from the Pwm0EventEnable register.")]
    public partial class TimestampedPwm0EventEnable
    {
        /// <summary>
        /// Represents the address of the <see cref="Pwm0EventEnable"/> register. This field is constant.
        /// </summary>
        public const int Address = Pwm0EventEnable.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Pwm0EventEnable"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<EnableFlag> GetPayload(HarpMessage message)
        {
            return Pwm0EventEnable.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the frequency of PWM1 (Hz).
    /// </summary>
    [Description("Sets the frequency of PWM1 (Hz).")]
    public partial class Pwm1Frequency
    {
        /// <summary>
        /// Represents the address of the <see cref="Pwm1Frequency"/> register. This field is constant.
        /// </summary>
        public const int Address = 50;

        /// <summary>
        /// Represents the payload type of the <see cref="Pwm1Frequency"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.Float;

        /// <summary>
        /// Represents the length of the <see cref="Pwm1Frequency"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Pwm1Frequency"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static float GetPayload(HarpMessage message)
        {
            return message.GetPayloadSingle();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Pwm1Frequency"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<float> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadSingle();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Pwm1Frequency"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Pwm1Frequency"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, float value)
        {
            return HarpMessage.FromSingle(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Pwm1Frequency"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Pwm1Frequency"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, float value)
        {
            return HarpMessage.FromSingle(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Pwm1Frequency register.
    /// </summary>
    /// <seealso cref="Pwm1Frequency"/>
    [Description("Filters and selects timestamped messages from the Pwm1Frequency register.")]
    public partial class TimestampedPwm1Frequency
    {
        /// <summary>
        /// Represents the address of the <see cref="Pwm1Frequency"/> register. This field is constant.
        /// </summary>
        public const int Address = Pwm1Frequency.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Pwm1Frequency"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<float> GetPayload(HarpMessage message)
        {
            return Pwm1Frequency.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the duty cycle of PWM1 (%).
    /// </summary>
    [Description("Sets the duty cycle of PWM1 (%).")]
    public partial class Pwm1DutyCycle
    {
        /// <summary>
        /// Represents the address of the <see cref="Pwm1DutyCycle"/> register. This field is constant.
        /// </summary>
        public const int Address = 51;

        /// <summary>
        /// Represents the payload type of the <see cref="Pwm1DutyCycle"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.Float;

        /// <summary>
        /// Represents the length of the <see cref="Pwm1DutyCycle"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Pwm1DutyCycle"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static float GetPayload(HarpMessage message)
        {
            return message.GetPayloadSingle();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Pwm1DutyCycle"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<float> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadSingle();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Pwm1DutyCycle"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Pwm1DutyCycle"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, float value)
        {
            return HarpMessage.FromSingle(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Pwm1DutyCycle"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Pwm1DutyCycle"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, float value)
        {
            return HarpMessage.FromSingle(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Pwm1DutyCycle register.
    /// </summary>
    /// <seealso cref="Pwm1DutyCycle"/>
    [Description("Filters and selects timestamped messages from the Pwm1DutyCycle register.")]
    public partial class TimestampedPwm1DutyCycle
    {
        /// <summary>
        /// Represents the address of the <see cref="Pwm1DutyCycle"/> register. This field is constant.
        /// </summary>
        public const int Address = Pwm1DutyCycle.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Pwm1DutyCycle"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<float> GetPayload(HarpMessage message)
        {
            return Pwm1DutyCycle.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the number of pulses to generate for PWM1.
    /// </summary>
    [Description("Sets the number of pulses to generate for PWM1.")]
    public partial class Pwm1PulseCount
    {
        /// <summary>
        /// Represents the address of the <see cref="Pwm1PulseCount"/> register. This field is constant.
        /// </summary>
        public const int Address = 52;

        /// <summary>
        /// Represents the payload type of the <see cref="Pwm1PulseCount"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="Pwm1PulseCount"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Pwm1PulseCount"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Pwm1PulseCount"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Pwm1PulseCount"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Pwm1PulseCount"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Pwm1PulseCount"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Pwm1PulseCount"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Pwm1PulseCount register.
    /// </summary>
    /// <seealso cref="Pwm1PulseCount"/>
    [Description("Filters and selects timestamped messages from the Pwm1PulseCount register.")]
    public partial class TimestampedPwm1PulseCount
    {
        /// <summary>
        /// Represents the address of the <see cref="Pwm1PulseCount"/> register. This field is constant.
        /// </summary>
        public const int Address = Pwm1PulseCount.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Pwm1PulseCount"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return Pwm1PulseCount.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that reports the actual frequency to be generated from PWM1 (Hz).
    /// </summary>
    [Description("Reports the actual frequency to be generated from PWM1 (Hz).")]
    public partial class Pwm1RealFrequency
    {
        /// <summary>
        /// Represents the address of the <see cref="Pwm1RealFrequency"/> register. This field is constant.
        /// </summary>
        public const int Address = 53;

        /// <summary>
        /// Represents the payload type of the <see cref="Pwm1RealFrequency"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.Float;

        /// <summary>
        /// Represents the length of the <see cref="Pwm1RealFrequency"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Pwm1RealFrequency"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static float GetPayload(HarpMessage message)
        {
            return message.GetPayloadSingle();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Pwm1RealFrequency"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<float> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadSingle();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Pwm1RealFrequency"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Pwm1RealFrequency"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, float value)
        {
            return HarpMessage.FromSingle(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Pwm1RealFrequency"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Pwm1RealFrequency"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, float value)
        {
            return HarpMessage.FromSingle(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Pwm1RealFrequency register.
    /// </summary>
    /// <seealso cref="Pwm1RealFrequency"/>
    [Description("Filters and selects timestamped messages from the Pwm1RealFrequency register.")]
    public partial class TimestampedPwm1RealFrequency
    {
        /// <summary>
        /// Represents the address of the <see cref="Pwm1RealFrequency"/> register. This field is constant.
        /// </summary>
        public const int Address = Pwm1RealFrequency.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Pwm1RealFrequency"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<float> GetPayload(HarpMessage message)
        {
            return Pwm1RealFrequency.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that reports the actual duty cycle to be generated from PWM1 (%).
    /// </summary>
    [Description("Reports the actual duty cycle to be generated from PWM1 (%).")]
    public partial class Pwm1RealDutyCycle
    {
        /// <summary>
        /// Represents the address of the <see cref="Pwm1RealDutyCycle"/> register. This field is constant.
        /// </summary>
        public const int Address = 54;

        /// <summary>
        /// Represents the payload type of the <see cref="Pwm1RealDutyCycle"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.Float;

        /// <summary>
        /// Represents the length of the <see cref="Pwm1RealDutyCycle"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Pwm1RealDutyCycle"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static float GetPayload(HarpMessage message)
        {
            return message.GetPayloadSingle();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Pwm1RealDutyCycle"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<float> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadSingle();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Pwm1RealDutyCycle"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Pwm1RealDutyCycle"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, float value)
        {
            return HarpMessage.FromSingle(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Pwm1RealDutyCycle"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Pwm1RealDutyCycle"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, float value)
        {
            return HarpMessage.FromSingle(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Pwm1RealDutyCycle register.
    /// </summary>
    /// <seealso cref="Pwm1RealDutyCycle"/>
    [Description("Filters and selects timestamped messages from the Pwm1RealDutyCycle register.")]
    public partial class TimestampedPwm1RealDutyCycle
    {
        /// <summary>
        /// Represents the address of the <see cref="Pwm1RealDutyCycle"/> register. This field is constant.
        /// </summary>
        public const int Address = Pwm1RealDutyCycle.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Pwm1RealDutyCycle"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<float> GetPayload(HarpMessage message)
        {
            return Pwm1RealDutyCycle.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the acquisition mode of PWM1.
    /// </summary>
    [Description("Sets the acquisition mode of PWM1.")]
    public partial class Pwm1AcquisitionMode
    {
        /// <summary>
        /// Represents the address of the <see cref="Pwm1AcquisitionMode"/> register. This field is constant.
        /// </summary>
        public const int Address = 55;

        /// <summary>
        /// Represents the payload type of the <see cref="Pwm1AcquisitionMode"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Pwm1AcquisitionMode"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Pwm1AcquisitionMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static AcquisitionMode GetPayload(HarpMessage message)
        {
            return (AcquisitionMode)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Pwm1AcquisitionMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<AcquisitionMode> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((AcquisitionMode)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Pwm1AcquisitionMode"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Pwm1AcquisitionMode"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, AcquisitionMode value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Pwm1AcquisitionMode"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Pwm1AcquisitionMode"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, AcquisitionMode value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Pwm1AcquisitionMode register.
    /// </summary>
    /// <seealso cref="Pwm1AcquisitionMode"/>
    [Description("Filters and selects timestamped messages from the Pwm1AcquisitionMode register.")]
    public partial class TimestampedPwm1AcquisitionMode
    {
        /// <summary>
        /// Represents the address of the <see cref="Pwm1AcquisitionMode"/> register. This field is constant.
        /// </summary>
        public const int Address = Pwm1AcquisitionMode.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Pwm1AcquisitionMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<AcquisitionMode> GetPayload(HarpMessage message)
        {
            return Pwm1AcquisitionMode.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the trigger source of PWM1.
    /// </summary>
    [Description("Sets the trigger source of PWM1.")]
    public partial class Pwm1TriggerSource
    {
        /// <summary>
        /// Represents the address of the <see cref="Pwm1TriggerSource"/> register. This field is constant.
        /// </summary>
        public const int Address = 56;

        /// <summary>
        /// Represents the payload type of the <see cref="Pwm1TriggerSource"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Pwm1TriggerSource"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Pwm1TriggerSource"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static TriggerSources GetPayload(HarpMessage message)
        {
            return (TriggerSources)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Pwm1TriggerSource"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<TriggerSources> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((TriggerSources)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Pwm1TriggerSource"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Pwm1TriggerSource"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, TriggerSources value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Pwm1TriggerSource"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Pwm1TriggerSource"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, TriggerSources value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Pwm1TriggerSource register.
    /// </summary>
    /// <seealso cref="Pwm1TriggerSource"/>
    [Description("Filters and selects timestamped messages from the Pwm1TriggerSource register.")]
    public partial class TimestampedPwm1TriggerSource
    {
        /// <summary>
        /// Represents the address of the <see cref="Pwm1TriggerSource"/> register. This field is constant.
        /// </summary>
        public const int Address = Pwm1TriggerSource.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Pwm1TriggerSource"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<TriggerSources> GetPayload(HarpMessage message)
        {
            return Pwm1TriggerSource.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that enables the generation of events for PWM1.
    /// </summary>
    [Description("Enables the generation of events for PWM1.")]
    public partial class Pwm1EventEnable
    {
        /// <summary>
        /// Represents the address of the <see cref="Pwm1EventEnable"/> register. This field is constant.
        /// </summary>
        public const int Address = 57;

        /// <summary>
        /// Represents the payload type of the <see cref="Pwm1EventEnable"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Pwm1EventEnable"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Pwm1EventEnable"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static EnableFlag GetPayload(HarpMessage message)
        {
            return (EnableFlag)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Pwm1EventEnable"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<EnableFlag> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((EnableFlag)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Pwm1EventEnable"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Pwm1EventEnable"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, EnableFlag value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Pwm1EventEnable"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Pwm1EventEnable"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, EnableFlag value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Pwm1EventEnable register.
    /// </summary>
    /// <seealso cref="Pwm1EventEnable"/>
    [Description("Filters and selects timestamped messages from the Pwm1EventEnable register.")]
    public partial class TimestampedPwm1EventEnable
    {
        /// <summary>
        /// Represents the address of the <see cref="Pwm1EventEnable"/> register. This field is constant.
        /// </summary>
        public const int Address = Pwm1EventEnable.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Pwm1EventEnable"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<EnableFlag> GetPayload(HarpMessage message)
        {
            return Pwm1EventEnable.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the frequency of PWM2 (Hz).
    /// </summary>
    [Description("Sets the frequency of PWM2 (Hz).")]
    public partial class Pwm2Frequency
    {
        /// <summary>
        /// Represents the address of the <see cref="Pwm2Frequency"/> register. This field is constant.
        /// </summary>
        public const int Address = 58;

        /// <summary>
        /// Represents the payload type of the <see cref="Pwm2Frequency"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.Float;

        /// <summary>
        /// Represents the length of the <see cref="Pwm2Frequency"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Pwm2Frequency"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static float GetPayload(HarpMessage message)
        {
            return message.GetPayloadSingle();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Pwm2Frequency"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<float> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadSingle();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Pwm2Frequency"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Pwm2Frequency"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, float value)
        {
            return HarpMessage.FromSingle(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Pwm2Frequency"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Pwm2Frequency"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, float value)
        {
            return HarpMessage.FromSingle(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Pwm2Frequency register.
    /// </summary>
    /// <seealso cref="Pwm2Frequency"/>
    [Description("Filters and selects timestamped messages from the Pwm2Frequency register.")]
    public partial class TimestampedPwm2Frequency
    {
        /// <summary>
        /// Represents the address of the <see cref="Pwm2Frequency"/> register. This field is constant.
        /// </summary>
        public const int Address = Pwm2Frequency.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Pwm2Frequency"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<float> GetPayload(HarpMessage message)
        {
            return Pwm2Frequency.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the duty cycle of PWM2 (%).
    /// </summary>
    [Description("Sets the duty cycle of PWM2 (%).")]
    public partial class Pwm2DutyCycle
    {
        /// <summary>
        /// Represents the address of the <see cref="Pwm2DutyCycle"/> register. This field is constant.
        /// </summary>
        public const int Address = 59;

        /// <summary>
        /// Represents the payload type of the <see cref="Pwm2DutyCycle"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.Float;

        /// <summary>
        /// Represents the length of the <see cref="Pwm2DutyCycle"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Pwm2DutyCycle"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static float GetPayload(HarpMessage message)
        {
            return message.GetPayloadSingle();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Pwm2DutyCycle"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<float> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadSingle();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Pwm2DutyCycle"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Pwm2DutyCycle"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, float value)
        {
            return HarpMessage.FromSingle(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Pwm2DutyCycle"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Pwm2DutyCycle"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, float value)
        {
            return HarpMessage.FromSingle(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Pwm2DutyCycle register.
    /// </summary>
    /// <seealso cref="Pwm2DutyCycle"/>
    [Description("Filters and selects timestamped messages from the Pwm2DutyCycle register.")]
    public partial class TimestampedPwm2DutyCycle
    {
        /// <summary>
        /// Represents the address of the <see cref="Pwm2DutyCycle"/> register. This field is constant.
        /// </summary>
        public const int Address = Pwm2DutyCycle.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Pwm2DutyCycle"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<float> GetPayload(HarpMessage message)
        {
            return Pwm2DutyCycle.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the number of pulses to generate for PWM2.
    /// </summary>
    [Description("Sets the number of pulses to generate for PWM2.")]
    public partial class Pwm2PulseCount
    {
        /// <summary>
        /// Represents the address of the <see cref="Pwm2PulseCount"/> register. This field is constant.
        /// </summary>
        public const int Address = 60;

        /// <summary>
        /// Represents the payload type of the <see cref="Pwm2PulseCount"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="Pwm2PulseCount"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Pwm2PulseCount"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Pwm2PulseCount"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Pwm2PulseCount"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Pwm2PulseCount"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Pwm2PulseCount"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Pwm2PulseCount"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Pwm2PulseCount register.
    /// </summary>
    /// <seealso cref="Pwm2PulseCount"/>
    [Description("Filters and selects timestamped messages from the Pwm2PulseCount register.")]
    public partial class TimestampedPwm2PulseCount
    {
        /// <summary>
        /// Represents the address of the <see cref="Pwm2PulseCount"/> register. This field is constant.
        /// </summary>
        public const int Address = Pwm2PulseCount.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Pwm2PulseCount"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return Pwm2PulseCount.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that reports the actual frequency to be generated from PWM2 (Hz).
    /// </summary>
    [Description("Reports the actual frequency to be generated from PWM2 (Hz).")]
    public partial class Pwm2RealFrequency
    {
        /// <summary>
        /// Represents the address of the <see cref="Pwm2RealFrequency"/> register. This field is constant.
        /// </summary>
        public const int Address = 61;

        /// <summary>
        /// Represents the payload type of the <see cref="Pwm2RealFrequency"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.Float;

        /// <summary>
        /// Represents the length of the <see cref="Pwm2RealFrequency"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Pwm2RealFrequency"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static float GetPayload(HarpMessage message)
        {
            return message.GetPayloadSingle();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Pwm2RealFrequency"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<float> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadSingle();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Pwm2RealFrequency"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Pwm2RealFrequency"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, float value)
        {
            return HarpMessage.FromSingle(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Pwm2RealFrequency"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Pwm2RealFrequency"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, float value)
        {
            return HarpMessage.FromSingle(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Pwm2RealFrequency register.
    /// </summary>
    /// <seealso cref="Pwm2RealFrequency"/>
    [Description("Filters and selects timestamped messages from the Pwm2RealFrequency register.")]
    public partial class TimestampedPwm2RealFrequency
    {
        /// <summary>
        /// Represents the address of the <see cref="Pwm2RealFrequency"/> register. This field is constant.
        /// </summary>
        public const int Address = Pwm2RealFrequency.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Pwm2RealFrequency"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<float> GetPayload(HarpMessage message)
        {
            return Pwm2RealFrequency.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that reports the actual duty cycle to be generated from PWM2 (%).
    /// </summary>
    [Description("Reports the actual duty cycle to be generated from PWM2 (%).")]
    public partial class Pwm2RealDutyCycle
    {
        /// <summary>
        /// Represents the address of the <see cref="Pwm2RealDutyCycle"/> register. This field is constant.
        /// </summary>
        public const int Address = 62;

        /// <summary>
        /// Represents the payload type of the <see cref="Pwm2RealDutyCycle"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.Float;

        /// <summary>
        /// Represents the length of the <see cref="Pwm2RealDutyCycle"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Pwm2RealDutyCycle"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static float GetPayload(HarpMessage message)
        {
            return message.GetPayloadSingle();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Pwm2RealDutyCycle"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<float> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadSingle();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Pwm2RealDutyCycle"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Pwm2RealDutyCycle"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, float value)
        {
            return HarpMessage.FromSingle(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Pwm2RealDutyCycle"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Pwm2RealDutyCycle"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, float value)
        {
            return HarpMessage.FromSingle(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Pwm2RealDutyCycle register.
    /// </summary>
    /// <seealso cref="Pwm2RealDutyCycle"/>
    [Description("Filters and selects timestamped messages from the Pwm2RealDutyCycle register.")]
    public partial class TimestampedPwm2RealDutyCycle
    {
        /// <summary>
        /// Represents the address of the <see cref="Pwm2RealDutyCycle"/> register. This field is constant.
        /// </summary>
        public const int Address = Pwm2RealDutyCycle.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Pwm2RealDutyCycle"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<float> GetPayload(HarpMessage message)
        {
            return Pwm2RealDutyCycle.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the acquisition mode of PWM2.
    /// </summary>
    [Description("Sets the acquisition mode of PWM2.")]
    public partial class Pwm2AcquisitionMode
    {
        /// <summary>
        /// Represents the address of the <see cref="Pwm2AcquisitionMode"/> register. This field is constant.
        /// </summary>
        public const int Address = 63;

        /// <summary>
        /// Represents the payload type of the <see cref="Pwm2AcquisitionMode"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Pwm2AcquisitionMode"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Pwm2AcquisitionMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static AcquisitionMode GetPayload(HarpMessage message)
        {
            return (AcquisitionMode)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Pwm2AcquisitionMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<AcquisitionMode> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((AcquisitionMode)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Pwm2AcquisitionMode"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Pwm2AcquisitionMode"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, AcquisitionMode value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Pwm2AcquisitionMode"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Pwm2AcquisitionMode"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, AcquisitionMode value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Pwm2AcquisitionMode register.
    /// </summary>
    /// <seealso cref="Pwm2AcquisitionMode"/>
    [Description("Filters and selects timestamped messages from the Pwm2AcquisitionMode register.")]
    public partial class TimestampedPwm2AcquisitionMode
    {
        /// <summary>
        /// Represents the address of the <see cref="Pwm2AcquisitionMode"/> register. This field is constant.
        /// </summary>
        public const int Address = Pwm2AcquisitionMode.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Pwm2AcquisitionMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<AcquisitionMode> GetPayload(HarpMessage message)
        {
            return Pwm2AcquisitionMode.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the trigger source of PWM2.
    /// </summary>
    [Description("Sets the trigger source of PWM2.")]
    public partial class Pwm2TriggerSource
    {
        /// <summary>
        /// Represents the address of the <see cref="Pwm2TriggerSource"/> register. This field is constant.
        /// </summary>
        public const int Address = 64;

        /// <summary>
        /// Represents the payload type of the <see cref="Pwm2TriggerSource"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Pwm2TriggerSource"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Pwm2TriggerSource"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static TriggerSources GetPayload(HarpMessage message)
        {
            return (TriggerSources)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Pwm2TriggerSource"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<TriggerSources> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((TriggerSources)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Pwm2TriggerSource"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Pwm2TriggerSource"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, TriggerSources value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Pwm2TriggerSource"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Pwm2TriggerSource"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, TriggerSources value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Pwm2TriggerSource register.
    /// </summary>
    /// <seealso cref="Pwm2TriggerSource"/>
    [Description("Filters and selects timestamped messages from the Pwm2TriggerSource register.")]
    public partial class TimestampedPwm2TriggerSource
    {
        /// <summary>
        /// Represents the address of the <see cref="Pwm2TriggerSource"/> register. This field is constant.
        /// </summary>
        public const int Address = Pwm2TriggerSource.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Pwm2TriggerSource"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<TriggerSources> GetPayload(HarpMessage message)
        {
            return Pwm2TriggerSource.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that enables the generation of events for PWM2.
    /// </summary>
    [Description("Enables the generation of events for PWM2.")]
    public partial class Pwm2EventEnable
    {
        /// <summary>
        /// Represents the address of the <see cref="Pwm2EventEnable"/> register. This field is constant.
        /// </summary>
        public const int Address = 65;

        /// <summary>
        /// Represents the payload type of the <see cref="Pwm2EventEnable"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Pwm2EventEnable"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Pwm2EventEnable"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static EnableFlag GetPayload(HarpMessage message)
        {
            return (EnableFlag)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Pwm2EventEnable"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<EnableFlag> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((EnableFlag)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Pwm2EventEnable"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Pwm2EventEnable"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, EnableFlag value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Pwm2EventEnable"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Pwm2EventEnable"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, EnableFlag value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Pwm2EventEnable register.
    /// </summary>
    /// <seealso cref="Pwm2EventEnable"/>
    [Description("Filters and selects timestamped messages from the Pwm2EventEnable register.")]
    public partial class TimestampedPwm2EventEnable
    {
        /// <summary>
        /// Represents the address of the <see cref="Pwm2EventEnable"/> register. This field is constant.
        /// </summary>
        public const int Address = Pwm2EventEnable.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Pwm2EventEnable"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<EnableFlag> GetPayload(HarpMessage message)
        {
            return Pwm2EventEnable.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that starts the a PWM on the specified channels. An event will be generated if the start was triggered by an auxiliary input.
    /// </summary>
    [Description("Starts the a PWM on the specified channels. An event will be generated if the start was triggered by an auxiliary input.")]
    public partial class PwmStart
    {
        /// <summary>
        /// Represents the address of the <see cref="PwmStart"/> register. This field is constant.
        /// </summary>
        public const int Address = 66;

        /// <summary>
        /// Represents the payload type of the <see cref="PwmStart"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="PwmStart"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="PwmStart"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static Pwm GetPayload(HarpMessage message)
        {
            return (Pwm)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="PwmStart"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<Pwm> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((Pwm)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="PwmStart"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PwmStart"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, Pwm value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="PwmStart"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PwmStart"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, Pwm value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// PwmStart register.
    /// </summary>
    /// <seealso cref="PwmStart"/>
    [Description("Filters and selects timestamped messages from the PwmStart register.")]
    public partial class TimestampedPwmStart
    {
        /// <summary>
        /// Represents the address of the <see cref="PwmStart"/> register. This field is constant.
        /// </summary>
        public const int Address = PwmStart.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="PwmStart"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<Pwm> GetPayload(HarpMessage message)
        {
            return PwmStart.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that stops the a PWM on the specified channels.
    /// </summary>
    [Description("Stops the a PWM on the specified channels.")]
    public partial class PwmStop
    {
        /// <summary>
        /// Represents the address of the <see cref="PwmStop"/> register. This field is constant.
        /// </summary>
        public const int Address = 67;

        /// <summary>
        /// Represents the payload type of the <see cref="PwmStop"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="PwmStop"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="PwmStop"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static Pwm GetPayload(HarpMessage message)
        {
            return (Pwm)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="PwmStop"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<Pwm> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((Pwm)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="PwmStop"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PwmStop"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, Pwm value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="PwmStop"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PwmStop"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, Pwm value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// PwmStop register.
    /// </summary>
    /// <seealso cref="PwmStop"/>
    [Description("Filters and selects timestamped messages from the PwmStop register.")]
    public partial class TimestampedPwmStop
    {
        /// <summary>
        /// Represents the address of the <see cref="PwmStop"/> register. This field is constant.
        /// </summary>
        public const int Address = PwmStop.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="PwmStop"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<Pwm> GetPayload(HarpMessage message)
        {
            return PwmStop.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that enables the generation of an event on every rising edge of the PWM line.
    /// </summary>
    [Description("Enables the generation of an event on every rising edge of the PWM line.")]
    public partial class PwmRiseEventEnable
    {
        /// <summary>
        /// Represents the address of the <see cref="PwmRiseEventEnable"/> register. This field is constant.
        /// </summary>
        public const int Address = 68;

        /// <summary>
        /// Represents the payload type of the <see cref="PwmRiseEventEnable"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="PwmRiseEventEnable"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="PwmRiseEventEnable"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static Pwm GetPayload(HarpMessage message)
        {
            return (Pwm)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="PwmRiseEventEnable"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<Pwm> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((Pwm)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="PwmRiseEventEnable"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PwmRiseEventEnable"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, Pwm value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="PwmRiseEventEnable"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="PwmRiseEventEnable"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, Pwm value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// PwmRiseEventEnable register.
    /// </summary>
    /// <seealso cref="PwmRiseEventEnable"/>
    [Description("Filters and selects timestamped messages from the PwmRiseEventEnable register.")]
    public partial class TimestampedPwmRiseEventEnable
    {
        /// <summary>
        /// Represents the address of the <see cref="PwmRiseEventEnable"/> register. This field is constant.
        /// </summary>
        public const int Address = PwmRiseEventEnable.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="PwmRiseEventEnable"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<Pwm> GetPayload(HarpMessage message)
        {
            return PwmRiseEventEnable.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the duration (us) each pulse is on for.
    /// </summary>
    [Description("Sets the duration (us) each pulse is on for.")]
    public partial class Stim0PulseOnTime
    {
        /// <summary>
        /// Represents the address of the <see cref="Stim0PulseOnTime"/> register. This field is constant.
        /// </summary>
        public const int Address = 69;

        /// <summary>
        /// Represents the payload type of the <see cref="Stim0PulseOnTime"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="Stim0PulseOnTime"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Stim0PulseOnTime"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Stim0PulseOnTime"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Stim0PulseOnTime"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Stim0PulseOnTime"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Stim0PulseOnTime"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Stim0PulseOnTime"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Stim0PulseOnTime register.
    /// </summary>
    /// <seealso cref="Stim0PulseOnTime"/>
    [Description("Filters and selects timestamped messages from the Stim0PulseOnTime register.")]
    public partial class TimestampedStim0PulseOnTime
    {
        /// <summary>
        /// Represents the address of the <see cref="Stim0PulseOnTime"/> register. This field is constant.
        /// </summary>
        public const int Address = Stim0PulseOnTime.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Stim0PulseOnTime"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return Stim0PulseOnTime.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the duration (us) each pulse is off for.
    /// </summary>
    [Description("Sets the duration (us) each pulse is off for.")]
    public partial class Stim0PulseOffTime
    {
        /// <summary>
        /// Represents the address of the <see cref="Stim0PulseOffTime"/> register. This field is constant.
        /// </summary>
        public const int Address = 70;

        /// <summary>
        /// Represents the payload type of the <see cref="Stim0PulseOffTime"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="Stim0PulseOffTime"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Stim0PulseOffTime"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Stim0PulseOffTime"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Stim0PulseOffTime"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Stim0PulseOffTime"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Stim0PulseOffTime"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Stim0PulseOffTime"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Stim0PulseOffTime register.
    /// </summary>
    /// <seealso cref="Stim0PulseOffTime"/>
    [Description("Filters and selects timestamped messages from the Stim0PulseOffTime register.")]
    public partial class TimestampedStim0PulseOffTime
    {
        /// <summary>
        /// Represents the address of the <see cref="Stim0PulseOffTime"/> register. This field is constant.
        /// </summary>
        public const int Address = Stim0PulseOffTime.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Stim0PulseOffTime"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return Stim0PulseOffTime.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the number of pulses to be generate when triggered.
    /// </summary>
    [Description("Sets the number of pulses to be generate when triggered.")]
    public partial class Stim0PulseCount
    {
        /// <summary>
        /// Represents the address of the <see cref="Stim0PulseCount"/> register. This field is constant.
        /// </summary>
        public const int Address = 71;

        /// <summary>
        /// Represents the payload type of the <see cref="Stim0PulseCount"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="Stim0PulseCount"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Stim0PulseCount"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Stim0PulseCount"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Stim0PulseCount"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Stim0PulseCount"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Stim0PulseCount"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Stim0PulseCount"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Stim0PulseCount register.
    /// </summary>
    /// <seealso cref="Stim0PulseCount"/>
    [Description("Filters and selects timestamped messages from the Stim0PulseCount register.")]
    public partial class TimestampedStim0PulseCount
    {
        /// <summary>
        /// Represents the address of the <see cref="Stim0PulseCount"/> register. This field is constant.
        /// </summary>
        public const int Address = Stim0PulseCount.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Stim0PulseCount"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return Stim0PulseCount.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the acquisition mode of Stim0.
    /// </summary>
    [Description("Sets the acquisition mode of Stim0.")]
    public partial class Stim0AcquisitionMode
    {
        /// <summary>
        /// Represents the address of the <see cref="Stim0AcquisitionMode"/> register. This field is constant.
        /// </summary>
        public const int Address = 72;

        /// <summary>
        /// Represents the payload type of the <see cref="Stim0AcquisitionMode"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Stim0AcquisitionMode"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Stim0AcquisitionMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static AcquisitionMode GetPayload(HarpMessage message)
        {
            return (AcquisitionMode)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Stim0AcquisitionMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<AcquisitionMode> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((AcquisitionMode)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Stim0AcquisitionMode"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Stim0AcquisitionMode"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, AcquisitionMode value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Stim0AcquisitionMode"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Stim0AcquisitionMode"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, AcquisitionMode value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Stim0AcquisitionMode register.
    /// </summary>
    /// <seealso cref="Stim0AcquisitionMode"/>
    [Description("Filters and selects timestamped messages from the Stim0AcquisitionMode register.")]
    public partial class TimestampedStim0AcquisitionMode
    {
        /// <summary>
        /// Represents the address of the <see cref="Stim0AcquisitionMode"/> register. This field is constant.
        /// </summary>
        public const int Address = Stim0AcquisitionMode.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Stim0AcquisitionMode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<AcquisitionMode> GetPayload(HarpMessage message)
        {
            return Stim0AcquisitionMode.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the trigger source of Stim0.
    /// </summary>
    [Description("Sets the trigger source of Stim0.")]
    public partial class Stim0TriggerSource
    {
        /// <summary>
        /// Represents the address of the <see cref="Stim0TriggerSource"/> register. This field is constant.
        /// </summary>
        public const int Address = 73;

        /// <summary>
        /// Represents the payload type of the <see cref="Stim0TriggerSource"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Stim0TriggerSource"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Stim0TriggerSource"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static TriggerSources GetPayload(HarpMessage message)
        {
            return (TriggerSources)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Stim0TriggerSource"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<TriggerSources> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((TriggerSources)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Stim0TriggerSource"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Stim0TriggerSource"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, TriggerSources value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Stim0TriggerSource"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Stim0TriggerSource"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, TriggerSources value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Stim0TriggerSource register.
    /// </summary>
    /// <seealso cref="Stim0TriggerSource"/>
    [Description("Filters and selects timestamped messages from the Stim0TriggerSource register.")]
    public partial class TimestampedStim0TriggerSource
    {
        /// <summary>
        /// Represents the address of the <see cref="Stim0TriggerSource"/> register. This field is constant.
        /// </summary>
        public const int Address = Stim0TriggerSource.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Stim0TriggerSource"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<TriggerSources> GetPayload(HarpMessage message)
        {
            return Stim0TriggerSource.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that starts a target Stim protocol.
    /// </summary>
    [Description("Starts a target Stim protocol.")]
    public partial class StimStart
    {
        /// <summary>
        /// Represents the address of the <see cref="StimStart"/> register. This field is constant.
        /// </summary>
        public const int Address = 74;

        /// <summary>
        /// Represents the payload type of the <see cref="StimStart"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="StimStart"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="StimStart"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static Stim GetPayload(HarpMessage message)
        {
            return (Stim)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="StimStart"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<Stim> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((Stim)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="StimStart"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="StimStart"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, Stim value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="StimStart"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="StimStart"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, Stim value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// StimStart register.
    /// </summary>
    /// <seealso cref="StimStart"/>
    [Description("Filters and selects timestamped messages from the StimStart register.")]
    public partial class TimestampedStimStart
    {
        /// <summary>
        /// Represents the address of the <see cref="StimStart"/> register. This field is constant.
        /// </summary>
        public const int Address = StimStart.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="StimStart"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<Stim> GetPayload(HarpMessage message)
        {
            return StimStart.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that stops the target Stim protocol.
    /// </summary>
    [Description("Stops the target Stim protocol.")]
    public partial class StimStop
    {
        /// <summary>
        /// Represents the address of the <see cref="StimStop"/> register. This field is constant.
        /// </summary>
        public const int Address = 75;

        /// <summary>
        /// Represents the payload type of the <see cref="StimStop"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="StimStop"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="StimStop"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static Stim GetPayload(HarpMessage message)
        {
            return (Stim)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="StimStop"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<Stim> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((Stim)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="StimStop"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="StimStop"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, Stim value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="StimStop"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="StimStop"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, Stim value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// StimStop register.
    /// </summary>
    /// <seealso cref="StimStop"/>
    [Description("Filters and selects timestamped messages from the StimStop register.")]
    public partial class TimestampedStimStop
    {
        /// <summary>
        /// Represents the address of the <see cref="StimStop"/> register. This field is constant.
        /// </summary>
        public const int Address = StimStop.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="StimStop"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<Stim> GetPayload(HarpMessage message)
        {
            return StimStop.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that enables pulse generation on the specified digital output line.
    /// </summary>
    [Description("Enables pulse generation on the specified digital output line.")]
    public partial class OutputPulse
    {
        /// <summary>
        /// Represents the address of the <see cref="OutputPulse"/> register. This field is constant.
        /// </summary>
        public const int Address = 76;

        /// <summary>
        /// Represents the payload type of the <see cref="OutputPulse"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="OutputPulse"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="OutputPulse"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static DigitalOutputs GetPayload(HarpMessage message)
        {
            return (DigitalOutputs)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="OutputPulse"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalOutputs> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((DigitalOutputs)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="OutputPulse"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="OutputPulse"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, DigitalOutputs value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="OutputPulse"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="OutputPulse"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, DigitalOutputs value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// OutputPulse register.
    /// </summary>
    /// <seealso cref="OutputPulse"/>
    [Description("Filters and selects timestamped messages from the OutputPulse register.")]
    public partial class TimestampedOutputPulse
    {
        /// <summary>
        /// Represents the address of the <see cref="OutputPulse"/> register. This field is constant.
        /// </summary>
        public const int Address = OutputPulse.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="OutputPulse"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalOutputs> GetPayload(HarpMessage message)
        {
            return OutputPulse.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the duration (ms) of the pulse to be generated on the specified digital output line.
    /// </summary>
    [Description("Sets the duration (ms) of the pulse to be generated on the specified digital output line.")]
    public partial class Out0PulseWidth
    {
        /// <summary>
        /// Represents the address of the <see cref="Out0PulseWidth"/> register. This field is constant.
        /// </summary>
        public const int Address = 77;

        /// <summary>
        /// Represents the payload type of the <see cref="Out0PulseWidth"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="Out0PulseWidth"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Out0PulseWidth"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Out0PulseWidth"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Out0PulseWidth"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Out0PulseWidth"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Out0PulseWidth"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Out0PulseWidth"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Out0PulseWidth register.
    /// </summary>
    /// <seealso cref="Out0PulseWidth"/>
    [Description("Filters and selects timestamped messages from the Out0PulseWidth register.")]
    public partial class TimestampedOut0PulseWidth
    {
        /// <summary>
        /// Represents the address of the <see cref="Out0PulseWidth"/> register. This field is constant.
        /// </summary>
        public const int Address = Out0PulseWidth.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Out0PulseWidth"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return Out0PulseWidth.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the duration (ms) of the pulse to be generated on the specified digital output line.
    /// </summary>
    [Description("Sets the duration (ms) of the pulse to be generated on the specified digital output line.")]
    public partial class Out1PulseWidth
    {
        /// <summary>
        /// Represents the address of the <see cref="Out1PulseWidth"/> register. This field is constant.
        /// </summary>
        public const int Address = 78;

        /// <summary>
        /// Represents the payload type of the <see cref="Out1PulseWidth"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="Out1PulseWidth"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Out1PulseWidth"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Out1PulseWidth"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Out1PulseWidth"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Out1PulseWidth"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Out1PulseWidth"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Out1PulseWidth"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Out1PulseWidth register.
    /// </summary>
    /// <seealso cref="Out1PulseWidth"/>
    [Description("Filters and selects timestamped messages from the Out1PulseWidth register.")]
    public partial class TimestampedOut1PulseWidth
    {
        /// <summary>
        /// Represents the address of the <see cref="Out1PulseWidth"/> register. This field is constant.
        /// </summary>
        public const int Address = Out1PulseWidth.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Out1PulseWidth"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return Out1PulseWidth.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the duration (ms) of the pulse to be generated on the specified digital output line.
    /// </summary>
    [Description("Sets the duration (ms) of the pulse to be generated on the specified digital output line.")]
    public partial class Out2PulseWidth
    {
        /// <summary>
        /// Represents the address of the <see cref="Out2PulseWidth"/> register. This field is constant.
        /// </summary>
        public const int Address = 79;

        /// <summary>
        /// Represents the payload type of the <see cref="Out2PulseWidth"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="Out2PulseWidth"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Out2PulseWidth"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Out2PulseWidth"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Out2PulseWidth"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Out2PulseWidth"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Out2PulseWidth"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Out2PulseWidth"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Out2PulseWidth register.
    /// </summary>
    /// <seealso cref="Out2PulseWidth"/>
    [Description("Filters and selects timestamped messages from the Out2PulseWidth register.")]
    public partial class TimestampedOut2PulseWidth
    {
        /// <summary>
        /// Represents the address of the <see cref="Out2PulseWidth"/> register. This field is constant.
        /// </summary>
        public const int Address = Out2PulseWidth.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Out2PulseWidth"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return Out2PulseWidth.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the duration (ms) of the pulse to be generated on the specified digital output line.
    /// </summary>
    [Description("Sets the duration (ms) of the pulse to be generated on the specified digital output line.")]
    public partial class Out3PulseWidth
    {
        /// <summary>
        /// Represents the address of the <see cref="Out3PulseWidth"/> register. This field is constant.
        /// </summary>
        public const int Address = 80;

        /// <summary>
        /// Represents the payload type of the <see cref="Out3PulseWidth"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="Out3PulseWidth"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Out3PulseWidth"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Out3PulseWidth"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Out3PulseWidth"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Out3PulseWidth"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Out3PulseWidth"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Out3PulseWidth"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Out3PulseWidth register.
    /// </summary>
    /// <seealso cref="Out3PulseWidth"/>
    [Description("Filters and selects timestamped messages from the Out3PulseWidth register.")]
    public partial class TimestampedOut3PulseWidth
    {
        /// <summary>
        /// Represents the address of the <see cref="Out3PulseWidth"/> register. This field is constant.
        /// </summary>
        public const int Address = Out3PulseWidth.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Out3PulseWidth"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return Out3PulseWidth.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the duration (ms) of the pulse to be generated on the specified digital output line.
    /// </summary>
    [Description("Sets the duration (ms) of the pulse to be generated on the specified digital output line.")]
    public partial class Out4PulseWidth
    {
        /// <summary>
        /// Represents the address of the <see cref="Out4PulseWidth"/> register. This field is constant.
        /// </summary>
        public const int Address = 81;

        /// <summary>
        /// Represents the payload type of the <see cref="Out4PulseWidth"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="Out4PulseWidth"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Out4PulseWidth"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Out4PulseWidth"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Out4PulseWidth"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Out4PulseWidth"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Out4PulseWidth"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Out4PulseWidth"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Out4PulseWidth register.
    /// </summary>
    /// <seealso cref="Out4PulseWidth"/>
    [Description("Filters and selects timestamped messages from the Out4PulseWidth register.")]
    public partial class TimestampedOut4PulseWidth
    {
        /// <summary>
        /// Represents the address of the <see cref="Out4PulseWidth"/> register. This field is constant.
        /// </summary>
        public const int Address = Out4PulseWidth.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Out4PulseWidth"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return Out4PulseWidth.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the duration (ms) of the pulse to be generated on the specified digital output line.
    /// </summary>
    [Description("Sets the duration (ms) of the pulse to be generated on the specified digital output line.")]
    public partial class Out5PulseWidth
    {
        /// <summary>
        /// Represents the address of the <see cref="Out5PulseWidth"/> register. This field is constant.
        /// </summary>
        public const int Address = 82;

        /// <summary>
        /// Represents the payload type of the <see cref="Out5PulseWidth"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="Out5PulseWidth"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Out5PulseWidth"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Out5PulseWidth"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Out5PulseWidth"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Out5PulseWidth"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Out5PulseWidth"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Out5PulseWidth"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Out5PulseWidth register.
    /// </summary>
    /// <seealso cref="Out5PulseWidth"/>
    [Description("Filters and selects timestamped messages from the Out5PulseWidth register.")]
    public partial class TimestampedOut5PulseWidth
    {
        /// <summary>
        /// Represents the address of the <see cref="Out5PulseWidth"/> register. This field is constant.
        /// </summary>
        public const int Address = Out5PulseWidth.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Out5PulseWidth"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return Out5PulseWidth.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the duration (ms) of the pulse to be generated on the specified digital output line.
    /// </summary>
    [Description("Sets the duration (ms) of the pulse to be generated on the specified digital output line.")]
    public partial class Out6PulseWidth
    {
        /// <summary>
        /// Represents the address of the <see cref="Out6PulseWidth"/> register. This field is constant.
        /// </summary>
        public const int Address = 83;

        /// <summary>
        /// Represents the payload type of the <see cref="Out6PulseWidth"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="Out6PulseWidth"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Out6PulseWidth"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Out6PulseWidth"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Out6PulseWidth"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Out6PulseWidth"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Out6PulseWidth"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Out6PulseWidth"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Out6PulseWidth register.
    /// </summary>
    /// <seealso cref="Out6PulseWidth"/>
    [Description("Filters and selects timestamped messages from the Out6PulseWidth register.")]
    public partial class TimestampedOut6PulseWidth
    {
        /// <summary>
        /// Represents the address of the <see cref="Out6PulseWidth"/> register. This field is constant.
        /// </summary>
        public const int Address = Out6PulseWidth.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Out6PulseWidth"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return Out6PulseWidth.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the duration (ms) of the pulse to be generated on the specified digital output line.
    /// </summary>
    [Description("Sets the duration (ms) of the pulse to be generated on the specified digital output line.")]
    public partial class Out7PulseWidth
    {
        /// <summary>
        /// Represents the address of the <see cref="Out7PulseWidth"/> register. This field is constant.
        /// </summary>
        public const int Address = 84;

        /// <summary>
        /// Represents the payload type of the <see cref="Out7PulseWidth"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="Out7PulseWidth"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Out7PulseWidth"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Out7PulseWidth"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Out7PulseWidth"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Out7PulseWidth"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Out7PulseWidth"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Out7PulseWidth"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Out7PulseWidth register.
    /// </summary>
    /// <seealso cref="Out7PulseWidth"/>
    [Description("Filters and selects timestamped messages from the Out7PulseWidth register.")]
    public partial class TimestampedOut7PulseWidth
    {
        /// <summary>
        /// Represents the address of the <see cref="Out7PulseWidth"/> register. This field is constant.
        /// </summary>
        public const int Address = Out7PulseWidth.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Out7PulseWidth"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return Out7PulseWidth.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the duration (ms) of the pulse to be generated on the specified digital output line.
    /// </summary>
    [Description("Sets the duration (ms) of the pulse to be generated on the specified digital output line.")]
    public partial class Out8PulseWidth
    {
        /// <summary>
        /// Represents the address of the <see cref="Out8PulseWidth"/> register. This field is constant.
        /// </summary>
        public const int Address = 85;

        /// <summary>
        /// Represents the payload type of the <see cref="Out8PulseWidth"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="Out8PulseWidth"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Out8PulseWidth"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Out8PulseWidth"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Out8PulseWidth"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Out8PulseWidth"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Out8PulseWidth"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Out8PulseWidth"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Out8PulseWidth register.
    /// </summary>
    /// <seealso cref="Out8PulseWidth"/>
    [Description("Filters and selects timestamped messages from the Out8PulseWidth register.")]
    public partial class TimestampedOut8PulseWidth
    {
        /// <summary>
        /// Represents the address of the <see cref="Out8PulseWidth"/> register. This field is constant.
        /// </summary>
        public const int Address = Out8PulseWidth.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Out8PulseWidth"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return Out8PulseWidth.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the duration (ms) of the pulse to be generated on the specified digital output line.
    /// </summary>
    [Description("Sets the duration (ms) of the pulse to be generated on the specified digital output line.")]
    public partial class Out9PulseWidth
    {
        /// <summary>
        /// Represents the address of the <see cref="Out9PulseWidth"/> register. This field is constant.
        /// </summary>
        public const int Address = 86;

        /// <summary>
        /// Represents the payload type of the <see cref="Out9PulseWidth"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="Out9PulseWidth"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Out9PulseWidth"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Out9PulseWidth"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Out9PulseWidth"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Out9PulseWidth"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Out9PulseWidth"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Out9PulseWidth"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Out9PulseWidth register.
    /// </summary>
    /// <seealso cref="Out9PulseWidth"/>
    [Description("Filters and selects timestamped messages from the Out9PulseWidth register.")]
    public partial class TimestampedOut9PulseWidth
    {
        /// <summary>
        /// Represents the address of the <see cref="Out9PulseWidth"/> register. This field is constant.
        /// </summary>
        public const int Address = Out9PulseWidth.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Out9PulseWidth"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return Out9PulseWidth.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that selects the board to be interfaced with via the expansion port.
    /// </summary>
    [Description("Selects the board to be interfaced with via the expansion port.")]
    public partial class ExpansionBoard
    {
        /// <summary>
        /// Represents the address of the <see cref="ExpansionBoard"/> register. This field is constant.
        /// </summary>
        public const int Address = 87;

        /// <summary>
        /// Represents the payload type of the <see cref="ExpansionBoard"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="ExpansionBoard"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="ExpansionBoard"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ExpansionBoardTypes GetPayload(HarpMessage message)
        {
            return (ExpansionBoardTypes)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="ExpansionBoard"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ExpansionBoardTypes> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((ExpansionBoardTypes)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="ExpansionBoard"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="ExpansionBoard"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ExpansionBoardTypes value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="ExpansionBoard"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="ExpansionBoard"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ExpansionBoardTypes value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// ExpansionBoard register.
    /// </summary>
    /// <seealso cref="ExpansionBoard"/>
    [Description("Filters and selects timestamped messages from the ExpansionBoard register.")]
    public partial class TimestampedExpansionBoard
    {
        /// <summary>
        /// Represents the address of the <see cref="ExpansionBoard"/> register. This field is constant.
        /// </summary>
        public const int Address = ExpansionBoard.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="ExpansionBoard"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ExpansionBoardTypes> GetPayload(HarpMessage message)
        {
            return ExpansionBoard.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that generated event with the latest read from the magnetic encoder.
    /// </summary>
    [Description("Generated event with the latest read from the magnetic encoder.")]
    public partial class Encoder
    {
        /// <summary>
        /// Represents the address of the <see cref="Encoder"/> register. This field is constant.
        /// </summary>
        public const int Address = 90;

        /// <summary>
        /// Represents the payload type of the <see cref="Encoder"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="Encoder"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Encoder"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Encoder"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Encoder"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Encoder"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Encoder"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Encoder"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Encoder register.
    /// </summary>
    /// <seealso cref="Encoder"/>
    [Description("Filters and selects timestamped messages from the Encoder register.")]
    public partial class TimestampedEncoder
    {
        /// <summary>
        /// Represents the address of the <see cref="Encoder"/> register. This field is constant.
        /// </summary>
        public const int Address = Encoder.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Encoder"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return Encoder.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the sampling rate of the magnetic encoder.
    /// </summary>
    [Description("Sets the sampling rate of the magnetic encoder.")]
    public partial class EncoderSampling
    {
        /// <summary>
        /// Represents the address of the <see cref="EncoderSampling"/> register. This field is constant.
        /// </summary>
        public const int Address = 91;

        /// <summary>
        /// Represents the payload type of the <see cref="EncoderSampling"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="EncoderSampling"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="EncoderSampling"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static EncoderSamplingRate GetPayload(HarpMessage message)
        {
            return (EncoderSamplingRate)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="EncoderSampling"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<EncoderSamplingRate> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((EncoderSamplingRate)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="EncoderSampling"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="EncoderSampling"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, EncoderSamplingRate value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="EncoderSampling"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="EncoderSampling"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, EncoderSamplingRate value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// EncoderSampling register.
    /// </summary>
    /// <seealso cref="EncoderSampling"/>
    [Description("Filters and selects timestamped messages from the EncoderSampling register.")]
    public partial class TimestampedEncoderSampling
    {
        /// <summary>
        /// Represents the address of the <see cref="EncoderSampling"/> register. This field is constant.
        /// </summary>
        public const int Address = EncoderSampling.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="EncoderSampling"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<EncoderSamplingRate> GetPayload(HarpMessage message)
        {
            return EncoderSampling.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the period of the servo pulses (us).
    /// </summary>
    [Description("Sets the period of the servo pulses (us).")]
    public partial class ServoPeriod
    {
        /// <summary>
        /// Represents the address of the <see cref="ServoPeriod"/> register. This field is constant.
        /// </summary>
        public const int Address = 94;

        /// <summary>
        /// Represents the payload type of the <see cref="ServoPeriod"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="ServoPeriod"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="ServoPeriod"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="ServoPeriod"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="ServoPeriod"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="ServoPeriod"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="ServoPeriod"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="ServoPeriod"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// ServoPeriod register.
    /// </summary>
    /// <seealso cref="ServoPeriod"/>
    [Description("Filters and selects timestamped messages from the ServoPeriod register.")]
    public partial class TimestampedServoPeriod
    {
        /// <summary>
        /// Represents the address of the <see cref="ServoPeriod"/> register. This field is constant.
        /// </summary>
        public const int Address = ServoPeriod.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="ServoPeriod"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return ServoPeriod.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the pulse width of the Servo0 pulses (us).
    /// </summary>
    [Description("Sets the pulse width of the Servo0 pulses (us).")]
    public partial class Servo0PulseWidth
    {
        /// <summary>
        /// Represents the address of the <see cref="Servo0PulseWidth"/> register. This field is constant.
        /// </summary>
        public const int Address = 95;

        /// <summary>
        /// Represents the payload type of the <see cref="Servo0PulseWidth"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="Servo0PulseWidth"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Servo0PulseWidth"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Servo0PulseWidth"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Servo0PulseWidth"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Servo0PulseWidth"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Servo0PulseWidth"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Servo0PulseWidth"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Servo0PulseWidth register.
    /// </summary>
    /// <seealso cref="Servo0PulseWidth"/>
    [Description("Filters and selects timestamped messages from the Servo0PulseWidth register.")]
    public partial class TimestampedServo0PulseWidth
    {
        /// <summary>
        /// Represents the address of the <see cref="Servo0PulseWidth"/> register. This field is constant.
        /// </summary>
        public const int Address = Servo0PulseWidth.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Servo0PulseWidth"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return Servo0PulseWidth.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the pulse width of the Servo1 pulses (us).
    /// </summary>
    [Description("Sets the pulse width of the Servo1 pulses (us).")]
    public partial class Servo1PulseWidth
    {
        /// <summary>
        /// Represents the address of the <see cref="Servo1PulseWidth"/> register. This field is constant.
        /// </summary>
        public const int Address = 96;

        /// <summary>
        /// Represents the payload type of the <see cref="Servo1PulseWidth"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="Servo1PulseWidth"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Servo1PulseWidth"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Servo1PulseWidth"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Servo1PulseWidth"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Servo1PulseWidth"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Servo1PulseWidth"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Servo1PulseWidth"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Servo1PulseWidth register.
    /// </summary>
    /// <seealso cref="Servo1PulseWidth"/>
    [Description("Filters and selects timestamped messages from the Servo1PulseWidth register.")]
    public partial class TimestampedServo1PulseWidth
    {
        /// <summary>
        /// Represents the address of the <see cref="Servo1PulseWidth"/> register. This field is constant.
        /// </summary>
        public const int Address = Servo1PulseWidth.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Servo1PulseWidth"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return Servo1PulseWidth.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that sets the pulse width of the Servo2 pulses (us).
    /// </summary>
    [Description("Sets the pulse width of the Servo2 pulses (us).")]
    public partial class Servo2PulseWidth
    {
        /// <summary>
        /// Represents the address of the <see cref="Servo2PulseWidth"/> register. This field is constant.
        /// </summary>
        public const int Address = 97;

        /// <summary>
        /// Represents the payload type of the <see cref="Servo2PulseWidth"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="Servo2PulseWidth"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="Servo2PulseWidth"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="Servo2PulseWidth"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="Servo2PulseWidth"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Servo2PulseWidth"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="Servo2PulseWidth"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="Servo2PulseWidth"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// Servo2PulseWidth register.
    /// </summary>
    /// <seealso cref="Servo2PulseWidth"/>
    [Description("Filters and selects timestamped messages from the Servo2PulseWidth register.")]
    public partial class TimestampedServo2PulseWidth
    {
        /// <summary>
        /// Represents the address of the <see cref="Servo2PulseWidth"/> register. This field is constant.
        /// </summary>
        public const int Address = Servo2PulseWidth.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="Servo2PulseWidth"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return Servo2PulseWidth.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that generated event with the latest read from the optical flow sensor.
    /// </summary>
    [Description("Generated event with the latest read from the optical flow sensor.")]
    public partial class OpticalFlow
    {
        /// <summary>
        /// Represents the address of the <see cref="OpticalFlow"/> register. This field is constant.
        /// </summary>
        public const int Address = 100;

        /// <summary>
        /// Represents the payload type of the <see cref="OpticalFlow"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.S16;

        /// <summary>
        /// Represents the length of the <see cref="OpticalFlow"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="OpticalFlow"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static short GetPayload(HarpMessage message)
        {
            return message.GetPayloadInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="OpticalFlow"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<short> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="OpticalFlow"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="OpticalFlow"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, short value)
        {
            return HarpMessage.FromInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="OpticalFlow"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="OpticalFlow"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, short value)
        {
            return HarpMessage.FromInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// OpticalFlow register.
    /// </summary>
    /// <seealso cref="OpticalFlow"/>
    [Description("Filters and selects timestamped messages from the OpticalFlow register.")]
    public partial class TimestampedOpticalFlow
    {
        /// <summary>
        /// Represents the address of the <see cref="OpticalFlow"/> register. This field is constant.
        /// </summary>
        public const int Address = OpticalFlow.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="OpticalFlow"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<short> GetPayload(HarpMessage message)
        {
            return OpticalFlow.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents an operator which creates standard message payloads for the
    /// OutputExpander device.
    /// </summary>
    /// <seealso cref="CreateAuxInPortPayload"/>
    /// <seealso cref="CreateAuxInEnableRisingEdgePayload"/>
    /// <seealso cref="CreateAuxInEnableFallingEdgePayload"/>
    /// <seealso cref="CreateOutputSetPayload"/>
    /// <seealso cref="CreateOutputClearPayload"/>
    /// <seealso cref="CreateOutputTogglePayload"/>
    /// <seealso cref="CreateOutputStatePayload"/>
    /// <seealso cref="CreatePwmAndStimEnablePayload"/>
    /// <seealso cref="CreatePwmAndStimDisablePayload"/>
    /// <seealso cref="CreatePwmAndStimWritePayload"/>
    /// <seealso cref="CreatePwm0FrequencyPayload"/>
    /// <seealso cref="CreatePwm0DutyCyclePayload"/>
    /// <seealso cref="CreatePwm0PulseCountPayload"/>
    /// <seealso cref="CreatePwm0RealFrequencyPayload"/>
    /// <seealso cref="CreatePwm0RealDutyCyclePayload"/>
    /// <seealso cref="CreatePwm0AcquisitionModePayload"/>
    /// <seealso cref="CreatePwm0TriggerSourcePayload"/>
    /// <seealso cref="CreatePwm0EventEnablePayload"/>
    /// <seealso cref="CreatePwm1FrequencyPayload"/>
    /// <seealso cref="CreatePwm1DutyCyclePayload"/>
    /// <seealso cref="CreatePwm1PulseCountPayload"/>
    /// <seealso cref="CreatePwm1RealFrequencyPayload"/>
    /// <seealso cref="CreatePwm1RealDutyCyclePayload"/>
    /// <seealso cref="CreatePwm1AcquisitionModePayload"/>
    /// <seealso cref="CreatePwm1TriggerSourcePayload"/>
    /// <seealso cref="CreatePwm1EventEnablePayload"/>
    /// <seealso cref="CreatePwm2FrequencyPayload"/>
    /// <seealso cref="CreatePwm2DutyCyclePayload"/>
    /// <seealso cref="CreatePwm2PulseCountPayload"/>
    /// <seealso cref="CreatePwm2RealFrequencyPayload"/>
    /// <seealso cref="CreatePwm2RealDutyCyclePayload"/>
    /// <seealso cref="CreatePwm2AcquisitionModePayload"/>
    /// <seealso cref="CreatePwm2TriggerSourcePayload"/>
    /// <seealso cref="CreatePwm2EventEnablePayload"/>
    /// <seealso cref="CreatePwmStartPayload"/>
    /// <seealso cref="CreatePwmStopPayload"/>
    /// <seealso cref="CreatePwmRiseEventEnablePayload"/>
    /// <seealso cref="CreateStim0PulseOnTimePayload"/>
    /// <seealso cref="CreateStim0PulseOffTimePayload"/>
    /// <seealso cref="CreateStim0PulseCountPayload"/>
    /// <seealso cref="CreateStim0AcquisitionModePayload"/>
    /// <seealso cref="CreateStim0TriggerSourcePayload"/>
    /// <seealso cref="CreateStimStartPayload"/>
    /// <seealso cref="CreateStimStopPayload"/>
    /// <seealso cref="CreateOutputPulsePayload"/>
    /// <seealso cref="CreateOut0PulseWidthPayload"/>
    /// <seealso cref="CreateOut1PulseWidthPayload"/>
    /// <seealso cref="CreateOut2PulseWidthPayload"/>
    /// <seealso cref="CreateOut3PulseWidthPayload"/>
    /// <seealso cref="CreateOut4PulseWidthPayload"/>
    /// <seealso cref="CreateOut5PulseWidthPayload"/>
    /// <seealso cref="CreateOut6PulseWidthPayload"/>
    /// <seealso cref="CreateOut7PulseWidthPayload"/>
    /// <seealso cref="CreateOut8PulseWidthPayload"/>
    /// <seealso cref="CreateOut9PulseWidthPayload"/>
    /// <seealso cref="CreateExpansionBoardPayload"/>
    /// <seealso cref="CreateEncoderPayload"/>
    /// <seealso cref="CreateEncoderSamplingPayload"/>
    /// <seealso cref="CreateServoPeriodPayload"/>
    /// <seealso cref="CreateServo0PulseWidthPayload"/>
    /// <seealso cref="CreateServo1PulseWidthPayload"/>
    /// <seealso cref="CreateServo2PulseWidthPayload"/>
    /// <seealso cref="CreateOpticalFlowPayload"/>
    [XmlInclude(typeof(CreateAuxInPortPayload))]
    [XmlInclude(typeof(CreateAuxInEnableRisingEdgePayload))]
    [XmlInclude(typeof(CreateAuxInEnableFallingEdgePayload))]
    [XmlInclude(typeof(CreateOutputSetPayload))]
    [XmlInclude(typeof(CreateOutputClearPayload))]
    [XmlInclude(typeof(CreateOutputTogglePayload))]
    [XmlInclude(typeof(CreateOutputStatePayload))]
    [XmlInclude(typeof(CreatePwmAndStimEnablePayload))]
    [XmlInclude(typeof(CreatePwmAndStimDisablePayload))]
    [XmlInclude(typeof(CreatePwmAndStimWritePayload))]
    [XmlInclude(typeof(CreatePwm0FrequencyPayload))]
    [XmlInclude(typeof(CreatePwm0DutyCyclePayload))]
    [XmlInclude(typeof(CreatePwm0PulseCountPayload))]
    [XmlInclude(typeof(CreatePwm0RealFrequencyPayload))]
    [XmlInclude(typeof(CreatePwm0RealDutyCyclePayload))]
    [XmlInclude(typeof(CreatePwm0AcquisitionModePayload))]
    [XmlInclude(typeof(CreatePwm0TriggerSourcePayload))]
    [XmlInclude(typeof(CreatePwm0EventEnablePayload))]
    [XmlInclude(typeof(CreatePwm1FrequencyPayload))]
    [XmlInclude(typeof(CreatePwm1DutyCyclePayload))]
    [XmlInclude(typeof(CreatePwm1PulseCountPayload))]
    [XmlInclude(typeof(CreatePwm1RealFrequencyPayload))]
    [XmlInclude(typeof(CreatePwm1RealDutyCyclePayload))]
    [XmlInclude(typeof(CreatePwm1AcquisitionModePayload))]
    [XmlInclude(typeof(CreatePwm1TriggerSourcePayload))]
    [XmlInclude(typeof(CreatePwm1EventEnablePayload))]
    [XmlInclude(typeof(CreatePwm2FrequencyPayload))]
    [XmlInclude(typeof(CreatePwm2DutyCyclePayload))]
    [XmlInclude(typeof(CreatePwm2PulseCountPayload))]
    [XmlInclude(typeof(CreatePwm2RealFrequencyPayload))]
    [XmlInclude(typeof(CreatePwm2RealDutyCyclePayload))]
    [XmlInclude(typeof(CreatePwm2AcquisitionModePayload))]
    [XmlInclude(typeof(CreatePwm2TriggerSourcePayload))]
    [XmlInclude(typeof(CreatePwm2EventEnablePayload))]
    [XmlInclude(typeof(CreatePwmStartPayload))]
    [XmlInclude(typeof(CreatePwmStopPayload))]
    [XmlInclude(typeof(CreatePwmRiseEventEnablePayload))]
    [XmlInclude(typeof(CreateStim0PulseOnTimePayload))]
    [XmlInclude(typeof(CreateStim0PulseOffTimePayload))]
    [XmlInclude(typeof(CreateStim0PulseCountPayload))]
    [XmlInclude(typeof(CreateStim0AcquisitionModePayload))]
    [XmlInclude(typeof(CreateStim0TriggerSourcePayload))]
    [XmlInclude(typeof(CreateStimStartPayload))]
    [XmlInclude(typeof(CreateStimStopPayload))]
    [XmlInclude(typeof(CreateOutputPulsePayload))]
    [XmlInclude(typeof(CreateOut0PulseWidthPayload))]
    [XmlInclude(typeof(CreateOut1PulseWidthPayload))]
    [XmlInclude(typeof(CreateOut2PulseWidthPayload))]
    [XmlInclude(typeof(CreateOut3PulseWidthPayload))]
    [XmlInclude(typeof(CreateOut4PulseWidthPayload))]
    [XmlInclude(typeof(CreateOut5PulseWidthPayload))]
    [XmlInclude(typeof(CreateOut6PulseWidthPayload))]
    [XmlInclude(typeof(CreateOut7PulseWidthPayload))]
    [XmlInclude(typeof(CreateOut8PulseWidthPayload))]
    [XmlInclude(typeof(CreateOut9PulseWidthPayload))]
    [XmlInclude(typeof(CreateExpansionBoardPayload))]
    [XmlInclude(typeof(CreateEncoderPayload))]
    [XmlInclude(typeof(CreateEncoderSamplingPayload))]
    [XmlInclude(typeof(CreateServoPeriodPayload))]
    [XmlInclude(typeof(CreateServo0PulseWidthPayload))]
    [XmlInclude(typeof(CreateServo1PulseWidthPayload))]
    [XmlInclude(typeof(CreateServo2PulseWidthPayload))]
    [XmlInclude(typeof(CreateOpticalFlowPayload))]
    [Description("Creates standard message payloads for the OutputExpander device.")]
    public partial class CreateMessage : CreateMessageBuilder, INamedElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateMessage"/> class.
        /// </summary>
        public CreateMessage()
        {
            Payload = new CreateAuxInPortPayload();
        }

        string INamedElement.Name => $"{nameof(OutputExpander)}.{GetElementDisplayName(Payload)}";
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that reports the state of the auxiliary inputs.
    /// </summary>
    [DisplayName("AuxInPortPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that reports the state of the auxiliary inputs.")]
    public partial class CreateAuxInPortPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that reports the state of the auxiliary inputs.
        /// </summary>
        [Description("The value that reports the state of the auxiliary inputs.")]
        public AuxiliaryInput Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that reports the state of the auxiliary inputs.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that reports the state of the auxiliary inputs.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => AuxInPort.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that enables rising edge detection on the auxiliary inputs.
    /// </summary>
    [DisplayName("AuxInEnableRisingEdgePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that enables rising edge detection on the auxiliary inputs.")]
    public partial class CreateAuxInEnableRisingEdgePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that enables rising edge detection on the auxiliary inputs.
        /// </summary>
        [Description("The value that enables rising edge detection on the auxiliary inputs.")]
        public AuxiliaryInput Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that enables rising edge detection on the auxiliary inputs.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that enables rising edge detection on the auxiliary inputs.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => AuxInEnableRisingEdge.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that enables falling edge detection on the auxiliary input port.
    /// </summary>
    [DisplayName("AuxInEnableFallingEdgePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that enables falling edge detection on the auxiliary input port.")]
    public partial class CreateAuxInEnableFallingEdgePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that enables falling edge detection on the auxiliary input port.
        /// </summary>
        [Description("The value that enables falling edge detection on the auxiliary input port.")]
        public AuxiliaryInput Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that enables falling edge detection on the auxiliary input port.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that enables falling edge detection on the auxiliary input port.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => AuxInEnableFallingEdge.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that set the specified digital output lines.
    /// </summary>
    [DisplayName("OutputSetPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that set the specified digital output lines.")]
    public partial class CreateOutputSetPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that set the specified digital output lines.
        /// </summary>
        [Description("The value that set the specified digital output lines.")]
        public DigitalOutputs Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that set the specified digital output lines.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that set the specified digital output lines.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => OutputSet.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that clear the specified digital output lines.
    /// </summary>
    [DisplayName("OutputClearPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that clear the specified digital output lines.")]
    public partial class CreateOutputClearPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that clear the specified digital output lines.
        /// </summary>
        [Description("The value that clear the specified digital output lines.")]
        public DigitalOutputs Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that clear the specified digital output lines.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that clear the specified digital output lines.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => OutputClear.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that toggle the specified digital output lines.
    /// </summary>
    [DisplayName("OutputTogglePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that toggle the specified digital output lines.")]
    public partial class CreateOutputTogglePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that toggle the specified digital output lines.
        /// </summary>
        [Description("The value that toggle the specified digital output lines.")]
        public DigitalOutputs Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that toggle the specified digital output lines.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that toggle the specified digital output lines.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => OutputToggle.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that write the state of all digital output lines.
    /// </summary>
    [DisplayName("OutputStatePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that write the state of all digital output lines.")]
    public partial class CreateOutputStatePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that write the state of all digital output lines.
        /// </summary>
        [Description("The value that write the state of all digital output lines.")]
        public DigitalOutputs Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that write the state of all digital output lines.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that write the state of all digital output lines.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => OutputState.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that enables PWM and stimulation on the specified digital output lines.
    /// </summary>
    [DisplayName("PwmAndStimEnablePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that enables PWM and stimulation on the specified digital output lines.")]
    public partial class CreatePwmAndStimEnablePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that enables PWM and stimulation on the specified digital output lines.
        /// </summary>
        [Description("The value that enables PWM and stimulation on the specified digital output lines.")]
        public PwmAndStimMapping Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that enables PWM and stimulation on the specified digital output lines.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that enables PWM and stimulation on the specified digital output lines.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => PwmAndStimEnable.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that disables PWM and stimulation on the specified digital output lines.
    /// </summary>
    [DisplayName("PwmAndStimDisablePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that disables PWM and stimulation on the specified digital output lines.")]
    public partial class CreatePwmAndStimDisablePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that disables PWM and stimulation on the specified digital output lines.
        /// </summary>
        [Description("The value that disables PWM and stimulation on the specified digital output lines.")]
        public PwmAndStimMapping Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that disables PWM and stimulation on the specified digital output lines.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that disables PWM and stimulation on the specified digital output lines.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => PwmAndStimDisable.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that writes the mapping between PWM/stimulation and the specified digital output lines in a single command.
    /// </summary>
    [DisplayName("PwmAndStimWritePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that writes the mapping between PWM/stimulation and the specified digital output lines in a single command.")]
    public partial class CreatePwmAndStimWritePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that writes the mapping between PWM/stimulation and the specified digital output lines in a single command.
        /// </summary>
        [Description("The value that writes the mapping between PWM/stimulation and the specified digital output lines in a single command.")]
        public PwmAndStimMapping Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that writes the mapping between PWM/stimulation and the specified digital output lines in a single command.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that writes the mapping between PWM/stimulation and the specified digital output lines in a single command.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => PwmAndStimWrite.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the frequency of PWM0 (Hz).
    /// </summary>
    [DisplayName("Pwm0FrequencyPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the frequency of PWM0 (Hz).")]
    public partial class CreatePwm0FrequencyPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the frequency of PWM0 (Hz).
        /// </summary>
        [Range(min: long.MinValue, max: 1000)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that sets the frequency of PWM0 (Hz).")]
        public float Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the frequency of PWM0 (Hz).
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that sets the frequency of PWM0 (Hz).
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => Pwm0Frequency.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the duty cycle of PWM0 (%).
    /// </summary>
    [DisplayName("Pwm0DutyCyclePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the duty cycle of PWM0 (%).")]
    public partial class CreatePwm0DutyCyclePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the duty cycle of PWM0 (%).
        /// </summary>
        [Range(min: 0, max: 100)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that sets the duty cycle of PWM0 (%).")]
        public float Value { get; set; } = 50;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the duty cycle of PWM0 (%).
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that sets the duty cycle of PWM0 (%).
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => Pwm0DutyCycle.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the number of pulses to generate for PWM0.
    /// </summary>
    [DisplayName("Pwm0PulseCountPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the number of pulses to generate for PWM0.")]
    public partial class CreatePwm0PulseCountPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the number of pulses to generate for PWM0.
        /// </summary>
        [Description("The value that sets the number of pulses to generate for PWM0.")]
        public ushort Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the number of pulses to generate for PWM0.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that sets the number of pulses to generate for PWM0.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => Pwm0PulseCount.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that reports the actual frequency to be generated from PWM0 (Hz).
    /// </summary>
    [DisplayName("Pwm0RealFrequencyPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that reports the actual frequency to be generated from PWM0 (Hz).")]
    public partial class CreatePwm0RealFrequencyPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that reports the actual frequency to be generated from PWM0 (Hz).
        /// </summary>
        [Description("The value that reports the actual frequency to be generated from PWM0 (Hz).")]
        public float Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that reports the actual frequency to be generated from PWM0 (Hz).
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that reports the actual frequency to be generated from PWM0 (Hz).
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => Pwm0RealFrequency.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that reports the actual duty cycle to be generated from PWM0 (%).
    /// </summary>
    [DisplayName("Pwm0RealDutyCyclePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that reports the actual duty cycle to be generated from PWM0 (%).")]
    public partial class CreatePwm0RealDutyCyclePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that reports the actual duty cycle to be generated from PWM0 (%).
        /// </summary>
        [Description("The value that reports the actual duty cycle to be generated from PWM0 (%).")]
        public float Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that reports the actual duty cycle to be generated from PWM0 (%).
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that reports the actual duty cycle to be generated from PWM0 (%).
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => Pwm0RealDutyCycle.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the acquisition mode of PWM0.
    /// </summary>
    [DisplayName("Pwm0AcquisitionModePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the acquisition mode of PWM0.")]
    public partial class CreatePwm0AcquisitionModePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the acquisition mode of PWM0.
        /// </summary>
        [Description("The value that sets the acquisition mode of PWM0.")]
        public AcquisitionMode Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the acquisition mode of PWM0.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that sets the acquisition mode of PWM0.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => Pwm0AcquisitionMode.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the trigger source of PWM0.
    /// </summary>
    [DisplayName("Pwm0TriggerSourcePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the trigger source of PWM0.")]
    public partial class CreatePwm0TriggerSourcePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the trigger source of PWM0.
        /// </summary>
        [Description("The value that sets the trigger source of PWM0.")]
        public TriggerSources Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the trigger source of PWM0.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that sets the trigger source of PWM0.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => Pwm0TriggerSource.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that enables the generation of events for PWM0.
    /// </summary>
    [DisplayName("Pwm0EventEnablePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that enables the generation of events for PWM0.")]
    public partial class CreatePwm0EventEnablePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that enables the generation of events for PWM0.
        /// </summary>
        [Description("The value that enables the generation of events for PWM0.")]
        public EnableFlag Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that enables the generation of events for PWM0.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that enables the generation of events for PWM0.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => Pwm0EventEnable.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the frequency of PWM1 (Hz).
    /// </summary>
    [DisplayName("Pwm1FrequencyPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the frequency of PWM1 (Hz).")]
    public partial class CreatePwm1FrequencyPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the frequency of PWM1 (Hz).
        /// </summary>
        [Range(min: long.MinValue, max: 1000)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that sets the frequency of PWM1 (Hz).")]
        public float Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the frequency of PWM1 (Hz).
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that sets the frequency of PWM1 (Hz).
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => Pwm1Frequency.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the duty cycle of PWM1 (%).
    /// </summary>
    [DisplayName("Pwm1DutyCyclePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the duty cycle of PWM1 (%).")]
    public partial class CreatePwm1DutyCyclePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the duty cycle of PWM1 (%).
        /// </summary>
        [Range(min: 0, max: 100)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that sets the duty cycle of PWM1 (%).")]
        public float Value { get; set; } = 50;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the duty cycle of PWM1 (%).
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that sets the duty cycle of PWM1 (%).
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => Pwm1DutyCycle.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the number of pulses to generate for PWM1.
    /// </summary>
    [DisplayName("Pwm1PulseCountPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the number of pulses to generate for PWM1.")]
    public partial class CreatePwm1PulseCountPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the number of pulses to generate for PWM1.
        /// </summary>
        [Description("The value that sets the number of pulses to generate for PWM1.")]
        public ushort Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the number of pulses to generate for PWM1.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that sets the number of pulses to generate for PWM1.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => Pwm1PulseCount.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that reports the actual frequency to be generated from PWM1 (Hz).
    /// </summary>
    [DisplayName("Pwm1RealFrequencyPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that reports the actual frequency to be generated from PWM1 (Hz).")]
    public partial class CreatePwm1RealFrequencyPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that reports the actual frequency to be generated from PWM1 (Hz).
        /// </summary>
        [Description("The value that reports the actual frequency to be generated from PWM1 (Hz).")]
        public float Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that reports the actual frequency to be generated from PWM1 (Hz).
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that reports the actual frequency to be generated from PWM1 (Hz).
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => Pwm1RealFrequency.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that reports the actual duty cycle to be generated from PWM1 (%).
    /// </summary>
    [DisplayName("Pwm1RealDutyCyclePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that reports the actual duty cycle to be generated from PWM1 (%).")]
    public partial class CreatePwm1RealDutyCyclePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that reports the actual duty cycle to be generated from PWM1 (%).
        /// </summary>
        [Description("The value that reports the actual duty cycle to be generated from PWM1 (%).")]
        public float Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that reports the actual duty cycle to be generated from PWM1 (%).
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that reports the actual duty cycle to be generated from PWM1 (%).
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => Pwm1RealDutyCycle.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the acquisition mode of PWM1.
    /// </summary>
    [DisplayName("Pwm1AcquisitionModePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the acquisition mode of PWM1.")]
    public partial class CreatePwm1AcquisitionModePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the acquisition mode of PWM1.
        /// </summary>
        [Description("The value that sets the acquisition mode of PWM1.")]
        public AcquisitionMode Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the acquisition mode of PWM1.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that sets the acquisition mode of PWM1.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => Pwm1AcquisitionMode.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the trigger source of PWM1.
    /// </summary>
    [DisplayName("Pwm1TriggerSourcePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the trigger source of PWM1.")]
    public partial class CreatePwm1TriggerSourcePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the trigger source of PWM1.
        /// </summary>
        [Description("The value that sets the trigger source of PWM1.")]
        public TriggerSources Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the trigger source of PWM1.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that sets the trigger source of PWM1.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => Pwm1TriggerSource.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that enables the generation of events for PWM1.
    /// </summary>
    [DisplayName("Pwm1EventEnablePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that enables the generation of events for PWM1.")]
    public partial class CreatePwm1EventEnablePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that enables the generation of events for PWM1.
        /// </summary>
        [Description("The value that enables the generation of events for PWM1.")]
        public EnableFlag Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that enables the generation of events for PWM1.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that enables the generation of events for PWM1.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => Pwm1EventEnable.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the frequency of PWM2 (Hz).
    /// </summary>
    [DisplayName("Pwm2FrequencyPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the frequency of PWM2 (Hz).")]
    public partial class CreatePwm2FrequencyPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the frequency of PWM2 (Hz).
        /// </summary>
        [Range(min: long.MinValue, max: 1000)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that sets the frequency of PWM2 (Hz).")]
        public float Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the frequency of PWM2 (Hz).
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that sets the frequency of PWM2 (Hz).
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => Pwm2Frequency.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the duty cycle of PWM2 (%).
    /// </summary>
    [DisplayName("Pwm2DutyCyclePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the duty cycle of PWM2 (%).")]
    public partial class CreatePwm2DutyCyclePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the duty cycle of PWM2 (%).
        /// </summary>
        [Range(min: 0, max: 100)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that sets the duty cycle of PWM2 (%).")]
        public float Value { get; set; } = 50;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the duty cycle of PWM2 (%).
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that sets the duty cycle of PWM2 (%).
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => Pwm2DutyCycle.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the number of pulses to generate for PWM2.
    /// </summary>
    [DisplayName("Pwm2PulseCountPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the number of pulses to generate for PWM2.")]
    public partial class CreatePwm2PulseCountPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the number of pulses to generate for PWM2.
        /// </summary>
        [Description("The value that sets the number of pulses to generate for PWM2.")]
        public ushort Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the number of pulses to generate for PWM2.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that sets the number of pulses to generate for PWM2.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => Pwm2PulseCount.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that reports the actual frequency to be generated from PWM2 (Hz).
    /// </summary>
    [DisplayName("Pwm2RealFrequencyPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that reports the actual frequency to be generated from PWM2 (Hz).")]
    public partial class CreatePwm2RealFrequencyPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that reports the actual frequency to be generated from PWM2 (Hz).
        /// </summary>
        [Description("The value that reports the actual frequency to be generated from PWM2 (Hz).")]
        public float Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that reports the actual frequency to be generated from PWM2 (Hz).
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that reports the actual frequency to be generated from PWM2 (Hz).
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => Pwm2RealFrequency.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that reports the actual duty cycle to be generated from PWM2 (%).
    /// </summary>
    [DisplayName("Pwm2RealDutyCyclePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that reports the actual duty cycle to be generated from PWM2 (%).")]
    public partial class CreatePwm2RealDutyCyclePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that reports the actual duty cycle to be generated from PWM2 (%).
        /// </summary>
        [Description("The value that reports the actual duty cycle to be generated from PWM2 (%).")]
        public float Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that reports the actual duty cycle to be generated from PWM2 (%).
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that reports the actual duty cycle to be generated from PWM2 (%).
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => Pwm2RealDutyCycle.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the acquisition mode of PWM2.
    /// </summary>
    [DisplayName("Pwm2AcquisitionModePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the acquisition mode of PWM2.")]
    public partial class CreatePwm2AcquisitionModePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the acquisition mode of PWM2.
        /// </summary>
        [Description("The value that sets the acquisition mode of PWM2.")]
        public AcquisitionMode Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the acquisition mode of PWM2.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that sets the acquisition mode of PWM2.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => Pwm2AcquisitionMode.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the trigger source of PWM2.
    /// </summary>
    [DisplayName("Pwm2TriggerSourcePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the trigger source of PWM2.")]
    public partial class CreatePwm2TriggerSourcePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the trigger source of PWM2.
        /// </summary>
        [Description("The value that sets the trigger source of PWM2.")]
        public TriggerSources Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the trigger source of PWM2.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that sets the trigger source of PWM2.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => Pwm2TriggerSource.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that enables the generation of events for PWM2.
    /// </summary>
    [DisplayName("Pwm2EventEnablePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that enables the generation of events for PWM2.")]
    public partial class CreatePwm2EventEnablePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that enables the generation of events for PWM2.
        /// </summary>
        [Description("The value that enables the generation of events for PWM2.")]
        public EnableFlag Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that enables the generation of events for PWM2.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that enables the generation of events for PWM2.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => Pwm2EventEnable.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that starts the a PWM on the specified channels. An event will be generated if the start was triggered by an auxiliary input.
    /// </summary>
    [DisplayName("PwmStartPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that starts the a PWM on the specified channels. An event will be generated if the start was triggered by an auxiliary input.")]
    public partial class CreatePwmStartPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that starts the a PWM on the specified channels. An event will be generated if the start was triggered by an auxiliary input.
        /// </summary>
        [Description("The value that starts the a PWM on the specified channels. An event will be generated if the start was triggered by an auxiliary input.")]
        public Pwm Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that starts the a PWM on the specified channels. An event will be generated if the start was triggered by an auxiliary input.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that starts the a PWM on the specified channels. An event will be generated if the start was triggered by an auxiliary input.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => PwmStart.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that stops the a PWM on the specified channels.
    /// </summary>
    [DisplayName("PwmStopPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that stops the a PWM on the specified channels.")]
    public partial class CreatePwmStopPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that stops the a PWM on the specified channels.
        /// </summary>
        [Description("The value that stops the a PWM on the specified channels.")]
        public Pwm Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that stops the a PWM on the specified channels.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that stops the a PWM on the specified channels.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => PwmStop.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that enables the generation of an event on every rising edge of the PWM line.
    /// </summary>
    [DisplayName("PwmRiseEventEnablePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that enables the generation of an event on every rising edge of the PWM line.")]
    public partial class CreatePwmRiseEventEnablePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that enables the generation of an event on every rising edge of the PWM line.
        /// </summary>
        [Description("The value that enables the generation of an event on every rising edge of the PWM line.")]
        public Pwm Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that enables the generation of an event on every rising edge of the PWM line.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that enables the generation of an event on every rising edge of the PWM line.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => PwmRiseEventEnable.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the duration (us) each pulse is on for.
    /// </summary>
    [DisplayName("Stim0PulseOnTimePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the duration (us) each pulse is on for.")]
    public partial class CreateStim0PulseOnTimePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the duration (us) each pulse is on for.
        /// </summary>
        [Description("The value that sets the duration (us) each pulse is on for.")]
        public ushort Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the duration (us) each pulse is on for.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that sets the duration (us) each pulse is on for.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => Stim0PulseOnTime.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the duration (us) each pulse is off for.
    /// </summary>
    [DisplayName("Stim0PulseOffTimePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the duration (us) each pulse is off for.")]
    public partial class CreateStim0PulseOffTimePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the duration (us) each pulse is off for.
        /// </summary>
        [Description("The value that sets the duration (us) each pulse is off for.")]
        public ushort Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the duration (us) each pulse is off for.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that sets the duration (us) each pulse is off for.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => Stim0PulseOffTime.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the number of pulses to be generate when triggered.
    /// </summary>
    [DisplayName("Stim0PulseCountPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the number of pulses to be generate when triggered.")]
    public partial class CreateStim0PulseCountPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the number of pulses to be generate when triggered.
        /// </summary>
        [Description("The value that sets the number of pulses to be generate when triggered.")]
        public ushort Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the number of pulses to be generate when triggered.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that sets the number of pulses to be generate when triggered.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => Stim0PulseCount.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the acquisition mode of Stim0.
    /// </summary>
    [DisplayName("Stim0AcquisitionModePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the acquisition mode of Stim0.")]
    public partial class CreateStim0AcquisitionModePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the acquisition mode of Stim0.
        /// </summary>
        [Description("The value that sets the acquisition mode of Stim0.")]
        public AcquisitionMode Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the acquisition mode of Stim0.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that sets the acquisition mode of Stim0.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => Stim0AcquisitionMode.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the trigger source of Stim0.
    /// </summary>
    [DisplayName("Stim0TriggerSourcePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the trigger source of Stim0.")]
    public partial class CreateStim0TriggerSourcePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the trigger source of Stim0.
        /// </summary>
        [Description("The value that sets the trigger source of Stim0.")]
        public TriggerSources Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the trigger source of Stim0.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that sets the trigger source of Stim0.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => Stim0TriggerSource.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that starts a target Stim protocol.
    /// </summary>
    [DisplayName("StimStartPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that starts a target Stim protocol.")]
    public partial class CreateStimStartPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that starts a target Stim protocol.
        /// </summary>
        [Description("The value that starts a target Stim protocol.")]
        public Stim Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that starts a target Stim protocol.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that starts a target Stim protocol.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => StimStart.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that stops the target Stim protocol.
    /// </summary>
    [DisplayName("StimStopPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that stops the target Stim protocol.")]
    public partial class CreateStimStopPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that stops the target Stim protocol.
        /// </summary>
        [Description("The value that stops the target Stim protocol.")]
        public Stim Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that stops the target Stim protocol.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that stops the target Stim protocol.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => StimStop.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that enables pulse generation on the specified digital output line.
    /// </summary>
    [DisplayName("OutputPulsePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that enables pulse generation on the specified digital output line.")]
    public partial class CreateOutputPulsePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that enables pulse generation on the specified digital output line.
        /// </summary>
        [Description("The value that enables pulse generation on the specified digital output line.")]
        public DigitalOutputs Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that enables pulse generation on the specified digital output line.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that enables pulse generation on the specified digital output line.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => OutputPulse.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the duration (ms) of the pulse to be generated on the specified digital output line.
    /// </summary>
    [DisplayName("Out0PulseWidthPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the duration (ms) of the pulse to be generated on the specified digital output line.")]
    public partial class CreateOut0PulseWidthPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the duration (ms) of the pulse to be generated on the specified digital output line.
        /// </summary>
        [Description("The value that sets the duration (ms) of the pulse to be generated on the specified digital output line.")]
        public ushort Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the duration (ms) of the pulse to be generated on the specified digital output line.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that sets the duration (ms) of the pulse to be generated on the specified digital output line.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => Out0PulseWidth.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the duration (ms) of the pulse to be generated on the specified digital output line.
    /// </summary>
    [DisplayName("Out1PulseWidthPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the duration (ms) of the pulse to be generated on the specified digital output line.")]
    public partial class CreateOut1PulseWidthPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the duration (ms) of the pulse to be generated on the specified digital output line.
        /// </summary>
        [Description("The value that sets the duration (ms) of the pulse to be generated on the specified digital output line.")]
        public ushort Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the duration (ms) of the pulse to be generated on the specified digital output line.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that sets the duration (ms) of the pulse to be generated on the specified digital output line.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => Out1PulseWidth.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the duration (ms) of the pulse to be generated on the specified digital output line.
    /// </summary>
    [DisplayName("Out2PulseWidthPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the duration (ms) of the pulse to be generated on the specified digital output line.")]
    public partial class CreateOut2PulseWidthPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the duration (ms) of the pulse to be generated on the specified digital output line.
        /// </summary>
        [Description("The value that sets the duration (ms) of the pulse to be generated on the specified digital output line.")]
        public ushort Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the duration (ms) of the pulse to be generated on the specified digital output line.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that sets the duration (ms) of the pulse to be generated on the specified digital output line.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => Out2PulseWidth.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the duration (ms) of the pulse to be generated on the specified digital output line.
    /// </summary>
    [DisplayName("Out3PulseWidthPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the duration (ms) of the pulse to be generated on the specified digital output line.")]
    public partial class CreateOut3PulseWidthPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the duration (ms) of the pulse to be generated on the specified digital output line.
        /// </summary>
        [Description("The value that sets the duration (ms) of the pulse to be generated on the specified digital output line.")]
        public ushort Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the duration (ms) of the pulse to be generated on the specified digital output line.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that sets the duration (ms) of the pulse to be generated on the specified digital output line.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => Out3PulseWidth.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the duration (ms) of the pulse to be generated on the specified digital output line.
    /// </summary>
    [DisplayName("Out4PulseWidthPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the duration (ms) of the pulse to be generated on the specified digital output line.")]
    public partial class CreateOut4PulseWidthPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the duration (ms) of the pulse to be generated on the specified digital output line.
        /// </summary>
        [Description("The value that sets the duration (ms) of the pulse to be generated on the specified digital output line.")]
        public ushort Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the duration (ms) of the pulse to be generated on the specified digital output line.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that sets the duration (ms) of the pulse to be generated on the specified digital output line.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => Out4PulseWidth.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the duration (ms) of the pulse to be generated on the specified digital output line.
    /// </summary>
    [DisplayName("Out5PulseWidthPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the duration (ms) of the pulse to be generated on the specified digital output line.")]
    public partial class CreateOut5PulseWidthPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the duration (ms) of the pulse to be generated on the specified digital output line.
        /// </summary>
        [Description("The value that sets the duration (ms) of the pulse to be generated on the specified digital output line.")]
        public ushort Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the duration (ms) of the pulse to be generated on the specified digital output line.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that sets the duration (ms) of the pulse to be generated on the specified digital output line.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => Out5PulseWidth.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the duration (ms) of the pulse to be generated on the specified digital output line.
    /// </summary>
    [DisplayName("Out6PulseWidthPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the duration (ms) of the pulse to be generated on the specified digital output line.")]
    public partial class CreateOut6PulseWidthPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the duration (ms) of the pulse to be generated on the specified digital output line.
        /// </summary>
        [Description("The value that sets the duration (ms) of the pulse to be generated on the specified digital output line.")]
        public ushort Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the duration (ms) of the pulse to be generated on the specified digital output line.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that sets the duration (ms) of the pulse to be generated on the specified digital output line.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => Out6PulseWidth.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the duration (ms) of the pulse to be generated on the specified digital output line.
    /// </summary>
    [DisplayName("Out7PulseWidthPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the duration (ms) of the pulse to be generated on the specified digital output line.")]
    public partial class CreateOut7PulseWidthPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the duration (ms) of the pulse to be generated on the specified digital output line.
        /// </summary>
        [Description("The value that sets the duration (ms) of the pulse to be generated on the specified digital output line.")]
        public ushort Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the duration (ms) of the pulse to be generated on the specified digital output line.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that sets the duration (ms) of the pulse to be generated on the specified digital output line.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => Out7PulseWidth.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the duration (ms) of the pulse to be generated on the specified digital output line.
    /// </summary>
    [DisplayName("Out8PulseWidthPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the duration (ms) of the pulse to be generated on the specified digital output line.")]
    public partial class CreateOut8PulseWidthPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the duration (ms) of the pulse to be generated on the specified digital output line.
        /// </summary>
        [Description("The value that sets the duration (ms) of the pulse to be generated on the specified digital output line.")]
        public ushort Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the duration (ms) of the pulse to be generated on the specified digital output line.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that sets the duration (ms) of the pulse to be generated on the specified digital output line.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => Out8PulseWidth.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the duration (ms) of the pulse to be generated on the specified digital output line.
    /// </summary>
    [DisplayName("Out9PulseWidthPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the duration (ms) of the pulse to be generated on the specified digital output line.")]
    public partial class CreateOut9PulseWidthPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the duration (ms) of the pulse to be generated on the specified digital output line.
        /// </summary>
        [Description("The value that sets the duration (ms) of the pulse to be generated on the specified digital output line.")]
        public ushort Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the duration (ms) of the pulse to be generated on the specified digital output line.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that sets the duration (ms) of the pulse to be generated on the specified digital output line.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => Out9PulseWidth.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that selects the board to be interfaced with via the expansion port.
    /// </summary>
    [DisplayName("ExpansionBoardPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that selects the board to be interfaced with via the expansion port.")]
    public partial class CreateExpansionBoardPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that selects the board to be interfaced with via the expansion port.
        /// </summary>
        [Description("The value that selects the board to be interfaced with via the expansion port.")]
        public ExpansionBoardTypes Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that selects the board to be interfaced with via the expansion port.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that selects the board to be interfaced with via the expansion port.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => ExpansionBoard.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that generated event with the latest read from the magnetic encoder.
    /// </summary>
    [DisplayName("EncoderPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that generated event with the latest read from the magnetic encoder.")]
    public partial class CreateEncoderPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that generated event with the latest read from the magnetic encoder.
        /// </summary>
        [Description("The value that generated event with the latest read from the magnetic encoder.")]
        public ushort Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that generated event with the latest read from the magnetic encoder.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that generated event with the latest read from the magnetic encoder.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => Encoder.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the sampling rate of the magnetic encoder.
    /// </summary>
    [DisplayName("EncoderSamplingPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the sampling rate of the magnetic encoder.")]
    public partial class CreateEncoderSamplingPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the sampling rate of the magnetic encoder.
        /// </summary>
        [Description("The value that sets the sampling rate of the magnetic encoder.")]
        public EncoderSamplingRate Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the sampling rate of the magnetic encoder.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that sets the sampling rate of the magnetic encoder.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => EncoderSampling.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the period of the servo pulses (us).
    /// </summary>
    [DisplayName("ServoPeriodPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the period of the servo pulses (us).")]
    public partial class CreateServoPeriodPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the period of the servo pulses (us).
        /// </summary>
        [Description("The value that sets the period of the servo pulses (us).")]
        public ushort Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the period of the servo pulses (us).
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that sets the period of the servo pulses (us).
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => ServoPeriod.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the pulse width of the Servo0 pulses (us).
    /// </summary>
    [DisplayName("Servo0PulseWidthPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the pulse width of the Servo0 pulses (us).")]
    public partial class CreateServo0PulseWidthPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the pulse width of the Servo0 pulses (us).
        /// </summary>
        [Description("The value that sets the pulse width of the Servo0 pulses (us).")]
        public ushort Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the pulse width of the Servo0 pulses (us).
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that sets the pulse width of the Servo0 pulses (us).
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => Servo0PulseWidth.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the pulse width of the Servo1 pulses (us).
    /// </summary>
    [DisplayName("Servo1PulseWidthPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the pulse width of the Servo1 pulses (us).")]
    public partial class CreateServo1PulseWidthPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the pulse width of the Servo1 pulses (us).
        /// </summary>
        [Description("The value that sets the pulse width of the Servo1 pulses (us).")]
        public ushort Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the pulse width of the Servo1 pulses (us).
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that sets the pulse width of the Servo1 pulses (us).
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => Servo1PulseWidth.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that sets the pulse width of the Servo2 pulses (us).
    /// </summary>
    [DisplayName("Servo2PulseWidthPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that sets the pulse width of the Servo2 pulses (us).")]
    public partial class CreateServo2PulseWidthPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that sets the pulse width of the Servo2 pulses (us).
        /// </summary>
        [Description("The value that sets the pulse width of the Servo2 pulses (us).")]
        public ushort Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that sets the pulse width of the Servo2 pulses (us).
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that sets the pulse width of the Servo2 pulses (us).
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => Servo2PulseWidth.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that generated event with the latest read from the optical flow sensor.
    /// </summary>
    [DisplayName("OpticalFlowPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that generated event with the latest read from the optical flow sensor.")]
    public partial class CreateOpticalFlowPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that generated event with the latest read from the optical flow sensor.
        /// </summary>
        [Description("The value that generated event with the latest read from the optical flow sensor.")]
        public short Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that generated event with the latest read from the optical flow sensor.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that generated event with the latest read from the optical flow sensor.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => OpticalFlow.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Specifies the state of auxiliary input lines.
    /// </summary>
    [Flags]
    public enum AuxiliaryInput : byte
    {
        Aux0 = 0x1,
        Aux1 = 0x2,
        Aux0Changed = 0x20,
        Aux1Changed = 0x40
    }

    /// <summary>
    /// Specifies the available digital output lines.
    /// </summary>
    [Flags]
    public enum DigitalOutputs : ushort
    {
        Out0 = 0x1,
        Out1 = 0x2,
        Out2 = 0x4,
        Out3 = 0x8,
        Out4 = 0x10,
        Out5 = 0x20,
        Out6 = 0x40,
        Out7 = 0x80,
        Out8 = 0x100,
        Out9 = 0x200
    }

    /// <summary>
    /// Specifies the mapping PWM and Stimulation to digital output lines.
    /// </summary>
    [Flags]
    public enum PwmAndStimMapping : ushort
    {
        Pwm0ToOut1 = 0x1,
        Pwm0ToOut2 = 0x2,
        Pwm0ToOut3 = 0x4,
        Pwm1ToOut6 = 0x8,
        Pwm1ToOut7 = 0x10,
        Pwm1ToOut8 = 0x20,
        Pwm2ToOut9 = 0x40,
        Stim0ToOut0 = 0x80,
        Stim0ToOut5 = 0x100
    }

    /// <summary>
    /// Specifies the available PWM protocols.
    /// </summary>
    [Flags]
    public enum Pwm : byte
    {
        Pwm0 = 0x1,
        Pwm1 = 0x2,
        Pwm2 = 0x4
    }

    /// <summary>
    /// Specifies the available Stim protocols.
    /// </summary>
    [Flags]
    public enum Stim : byte
    {
        Stim0 = 0x1
    }

    /// <summary>
    /// Available configurations for a PWM line.
    /// </summary>
    public enum AcquisitionMode : byte
    {
        Continuous = 0,
        Finite = 1
    }

    /// <summary>
    /// Available trigger sources.
    /// </summary>
    public enum TriggerSources : byte
    {
        Software = 0,
        Aux0Rising = 1,
        Aux0Falling = 2,
        Aux0WhileHigh = 3,
        Aux0WhileLow = 4,
        Aux1Rising = 16,
        Aux1Falling = 32,
        Aux1WhileHigh = 64,
        Aux1WhileLow = 128
    }

    /// <summary>
    /// The state of a configuration.
    /// </summary>
    public enum EnableFlag : byte
    {
        Disabled = 0,
        Enabled = 1
    }

    /// <summary>
    /// Specifies the available expansion boards implemented.
    /// </summary>
    public enum ExpansionBoardTypes : byte
    {
        Breakout = 0,
        Encoder = 1,
        ServoMotor1 = 2,
        ServoMotor2 = 3,
        ServoMotor3 = 4,
        OpticalFlow = 5
    }

    /// <summary>
    /// Specifies the sampling rate of the encoder.
    /// </summary>
    public enum EncoderSamplingRate : byte
    {
        At50Hz = 0,
        At00Hz = 1,
        At200Hz = 2,
        At250Hz = 3,
        At500Hz = 4,
        At1000Hz = 5
    }
}
