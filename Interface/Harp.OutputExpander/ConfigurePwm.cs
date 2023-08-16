using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using Bonsai.Harp;
using Bonsai;

namespace Harp.OutputExpander
{
    /// <summary>
    /// Represents an operator that generates a sequence of Harp messages to
    /// configure the PWM feature.
    /// </summary>
    [Description("Generates a sequence of Harp messages to configure the PWM feature.")]
    public class ConfigurePwm : Source<HarpMessage>
    {
        /// <summary>
        /// Gets or sets a value specifying which PWM digital outputs to configure.
        /// </summary>
        [Description("Specifies which PWM digital outputs to enable.")]
        public PwmOutputs Mask { get; set; } = PwmOutputs.PwmOutput1;

        /// <summary>
        /// Gets or sets the frequency of the PWM. The maximum frequency is 1000Hz.
        /// </summary>
        [Description("The frequency of the PWM. The maximum frequency is 1000Hz.")]
        public float Frequency { get; set; }

        /// <summary>
        /// Gets or sets the duty cycle of the PWM. The maximum value is 100%.
        /// </summary>
        [Description("The duty cycle of the PWM. The maximum value is 100%.")]
        public float DutyCycle { get; set; }

        /// <summary>
        /// Gets or sets the number of pulses to trigger on the specified PWM.
        /// If the default value of zero is specified, the PWM will be infinite.
        /// </summary>
        [Description("The number of pulses to trigger on the specified PWM. If the default value of zero is specified, the PWM will be infinite.")]
        public int PulseCount { get; set; }

        IEnumerable<HarpMessage> CreateMessageSequence()
        {
            byte pwmOffset;
            var mask = Mask;
            if (mask >= PwmOutputs.PwmOutput9) pwmOffset = Pwm2Frequency.Address;
            else if (mask >= PwmOutputs.PwmOutput6) pwmOffset = Pwm1Frequency.Address;
            else pwmOffset = Pwm0Frequency.Address;
            yield return PwmAndStimEnable.FromPayload(MessageType.Write, (PwmAndStimMappings)Mask);
            yield return HarpCommand.WriteSingle(pwmOffset + PwmFrequency, Frequency);
            yield return HarpCommand.WriteSingle(pwmOffset + PwmDutyCycle, DutyCycle);
            if (PulseCount > 0)
            {
                yield return HarpCommand.WriteUInt16(pwmOffset + PwmPulseCount, (ushort)PulseCount);
                yield return HarpCommand.WriteByte(pwmOffset + PwmAcquisitionMode, (byte)AcquisitionMode.Finite);
            }
            else yield return HarpCommand.WriteByte(pwmOffset + PwmAcquisitionMode, (byte)AcquisitionMode.Continuous);
            yield return HarpCommand.WriteByte(pwmOffset + PwmTriggerSource, (byte)TriggerSource.Software);
            yield return HarpCommand.WriteByte(pwmOffset + PwmEventConfig, (byte)EnableFlag.Enabled);
        }

        /// <summary>
        /// Generates an observable sequence of Harp messages to configure the
        /// PWM feature.
        /// </summary>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing the commands
        /// needed to fully configure the PWM feature.
        /// </returns>
        public override IObservable<HarpMessage> Generate()
        {
            return CreateMessageSequence().ToObservable();
        }

        /// <summary>
        /// Generates an observable sequence of Harp messages to configure the
        /// PWM feature whenever the source sequence emits a notification.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used to emit new configuration
        /// messages.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing the commands
        /// needed to fully configure the PWM feature.
        /// </returns>
        public IObservable<HarpMessage> Generate<TSource>(IObservable<TSource> source)
        {
            return source.SelectMany(input => CreateMessageSequence());
        }

        const int PwmFrequency = Pwm0Frequency.Address - Pwm0Frequency.Address;
        const int PwmDutyCycle = Pwm0DutyCycle.Address - Pwm0Frequency.Address;
        const int PwmPulseCount = Pwm0PulseCount.Address - Pwm0Frequency.Address;
        const int PwmAcquisitionMode = Pwm0AcquisitionMode.Address - Pwm0Frequency.Address;
        const int PwmTriggerSource = Pwm0TriggerSource.Address - Pwm0Frequency.Address;
        const int PwmEventConfig = Pwm0EventConfig.Address - Pwm0Frequency.Address;
    }

    /// <summary>
    /// Specifies the digital output lines available for PWM.
    /// </summary>
    [Flags]
    public enum PwmOutputs : ushort
    {
        None = PwmAndStimMappings.None,
        PwmOutput1 = PwmAndStimMappings.Pwm0ToOut1,
        PwmOutput2 = PwmAndStimMappings.Pwm0ToOut2,
        PwmOutput3 = PwmAndStimMappings.Pwm0ToOut3,
        PwmOutput6 = PwmAndStimMappings.Pwm1ToOut6,
        PwmOutput7 = PwmAndStimMappings.Pwm1ToOut7,
        PwmOutput8 = PwmAndStimMappings.Pwm1ToOut8,
        PwmOutput9 = PwmAndStimMappings.Pwm2ToOut9
    }
}
