using System;
using System.Linq;
using UnityEngine;

// Shapes © Freya Holmér - https://twitter.com/FreyaHolmer/
// Website & Documentation - https://acegikmo.com/shapes/
namespace Shapes
{
    internal interface IParamSelector
    {
        int Variants { get; }
        Param[] GetVariant(int i);
    }

    internal class CombinatorialParams : IParamSelector
    {
        private readonly Param[] @params;

        public CombinatorialParams(params Param[] @params)
        {
            this.@params = @params;
        }

        public int Variants => 1 << @params.Length;

        public Param[] GetVariant(int i)
        {
            var bits = (uint)i;
            var paramCount = bits.PopCount();
            if (paramCount == 0) return null;
            var retParams = new Param[paramCount];
            var p = 0;
            for (var j = 0; j < @params.Length; j++)
                if ((bits & (1 << j)) > 0)
                    retParams[p++] = @params[j];

            return retParams;
        }
    }

    internal class OrSelectorParams : IParamSelector
    {
        private readonly Param[][] paramList;

        public OrSelectorParams(params Param[][] paramList)
        {
            this.paramList = paramList;
        }

        public int Variants => paramList.Length;

        public Param[] GetVariant(int i)
        {
            return paramList[i];
        }
    }


    internal class Param : IParamSelector
    {
        public string desc;
        public string methodCallStr;
        public string methodSigDefault = null;
        public string methodSigName;

        public string methodSigType;
        public MtxFlags mtxFlags = MtxFlags.None;
        public string[] targetArgNames;

        public Param(string methodSigParam, params string[] targetArgNames)
        {
            methodSigDefault = null;
            if (methodSigParam.Contains("//")) // this means we have a param description, extract it
                (methodSigParam, desc) = methodSigParam.Split(new[] { "//" }, StringSplitOptions.None)
                    .Select(x => x.Trim()).ToArray();
            else
                Debug.Log($"Missing param description: {methodSigParam}");
            if (methodSigParam.Contains("=")) // this means we have a default value, extract it
                (methodSigParam, methodSigDefault) =
                    methodSigParam.Split('=').Select(x => x.Trim()).ToArray(); // this is fine don't judge me okay
            // methodSigParam is in the form "Vector3 start" now
            (methodSigType, methodSigName) = methodSigParam.Split(' ').Select(x => x.Trim()).ToArray();
            this.targetArgNames = targetArgNames.Length == 0 ? new[] { methodSigName } : targetArgNames;
            methodCallStr = methodSigName; // default
        }

        public string FullMethodSig
        {
            get
            {
                var s = $"{methodSigType} {methodSigName}";
                if (methodSigDefault != null)
                    s += $" = {methodSigDefault}";
                return s;
            }
        }

        public int Variants => 1;

        public Param[] GetVariant(int i)
        {
            return new[] { this };
        }

        public static implicit operator Param(string s)
        {
            return new(s);
        }

        [Flags]
        internal enum MtxFlags
        {
            None = 0,
            Position = 1 << 0,
            Rotation = 1 << 1,
            Normal = 1 << 2,
            Angle = 1 << 3,
            PosRot = Position | Rotation,
            PosNormal = Position | Normal,
            PosAngle = Position | Angle
        }
    }
}