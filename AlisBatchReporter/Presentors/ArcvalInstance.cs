using System;
using System.Collections.Generic;
using System.Linq;
using AlisBatchReporter.Classes;

namespace AlisBatchReporter.Presentors
{
    internal class ArcvalInstance
    {
        private readonly string _outboundRow;
        private string[] _splitted;

        public string Key { get; private set; }
        public string PolicyNo { get; private set; }
        public ArcvalRowStatus Status { get; private set; }
        public ArcvalRowType Type { get; private set; }
        public bool Valid { get; private set; }
        public List<ArcvalProps> ArcvalProps { get; set; } = new List<ArcvalProps>();

        public ArcvalInstance(string outboundRow)
        {
            _outboundRow = outboundRow;
        }

        private bool Validate()
        {
            bool val = false;
            if (_outboundRow.Length >= 30)
            {
                Valid = true;
                val = true;
            }
            return val;
        }

        public void SetKeyAndType()
        {
            Validate();
            if (!Valid) return;
            PolicyNo = _outboundRow.Substring(30, 12);
            int.TryParse(_outboundRow.Substring(42, 1), out var type);
            Type = (ArcvalRowType) type;
            GetKey(Type);
            if (Type == ArcvalRowType.BaseCover)
            {
                int.TryParse(_outboundRow[43].ToString(), out var status);
                Status = GetStatus(status);
                // Type 1 - RPU
                if (Status == ArcvalRowStatus.Rpu)
                {
                    Type = ArcvalRowType.Rpu;
                }
            }
            _splitted = CutArcval();
            GetProperties(Type);
        }

        private void GetProperties(ArcvalRowType type)
        {
            Values[] arcvalValues;
            switch (type)
            {
                case ArcvalRowType.BaseCover:
                    arcvalValues = ArcvalValues.ValusArr;
                    break;
                case ArcvalRowType.Rpu:
                    arcvalValues = ArcvalValuesType4.ValusArr;
                    break;
                case ArcvalRowType.Rider:
                    arcvalValues = ArcvalValuesType5.ValusArr;
                    break;
                case ArcvalRowType.Waiver:
                    arcvalValues = ArcvalValuesType6A.ValusArr;
                    break;
                case ArcvalRowType.UserDefined:
                    arcvalValues = ArcvalValuesType7.ValusArr;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
            for (var i = 0; i <= arcvalValues.Length - 1; i++)
            {
                ArcvalProps.Add(new ArcvalProps
                {
                    Intable = arcvalValues[i].ToRound,
                    Name = arcvalValues[i].Name,
                    ToIgnore = arcvalValues[i].ToIgnore,
                    Value = _splitted[i]
                });
            }
        }

        private string[] CutArcval()
        {
            int[] cuts = { };
            switch (Type)
            {
                case ArcvalRowType.BaseCover:
                    cuts = new[]
                        {2, 14, 15, 16, 18, 19, 20, 28, 30, 42, 43, 44, 46, 47, 56, 64, 73, 82, 90, 91};
                    break;
                case ArcvalRowType.Rpu:
                    cuts = new[]
                        {2, 14, 15, 16, 18, 19, 20, 28, 30, 42, 43, 44, 46, 47, 56, 64, 73, 75, 83};
                    break;
                case ArcvalRowType.UserDefined:
                    cuts = new[] { 2, 14, 15, 16, 18, 19, 20, 28, 30, 42, 43, 44, 46, 48 };
                    break;
                case ArcvalRowType.Waiver:
                    cuts = new[]
                    {
                        2, 14, 15, 16, 18, 19, 20, 28, 30, 42, 43, 44, 56, 57, 69, 71, 72, 73, 75, 77,
                        85, 93,
                        101, 110, 119
                    };
                    break;
                case ArcvalRowType.Rider:
                    cuts = new[]
                    {
                        2, 14, 15, 16, 18, 19, 20, 28, 30, 42, 43, 55, 56, 57, 58, 60, 62, 70, 78,
                        101, 110, 119, 128, 137, 146
                    };
                    break;
            }
            return SplitAt(_outboundRow.TrimEnd(), cuts);
        }

        private string[] SplitAt(string source, params int[] index)
        {
            index = index.Distinct().OrderBy(x => x).ToArray();
            var output = new string[index.Length + 1];
            var pos = 0;

            for (var i = 0; i < index.Length; pos = index[i++])
                try
                {
                    output[i] = source.Substring(pos, index[i] - pos);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }

            output[index.Length] = source.Substring(pos);
            return output;
        }

        private ArcvalRowStatus GetStatus(int status)
        {
            switch (status)
            {
                case 1:
                    return ArcvalRowStatus.Active;
                case 4:
                    return ArcvalRowStatus.Rpu;
                case 5:
                    return ArcvalRowStatus.Eti;
                case 6:
                    return ArcvalRowStatus.Disability;
                case 7:
                    return ArcvalRowStatus.PaidUp;
                default:
                    return ArcvalRowStatus.Inactive;
            }
        }

        private void GetKey(ArcvalRowType type)
        {
            switch (type)
            {
                case ArcvalRowType.BaseCover:
                case ArcvalRowType.Rpu:
                case ArcvalRowType.Waiver:
                case ArcvalRowType.UserDefined:
                    Key = PolicyNo + (int)Type;
                    break;
                case ArcvalRowType.Rider:
                    var secondKey = _outboundRow.Substring(62, 8);
                    var thirdKey = _outboundRow.Substring(70, 8);
                    Key = PolicyNo + (int)Type + $@"-{secondKey}-{thirdKey}";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }

    public class ArcvalProps
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public bool Intable { get; set; }
        public bool ToIgnore { get; set; }
    }

    public enum ArcvalRowType
    {
        BaseCover = 1,
        Rpu = 4,
        Rider = 5,
        Waiver = 6,
        UserDefined = 7
    }

    public enum ArcvalRowStatus
    {
        Inactive = 0,
        Active = 1,
        Rpu = 4,
        Eti = 5,
        Disability = 6,
        PaidUp = 7

    }
}