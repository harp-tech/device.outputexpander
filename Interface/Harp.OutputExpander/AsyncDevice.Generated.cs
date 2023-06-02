using Bonsai.Harp;
using System.Threading.Tasks;

namespace Harp.OutputExpander
{
    /// <inheritdoc/>
    public partial class Device
    {
        /// <summary>
        /// Initializes a new instance of the asynchronous API to configure and interface
        /// with OutputExpander devices on the specified serial port.
        /// </summary>
        /// <param name="portName">
        /// The name of the serial port used to communicate with the Harp device.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous initialization operation. The value of
        /// the <see cref="Task{TResult}.Result"/> parameter contains a new instance of
        /// the <see cref="AsyncDevice"/> class.
        /// </returns>
        public static async Task<AsyncDevice> CreateAsync(string portName)
        {
            var device = new AsyncDevice(portName);
            var whoAmI = await device.ReadWhoAmIAsync();
            if (whoAmI != Device.WhoAmI)
            {
                var errorMessage = string.Format(
                    "The device ID {1} on {0} was unexpected. Check whether a OutputExpander device is connected to the specified serial port.",
                    portName, whoAmI);
                throw new HarpException(errorMessage);
            }

            return device;
        }
    }

    /// <summary>
    /// Represents an asynchronous API to configure and interface with OutputExpander devices.
    /// </summary>
    public partial class AsyncDevice : Bonsai.Harp.AsyncDevice
    {
        internal AsyncDevice(string portName)
            : base(portName)
        {
        }

