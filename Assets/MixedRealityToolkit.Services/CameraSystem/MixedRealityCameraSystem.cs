﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Microsoft.MixedReality.Toolkit.Utilities;
using System.Collections;
using UnityEngine;

namespace Microsoft.MixedReality.Toolkit.CameraSystem
{
    /// <summary>
    /// The Camera system controls the settings of the main camera.
    /// </summary>
    public class MixedRealityCameraSystem : BaseCoreSystem, IMixedRealityCameraSystem
    {
        public MixedRealityCameraSystem(
            IMixedRealityServiceRegistrar registrar,
            BaseMixedRealityProfile profile = null) : base(registrar, profile)
        {
            cameraProfile = (MixedRealityCameraProfile)profile;
        }

        private MixedRealityCameraProfile cameraProfile;

        /// <inheritdoc />
        public uint SourceId { get; } = 0;

        /// <inheritdoc />
        public string SourceName { get; } = "Mixed Reality Camera System";

        /// <inheritdoc />
        public override void Initialize()
        {
            if (cameraProfile.IsOpaque)
            {
                ApplySettingsForOpaqueDisplay();
            }
            else
            {
                ApplySettingsForTransparentDisplay();
            }
        }

        /// <inheritdoc />
        public override void Update()
        {
            if (cameraProfile.IsOpaque)
            {
                ApplySettingsForOpaqueDisplay();
            }
            else
            {
                ApplySettingsForTransparentDisplay();
            }
        }

        /// <summary>
        /// Applies opaque settings from camera profile.
        /// </summary>
        private void ApplySettingsForOpaqueDisplay()
        {
            CameraCache.Main.clearFlags = cameraProfile.CameraClearFlagsOpaqueDisplay;
            CameraCache.Main.nearClipPlane = cameraProfile.NearClipPlaneOpaqueDisplay;
            CameraCache.Main.backgroundColor = cameraProfile.BackgroundColorOpaqueDisplay;
            QualitySettings.SetQualityLevel(cameraProfile.OpaqueQualityLevel, false);
        }

        /// <summary>
        /// Applies transparent settings from camera profile.
        /// </summary>
        private void ApplySettingsForTransparentDisplay()
        {
            CameraCache.Main.clearFlags = cameraProfile.CameraClearFlagsTransparentDisplay;
            CameraCache.Main.backgroundColor = cameraProfile.BackgroundColorTransparentDisplay;
            CameraCache.Main.nearClipPlane = cameraProfile.NearClipPlaneTransparentDisplay;
            QualitySettings.SetQualityLevel(cameraProfile.HoloLensQualityLevel, false);
        }

        /// <inheritdoc />
        bool IEqualityComparer.Equals(object x, object y)
        {
            // There shouldn't be other Camera Systems to compare to.
            return false;
        }

        /// <inheritdoc />
        int IEqualityComparer.GetHashCode(object obj)
        {
            return Mathf.Abs(SourceName.GetHashCode());
        }
    }
}