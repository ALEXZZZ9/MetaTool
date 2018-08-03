using System;
using System.IO;
using AX9.MetaTool.Models;
using AX9.MetaTool.Structs;
using System.Runtime.InteropServices;
using AX9.MetaTool.Enums;

namespace AX9.MetaTool
{
    public class NpdmGenerator
    {
        public static void CreateNpdm(DescModel desc, MetaModel meta, bool forceGenerateAcid, string saveFilePach)
        {
            byte[] bytes = GetNpdmBytes(desc, meta, forceGenerateAcid);

            File.WriteAllBytes(saveFilePach, bytes);
        }

        public static byte[] GetNpdmBytes(DescModel desc, MetaModel meta, bool forceGenerateAcid = false)
        {
            if (desc == null) throw new ApplicationException("File .desc was not specified.");
            if (meta?.Core == null) throw new ApplicationException("File .nmeta was not specified.");
            if (meta.Core.ProgramIdValue == 0) throw new ApplicationException("Core/ProgramId was not specified within the file .nmeta.");

            MetaModel mMeta = MergeMeta(desc, meta);
            mMeta.Header.Fill(mMeta);

            if (mMeta.Core.SystemResourceSizeValue != 0 &&
                (desc.KernelCapabilityDescriptor.MiscParams == null ||
                desc.KernelCapabilityDescriptor.MiscParams.ProgramTypeValue != ProgramTypeEnum.Application ||
                mMeta.Core.ProcessAddressSpaceValue != ProcessAddressSpacesEnum.AddressSpace64Bit))
            {
                throw new ApplicationException("Core/SystemResourceSize can only be specified for 64 - bit applications.");
            }

            desc.AciHeader.ProgramId = mMeta.Core.ProgramIdValue;

            byte[] aciBytes = GetAciBytes(desc, mMeta);
            byte[] acidBytes = (string.IsNullOrEmpty(desc.Acid) || forceGenerateAcid) ? GetAcidBytes(desc, mMeta) : Convert.FromBase64String(desc.Acid);

            mMeta.Header.AciSize = (uint)aciBytes.Length;
            mMeta.Header.AcidSize = (uint)acidBytes.Length;

            uint headerSize = (uint)Marshal.SizeOf(typeof(NpdmHeader));

            uint align = 16;
            mMeta.Header.AcidOffset = headerSize;
            mMeta.Header.AciOffset = mMeta.Header.AcidOffset + Utils.RoundUp(mMeta.Header.AcidSize, align);

            uint acidSize = Utils.RoundUp(mMeta.Header.AcidSize, align);
            byte[] npdmBytes = new byte[headerSize + acidSize + mMeta.Header.AciSize];

            byte[] headerBytes = Utils.ToBytes(mMeta.Header, headerSize);
            Array.Copy(headerBytes, 0, npdmBytes, 0, headerSize);

            Array.Copy(acidBytes, 0, npdmBytes, mMeta.Header.AcidOffset, mMeta.Header.AcidSize);
            Array.Copy(aciBytes, 0, npdmBytes, mMeta.Header.AciOffset, mMeta.Header.AciSize);

            return npdmBytes;
        }

        public static byte[] GetAcidBytes(DescModel desc, MetaModel meta = null)
        {
            AcidHeader header = desc.AcidHeader;
            DescModel mDesc = (meta != null) ? MergeDesc(desc, meta) : desc;

            header.Fill(desc);

            if (header.ProgramIdMin == 0 || header.ProgramIdMax == 0) throw new ArgumentException("ProgramIdMin/ProgramIdMax is incorrect");

            mDesc.KernelCapabilityDescriptor.CheckCapabilities(mDesc.Default.KernelCapabilityData);
            mDesc.SrvAccessControlDescriptor.CheckCapabilities(mDesc.Default.SrvAccessControlData);

            uint headerSize = (uint)Marshal.SizeOf(typeof(AcidHeader));
            byte[] faBinary = mDesc.FsAccessControlDescriptor?.ExportBinary(true);
            byte[] saBinary = mDesc.SrvAccessControlDescriptor?.ExportBinary();
            byte[] kcBinary = mDesc.KernelCapabilityDescriptor?.ExportBinary();

            header.FacOffset = headerSize;
            header.FacSize = (uint)(faBinary?.Length ?? 0);
            header.SacOffset = header.FacOffset + Utils.RoundUp(header.FacSize, 16);
            header.SacSize = (uint)(saBinary?.Length ?? 0);
            header.KcOffset = header.SacOffset + Utils.RoundUp(header.SacSize, 16);
            header.KcSize = (uint)(kcBinary?.Length ?? 0);

            uint facSize = Utils.RoundUp(header.FacSize, 16);
            uint sacSize = Utils.RoundUp(header.SacSize, 16);
            uint kacSize = Utils.RoundUp(header.KcSize, 16);
            byte[] aciBytes = new byte[headerSize + facSize + sacSize + kacSize];

            byte[] headerBytes = Utils.ToBytes(header, headerSize);
            Array.Copy(headerBytes, 0, aciBytes, 0, headerSize);

            if (faBinary != null && header.FacSize > 0)
            {
                Array.Copy(faBinary, 0, aciBytes, header.FacOffset, header.FacSize);
            }
            if (saBinary != null && header.SacSize > 0)
            {
                Array.Copy(saBinary, 0, aciBytes, header.SacOffset, header.SacSize);
            }
            if (kcBinary != null && header.KcSize > 0)
            {
                Array.Copy(kcBinary, 0, aciBytes, header.KcOffset, header.KcSize);
            }

            return aciBytes;
        }