        /// <summary>
        /// Asynchronously reads the contents of the AuxInState register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<AuxiliaryInputs> ReadAuxInStateAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(AuxInState.Address));
            return AuxInState.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the AuxInState register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<AuxiliaryInputs>> ReadTimestampedAuxInStateAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(AuxInState.Address));
            return AuxInState.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the contents of the AuxInRisingEdge register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<AuxiliaryInputs> ReadAuxInRisingEdgeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(AuxInRisingEdge.Address));
            return AuxInRisingEdge.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the AuxInRisingEdge register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<AuxiliaryInputs>> ReadTimestampedAuxInRisingEdgeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(AuxInRisingEdge.Address));
            return AuxInRisingEdge.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the AuxInRisingEdge register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteAuxInRisingEdgeAsync(AuxiliaryInputs value)
        {
            var request = AuxInRisingEdge.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the AuxInFallingEdge register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<AuxiliaryInputs> ReadAuxInFallingEdgeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(AuxInFallingEdge.Address));
            return AuxInFallingEdge.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the AuxInFallingEdge register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<AuxiliaryInputs>> ReadTimestampedAuxInFallingEdgeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(AuxInFallingEdge.Address));
            return AuxInFallingEdge.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the AuxInFallingEdge register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteAuxInFallingEdgeAsync(AuxiliaryInputs value)
        {
            var request = AuxInFallingEdge.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the OutputSet register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DigitalOutputs> ReadOutputSetAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(OutputSet.Address));
            return OutputSet.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the OutputSet register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DigitalOutputs>> ReadTimestampedOutputSetAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(OutputSet.Address));
            return OutputSet.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the OutputSet register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteOutputSetAsync(DigitalOutputs value)
        {
            var request = OutputSet.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the OutputClear register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DigitalOutputs> ReadOutputClearAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(OutputClear.Address));
            return OutputClear.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the OutputClear register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DigitalOutputs>> ReadTimestampedOutputClearAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(OutputClear.Address));
            return OutputClear.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the OutputClear register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteOutputClearAsync(DigitalOutputs value)
        {
            var request = OutputClear.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the OutputToggle register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DigitalOutputs> ReadOutputToggleAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(OutputToggle.Address));
            return OutputToggle.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the OutputToggle register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DigitalOutputs>> ReadTimestampedOutputToggleAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(OutputToggle.Address));
            return OutputToggle.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the OutputToggle register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteOutputToggleAsync(DigitalOutputs value)
        {
            var request = OutputToggle.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the OutputState register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DigitalOutputs> ReadOutputStateAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(OutputState.Address));
            return OutputState.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the OutputState register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DigitalOutputs>> ReadTimestampedOutputStateAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(OutputState.Address));
            return OutputState.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the OutputState register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteOutputStateAsync(DigitalOutputs value)
        {
            var request = OutputState.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the PwmAndStimEnable register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<PwmAndStimMapping> ReadPwmAndStimEnableAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PwmAndStimEnable.Address));
            return PwmAndStimEnable.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the PwmAndStimEnable register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<PwmAndStimMapping>> ReadTimestampedPwmAndStimEnableAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PwmAndStimEnable.Address));
            return PwmAndStimEnable.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the PwmAndStimEnable register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePwmAndStimEnableAsync(PwmAndStimMapping value)
        {
            var request = PwmAndStimEnable.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the PwmAndStimDisable register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<PwmAndStimMapping> ReadPwmAndStimDisableAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PwmAndStimDisable.Address));
            return PwmAndStimDisable.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the PwmAndStimDisable register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<PwmAndStimMapping>> ReadTimestampedPwmAndStimDisableAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PwmAndStimDisable.Address));
            return PwmAndStimDisable.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the PwmAndStimDisable register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePwmAndStimDisableAsync(PwmAndStimMapping value)
        {
            var request = PwmAndStimDisable.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the PwmAndStimState register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<PwmAndStimMapping> ReadPwmAndStimStateAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PwmAndStimState.Address));
            return PwmAndStimState.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the PwmAndStimState register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<PwmAndStimMapping>> ReadTimestampedPwmAndStimStateAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(PwmAndStimState.Address));
            return PwmAndStimState.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the PwmAndStimState register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePwmAndStimStateAsync(PwmAndStimMapping value)
        {
            var request = PwmAndStimState.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Pwm0Frequency register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<float> ReadPwm0FrequencyAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadSingle(Pwm0Frequency.Address));
            return Pwm0Frequency.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Pwm0Frequency register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<float>> ReadTimestampedPwm0FrequencyAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadSingle(Pwm0Frequency.Address));
            return Pwm0Frequency.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Pwm0Frequency register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePwm0FrequencyAsync(float value)
        {
            var request = Pwm0Frequency.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Pwm0DutyCycle register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<float> ReadPwm0DutyCycleAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadSingle(Pwm0DutyCycle.Address));
            return Pwm0DutyCycle.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Pwm0DutyCycle register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<float>> ReadTimestampedPwm0DutyCycleAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadSingle(Pwm0DutyCycle.Address));
            return Pwm0DutyCycle.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Pwm0DutyCycle register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePwm0DutyCycleAsync(float value)
        {
            var request = Pwm0DutyCycle.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Pwm0PulseCount register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadPwm0PulseCountAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Pwm0PulseCount.Address));
            return Pwm0PulseCount.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Pwm0PulseCount register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedPwm0PulseCountAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Pwm0PulseCount.Address));
            return Pwm0PulseCount.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Pwm0PulseCount register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePwm0PulseCountAsync(ushort value)
        {
            var request = Pwm0PulseCount.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Pwm0ActualFrequency register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<float> ReadPwm0ActualFrequencyAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadSingle(Pwm0ActualFrequency.Address));
            return Pwm0ActualFrequency.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Pwm0ActualFrequency register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<float>> ReadTimestampedPwm0ActualFrequencyAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadSingle(Pwm0ActualFrequency.Address));
            return Pwm0ActualFrequency.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Pwm0ActualDutyCycle register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<float> ReadPwm0ActualDutyCycleAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadSingle(Pwm0ActualDutyCycle.Address));
            return Pwm0ActualDutyCycle.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Pwm0ActualDutyCycle register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<float>> ReadTimestampedPwm0ActualDutyCycleAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadSingle(Pwm0ActualDutyCycle.Address));
            return Pwm0ActualDutyCycle.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Pwm0AcquisitionMode register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<AcquisitionMode> ReadPwm0AcquisitionModeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Pwm0AcquisitionMode.Address));
            return Pwm0AcquisitionMode.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Pwm0AcquisitionMode register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<AcquisitionMode>> ReadTimestampedPwm0AcquisitionModeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Pwm0AcquisitionMode.Address));
            return Pwm0AcquisitionMode.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Pwm0AcquisitionMode register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePwm0AcquisitionModeAsync(AcquisitionMode value)
        {
            var request = Pwm0AcquisitionMode.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Pwm0TriggerSource register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<TriggerSource> ReadPwm0TriggerSourceAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Pwm0TriggerSource.Address));
            return Pwm0TriggerSource.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Pwm0TriggerSource register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<TriggerSource>> ReadTimestampedPwm0TriggerSourceAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Pwm0TriggerSource.Address));
            return Pwm0TriggerSource.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Pwm0TriggerSource register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePwm0TriggerSourceAsync(TriggerSource value)
        {
            var request = Pwm0TriggerSource.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Pwm0EventConfig register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<EnableFlag> ReadPwm0EventConfigAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Pwm0EventConfig.Address));
            return Pwm0EventConfig.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Pwm0EventConfig register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<EnableFlag>> ReadTimestampedPwm0EventConfigAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Pwm0EventConfig.Address));
            return Pwm0EventConfig.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Pwm0EventConfig register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePwm0EventConfigAsync(EnableFlag value)
        {
            var request = Pwm0EventConfig.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Pwm1Frequency register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<float> ReadPwm1FrequencyAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadSingle(Pwm1Frequency.Address));
            return Pwm1Frequency.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Pwm1Frequency register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<float>> ReadTimestampedPwm1FrequencyAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadSingle(Pwm1Frequency.Address));
            return Pwm1Frequency.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Pwm1Frequency register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePwm1FrequencyAsync(float value)
        {
            var request = Pwm1Frequency.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Pwm1DutyCycle register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<float> ReadPwm1DutyCycleAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadSingle(Pwm1DutyCycle.Address));
            return Pwm1DutyCycle.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Pwm1DutyCycle register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<float>> ReadTimestampedPwm1DutyCycleAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadSingle(Pwm1DutyCycle.Address));
            return Pwm1DutyCycle.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Pwm1DutyCycle register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePwm1DutyCycleAsync(float value)
        {
            var request = Pwm1DutyCycle.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Pwm1PulseCount register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadPwm1PulseCountAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Pwm1PulseCount.Address));
            return Pwm1PulseCount.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Pwm1PulseCount register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedPwm1PulseCountAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Pwm1PulseCount.Address));
            return Pwm1PulseCount.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Pwm1PulseCount register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePwm1PulseCountAsync(ushort value)
        {
            var request = Pwm1PulseCount.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Pwm1ActualFrequency register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<float> ReadPwm1ActualFrequencyAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadSingle(Pwm1ActualFrequency.Address));
            return Pwm1ActualFrequency.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Pwm1ActualFrequency register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<float>> ReadTimestampedPwm1ActualFrequencyAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadSingle(Pwm1ActualFrequency.Address));
            return Pwm1ActualFrequency.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Pwm1ActualDutyCycle register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<float> ReadPwm1ActualDutyCycleAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadSingle(Pwm1ActualDutyCycle.Address));
            return Pwm1ActualDutyCycle.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Pwm1ActualDutyCycle register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<float>> ReadTimestampedPwm1ActualDutyCycleAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadSingle(Pwm1ActualDutyCycle.Address));
            return Pwm1ActualDutyCycle.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Pwm1AcquisitionMode register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<AcquisitionMode> ReadPwm1AcquisitionModeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Pwm1AcquisitionMode.Address));
            return Pwm1AcquisitionMode.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Pwm1AcquisitionMode register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<AcquisitionMode>> ReadTimestampedPwm1AcquisitionModeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Pwm1AcquisitionMode.Address));
            return Pwm1AcquisitionMode.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Pwm1AcquisitionMode register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePwm1AcquisitionModeAsync(AcquisitionMode value)
        {
            var request = Pwm1AcquisitionMode.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Pwm1TriggerSource register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<TriggerSource> ReadPwm1TriggerSourceAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Pwm1TriggerSource.Address));
            return Pwm1TriggerSource.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Pwm1TriggerSource register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<TriggerSource>> ReadTimestampedPwm1TriggerSourceAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Pwm1TriggerSource.Address));
            return Pwm1TriggerSource.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Pwm1TriggerSource register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePwm1TriggerSourceAsync(TriggerSource value)
        {
            var request = Pwm1TriggerSource.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Pwm1EventConfig register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<EnableFlag> ReadPwm1EventConfigAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Pwm1EventConfig.Address));
            return Pwm1EventConfig.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Pwm1EventConfig register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<EnableFlag>> ReadTimestampedPwm1EventConfigAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Pwm1EventConfig.Address));
            return Pwm1EventConfig.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Pwm1EventConfig register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePwm1EventConfigAsync(EnableFlag value)
        {
            var request = Pwm1EventConfig.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Pwm2Frequency register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<float> ReadPwm2FrequencyAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadSingle(Pwm2Frequency.Address));
            return Pwm2Frequency.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Pwm2Frequency register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<float>> ReadTimestampedPwm2FrequencyAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadSingle(Pwm2Frequency.Address));
            return Pwm2Frequency.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Pwm2Frequency register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePwm2FrequencyAsync(float value)
        {
            var request = Pwm2Frequency.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Pwm2DutyCycle register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<float> ReadPwm2DutyCycleAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadSingle(Pwm2DutyCycle.Address));
            return Pwm2DutyCycle.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Pwm2DutyCycle register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<float>> ReadTimestampedPwm2DutyCycleAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadSingle(Pwm2DutyCycle.Address));
            return Pwm2DutyCycle.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Pwm2DutyCycle register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePwm2DutyCycleAsync(float value)
        {
            var request = Pwm2DutyCycle.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Pwm2PulseCount register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadPwm2PulseCountAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Pwm2PulseCount.Address));
            return Pwm2PulseCount.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Pwm2PulseCount register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedPwm2PulseCountAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Pwm2PulseCount.Address));
            return Pwm2PulseCount.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Pwm2PulseCount register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePwm2PulseCountAsync(ushort value)
        {
            var request = Pwm2PulseCount.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Pwm2ActualFrequency register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<float> ReadPwm2ActualFrequencyAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadSingle(Pwm2ActualFrequency.Address));
            return Pwm2ActualFrequency.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Pwm2ActualFrequency register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<float>> ReadTimestampedPwm2ActualFrequencyAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadSingle(Pwm2ActualFrequency.Address));
            return Pwm2ActualFrequency.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Pwm2ActualDutyCycle register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<float> ReadPwm2ActualDutyCycleAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadSingle(Pwm2ActualDutyCycle.Address));
            return Pwm2ActualDutyCycle.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Pwm2ActualDutyCycle register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<float>> ReadTimestampedPwm2ActualDutyCycleAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadSingle(Pwm2ActualDutyCycle.Address));
            return Pwm2ActualDutyCycle.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Pwm2AcquisitionMode register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<AcquisitionMode> ReadPwm2AcquisitionModeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Pwm2AcquisitionMode.Address));
            return Pwm2AcquisitionMode.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Pwm2AcquisitionMode register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<AcquisitionMode>> ReadTimestampedPwm2AcquisitionModeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Pwm2AcquisitionMode.Address));
            return Pwm2AcquisitionMode.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Pwm2AcquisitionMode register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePwm2AcquisitionModeAsync(AcquisitionMode value)
        {
            var request = Pwm2AcquisitionMode.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Pwm2TriggerSource register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<TriggerSource> ReadPwm2TriggerSourceAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Pwm2TriggerSource.Address));
            return Pwm2TriggerSource.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Pwm2TriggerSource register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<TriggerSource>> ReadTimestampedPwm2TriggerSourceAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Pwm2TriggerSource.Address));
            return Pwm2TriggerSource.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Pwm2TriggerSource register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePwm2TriggerSourceAsync(TriggerSource value)
        {
            var request = Pwm2TriggerSource.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Pwm2EventConfig register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<EnableFlag> ReadPwm2EventConfigAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Pwm2EventConfig.Address));
            return Pwm2EventConfig.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Pwm2EventConfig register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<EnableFlag>> ReadTimestampedPwm2EventConfigAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Pwm2EventConfig.Address));
            return Pwm2EventConfig.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Pwm2EventConfig register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePwm2EventConfigAsync(EnableFlag value)
        {
            var request = Pwm2EventConfig.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the PwmStart register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<PwmChannels> ReadPwmStartAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(PwmStart.Address));
            return PwmStart.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the PwmStart register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<PwmChannels>> ReadTimestampedPwmStartAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(PwmStart.Address));
            return PwmStart.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the PwmStart register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePwmStartAsync(PwmChannels value)
        {
            var request = PwmStart.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the PwmStop register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<PwmChannels> ReadPwmStopAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(PwmStop.Address));
            return PwmStop.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the PwmStop register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<PwmChannels>> ReadTimestampedPwmStopAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(PwmStop.Address));
            return PwmStop.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the PwmStop register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePwmStopAsync(PwmChannels value)
        {
            var request = PwmStop.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the PwmRiseEvent register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<PwmChannels> ReadPwmRiseEventAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(PwmRiseEvent.Address));
            return PwmRiseEvent.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the PwmRiseEvent register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<PwmChannels>> ReadTimestampedPwmRiseEventAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(PwmRiseEvent.Address));
            return PwmRiseEvent.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the PwmRiseEvent register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WritePwmRiseEventAsync(PwmChannels value)
        {
            var request = PwmRiseEvent.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Stim0PulseOnTime register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadStim0PulseOnTimeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Stim0PulseOnTime.Address));
            return Stim0PulseOnTime.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Stim0PulseOnTime register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedStim0PulseOnTimeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Stim0PulseOnTime.Address));
            return Stim0PulseOnTime.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Stim0PulseOnTime register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteStim0PulseOnTimeAsync(ushort value)
        {
            var request = Stim0PulseOnTime.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Stim0PulseOffTime register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadStim0PulseOffTimeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Stim0PulseOffTime.Address));
            return Stim0PulseOffTime.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Stim0PulseOffTime register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedStim0PulseOffTimeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Stim0PulseOffTime.Address));
            return Stim0PulseOffTime.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Stim0PulseOffTime register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteStim0PulseOffTimeAsync(ushort value)
        {
            var request = Stim0PulseOffTime.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Stim0PulseCount register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadStim0PulseCountAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Stim0PulseCount.Address));
            return Stim0PulseCount.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Stim0PulseCount register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedStim0PulseCountAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Stim0PulseCount.Address));
            return Stim0PulseCount.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Stim0PulseCount register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteStim0PulseCountAsync(ushort value)
        {
            var request = Stim0PulseCount.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Stim0AcquisitionMode register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<AcquisitionMode> ReadStim0AcquisitionModeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Stim0AcquisitionMode.Address));
            return Stim0AcquisitionMode.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Stim0AcquisitionMode register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<AcquisitionMode>> ReadTimestampedStim0AcquisitionModeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Stim0AcquisitionMode.Address));
            return Stim0AcquisitionMode.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Stim0AcquisitionMode register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteStim0AcquisitionModeAsync(AcquisitionMode value)
        {
            var request = Stim0AcquisitionMode.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Stim0TriggerSource register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<TriggerSource> ReadStim0TriggerSourceAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Stim0TriggerSource.Address));
            return Stim0TriggerSource.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Stim0TriggerSource register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<TriggerSource>> ReadTimestampedStim0TriggerSourceAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(Stim0TriggerSource.Address));
            return Stim0TriggerSource.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Stim0TriggerSource register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteStim0TriggerSourceAsync(TriggerSource value)
        {
            var request = Stim0TriggerSource.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the StimStart register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<StimChannels> ReadStimStartAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(StimStart.Address));
            return StimStart.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the StimStart register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<StimChannels>> ReadTimestampedStimStartAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(StimStart.Address));
            return StimStart.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the StimStart register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteStimStartAsync(StimChannels value)
        {
            var request = StimStart.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the StimStop register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<StimChannels> ReadStimStopAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(StimStop.Address));
            return StimStop.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the StimStop register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<StimChannels>> ReadTimestampedStimStopAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(StimStop.Address));
            return StimStop.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the StimStop register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteStimStopAsync(StimChannels value)
        {
            var request = StimStop.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the OutputPulse register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DigitalOutputs> ReadOutputPulseAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(OutputPulse.Address));
            return OutputPulse.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the OutputPulse register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DigitalOutputs>> ReadTimestampedOutputPulseAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(OutputPulse.Address));
            return OutputPulse.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the OutputPulse register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteOutputPulseAsync(DigitalOutputs value)
        {
            var request = OutputPulse.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Out0PulseWidth register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadOut0PulseWidthAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Out0PulseWidth.Address));
            return Out0PulseWidth.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Out0PulseWidth register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedOut0PulseWidthAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Out0PulseWidth.Address));
            return Out0PulseWidth.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Out0PulseWidth register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteOut0PulseWidthAsync(ushort value)
        {
            var request = Out0PulseWidth.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Out1PulseWidth register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadOut1PulseWidthAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Out1PulseWidth.Address));
            return Out1PulseWidth.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Out1PulseWidth register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedOut1PulseWidthAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Out1PulseWidth.Address));
            return Out1PulseWidth.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Out1PulseWidth register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteOut1PulseWidthAsync(ushort value)
        {
            var request = Out1PulseWidth.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Out2PulseWidth register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadOut2PulseWidthAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Out2PulseWidth.Address));
            return Out2PulseWidth.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Out2PulseWidth register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedOut2PulseWidthAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Out2PulseWidth.Address));
            return Out2PulseWidth.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Out2PulseWidth register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteOut2PulseWidthAsync(ushort value)
        {
            var request = Out2PulseWidth.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Out3PulseWidth register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadOut3PulseWidthAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Out3PulseWidth.Address));
            return Out3PulseWidth.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Out3PulseWidth register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedOut3PulseWidthAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Out3PulseWidth.Address));
            return Out3PulseWidth.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Out3PulseWidth register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteOut3PulseWidthAsync(ushort value)
        {
            var request = Out3PulseWidth.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Out4PulseWidth register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadOut4PulseWidthAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Out4PulseWidth.Address));
            return Out4PulseWidth.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Out4PulseWidth register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedOut4PulseWidthAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Out4PulseWidth.Address));
            return Out4PulseWidth.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Out4PulseWidth register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteOut4PulseWidthAsync(ushort value)
        {
            var request = Out4PulseWidth.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Out5PulseWidth register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadOut5PulseWidthAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Out5PulseWidth.Address));
            return Out5PulseWidth.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Out5PulseWidth register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedOut5PulseWidthAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Out5PulseWidth.Address));
            return Out5PulseWidth.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Out5PulseWidth register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteOut5PulseWidthAsync(ushort value)
        {
            var request = Out5PulseWidth.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Out6PulseWidth register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadOut6PulseWidthAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Out6PulseWidth.Address));
            return Out6PulseWidth.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Out6PulseWidth register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedOut6PulseWidthAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Out6PulseWidth.Address));
            return Out6PulseWidth.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Out6PulseWidth register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteOut6PulseWidthAsync(ushort value)
        {
            var request = Out6PulseWidth.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Out7PulseWidth register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadOut7PulseWidthAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Out7PulseWidth.Address));
            return Out7PulseWidth.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Out7PulseWidth register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedOut7PulseWidthAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Out7PulseWidth.Address));
            return Out7PulseWidth.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Out7PulseWidth register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteOut7PulseWidthAsync(ushort value)
        {
            var request = Out7PulseWidth.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Out8PulseWidth register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadOut8PulseWidthAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Out8PulseWidth.Address));
            return Out8PulseWidth.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Out8PulseWidth register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedOut8PulseWidthAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Out8PulseWidth.Address));
            return Out8PulseWidth.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Out8PulseWidth register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteOut8PulseWidthAsync(ushort value)
        {
            var request = Out8PulseWidth.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Out9PulseWidth register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadOut9PulseWidthAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Out9PulseWidth.Address));
            return Out9PulseWidth.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Out9PulseWidth register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedOut9PulseWidthAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Out9PulseWidth.Address));
            return Out9PulseWidth.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Out9PulseWidth register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteOut9PulseWidthAsync(ushort value)
        {
            var request = Out9PulseWidth.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the ExpansionBoard register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ExpansionBoardType> ReadExpansionBoardAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(ExpansionBoard.Address));
            return ExpansionBoard.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the ExpansionBoard register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ExpansionBoardType>> ReadTimestampedExpansionBoardAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(ExpansionBoard.Address));
            return ExpansionBoard.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the ExpansionBoard register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteExpansionBoardAsync(ExpansionBoardType value)
        {
            var request = ExpansionBoard.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Encoder register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadEncoderAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Encoder.Address));
            return Encoder.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Encoder register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedEncoderAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Encoder.Address));
            return Encoder.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the contents of the EncoderSamplingRate register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<EncoderSamplingRateMode> ReadEncoderSamplingRateAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(EncoderSamplingRate.Address));
            return EncoderSamplingRate.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the EncoderSamplingRate register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<EncoderSamplingRateMode>> ReadTimestampedEncoderSamplingRateAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(EncoderSamplingRate.Address));
            return EncoderSamplingRate.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the EncoderSamplingRate register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteEncoderSamplingRateAsync(EncoderSamplingRateMode value)
        {
            var request = EncoderSamplingRate.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the ServoPeriod register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadServoPeriodAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(ServoPeriod.Address));
            return ServoPeriod.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the ServoPeriod register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedServoPeriodAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(ServoPeriod.Address));
            return ServoPeriod.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the ServoPeriod register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteServoPeriodAsync(ushort value)
        {
            var request = ServoPeriod.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Servo0PulseWidth register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadServo0PulseWidthAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Servo0PulseWidth.Address));
            return Servo0PulseWidth.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Servo0PulseWidth register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedServo0PulseWidthAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Servo0PulseWidth.Address));
            return Servo0PulseWidth.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Servo0PulseWidth register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteServo0PulseWidthAsync(ushort value)
        {
            var request = Servo0PulseWidth.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Servo1PulseWidth register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadServo1PulseWidthAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Servo1PulseWidth.Address));
            return Servo1PulseWidth.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Servo1PulseWidth register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedServo1PulseWidthAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Servo1PulseWidth.Address));
            return Servo1PulseWidth.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Servo1PulseWidth register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteServo1PulseWidthAsync(ushort value)
        {
            var request = Servo1PulseWidth.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the Servo2PulseWidth register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadServo2PulseWidthAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Servo2PulseWidth.Address));
            return Servo2PulseWidth.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the Servo2PulseWidth register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedServo2PulseWidthAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(Servo2PulseWidth.Address));
            return Servo2PulseWidth.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the Servo2PulseWidth register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteServo2PulseWidthAsync(ushort value)
        {
            var request = Servo2PulseWidth.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the OpticalFlow register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<short> ReadOpticalFlowAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadInt16(OpticalFlow.Address));
            return OpticalFlow.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the OpticalFlow register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<short>> ReadTimestampedOpticalFlowAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadInt16(OpticalFlow.Address));
            return OpticalFlow.GetTimestampedPayload(reply);
        }
    }
}
