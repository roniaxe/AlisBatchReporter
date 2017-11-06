using System;
using System.Collections.Generic;
using System.Linq;
using AlisBatchReporter.Classes;

namespace AlisBatchReporter.Presentors
{
    internal class ArcvalInstance
    {
        #region Properties
        private readonly string _outboundRow;
        private string[] _splitted;

        public string Key { get; private set; }
        public string PolicyNo { get; private set; }
        public ArcvalRowStatus? Status { get; set; }
        public ArcvalRowType Type { get; private set; }
        public StructureType StructureType { get; private set; }
        public List<ArcvalProps> ArcvalProps { get; set; } = new List<ArcvalProps>();
        #endregion

        #region Constructor
        public ArcvalInstance(string outboundRow)
        {
            _outboundRow = outboundRow;
        }
        #endregion

        #region Methods
        public bool Validate()
        {
            return _outboundRow.Length >= 30;
        }

        public void SetPolicyNo()
        {
            PolicyNo = _outboundRow.Substring(30, 12);
        }

        public void Configure()
        {
            // Get Type
            int.TryParse(_outboundRow.Substring(42, 1), out var type);
            Type = (ArcvalRowType)type;

            // Get Key
            Key = GetKey(Type);


            if (Type == ArcvalRowType.PolicyRecord)
            {
                int.TryParse(_outboundRow[43].ToString(), out var status);
                Status = GetStatus(status);
                ArcvalFactory.PolicyStatusDic.Add(PolicyNo, Status);
            }
        }

        public void Process()
        {
            if (Status == null)
                GetStatusNonPolicyRecord();
            StructureType = GetStructureType();
            _splitted = CutArcval();
            GetProperties(StructureType);
        }

        private void GetStatusNonPolicyRecord()
        {
            if (ArcvalFactory.PolicyStatusDic.ContainsKey(PolicyNo))
                Status = ArcvalFactory.PolicyStatusDic[PolicyNo];
        }

        private StructureType GetStructureType()
        {
            StructureType result = 0;

            switch (Type)
            {
                case ArcvalRowType.PolicyRecord:
                    switch (Status)
                    {
                        case ArcvalRowStatus.Active:
                            result = StructureType.PolicyStructure;
                            break;
                        case ArcvalRowStatus.Rpu:
                            result = StructureType.RpuStructure;
                            break;
                        case ArcvalRowStatus.Eti:
                            result = StructureType.EtiStructure;
                            break;
                        case ArcvalRowStatus.Disability:
                            break;
                        case ArcvalRowStatus.PaidUp:
                            break;
                        case null:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    break;
                case ArcvalRowType.Rider:
                    result = StructureType.RiderStructure;
                    break;
                case ArcvalRowType.Waiver:
                    result = StructureType.WaiverStructure;
                    break;
                case ArcvalRowType.UserDefined:
                    result = StructureType.UserDefinedStructure;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return result;
        }

        private void GetProperties(StructureType type)
        {
            Values[] arcvalValues;
            switch (type)
            {
                case StructureType.PolicyStructure:
                    arcvalValues = ArcvalValues.ValusArr;
                    break;
                case StructureType.RpuStructure:
                    arcvalValues = ArcvalValuesStatusRpu.ValusArr;
                    break;
                case StructureType.EtiStructure:
                    arcvalValues = ArcvalValuesStatusEti.ValusArr;
                    break;
                case StructureType.RiderStructure:
                    arcvalValues = ArcvalValuesType5.ValusArr;
                    break;
                case StructureType.WaiverStructure:
                    arcvalValues = ArcvalValuesType6A.ValusArr;
                    break;
                case StructureType.UserDefinedStructure:
                    arcvalValues = ArcvalValuesType7.ValusArr;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
            for (var i = 0; i <= arcvalValues.Length - 1; i++)
                ArcvalProps.Add(new ArcvalProps
                {
                    Intable = arcvalValues[i].ToRound,
                    Name = arcvalValues[i].Name,
                    ToIgnore = arcvalValues[i].ToIgnore,
                    Value = _splitted[i]
                });
        }

        private string[] CutArcval()
        {
            int[] cuts = { };
            switch (StructureType)
            {
                case StructureType.PolicyStructure:
                    cuts = new[]
                        {2, 14, 15, 16, 18, 19, 20, 28, 30, 42, 43, 44, 46, 47, 56, 64, 73, 82, 90, 91};
                    break;
                case StructureType.RpuStructure:
                    cuts = new[] { 2, 14, 15, 16, 18, 19, 20, 28, 30, 42, 43, 44, 46, 47, 56, 64, 73, 75, 83 };
                    break;
                case StructureType.EtiStructure:
                    cuts = new[] { 2, 14, 15, 16, 18, 19, 20, 28, 30, 42, 43, 44, 46, 53, 56, 64, 73, 81 };
                    break;
                case StructureType.UserDefinedStructure:
                    cuts = new[] { 2, 14, 15, 16, 18, 19, 20, 28, 30, 42, 43, 44, 46, 48 };
                    break;
                case StructureType.WaiverStructure:
                    cuts = new[] { 2, 14, 15, 16, 18, 19, 20, 28, 30, 42, 43, 44, 56, 57, 69, 71, 72, 73, 75, 77, 85, 93, 101, 110, 119 };
                    break;
                case StructureType.RiderStructure:
                    cuts = new[] { 2, 14, 15, 16, 18, 19, 20, 28, 30, 42, 43, 55, 56, 57, 58, 60, 62, 70, 78, 101, 110, 119, 128, 137, 146 };
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

        private string GetKey(ArcvalRowType type)
        {
            switch (type)
            {
                case ArcvalRowType.PolicyRecord:
                case ArcvalRowType.Waiver:
                case ArcvalRowType.UserDefined:
                    return PolicyNo + (int)Type;
                case ArcvalRowType.Rider:
                    var secondKey = _outboundRow.Substring(62, 8);
                    var thirdKey = _outboundRow.Substring(70, 8);
                    return PolicyNo + (int)Type + $@"-{secondKey}-{thirdKey}";
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, @"Unrecognized Arcval Type");
            }
        } 
        #endregion
    }

    #region ArcvalPropsClass
    public class ArcvalProps
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public bool Intable { get; set; }
        public bool ToIgnore { get; set; }
    }
    #endregion

    #region Enums
    public enum ArcvalRowType
    {
        PolicyRecord = 1,
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

    public enum StructureType
    {
        PolicyStructure = 1,
        RpuStructure = 4,
        EtiStructure = 5,
        RiderStructure = 55,
        WaiverStructure = 6,
        UserDefinedStructure = 7
    } 
    #endregion
}