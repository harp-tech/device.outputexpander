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
    /// configure the magnetic encoder expansion.
    /// </summary>
    [Description("Generates a sequence of Harp messages to configure the magnetic encoder in OutputExpander devices.")]
    public class ConfigureMagneticEncoder : Source<HarpMessage>
    {
        /// <summary>
        /// Gets or sets a value specifying the sampling rate of the magnetic encoder.
        /// </summary>
        [Description("Specifies the sampling rate of the magnetic encoder.")]
        public MagneticEncoderSampleRateMode SampleRate { get; set; } = MagneticEncoderSampleRateMode.SampleRate1000Hz;

        IEnumerable<HarpMessage> CreateMessageSequence()
        {
            yield return ExpansionBoard.FromPayload(MessageType.Write, ExpansionBoardType.MagneticEncoder);
            yield return MagneticEncoderSampleRate.FromPayload(MessageType.Write, SampleRate);
        }

        /// <summary>
        /// Generates an observable sequence of Harp messages to configure the
        /// magnetic encoder expansion.
        /// </summary>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing the commands
        /// needed to fully configure the magnetic encoder expansion.
        /// </returns>
        public override IObservable<HarpMessage> Generate()
        {
            return CreateMessageSequence().ToObservable();
        }

        /// <summary>
        /// Generates an observable sequence of Harp messages to configure the
        /// magnetic encoder expansion whenever the source sequence emits a notification.
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
        /// needed to fully configure the magnetic encoder expansion.
        /// </returns>
        public IObservable<HarpMessage> Generate<TSource>(IObservable<TSource> source)
        {
            return source.SelectMany(input => CreateMessageSequence());
        }
    }
}
