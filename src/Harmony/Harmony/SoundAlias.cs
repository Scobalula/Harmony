//  ------------------------------------------------------------------------------------
//  Copyright(c) 2020 Philip/Scobalula
//  ------------------------------------------------------------------------------------
//  Permission is hereby granted, free of charge, to any person obtaining a copy of this
//  software and associated documentation files (the "Software"), to deal in the Software
//  without restriction, including without limitation the rights to use, copy, modify,
//  merge, publish, distribute, sublicense, and/or sell copies of the Software, and to
//  permit persons to whom the Software is furnished to do so, subject to the following
//  conditions:
//  ------------------------------------------------------------------------------------
//  The above copyright notice and this permission notice shall be included in all copies
//  or substantial portions of the Software.
//  ------------------------------------------------------------------------------------
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
//  INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A
//  PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
//  HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
//  CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE
//  OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//  ------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Harmony
{
    /// <summary>
    /// Native Sound Value Types
    /// </summary>
    internal enum NativeSoundValueType
    {
        String      = 0x0,
        Float       = 0x1,
        Int         = 0x2,
        Enum        = 0x3,
        Flag        = 0x4,
        UShortDBSPL = 0x5,
        FloatDBSPL  = 0x6,
        Cents       = 0x7,
        Hash        = 0x8,
        EnumBits    = 0x9,
        FlagBits    = 0xA,
        Byte        = 0xB,
        EnumByte    = 0xC,
        Short       = 0xD,
        UShort      = 0xE,
        NormByte    = 0xF,
        Distance    = 0x10,
    }

    /// <summary>
    /// Class to hold info on native Sound structures
    /// </summary>
    internal class NativeSoundValueAttribute : Attribute
    {
        /// <summary>
        /// Gets or Sets the CSV Header Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets the offset within the native Sound Alias structure
        /// </summary>
        public int Offset { get; set; }

        /// <summary>
        /// Gets or Sets the native Sound value type
        /// </summary>
        public NativeSoundValueType Type { get; set; }

        /// <summary>
        /// Gets or Sets if this value
        /// </summary>
        public bool Editable { get; set; }

        /// <summary>
        /// Gets or Sets the minimum value (or enum value)
        /// </summary>
        public double Minimum { get; set; }

        /// <summary>
        /// Gets or Sets the maximum value (or enum value)
        /// </summary>
        public double Maximum { get; set; }

        /// <summary>
        /// Gets or Sets the default value
        /// </summary>
        public double Default { get; set; }

        /// <summary>
        /// Gets or Sets the array name (for enums/types)
        /// </summary>
        public string ArrayName { get; set; }

    }

    /// <summary>
    /// A class to hold a Sound Alias
    /// </summary>
    internal class SoundAlias
    {
        /// <summary>
        /// Gets the Sound Alias Properties
        /// </summary>
        public static Dictionary<string, PropertyInfo> Properties { get; } = new Dictionary<string, PropertyInfo>();

        /// <summary>
        /// The size of the native sound alias structure
        /// </summary>
        public const int SizeOfStructure = 216;

        /// <summary>
        /// Gets or Sets the Address of this Entry
        /// </summary>
        private long Address { get; set; }

        /// <summary>
        /// Gets or Sets the Parent Sound Bank
        /// </summary>
        private SoundBank Bank { get; set; }

        /// <summary>
        /// Gets or Sets the Buffer
        /// </summary>
        private byte[] Buffer { get; set; }

        /// <summary>
        /// Gets or Sets the Value for this Alias Column
        /// </summary>
        [NativeSoundValue(Name = "Name", Offset = 000, Type = NativeSoundValueType.String)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets the Value for this Alias Column
        /// </summary>
        [NativeSoundValue(Name = "Subtitle", Offset = 016, Type = NativeSoundValueType.String)]
        public string Subtitle { get; set; }

        /// <summary>
        /// Gets or Sets the Value for this Alias Column
        /// </summary>
        [NativeSoundValue(Name = "Secondary", Offset = 024, Type = NativeSoundValueType.String)]
        public string Secondary { get; set; }

        /// <summary>
        /// Gets or Sets the Value for this Alias Column
        /// </summary>
        [NativeSoundValue(Name = "StopAlias", Offset = 040, Type = NativeSoundValueType.String)]
        public string StopAlias { get; set; }

        /// <summary>
        /// Gets or Sets the Value for this Alias Column
        /// </summary>
        [NativeSoundValue(Name = "Looping", Offset = 104, Type = NativeSoundValueType.EnumBits, Minimum = 1, Maximum = 0, Editable = true, ArrayName = "Looping")]
        public string Looping { get; set; }

        /// <summary>
        /// Gets or Sets the Value for this Alias Column
        /// </summary>
        [NativeSoundValue(Name = "PanType", Offset = 104, Type = NativeSoundValueType.EnumBits, Minimum = 1, Maximum = 1, Editable = true, ArrayName = "PanType")]
        public string PanType { get; set; }

        /// <summary>
        /// Gets or Sets the Value for this Alias Column
        /// </summary>
        [NativeSoundValue(Name = "SilentInCPZ", Offset = 104, Type = NativeSoundValueType.EnumBits, Minimum = 1, Maximum = 2, Editable = true, ArrayName = "Bool")]
        public string SilentInCPZ { get; set; }

        /// <summary>
        /// Gets or Sets the Value for this Alias Column
        /// </summary>
        [NativeSoundValue(Name = "ContextFailsafe", Offset = 104, Type = NativeSoundValueType.EnumBits, Minimum = 1, Maximum = 3, Editable = true, ArrayName = "Bool")]
        public string ContextFailsafe { get; set; }

        /// <summary>
        /// Gets or Sets the Value for this Alias Column
        /// </summary>
        [NativeSoundValue(Name = "DistanceLpf", Offset = 104, Type = NativeSoundValueType.EnumBits, Minimum = 1, Maximum = 4, Editable = true, ArrayName = "Bool")]
        public string DistanceLpf { get; set; }

        /// <summary>
        /// Gets or Sets the Value for this Alias Column
        /// </summary>
        [NativeSoundValue(Name = "Doppler", Offset = 104, Type = NativeSoundValueType.EnumBits, Minimum = 1, Maximum = 5, Editable = true, ArrayName = "Bool")]
        public string Doppler { get; set; }

        /// <summary>
        /// Gets or Sets the Value for this Alias Column
        /// </summary>
        [NativeSoundValue(Name = "Pauseable", Offset = 104, Type = NativeSoundValueType.EnumBits, Minimum = 1, Maximum = 6, Editable = true, ArrayName = "Bool")]
        public string Pauseable { get; set; }

        /// <summary>
        /// Gets or Sets the Value for this Alias Column
        /// </summary>
        [NativeSoundValue(Name = "IsMusic", Offset = 104, Type = NativeSoundValueType.EnumBits, Minimum = 1, Maximum = 7, Editable = true, ArrayName = "Bool")]
        public string IsMusic { get; set; }

        /// <summary>
        /// Gets or Sets the Value for this Alias Column
        /// </summary>
        [NativeSoundValue(Name = "StopOnEntDeath", Offset = 104, Type = NativeSoundValueType.EnumBits, Minimum = 1, Maximum = 8, Editable = true, ArrayName = "Bool")]
        public string StopOnEntDeath { get; set; }

        /// <summary>
        /// Gets or Sets the Value for this Alias Column
        /// </summary>
        [NativeSoundValue(Name = "Timescale", Offset = 104, Type = NativeSoundValueType.EnumBits, Minimum = 1, Maximum = 9, Editable = true, ArrayName = "Bool")]
        public string Timescale { get; set; }

        /// <summary>
        /// Gets or Sets the Value for this Alias Column
        /// </summary>
        [NativeSoundValue(Name = "VoiceLimit", Offset = 104, Type = NativeSoundValueType.EnumBits, Minimum = 1, Maximum = 10, Editable = true, ArrayName = "Bool")]
        public string VoiceLimit { get; set; }

        /// <summary>
        /// Gets or Sets the Value for this Alias Column
        /// </summary>
        [NativeSoundValue(Name = "IgnoreMaxDist", Offset = 104, Type = NativeSoundValueType.EnumBits, Minimum = 1, Maximum = 11, Editable = true, ArrayName = "Bool")]
        public string IgnoreMaxDist { get; set; }

        /// <summary>
        /// Gets or Sets the Value for this Alias Column
        /// </summary>
        [NativeSoundValue(Name = "Storage", Offset = 104, Type = NativeSoundValueType.EnumBits, Minimum = 2, Maximum = 12, Editable = true, ArrayName = "Storage")]
        public string Storage { get; set; }

        /// <summary>
        /// Gets or Sets the Value for this Alias Column
        /// </summary>
        [NativeSoundValue(Name = "FluxType", Offset = 104, Type = NativeSoundValueType.EnumBits, Minimum = 4, Maximum = 14, Editable = true, ArrayName = "FluxType")]
        public string FluxType { get; set; }

        /// <summary>
        /// Gets or Sets the Value for this Alias Column
        /// </summary>
        [NativeSoundValue(Name = "RandomizeType", Offset = 104, Type = NativeSoundValueType.EnumBits, Minimum = 3, Maximum = 22, Editable = true, ArrayName = "RandomizeType")]
        public string RandomizeType { get; set; }

        /// <summary>
        /// Gets or Sets the Value for this Alias Column
        /// </summary>
        [NativeSoundValue(Name = "LimitType", Offset = 104, Type = NativeSoundValueType.EnumBits, Minimum = 2, Maximum = 18, Editable = true, ArrayName = "LimitType")]
        public string LimitType { get; set; }

        /// <summary>
        /// Gets or Sets the Value for this Alias Column
        /// </summary>
        [NativeSoundValue(Name = "EntityLimitType", Offset = 104, Type = NativeSoundValueType.EnumBits, Minimum = 2, Maximum = 20, Editable = true, ArrayName = "LimitType")]
        public string EntityLimitType { get; set; }

        /// <summary>
        /// Gets or Sets the Value for this Alias Column
        /// </summary>
        [NativeSoundValue(Name = "IsCinematic", Offset = 104, Type = NativeSoundValueType.EnumBits, Minimum = 1, Maximum = 27, Editable = true, ArrayName = "Bool")]
        public string IsCinematic { get; set; }

        /// <summary>
        /// Gets or Sets the Value for this Alias Column
        /// </summary>
        [NativeSoundValue(Name = "IsBig", Offset = 104, Type = NativeSoundValueType.EnumBits, Minimum = 1, Maximum = 28, Editable = true, ArrayName = "Bool")]
        public string IsBig { get; set; }

        /// <summary>
        /// Gets or Sets the Value for this Alias Column
        /// </summary>
        [NativeSoundValue(Name = "AmplitudePriority", Offset = 104, Type = NativeSoundValueType.EnumBits, Minimum = 1, Maximum = 28, Editable = true, ArrayName = "Bool")]
        public string AmplitudePriority { get; set; }

        /// <summary>
        /// Gets or Sets the Value for this Alias Column
        /// </summary>
        [NativeSoundValue(Name = "ContinuousPan", Offset = 104, Type = NativeSoundValueType.EnumBits, Minimum = 1, Maximum = 30, Editable = true, ArrayName = "Bool")]
        public string ContinuousPan { get; set; }

        /// <summary>
        /// Gets or Sets the Value for this Alias Column
        /// </summary>
        [NativeSoundValue(Name = "RestartContextLoops", Offset = 104, Type = NativeSoundValueType.EnumBits, Minimum = 1, Maximum = 31, Editable = true, ArrayName = "Bool")]
        public string RestartContextLoops { get; set; }

        /// <summary>
        /// Gets or Sets the Value for this Alias Column
        /// </summary>
        [NativeSoundValue(Name = "DryMinCurve", Offset = 108, Type = NativeSoundValueType.EnumBits, Minimum = 6, Maximum = 14, Editable = true, ArrayName = "Curve")]
        public string DryMinCurve { get; set; }

        /// <summary>
        /// Gets or Sets the Value for this Alias Column
        /// </summary>
        [NativeSoundValue(Name = "DryMaxCurve", Offset = 108, Type = NativeSoundValueType.EnumBits, Minimum = 6, Maximum = 2, Editable = true, ArrayName = "Curve")]
        public string DryMaxCurve { get; set; }

        /// <summary>
        /// Gets or Sets the Value for this Alias Column
        /// </summary>
        [NativeSoundValue(Name = "WetMinCurve", Offset = 108, Type = NativeSoundValueType.EnumBits, Minimum = 6, Maximum = 20, Editable = true, ArrayName = "Curve")]
        public string WetMinCurve { get; set; }

        /// <summary>
        /// Gets or Sets the Value for this Alias Column
        /// </summary>
        [NativeSoundValue(Name = "WetMaxCurve", Offset = 108, Type = NativeSoundValueType.EnumBits, Minimum = 6, Maximum = 8, Editable = true, ArrayName = "Curve")]
        public string WetMaxCurve { get; set; }

        /// <summary>
        /// Gets or Sets the Value for this Alias Column
        /// </summary>
        [NativeSoundValue(Name = "NeverPlayTwice", Offset = 108, Type = NativeSoundValueType.EnumBits, Minimum = 1, Maximum = 0, Editable = true, ArrayName = "Bool")]
        public string NeverPlayTwice { get; set; }

        /// <summary>
        /// Gets or Sets the Value for this Alias Column
        /// </summary>
        [NativeSoundValue(Name = "Duck", Offset = 112, Type = NativeSoundValueType.Hash)]
        public string Duck { get; set; }

        /// <summary>
        /// Gets or Sets the Value for this Alias Column
        /// </summary>
        [NativeSoundValue(Name = "ContextType", Offset = 116, Type = NativeSoundValueType.Hash)]
        public string ContextType { get; set; }

        /// <summary>
        /// Gets or Sets the Value for this Alias Column
        /// </summary>
        [NativeSoundValue(Name = "ContextValue", Offset = 120, Type = NativeSoundValueType.Hash)]
        public string ContextValue { get; set; }

        /// <summary>
        /// Gets or Sets the Value for this Alias Column
        /// </summary>
        [NativeSoundValue(Name = "ContextType1", Offset = 124, Type = NativeSoundValueType.Hash)]
        public string ContextType1 { get; set; }

        /// <summary>
        /// Gets or Sets the Value for this Alias Column
        /// </summary>
        [NativeSoundValue(Name = "ContextValue1", Offset = 128, Type = NativeSoundValueType.Hash)]
        public string ContextValue1 { get; set; }

        /// <summary>
        /// Gets or Sets the Value for this Alias Column
        /// </summary>
        [NativeSoundValue(Name = "ContextType2", Offset = 132, Type = NativeSoundValueType.Hash)]
        public string ContextType2 { get; set; }

        /// <summary>
        /// Gets or Sets the Value for this Alias Column
        /// </summary>
        [NativeSoundValue(Name = "ContextValu2", Offset = 136, Type = NativeSoundValueType.Hash)]
        public string ContextValu2 { get; set; }

        /// <summary>
        /// Gets or Sets the Value for this Alias Column
        /// </summary>
        [NativeSoundValue(Name = "ContextType3", Offset = 140, Type = NativeSoundValueType.Hash)]
        public string ContextType3 { get; set; }

        /// <summary>
        /// Gets or Sets the Value for this Alias Column
        /// </summary>
        [NativeSoundValue(Name = "ContextValue3", Offset = 144, Type = NativeSoundValueType.Hash)]
        public string ContextValue3 { get; set; }

        /// <summary>
        /// Gets or Sets the Value for this Alias Column
        /// </summary>
        [NativeSoundValue(Name = "StopOnPlay", Offset = 148, Type = NativeSoundValueType.Hash)]
        public string StopOnPlay { get; set; }

        /// <summary>
        /// Gets or Sets the Value for this Alias Column
        /// </summary>
        [NativeSoundValue(Name = "FutzPatch", Offset = 152, Type = NativeSoundValueType.Hash)]
        public string FutzPatch { get; set; }

        /// <summary>
        /// Gets or Sets the Value for this Alias Column
        /// </summary>
        [NativeSoundValue(Name = "ReverbSend", Offset = 156, Type = NativeSoundValueType.FloatDBSPL, Minimum = 0, Maximum = 100, Editable = true)]
        public double? ReverbSend { get; set; }

        /// <summary>
        /// Gets or Sets the Value for this Alias Column
        /// </summary>
        [NativeSoundValue(Name = "CenterSend", Offset = 160, Type = NativeSoundValueType.FloatDBSPL, Minimum = 0, Maximum = 100, Editable = true)]
        public double? CenterSend { get; set; }

        /// <summary>
        /// Gets or Sets the Value for this Alias Column
        /// </summary>
        [NativeSoundValue(Name = "VolMin", Offset = 164, Type = NativeSoundValueType.FloatDBSPL, Minimum=0, Maximum = 100, Editable = true)]
        public double? VolMin { get; set; }

        /// <summary>
        /// Gets or Sets the Value for this Alias Column
        /// </summary>
        [NativeSoundValue(Name = "VolMax", Offset = 168, Type = NativeSoundValueType.FloatDBSPL, Minimum = 0, Maximum = 100, Editable = true)]
        public double? VolMax { get; set; }

        /// <summary>
        /// Gets or Sets the Value for this Alias Column
        /// </summary>
        [NativeSoundValue(Name = "EnvelopPercent", Offset = 172, Type = NativeSoundValueType.FloatDBSPL, Minimum = 0, Maximum = 100, Editable = true)]
        public double? EnvelopPercent { get; set; }

        /// <summary>
        /// Gets or Sets the Value for this Alias Column
        /// </summary>
        [NativeSoundValue(Name = "FluxTime", Offset = 176, Type = NativeSoundValueType.UShort, Minimum = 0, Maximum = 65535, Editable = true)]
        public double? FluxTime { get; set; }

        /// <summary>
        /// Gets or Sets the Value for this Alias Column
        /// </summary>
        [NativeSoundValue(Name = "StartDelay", Offset = 178, Type = NativeSoundValueType.UShort, Minimum = 0, Maximum = 65535, Editable = true)]
        public double? StartDelay { get; set; }

        /// <summary>
        /// Gets or Sets the Value for this Alias Column
        /// </summary>
        [NativeSoundValue(Name = "PitchMin", Offset = 180, Type = NativeSoundValueType.Cents, Minimum = -2400, Maximum = 1200, Editable = true)]
        public double? PitchMin { get; set; }

        /// <summary>
        /// Gets or Sets the Value for this Alias Column
        /// </summary>
        [NativeSoundValue(Name = "PitchMax", Offset = 182, Type = NativeSoundValueType.Cents, Minimum = -2400, Maximum = 1200, Editable = true)]
        public double? PitchMax { get; set; }

        /// <summary>
        /// Gets or Sets the Value for this Alias Column
        /// </summary>
        [NativeSoundValue(Name = "DistMin", Offset = 184, Type = NativeSoundValueType.Distance, Minimum = 0, Maximum = 131070, Editable = true)]
        public double? DistMin { get; set; }

        /// <summary>
        /// Gets or Sets the Value for this Alias Column
        /// </summary>
        [NativeSoundValue(Name = "DistMaxDry", Offset = 186, Type = NativeSoundValueType.Distance, Minimum = 0, Maximum = 131070, Editable = true)]
        public double? DistMaxDry { get; set; }

        /// <summary>
        /// Gets or Sets the Value for this Alias Column
        /// </summary
        [NativeSoundValue(Name = "DistMaxWet", Offset = 188, Type = NativeSoundValueType.Distance, Minimum = 0, Maximum = 131070, Editable = true)]
        public double? DistMaxWet { get; set; }

        /// <summary>
        /// Gets or Sets the Value for this Alias Column
        /// </summary
        [NativeSoundValue(Name = "EnvelopMin", Offset = 190, Type = NativeSoundValueType.Distance, Minimum = 0, Maximum = 131070, Editable = true)]
        public double? EnvelopMin { get; set; }

        /// <summary>
        /// Gets or Sets the Value for this Alias Column
        /// </summary>
        [NativeSoundValue(Name = "EnvelopMax", Offset = 192, Type = NativeSoundValueType.Distance, Minimum = 0, Maximum = 131070, Editable = true)]
        public double? EnvelopMax { get; set; }

        /// <summary>
        /// Gets or Sets the Value for this Alias Column
        /// </summary
        [NativeSoundValue(Name = "FadeIn", Offset = 202, Type = NativeSoundValueType.Short, Minimum = 0, Maximum = 32768, Editable = true)]
        public double? FadeIn { get; set; }

        /// <summary>
        /// Gets or Sets the Value for this Alias Column
        /// </summary>
        [NativeSoundValue(Name = "FadeOut", Offset = 202, Type = NativeSoundValueType.Short, Minimum = 0, Maximum = 32768, Editable = true)]
        public double? FadeOut { get; set; }

        /// <summary>
        /// Gets or Sets the Value for this Alias Column
        /// </summary>
        [NativeSoundValue(Name = "DopplerScale", Offset = 202, Type = NativeSoundValueType.Short, Minimum = -100, Maximum = 100, Editable = true)]
        public double? DopplerScale { get; set; }

        /// <summary>
        /// Gets or Sets the Value for this Alias Column
        /// </summary>
        [NativeSoundValue(Name = "PriorityThresholdMin", Offset = 204, Type = NativeSoundValueType.NormByte, Minimum = 0, Maximum = 1, Editable = true)]
        public double? PriorityThresholdMin { get; set; }

        /// <summary>
        /// Gets or Sets the Value for this Alias Column
        /// </summary>
        [NativeSoundValue(Name = "PriorityThresholdMax", Offset = 205, Type = NativeSoundValueType.NormByte, Minimum = 0, Maximum = 1, Editable = true)]
        public double? PriorityThresholdMax { get; set; }

        /// <summary>
        /// Gets or Sets the Value for this Alias Column
        /// </summary
        [NativeSoundValue(Name = "Probability", Offset = 206, Type = NativeSoundValueType.NormByte, Minimum = 0, Maximum = 1, Editable = true)]
        public double? Probability { get; set; }

        /// <summary>
        /// Gets or Sets the Value for this Alias Column
        /// </summary>
        [NativeSoundValue(Name = "PriorityMin", Offset = 208, Type = NativeSoundValueType.Byte, Minimum = 0, Maximum = 128, Editable = true)]
        public double? PriorityMin { get; set; }

        /// <summary>
        /// Gets or Sets the Value for this Alias Column
        /// </summary>
        [NativeSoundValue(Name = "PriorityMax", Offset = 209, Type = NativeSoundValueType.Byte, Minimum = 0, Maximum = 128, Editable = true)]
        public double? PriorityMax { get; set; }

        /// <summary>
        /// Gets or Sets the Value for this Alias Column
        /// </summary>
        [NativeSoundValue(Name = "Pan", Offset = 210, Type = NativeSoundValueType.EnumByte, Editable = true, ArrayName = "Pan")]
        public string Pan { get; set; }

        /// <summary>
        /// Gets or Sets the Value for this Alias Column
        /// </summary>
        [NativeSoundValue(Name = "LimitCount", Offset = 211, Type = NativeSoundValueType.Byte, Minimum = 0, Maximum = 128, Editable = true)]
        public double? LimitCount { get; set; }

        /// <summary>
        /// Gets or Sets the Value for this Alias Column
        /// </summary>
        [NativeSoundValue(Name = "EntityLimitCount", Offset = 212, Type = NativeSoundValueType.Byte, Minimum = 0, Maximum = 128, Editable = true)]
        public double? EntityLimitCount { get; set; }

        /// <summary>
        /// Gets or Sets the Value for this Alias Column
        /// </summary>
        [NativeSoundValue(Name = "DuckGroup", Offset = 213, Type = NativeSoundValueType.EnumByte, Editable = true, ArrayName = "DuckGroup")]
        public string DuckGroup { get; set; }

        /// <summary>
        /// Gets or Sets the Value for this Alias Column
        /// </summary>
        [NativeSoundValue(Name = "Bus", Offset = 214, Type = NativeSoundValueType.EnumByte, Minimum = 0, Maximum = 128, Editable = true, ArrayName = "Bus")]
        public string Bus { get; set; }

        /// <summary>
        /// Gets or Sets the Value for this Alias Column
        /// </summary>
        [NativeSoundValue(Name = "VolumeGroup", Offset = 215, Type = NativeSoundValueType.EnumByte, Minimum = 0, Maximum = 128, Editable = true, ArrayName = "VolumeGroup")]
        public string VolumeGroup { get; set; }

        /// <summary>
        /// Gets or Sets the Template
        /// </summary>
        [NativeSoundValue(Name = "Template", Offset = -1)]
        public string Template { get; set; }

        /// <summary>
        /// Initializes a new Sound Alias
        /// </summary>
        public SoundAlias(long address, SoundBank bank)
        {
            Address = address;
            Bank = bank;
        }

        /// <summary>
        /// Initializes a new Sound Alias
        /// </summary>
        public SoundAlias(string[] line, string[] header, SoundBank bank)
        {
            Address = -1;
            Bank = bank;

            DeserializeFromCSV(line, header);
            SetFromTemplate();
        }

        /// <summary>
        /// Initializes static data on Sound Aliases
        /// </summary>
        static SoundAlias()
        {
            foreach (var prop in typeof(SoundAlias).GetProperties())
            {
                foreach (var attribute in prop.GetCustomAttributes(true))
                {
                    if (attribute is NativeSoundValueAttribute nativeInfo)
                    {
                        Properties.Add(nativeInfo.Name, prop);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Converts from a DBSPL Float Value
        /// </summary>
        public static double ConvertFromFloatDBSPL(double input)
        {
            return input > 0 ? Math.Round((Math.Log(input, 10.0) / 0.05) + 100.0, 2) : 0;
        }

        /// <summary>
        /// Converts to a DBSPL Float Value
        /// </summary>
        public static double ConvertToFloatDBSPL(double input)
        {
            return input > 0 ? Math.Pow(10.0, (input - 100.0) * 0.05) : 0;
        }

        /// <summary>
        /// Converts from Cents
        /// </summary>
        public static double ConvertFromCents(ushort input)
        {
            return Math.Ceiling(Math.Log(input / 32767.0, 2.0) * 1200);
        }

        /// <summary>
        /// Converts to Cents
        /// </summary>
        public static ushort ConvertToCents(double input)
        {
            return (ushort)(Math.Pow(2.0, input / 1200.0) * 32767.0);
        }

        /// <summary>
        /// Decodes array index from bits
        /// </summary>
        public int DecodeArrayBits(int bits, int min, int max)
        {
            return (bits >> max) & ((1 << min) - 1);
        }

        /// <summary>
        /// Encodes array index to bits
        /// </summary>
        public int EncodeArrayBits(int bits, int min, int max, int value)
        {
            int mask = (1 << min) - 1;
            return ((value & mask) << max) | bits & ~(mask << max);
        }

        /// <summary>
        /// Copies an alias to another
        /// </summary>
        public void CopyFrom(SoundAlias alias)
        {
            var props = GetType().GetProperties();

            foreach (var prop in props)
            {
                foreach (var attribute in prop.GetCustomAttributes(true))
                {
                    if (attribute is NativeSoundValueAttribute nativeInfo)
                    {
                        prop.SetValue(this, prop.GetValue(alias));
                    }
                }
            }
        }

        /// <summary>
        /// Decompiles Alias from Bo3
        /// </summary>
        public void DeserializeFromBo3()
        {
            Buffer = Instance.BlackOpsIII.ReadBytes(Address, SizeOfStructure);

            var props = GetType().GetProperties();

            foreach(var prop in props)
            {
                foreach(var attribute in prop.GetCustomAttributes(true))
                {
                    if(attribute is NativeSoundValueAttribute nativeInfo && nativeInfo.Offset > -1)
                    {
                        switch(nativeInfo.Type)
                        {
                            case NativeSoundValueType.String:
                                prop.SetValue(this, Instance.BlackOpsIII.ReadNullTerminatedString(BitConverter.ToInt64(Buffer, nativeInfo.Offset)));
                                break;
                            case NativeSoundValueType.Float:
                                prop.SetValue(this, (double?)BitConverter.ToSingle(Buffer, nativeInfo.Offset));
                                break;
                            case NativeSoundValueType.Int:
                                prop.SetValue(this, (double?)BitConverter.ToUInt32(Buffer, nativeInfo.Offset));
                                break;
                            case NativeSoundValueType.Enum:
                                prop.SetValue(this, Bank.GetArrayValue(nativeInfo.ArrayName, BitConverter.ToInt32(Buffer, nativeInfo.Offset)));
                                break;
                            case NativeSoundValueType.Flag:
                                prop.SetValue(this, Bank.GetArrayValue(nativeInfo.ArrayName, BitConverter.ToInt32(Buffer, nativeInfo.Offset)));
                                break;
                            case NativeSoundValueType.UShortDBSPL:
                                prop.SetValue(this, (double?)ConvertFromFloatDBSPL(BitConverter.ToUInt16(Buffer, nativeInfo.Offset) / 65535.0));
                                break;
                            case NativeSoundValueType.FloatDBSPL:
                                prop.SetValue(this, (double?)ConvertFromFloatDBSPL(BitConverter.ToSingle(Buffer, nativeInfo.Offset)));
                                break;
                            case NativeSoundValueType.Cents:
                                prop.SetValue(this, (double?)ConvertFromCents(BitConverter.ToUInt16(Buffer, nativeInfo.Offset)));
                                break;
                            case NativeSoundValueType.Hash:
                                prop.SetValue(this, Bank.LookUpHash(BitConverter.ToUInt32(Buffer, nativeInfo.Offset)));
                                break;
                            case NativeSoundValueType.EnumBits:
                            case NativeSoundValueType.FlagBits:
                                prop.SetValue(this, Bank.GetArrayValue(nativeInfo.ArrayName, DecodeArrayBits(BitConverter.ToInt32(Buffer, nativeInfo.Offset), (int)nativeInfo.Minimum, (int)nativeInfo.Maximum)));
                                break;
                            case NativeSoundValueType.Byte:
                                prop.SetValue(this, (double?)Buffer[nativeInfo.Offset]);
                                break;
                            case NativeSoundValueType.EnumByte:
                                prop.SetValue(this, Bank.GetArrayValue(nativeInfo.ArrayName, Buffer[nativeInfo.Offset]));
                                break;
                            case NativeSoundValueType.Short:
                                prop.SetValue(this, (double?)BitConverter.ToInt16(Buffer, nativeInfo.Offset));
                                break;
                            case NativeSoundValueType.UShort:
                                prop.SetValue(this, (double?)BitConverter.ToUInt16(Buffer, nativeInfo.Offset));
                                break;
                            case NativeSoundValueType.NormByte:
                                prop.SetValue(this, (double?)(Buffer[nativeInfo.Offset] / 255.0f));
                                break;
                            case NativeSoundValueType.Distance:
                                prop.SetValue(this, (double?)BitConverter.ToUInt16(Buffer, nativeInfo.Offset) * 2);
                                break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Compiles Alias to Bo3
        /// </summary>
        public void SerializeToBo3()
        {
            var props = GetType().GetProperties();

            foreach (var prop in props)
            {
                foreach (var attribute in prop.GetCustomAttributes(true))
                {
                    if (attribute is NativeSoundValueAttribute nativeInfo && nativeInfo.Editable && nativeInfo.Offset > -1)
                    {
                        var value = prop.GetValue(this);

                        if (value == null)
                            continue;

                        switch (nativeInfo.Type)
                        {
                            case NativeSoundValueType.Float:
                                SetBytes((float)ConvertToFloatDBSPL((double)prop.GetValue(this)), Buffer, nativeInfo.Offset);
                                break;
                            case NativeSoundValueType.Int:
                                SetBytes(Convert.ToUInt32(prop.GetValue(this)), Buffer, nativeInfo.Offset);
                                break;
                            case NativeSoundValueType.Enum:
                            case NativeSoundValueType.Flag:
                                SetBytes(Bank.LookUpArrayIndex(nativeInfo.ArrayName, (string)prop.GetValue(this)), Buffer, nativeInfo.Offset);
                                break;
                            case NativeSoundValueType.UShortDBSPL:
                                SetBytes((ushort)(ConvertToFloatDBSPL((double)prop.GetValue(this)) * 65535.0), Buffer, nativeInfo.Offset);
                                break;
                            case NativeSoundValueType.FloatDBSPL:
                                SetBytes((float)ConvertToFloatDBSPL((double)prop.GetValue(this)), Buffer, nativeInfo.Offset);
                                break;
                            case NativeSoundValueType.Cents:
                                SetBytes(ConvertToCents((double)prop.GetValue(this)), Buffer, nativeInfo.Offset);
                                break;
                            case NativeSoundValueType.Hash:
                                SetBytes(HashString((string)prop.GetValue(this)), Buffer, nativeInfo.Offset);
                                break;
                            case NativeSoundValueType.EnumBits:
                            case NativeSoundValueType.FlagBits:
                                SetBytes(EncodeArrayBits(BitConverter.ToInt32(Buffer, nativeInfo.Offset), (int)nativeInfo.Minimum, (int)nativeInfo.Maximum, Bank.LookUpArrayIndex(nativeInfo.ArrayName, (string)prop.GetValue(this))), Buffer, nativeInfo.Offset);
                                break;
                            case NativeSoundValueType.Byte:
                                SetBytes(Convert.ToByte(prop.GetValue(this)), Buffer, nativeInfo.Offset);
                                break;
                            case NativeSoundValueType.EnumByte:
                                SetBytes((byte)Bank.LookUpArrayIndex(nativeInfo.ArrayName, (string)prop.GetValue(this)), Buffer, nativeInfo.Offset);
                                break;
                            case NativeSoundValueType.Short:
                                SetBytes(Convert.ToInt16(prop.GetValue(this)), Buffer, nativeInfo.Offset);
                                break;
                            case NativeSoundValueType.UShort:
                                SetBytes(Convert.ToUInt16(prop.GetValue(this)), Buffer, nativeInfo.Offset);
                                break;
                            case NativeSoundValueType.NormByte:
                                SetBytes((byte)((double)prop.GetValue(this) * 255.0), Buffer, nativeInfo.Offset);
                                break;
                            case NativeSoundValueType.Distance:
                                SetBytes((ushort)((double)prop.GetValue(this) / 2), Buffer, nativeInfo.Offset);
                                break;
                        }
                    }
                }
            }

            Instance.BlackOpsIII.WriteBytes(Address, Buffer);
        }

        /// <summary>
        /// Parses Alias from CSV
        /// </summary>
        /// <param name="line"></param>
        /// <param name="header"></param>
        public void DeserializeFromCSV(string[] line, string[] header)
        {
            for (int i = 0; i < line.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(line[i]))
                    continue;

                if (i >= header.Length)
                    continue;

                if (Properties.TryGetValue(header[i], out var prop))
                {
                    foreach (var attribute in prop.GetCustomAttributes(true))
                    {
                        if (attribute is NativeSoundValueAttribute nativeInfo)
                        {
                            switch (nativeInfo.Type)
                            {
                                case NativeSoundValueType.String:
                                case NativeSoundValueType.Enum:
                                case NativeSoundValueType.EnumBits:
                                case NativeSoundValueType.EnumByte:
                                case NativeSoundValueType.Flag:
                                case NativeSoundValueType.Hash:
                                    prop.SetValue(this, line[i]);
                                    break;
                                case NativeSoundValueType.Int:
                                case NativeSoundValueType.Cents:
                                case NativeSoundValueType.UShortDBSPL:
                                case NativeSoundValueType.FloatDBSPL:
                                case NativeSoundValueType.Short:
                                case NativeSoundValueType.UShort:
                                case NativeSoundValueType.Byte:
                                case NativeSoundValueType.NormByte:
                                case NativeSoundValueType.Distance:
                                    prop.SetValue(this, (double?)(double.TryParse(line[i], out var flt) ? flt : 0));
                                    break;
                            }
                        }
                    }
                }
            }
        }

        // TEMP
        private static unsafe void SetBytes<T>(T value, byte[] buffer, int offset) where T : unmanaged
        {
            fixed (byte* p = &buffer[0])
                SetBytes(value, p, offset);
        }

        private static unsafe void SetBytes<T>(T value, byte* buffer, int offset) where T : unmanaged
        {
            *((T*)(buffer + offset)) = value;
        }

        /// <summary>
        /// Computes hash for the given string, if it has HASH_ at the beginning, the raw hash is parsed from the string
        /// </summary>
        private static unsafe uint HashString(string value)
        {
            if (value.StartsWith("HASH_"))
                if (uint.TryParse(value.Replace("HASH_", ""), out var r))
                    return r;

            uint result = 5381;

            for (int i = 0; i < value.Length; ++i)
                result = (uint)(value[i] + ((int)result << 6) + ((int)result << 16)) - result;

            return result;
        }

        /// <summary>
        /// Sets values from template
        /// </summary>
        public void SetFromTemplate()
        {
            if (Template == null)
                return;

            var props = GetType().GetProperties();

            if (Instance.TemplateBank.Aliases.TryGetValue(Template, out var templateList))
            {
                var template = templateList.FirstOrDefault();

                if (template == null)
                    return;

                foreach (var prop in props)
                {
                    // Override only if the value isn't set
                    if(prop.GetValue(this) == null && prop.GetValue(template) != null)
                    {
                        foreach (var attribute in prop.GetCustomAttributes(true))
                        {
                            if (attribute is NativeSoundValueAttribute nativeInfo)
                            {
                                prop.SetValue(this, prop.GetValue(template));
                            }
                        }
                    }
                }
            }
        }
    }
}