        public static byte[] GetAciBytes(DescModel desc, MetaModel meta)
        {
            desc.AciHeader.Fill();

            if (desc.AciHeader.ProgramId == 0) throw new ArgumentException("Not Found ProgramId");

            DescModel mDesc = MergeDesc(desc, meta);

            mDesc.KernelCapabilityDescriptor.CheckCapabilities(mDesc.Default.KernelCapabilityData);
            mDesc.SrvAccessControlDescriptor.CheckCapabilities(mDesc.Default.SrvAccessControlData);

            uint headerSize = (uint)Marshal.SizeOf(typeof(AciHeader));
            byte[] faBinary = mDesc.FsAccessControlDescriptor?.ExportBinary();
            byte[] saBinary = mDesc.SrvAccessControlDescriptor?.ExportBinary();
            byte[] kcBinary = mDesc.KernelCapabilityDescriptor?.ExportBinary();

            mDesc.AciHeader.FacOffset = headerSize;
            mDesc.AciHeader.FacSize = (uint)(faBinary?.Length ?? 0);
            mDesc.AciHeader.SacOffset = mDesc.AciHeader.FacOffset + Utils.RoundUp(mDesc.AciHeader.FacSize, 16);
            mDesc.AciHeader.SacSize = (uint)(saBinary?.Length ?? 0);
            mDesc.AciHeader.KcOffset = mDesc.AciHeader.SacOffset + Utils.RoundUp(mDesc.AciHeader.SacSize, 16);
            mDesc.AciHeader.KcSize = (uint)(kcBinary?.Length ?? 0);

            uint facSize = (mDesc.AciHeader.SacSize > 0 || mDesc.AciHeader.KcSize > 0) ? Utils.RoundUp(mDesc.AciHeader.FacSize, 16) : mDesc.AciHeader.FacSize;
            uint sacSize = (mDesc.AciHeader.KcSize > 0) ? Utils.RoundUp(mDesc.AciHeader.SacSize, 16) : mDesc.AciHeader.SacSize;
            byte[] aciBytes = new byte[headerSize + facSize + sacSize + mDesc.AciHeader.KcSize];

            byte[] headerBytes = Utils.ToBytes(mDesc.AciHeader, headerSize);
            Array.Copy(headerBytes, 0, aciBytes, 0, headerSize);

            if (faBinary != null && mDesc.AciHeader.FacSize > 0)
            {
                Array.Copy(faBinary, 0, aciBytes, mDesc.AciHeader.FacOffset, mDesc.AciHeader.FacSize);
            }
            if (saBinary != null && mDesc.AciHeader.SacSize > 0)
            {
                Array.Copy(saBinary, 0, aciBytes, mDesc.AciHeader.SacOffset, mDesc.AciHeader.SacSize);
            }
            if (kcBinary != null && mDesc.AciHeader.KcSize > 0)
            {
                Array.Copy(kcBinary, 0, aciBytes, mDesc.AciHeader.KcOffset, mDesc.AciHeader.KcSize);
            }

            return aciBytes;
        }

