// <copyright file="OperationTypeEnum.cs" company="APIMatic">
// Copyright (c) APIMatic. All rights reserved.
// </copyright>
namespace FirstAPICalculator.Standard.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using FirstAPICalculator.Standard;
    using FirstAPICalculator.Standard.Utilities;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// OperationTypeEnum.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum OperationTypeEnum
    {
        /// <summary>
        ///addition
        /// SUM.
        /// </summary>
        [EnumMember(Value = "SUM")]
        SUM,

        /// <summary>
        ///subtration
        /// SUBTRACT.
        /// </summary>
        [EnumMember(Value = "SUBTRACT")]
        SUBTRACT,

        /// <summary>
        ///multiplication
        /// MULTIPLY.
        /// </summary>
        [EnumMember(Value = "MULTIPLY")]
        MULTIPLY,

        /// <summary>
        ///division
        /// DIVIDE.
        /// </summary>
        [EnumMember(Value = "DIVIDE")]
        DIVIDE
    }
}