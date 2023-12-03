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
        /// Gets or sets the PWM protocol channels to configure.
        /// </summary>
        [Description("The PWM protocol channels to configure.")]
        public PwmChannels PwmChannels { get; set; }

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
            var mask = PwmChannels;
            while (mask != PwmChannels.None)
            {
                if ((mask & PwmChannels.Pwm0) != 0)
                {
                    pwmOffset = Pwm0Frequency.Address;
                    mask &= ~PwmChannels.Pwm0;
                }
                else if ((mask & PwmChannels.Pwm1) != 0)
                {
                    pwmOffset = Pwm1Frequency.Address;
                    mask &= ~PwmChannels.Pwm1;
                }
                else
                {
                    pwmOffset = Pwm2Frequency.Address;
                    mask = PwmChannels.None;
                }

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
}
