using System;
using System.IO;
using AX9.MetaTool.Models;
using AX9.MetaTool.Structs;
using System.Runtime.InteropServices;
using System.Text;
using AX9.MetaTool.Enums;

namespace AX9.MetaTool
{
    public class NpdmGenerator
    {
        public static void CreateNpdm(DescModel desc, MetaModel meta, string saveFilePach)
        {
            byte[] bytes = GetNpdmBytes(desc, meta);

            File.WriteAllBytes(saveFilePach, bytes);
        }

        public static byte[] GetNpdmBytes(DescModel desc, MetaModel meta)
        {
            if (desc == null) throw new ApplicationException("File .desc was not specified.");
            if (meta?.Core == null) throw new ApplicationException("File .nmeta was not specified.");
            if (meta.Core.ProgramIdValue == 0) throw new ApplicationException("Core/ProgramId was not specified within the file .nmeta.");

            meta = MergeMeta(desc, meta);
            meta.Header.Fill(meta);

            if (meta.Core.SystemResourceSizeValue != 0 &&
                (desc.KernelCapabilityDescriptor.MiscParams == null ||
                desc.KernelCapabilityDescriptor.MiscParams.ProgramTypeValue != ProgramTypeEnum.Application ||
                meta.Core.ProcessAddressSpaceValue != ProcessAddressSpacesEnum.AddressSpace64Bit))
            {
                throw new ApplicationException("Core/SystemResourceSize can only be specified for 64 - bit applications.");
            }

            desc.AciHeader.ProgramId = meta.Core.ProgramIdValue;

            byte[] aciBytes = GetAciBytes(desc, meta);
            byte[] acidBytes = Convert.FromBase64String(desc.Acid);

            meta.Header.AciSize = (uint)aciBytes.Length;
            meta.Header.AcidSize = (uint)acidBytes.Length;

            uint headerSize = (uint)Marshal.SizeOf(typeof(NpdmHeader));

            uint align = 16;
            meta.Header.AcidOffset = headerSize;
            meta.Header.AciOffset = meta.Header.AcidOffset + Utils.RoundUp(meta.Header.AcidSize, align);

            uint acidSize = Utils.RoundUp(meta.Header.AcidSize, align);
            byte[] npdmBytes = new byte[headerSize + acidSize + meta.Header.AciSize];

            byte[] headerBytes = Utils.ToBytes(meta.Header, headerSize);
            Array.Copy(headerBytes, 0, npdmBytes, 0, headerSize);

            Array.Copy(acidBytes, 0, npdmBytes, meta.Header.AcidOffset, meta.Header.AcidSize);
            Array.Copy(aciBytes, 0, npdmBytes, meta.Header.AciOffset, meta.Header.AciSize);

            return npdmBytes;
        }

        public static byte[] GetAciBytes(DescModel desc, MetaModel meta)
        {
            desc.AciHeader.Fill();

            if (desc.AciHeader.ProgramId == 0) throw new ArgumentException("Not Found ProgramId");

            desc = MergeDesc(desc, meta);

            desc.KernelCapabilityDescriptor.CheckCapabilities(desc.Default);
            desc.SrvAccessControlDescriptor.CheckCapabilities(desc.Default);

            uint headerSize = (uint)Marshal.SizeOf(typeof(AciHeader));
            byte[] faBinary = desc.FsAccessControlDescriptor?.ExportBinary();
            byte[] saBinary = desc.SrvAccessControlDescriptor?.ExportBinary();
            byte[] kcBinary = desc.KernelCapabilityDescriptor?.ExportBinary();

            desc.AciHeader.FacOffset = headerSize;
            desc.AciHeader.FacSize = (uint)(faBinary?.Length ?? 0);
            desc.AciHeader.SacOffset = desc.AciHeader.FacOffset + Utils.RoundUp(desc.AciHeader.FacSize, 16);
            desc.AciHeader.SacSize = (uint)(saBinary?.Length ?? 0);
            desc.AciHeader.KcOffset = desc.AciHeader.SacOffset + Utils.RoundUp(desc.AciHeader.SacSize, 16);
            desc.AciHeader.KcSize = (uint)(kcBinary?.Length ?? 0);

            uint facSize = (desc.AciHeader.SacSize > 0 || desc.AciHeader.KcSize > 0) ? Utils.RoundUp(desc.AciHeader.FacSize, 16) : desc.AciHeader.FacSize;
            uint sacSize = (desc.AciHeader.KcSize > 0) ? Utils.RoundUp(desc.AciHeader.SacSize, 16) : desc.AciHeader.SacSize;
            byte[] aciBytes = new byte[headerSize + facSize + sacSize + desc.AciHeader.KcSize];

            byte[] headerBytes = Utils.ToBytes(desc.AciHeader, headerSize);
            Array.Copy(headerBytes, 0, aciBytes, 0, headerSize);

            if (faBinary != null && desc.AciHeader.FacSize > 0)
            {
                Array.Copy(faBinary, 0, aciBytes, desc.AciHeader.FacOffset, desc.AciHeader.FacSize);
            }
            if (saBinary != null && desc.AciHeader.SacSize > 0)
            {
                Array.Copy(saBinary, 0, aciBytes, desc.AciHeader.SacOffset, desc.AciHeader.SacSize);
            }
            if (kcBinary != null && desc.AciHeader.KcSize > 0)
            {
                Array.Copy(kcBinary, 0, aciBytes, desc.AciHeader.KcOffset, desc.AciHeader.KcSize);
            }

            return aciBytes;
        }

