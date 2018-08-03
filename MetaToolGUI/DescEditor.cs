using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AX9.MetaTool;
using AX9.MetaTool.Enums;
using AX9.MetaTool.Models;
using AX9.MetaToolGUI.Enums;

namespace AX9.MetaToolGUI
{
    public partial class DescEditor : Form
    {
        public DescEditor(DescModel desc, string descPach = null)
        {
            InitializeComponent();

            this.desc = desc;
            this.descPach = descPach;
        }


        private DescModel desc { get; }
        private string descPach
        {
            get => _descPach;
            set
            {
                DEB_SaveAs.Visible = !string.IsNullOrEmpty(value) && File.Exists(value);
                _descPach = value;
            }
        }

        private string _descPach;


        private void DescEditor_Load(object sender, EventArgs e)
        {
            DescToUI();
        }

        private void DescEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Close without saving?", "Close without saving?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Program.MainForm.Show();
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void DECB_AcidType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (DECB_AcidType.SelectedIndex)
            {
                case (int)AcidTypeEnum.DoNotChange:
                    DETB_Acid.ReadOnly = true;
                    DETB_Acid.Text = desc.Acid;
                    break;
                case (int)AcidTypeEnum.Generate:
                    MessageBox.Show("Not yet implemented!");
                    DECB_AcidType.SelectedIndex = 0;
                    return;
                    DETB_Acid.ReadOnly = true;
                    // TODO: Generate acid
                    //DETB_Acid.Text = desc.GenerateAcid();
                    break;
                case (int)AcidTypeEnum.Custom:
                    DETB_Acid.ReadOnly = false;
                    break;
            }
        }