        public static MetaModel MergeMeta(DescModel desc, MetaModel meta, bool replace = false)
        {
            if (desc == null) throw new ApplicationException("File .desc was not specified.");
            if (meta == null) throw new ApplicationException("File .nmeta was not specified.");
            if (desc.Default == null) throw new ApplicationException("Default was not specified within the file .desc.");

            desc.Default.CheckReadSuccess();

            MetaModel mMeta = replace ? meta : (MetaModel)meta.Clone();

            if (string.IsNullOrEmpty(mMeta.Core.Is64BitInstruction)) mMeta.Core.Is64BitInstruction = desc.Default.Is64BitInstruction;
            if (string.IsNullOrEmpty(mMeta.Core.ProcessAddressSpace)) mMeta.Core.ProcessAddressSpace = desc.Default.ProcessAddressSpace;
            if (string.IsNullOrEmpty(mMeta.Core.MainThreadPriority)) mMeta.Core.MainThreadPriority = desc.Default.MainThreadPriority;
            if (string.IsNullOrEmpty(mMeta.Core.MainThreadCoreNumber)) mMeta.Core.MainThreadCoreNumber = desc.Default.MainThreadCoreNumber;
            if (string.IsNullOrEmpty(mMeta.Core.MainThreadStackSize)) mMeta.Core.MainThreadStackSize = desc.Default.MainThreadStackSize;
            if (string.IsNullOrEmpty(mMeta.Core.SystemResourceSize)) mMeta.Core.SystemResourceSize = "0x0";
            if (string.IsNullOrEmpty(mMeta.Core.Version)) mMeta.Core.Version = "0";
            if (string.IsNullOrEmpty(mMeta.Core.Name)) mMeta.Core.Name = "Application";
            //if (string.IsNullOrEmpty(meta.Core.ProductCode)) meta.Core.ProductCode = string.Empty;

            return mMeta;
        }

        public static DescModel MergeDesc(DescModel desc, MetaModel meta, bool replace = false)
        {
            if (desc == null) throw new ApplicationException("File .desc was not specified.");
            if (meta == null) throw new ApplicationException("File .nmeta was not specified.");

            DescModel mDesc = replace ? desc : (DescModel)desc.Clone();

            if (meta.Core?.FsAccessControlData != null)
            {
                mDesc.FsAccessControlDescriptor = new FaDescriptorModel(meta.Core.FsAccessControlData);
            }
            else if (mDesc.FsAccessControlDescriptor == null && mDesc.Default?.FsAccessControlData != null)
            {
                mDesc.FsAccessControlDescriptor = new FaDescriptorModel(mDesc.Default.FsAccessControlData);
            }

            if (meta.Core?.SrvAccessControlData != null)
            {
                mDesc.SrvAccessControlDescriptor = new SaDescriptorModel(meta.Core.SrvAccessControlData);
            }
            else if (mDesc.SrvAccessControlDescriptor == null && mDesc.Default?.SrvAccessControlData != null)
            {
                mDesc.SrvAccessControlDescriptor = new SaDescriptorModel(mDesc.Default.SrvAccessControlData);
            }

            if (meta.Core?.KernelCapabilityData != null && mDesc.KernelCapabilityDescriptor != null)
            {
                if (meta.Core.KernelCapabilityData.ThreadInfo != null) mDesc.KernelCapabilityDescriptor.ThreadInfo = meta.Core.KernelCapabilityData.ThreadInfo;
                if (meta.Core.KernelCapabilityData.EnableSystemCalls != null) mDesc.KernelCapabilityDescriptor.EnableSystemCalls = meta.Core.KernelCapabilityData.EnableSystemCalls;
                if (meta.Core.KernelCapabilityData.AllMemoryMap != null) mDesc.KernelCapabilityDescriptor.AllMemoryMap = meta.Core.KernelCapabilityData.AllMemoryMap;
                if (meta.Core.KernelCapabilityData.EnableInterrupts != null) mDesc.KernelCapabilityDescriptor.EnableInterrupts = meta.Core.KernelCapabilityData.EnableInterrupts;
                if (meta.Core.KernelCapabilityData.MiscParams != null) mDesc.KernelCapabilityDescriptor.MiscParams = meta.Core.KernelCapabilityData.MiscParams;
                if (meta.Core.KernelCapabilityData.KernelVersion != null) mDesc.KernelCapabilityDescriptor.KernelVersion = meta.Core.KernelCapabilityData.KernelVersion;
                if (meta.Core.KernelCapabilityData.HandleTableSize != null) mDesc.KernelCapabilityDescriptor.HandleTableSize = meta.Core.KernelCapabilityData.HandleTableSize;
                if (meta.Core.KernelCapabilityData.MiscFlags != null) mDesc.KernelCapabilityDescriptor.MiscFlags = meta.Core.KernelCapabilityData.MiscFlags;
            }
            else if (meta.Core?.KernelCapabilityData != null)
            {
                mDesc.KernelCapabilityDescriptor = new KcDescriptorModel(meta.Core.KernelCapabilityData);
            }
            else if (mDesc.KernelCapabilityDescriptor == null && mDesc.Default?.KernelCapabilityData != null)
            {
                mDesc.KernelCapabilityDescriptor = new KcDescriptorModel(mDesc.Default.KernelCapabilityData);
            }

            return mDesc;
        }
    }
}