        public static MetaModel MergeMeta(DescModel desc, MetaModel meta)
        {
            if (desc == null) throw new ApplicationException("File .desc was not specified.");
            if (meta == null) throw new ApplicationException("File .nmeta was not specified.");
            if (desc.Default == null) throw new ApplicationException("Default was not specified within the file .desc.");

            desc.Default.CheckReadSuccess();

            if (string.IsNullOrEmpty(meta.Core.Is64BitInstruction)) meta.Core.Is64BitInstruction = desc.Default.Is64BitInstruction;
            if (string.IsNullOrEmpty(meta.Core.ProcessAddressSpace)) meta.Core.ProcessAddressSpace = desc.Default.ProcessAddressSpace;
            if (string.IsNullOrEmpty(meta.Core.MainThreadPriority)) meta.Core.MainThreadPriority = desc.Default.MainThreadPriority;
            if (string.IsNullOrEmpty(meta.Core.MainThreadCoreNumber)) meta.Core.MainThreadCoreNumber = desc.Default.MainThreadCoreNumber;
            if (string.IsNullOrEmpty(meta.Core.MainThreadStackSize)) meta.Core.MainThreadStackSize = desc.Default.MainThreadStackSize;
            if (string.IsNullOrEmpty(meta.Core.SystemResourceSize)) meta.Core.SystemResourceSize = "0x0";
            if (string.IsNullOrEmpty(meta.Core.Version)) meta.Core.Version = "0";
            if (string.IsNullOrEmpty(meta.Core.Name)) meta.Core.Name = "Application";
            //if (string.IsNullOrEmpty(meta.Core.ProductCode)) meta.Core.ProductCode = string.Empty;

            return meta;
        }

        public static DescModel MergeDesc(DescModel desc, MetaModel meta)
        {
            if (desc == null) throw new ApplicationException("File .desc was not specified.");
            if (meta == null) throw new ApplicationException("File .nmeta was not specified.");

            if (meta.Core?.FsAccessControlData != null)
            {
                desc.FsAccessControlDescriptor = new FaDescriptorModel(meta.Core.FsAccessControlData);
            }
            else if (desc.FsAccessControlDescriptor == null && desc.Default?.FsAccessControlData != null)
            {
                desc.FsAccessControlDescriptor = new FaDescriptorModel(desc.Default.FsAccessControlData);
            }

            if (meta.Core?.SrvAccessControlData != null)
            {
                desc.SrvAccessControlDescriptor = new SaDescriptorModel(meta.Core.SrvAccessControlData);
            }
            else if (desc.SrvAccessControlDescriptor == null && desc.Default?.SrvAccessControlData != null)
            {
                desc.SrvAccessControlDescriptor = new SaDescriptorModel(desc.Default.SrvAccessControlData);
            }

            if (meta.Core?.KernelCapabilityData != null && desc.KernelCapabilityDescriptor != null)
            {
                if (meta.Core.KernelCapabilityData.ThreadInfo != null) desc.KernelCapabilityDescriptor.ThreadInfo = meta.Core.KernelCapabilityData.ThreadInfo;
                if (meta.Core.KernelCapabilityData.EnableSystemCalls != null) desc.KernelCapabilityDescriptor.EnableSystemCalls = meta.Core.KernelCapabilityData.EnableSystemCalls;
                if (meta.Core.KernelCapabilityData.AllMemoryMap != null) desc.KernelCapabilityDescriptor.AllMemoryMap = meta.Core.KernelCapabilityData.AllMemoryMap;
                if (meta.Core.KernelCapabilityData.EnableInterrupts != null) desc.KernelCapabilityDescriptor.EnableInterrupts = meta.Core.KernelCapabilityData.EnableInterrupts;
                if (meta.Core.KernelCapabilityData.MiscParams != null) desc.KernelCapabilityDescriptor.MiscParams = meta.Core.KernelCapabilityData.MiscParams;
                if (meta.Core.KernelCapabilityData.KernelVersion != null) desc.KernelCapabilityDescriptor.KernelVersion = meta.Core.KernelCapabilityData.KernelVersion;
                if (meta.Core.KernelCapabilityData.HandleTableSize != null) desc.KernelCapabilityDescriptor.HandleTableSize = meta.Core.KernelCapabilityData.HandleTableSize;
                if (meta.Core.KernelCapabilityData.MiscFlags != null) desc.KernelCapabilityDescriptor.MiscFlags = meta.Core.KernelCapabilityData.MiscFlags;
            }
            else if (meta.Core?.KernelCapabilityData != null)
            {
                desc.KernelCapabilityDescriptor = new KcDescriptorModel(meta.Core.KernelCapabilityData);
            }
            else if (desc.KernelCapabilityDescriptor == null && desc.Default?.KernelCapabilityData != null)
            {
                desc.KernelCapabilityDescriptor = new KcDescriptorModel(desc.Default.KernelCapabilityData);
            }

            return desc;
        }
    }
}