        private void DEDGV_SrvAccess_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2 && !DEDGV_SrvAccess.Rows[e.RowIndex].IsNewRow)
            {
                DEDGV_SrvAccess.Rows.RemoveAt(e.RowIndex);
            }
        }

        private void DEB_AddPreset_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(DECB_Presets.Text))
            {
                DataGridViewRow row = DEDGV_SrvAccess.Rows.Cast<DataGridViewRow>().FirstOrDefault(r => r.Cells[0].Value != null && r.Cells[0].Value.ToString().Equals(DECB_Presets.Text));

                DEDGV_SrvAccess.FirstDisplayedScrollingRowIndex = row?.Index ?? DEDGV_SrvAccess.Rows.Add(DECB_Presets.Text, false);
            }
        }

        private void DEB_Save_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(descPach) && !File.Exists(descPach))
            {
                SaveAs();
            }
            else
            {
                Save(descPach);
            }
        }

        private void DEB_SaveAs_Click(object sender, EventArgs e)
        {
            SaveAs();
        }


        private void Save(string filePath)
        {
            try
            {
                UIToDesc();

                File.WriteAllText(filePath, desc.XMLSerialize());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SaveAs()
        {
            FileInfo inputFile = (string.IsNullOrEmpty(descPach)) ? null : new FileInfo(descPach);

            using (SaveFileDialog sFD = new SaveFileDialog
            {
                Filter = "DESC files|*.desc",
                RestoreDirectory = true,
                FileName = (inputFile != null) ? Path.ChangeExtension(inputFile.FullName, ".desc") : "main.desc"
            })
            {
                if (sFD.ShowDialog() == DialogResult.OK)
                {
                    Save(sFD.FileName);

                    if (string.IsNullOrEmpty(descPach)) descPach = sFD.FileName;
                }
            }
        }

        private void DescToUI()
        {
            DENUD_MemoryRegion.Value = desc.MemoryRegion;
            DETB_ProgramIdMin.Text = desc.ProgramIdMin;
            DETB_ProgramIdMax.Text = desc.ProgramIdMax;

            foreach (string flag in Enum.GetNames(typeof(FsAccessFlagPresetEnum)))
            {
                DECL_FsAccess.Items.Add(flag, desc.FsAccessControlDescriptor.FlagPresets.Contains(flag));
            }

            foreach (string type in Enum.GetNames(typeof(AcidTypeEnum)))
            {
                DECB_AcidType.Items.Add(type);
            }
            DECB_AcidType.SelectedIndex = string.IsNullOrEmpty(desc.Acid) ? (int)AcidTypeEnum.Custom : (int)AcidTypeEnum.DoNotChange;

            foreach (SaEntry saEntry in desc.SrvAccessControlDescriptor.Entries)
            {
                DEDGV_SrvAccess.Rows.Add(saEntry.Name, saEntry.IsServerValue);
            }

            DENUD_HighestPriority.Value = desc.KernelCapabilityDescriptor.ThreadInfo.HighestPriorityValue;
            DENUD_LowestPriority.Value = desc.KernelCapabilityDescriptor.ThreadInfo.LowestPriorityValue;
            DENUD_MaxCoreNumber.Value = desc.KernelCapabilityDescriptor.ThreadInfo.MaxCoreNumberValue;
            DENUD_MinCoreNumber.Value = desc.KernelCapabilityDescriptor.ThreadInfo.MinCoreNumberValue;

            foreach (string type in Enum.GetNames(typeof(ProgramTypeEnum)))
            {
                DECB_ProgramType.Items.Add(type);
            }
            DECB_ProgramType.SelectedIndex = (int)desc.KernelCapabilityDescriptor.MiscParams.ProgramTypeValue;

            DENUD_MajorVersion.Value = desc.KernelCapabilityDescriptor.KernelVersion.MajorVersionVlaue;
            DENUD_MinorVersion.Value = desc.KernelCapabilityDescriptor.KernelVersion.MinorVersionVlaue;

            DENUD_HandleTableSize.Value = desc.KernelCapabilityDescriptor.HandleTableSizeValue.HandleTableSize;

            foreach (byte systemCall in Enum.GetValues(typeof(SystemCallsEnum)))
            {
                DECLB_SystemCalls.Items.Add(((SystemCallsEnum)systemCall).ToString(), desc.KernelCapabilityDescriptor.SystemCallList.Contains(systemCall));
            }

            if (desc.KernelCapabilityDescriptor.MiscFlags != null)
            {
                DECB_EnableDebug.Checked = desc.KernelCapabilityDescriptor.MiscFlags.EnableDebugValue;
                DECB_ForceDebug.Checked = desc.KernelCapabilityDescriptor.MiscFlags.ForceDebugValue;
            }

            foreach (string space in Enum.GetNames(typeof(ProcessAddressSpacesEnum)))
            {
                DECB_ProcessAddressSpace.Items.Add(space);
            }
            DECB_ProcessAddressSpace.SelectedIndex = (int)desc.Default.ProcessAddressSpaceValue;

            DENUD_MainThreadPriority.Value = desc.Default.MainThreadPriorityValue;
            DENUD_MainThreadCoreNumber.Value = desc.Default.MainThreadCoreNumberValue;
            DETB_MainThreadStackSize.Text = desc.Default.MainThreadStackSize;
        }

        private void UIToDesc()
        {
            desc.MemoryRegion = (byte)DENUD_MemoryRegion.Value;
            desc.ProgramIdMin = DETB_ProgramIdMin.Text;
            desc.ProgramIdMax = DETB_ProgramIdMax.Text;

            desc.FsAccessControlDescriptor.FlagPresets.Clear();
            foreach (string flag in DECL_FsAccess.CheckedItems)
            {
                desc.FsAccessControlDescriptor.FlagPresets.Add(flag);
            }

            desc.Acid = DETB_Acid.Text;

            desc.SrvAccessControlDescriptor.Entries.Clear();
            foreach (DataGridViewRow saEntry in DEDGV_SrvAccess.Rows)
            {
                if (!saEntry.IsNewRow && !string.IsNullOrEmpty((string)saEntry.Cells[0].Value)) desc.SrvAccessControlDescriptor.Entries.Add(new SaEntry { IsServerValue = (bool)saEntry.Cells[1].Value, Name = (string)saEntry.Cells[0].Value });
            }

            desc.KernelCapabilityDescriptor.ThreadInfo.HighestPriorityValue = (byte)DENUD_HighestPriority.Value;
            desc.KernelCapabilityDescriptor.ThreadInfo.LowestPriorityValue = (byte)DENUD_LowestPriority.Value;
            desc.KernelCapabilityDescriptor.ThreadInfo.MaxCoreNumberValue = (byte)DENUD_MaxCoreNumber.Value;
            desc.KernelCapabilityDescriptor.ThreadInfo.MinCoreNumberValue = (byte)DENUD_MinCoreNumber.Value;

            desc.KernelCapabilityDescriptor.MiscParams.ProgramTypeValue = (ProgramTypeEnum)DECB_ProgramType.SelectedIndex;

            desc.KernelCapabilityDescriptor.KernelVersion.MajorVersionVlaue = (ushort)DENUD_MajorVersion.Value;
            desc.KernelCapabilityDescriptor.KernelVersion.MinorVersionVlaue = (byte)DENUD_MinorVersion.Value;

            desc.KernelCapabilityDescriptor.HandleTableSizeValue.HandleTableSize = (ushort)DENUD_HandleTableSize.Value;

            desc.KernelCapabilityDescriptor.EnableSystemCalls.Clear();
            foreach (string call in DECLB_SystemCalls.CheckedItems)
            {
                desc.KernelCapabilityDescriptor.EnableSystemCalls.Add(new KcEnableSystemCallsModel(call, (byte)Enum.Parse(typeof(SystemCallsEnum), call)));
            }

            if (DECB_EnableDebug.Checked || DECB_ForceDebug.Checked)
            {
                desc.KernelCapabilityDescriptor.MiscFlags = new KcMiscFlags(DECB_EnableDebug.Checked, DECB_ForceDebug.Checked);
            }
            else
            {
                desc.KernelCapabilityDescriptor.MiscFlags = null;
            }

            desc.Default.ProcessAddressSpaceValue = (ProcessAddressSpacesEnum)DECB_ProcessAddressSpace.SelectedIndex;

            desc.Default.MainThreadPriorityValue = (byte)DENUD_MainThreadPriority.Value;
            desc.Default.MainThreadCoreNumberValue = (byte)DENUD_MainThreadCoreNumber.Value;
            desc.Default.MainThreadStackSize = DETB_MainThreadStackSize.Text;

            desc.Default.FsAccessControlData = new FaDataModel(desc.FsAccessControlDescriptor);
            desc.Default.SrvAccessControlData = new SaDataModel(desc.SrvAccessControlDescriptor);
            desc.Default.KernelCapabilityData = new KcDataModel(desc.KernelCapabilityDescriptor);
        }
    }
}
