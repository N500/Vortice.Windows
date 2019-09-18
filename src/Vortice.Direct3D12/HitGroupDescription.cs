﻿// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Vortice.Direct3D12
{
    /// <summary>
    /// Describes a raytracing hit group state subobject that can be included in a state object.
    /// </summary>
    public partial struct HitGroupDescription : IStateSubObjectDescription, IStateSubObjectDescriptionMarshal
    {
        StateSubObjectType IStateSubObjectDescription.SubObjectType => StateSubObjectType.HitGroup;

        /// <summary>
        /// Initializes a new instance of the <see cref="HitGroupDescription"/> struct.
        /// </summary>
        /// <param name="hitGroupExport">The name of the hit group.</param>
        /// <param name="type">A value from the <see cref="HitGroupType"/> enumeration specifying the type of the hit group.</param>
        /// <param name="anyHitShaderImport">Optional name of the any-hit shader associated with the hit group. This field can be used with all hit group types.</param>
        /// <param name="closestHitShaderImport">Optional name of the closest-hit shader associated with the hit group. This field can be used with all hit group types.</param>
        /// <param name="intersectionShaderImport">Optional name of the intersection shader associated with the hit group. This field can only be used with hit groups of type procedural primitive.</param>
        public HitGroupDescription(
            string hitGroupExport,
            HitGroupType type,
            string anyHitShaderImport = "",
            string closestHitShaderImport = "",
            string intersectionShaderImport = "")
        {
            HitGroupExport = hitGroupExport;
            Type = type;
            AnyHitShaderImport = anyHitShaderImport;
            ClosestHitShaderImport = closestHitShaderImport;
            IntersectionShaderImport = intersectionShaderImport;
        }

        #region Marshal
        unsafe IntPtr IStateSubObjectDescriptionMarshal.__MarshalAlloc()
        {
            __Native* native = (__Native*)Marshal.AllocHGlobal(sizeof(__Native));
            native->HitGroupExport = Marshal.StringToHGlobalUni(HitGroupExport);
            native->Type = Type;
            native->AnyHitShaderImport = string.IsNullOrEmpty(AnyHitShaderImport) ? IntPtr.Zero : Marshal.StringToHGlobalUni(AnyHitShaderImport);
            native->ClosestHitShaderImport = string.IsNullOrEmpty(ClosestHitShaderImport) ? IntPtr.Zero : Marshal.StringToHGlobalUni(ClosestHitShaderImport);
            native->IntersectionShaderImport = string.IsNullOrEmpty(IntersectionShaderImport) ? IntPtr.Zero : Marshal.StringToHGlobalUni(IntersectionShaderImport);
            return (IntPtr)native;
        }

        unsafe void IStateSubObjectDescriptionMarshal.__MarshalFree(ref IntPtr pDesc)
        {
            ref __Native native = ref Unsafe.AsRef<__Native>(pDesc.ToPointer());
            __MarshalFree(ref native);
            Marshal.FreeHGlobal(pDesc);
        }
        #endregion Marshal
    }
}