﻿// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using IpTviewr.Common;
using IpTviewr.Common.Telemetry;
using IpTviewr.Services.Record.Serialization;
using IpTviewr.UiServices.Common;
using IpTviewr.UiServices.Common.Forms;
using IpTviewr.UiServices.Configuration;
using IpTviewr.UiServices.Configuration.Logos;
using IpTviewr.UiServices.Configuration.Schema2014.Config;
using IpTviewr.UiServices.Record.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace IpTviewr.UiServices.Record
{
    public partial class RecordChannelDialog : CommonBaseForm
    {
        public static readonly TimeSpan DefaultExpiryDateTimeSpan = new TimeSpan(23, 0, 0);

        private const string ListLocationsSelectedImageKey = "selected";
        private const string ListLocationsDefaultImageKey = "folder";

        private bool IsTaskNameUserProvided;
        private DateTime CurrentStartDateTime;
        private RecordScheduleKind CurrentScheduleKind;
        private int CurrentSelectedLocationIndex;

        public RecordTask Task
        {
            get;
            set;
        } // Task

        public bool IsNewTask
        {
            get;
            set;
        } // IsNewTask

        public DateTime LocalReferenceTime
        {
            get;
            set;
        } // LocalReferenceTime

        public static string[] GetFilenameExtensions()
        {
            var separators = new string[] { "\r\n" };
            var extensions = Properties.RecordChannel.FileExtensions.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            return extensions;
        } // GetFilenameExtensions

        public RecordChannelDialog()
        {
            InitializeComponent();
            Icon = Properties.Resources.Icon_Recorder;
            LocalReferenceTime = DateTime.Now;
        } // constructor

        #region Form events

        private void DialogRecordChannel_Load(object sender, EventArgs e)
        {
            SafeCall(DialogRecordChannel_Load_Implementation, sender, e);
        } // DialogRecordChannel_Load

        private void DialogRecordChannel_Shown(object sender, EventArgs e)
        {
            SafeCall(DialogRecordChannel_Shown_Implementation, sender, e);
        } // DialogRecordChannel_Shown

        #endregion

        #region Form events implementation

        private void DialogRecordChannel_Load_Implementation(object sender, EventArgs e)
        {
            BasicGoogleTelemetry.SendScreenHit(this);

            // Initialize
            if (Task == null)
            {
                if (this.DesignMode)
                {
                    Task = RecordTask.CreateWithDefaultValues(null);
                    IsNewTask = true;
                }
                else
                {
                    throw new ArgumentNullException();
                } // if-else
            } // if

            // General
            InitGeneralData();
            // Schedule tab
            InitScheduleData();
            // Duration tab
            InitDurationData();
            // Description tab
            InitDescriptionData();
            // Save tab
            InitSaveData();
            // Advanced tab
            InitAdvancedData();
        } // DialogRecordChannel_Load_Implementation

        private void DialogRecordChannel_Shown_Implementation(object sender, EventArgs e)
        {
            if (CurrentSelectedLocationIndex >= 0)
            {
                // force creation of the control
                // .NET WinForms BUG: selecting an item when the control is NOT created does not update the SelectedItems/SelectedIndexes collection
                var x = listViewLocations.Handle;
                Debug.Assert(listViewLocations.IsHandleCreated);
                listViewLocations.Items[CurrentSelectedLocationIndex].Selected = true;
            } // if
        } // DialogRecordChannel_Shown_Implementation

        #endregion

        #region Form control events

        private void buttonOk_Click(object sender, EventArgs e)
        {
            SafeCall(buttonOk_Click_Implementation, sender, e);
        } // buttonOk_Click

        private void buttonOk_Click_Implementation(object sender, EventArgs e)
        {
            var isValid = this.ValidateChildren(ValidationConstraints.Enabled);
            if (!isValid) return;

            // General
            GetGeneralData();
            // Schedule tab
            GetScheduleData();
            // Duration tab
            GetDurationData();
            // Description tab
            GetDescriptionData();
            // Save tab
            GetSaveData();
            // Advanced tab
            GetAdvancedData();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        } // buttonOk_Click_Implementation

        #endregion

        #region 'General' form setup & get data

        private void InitGeneralData()
        {
            // service logo
            var serviceLogo = AppUiConfiguration.Current.ServiceLogoMappings.FromServiceKey(Task.Channel.ServiceKey);
            pictureChannelLogo.Image = serviceLogo.GetImage(LogoSize.Size64);

            // service name
            labelChannelName.Text = string.Format("{0} {1}", Task.Channel.LogicalNumber, Task.Channel.Name);

            // program name
            if (Task.Program != null)
            {
                labelProgramDescription.Text = Task.Program.Title;
                labelProgramSchedule.Text = string.Format("{0} ({1})", FormatString.DateTimeFromToMinutes(Task.Program.LocalStartTime, Task.Program.LocalEndTime, LocalReferenceTime),
                    FormatString.TimeSpanTotalMinutes(Task.Program.Duration, FormatString.Format.Extended));
            }
            else
            {
                labelChannelName.Top = pictureChannelLogo.Top;
                labelChannelName.Height = pictureChannelLogo.Height;

                labelProgramDescription.Visible = false;
                labelProgramSchedule.Visible = false;
            } // if-else
        } // InitGeneralData

        private void GetGeneralData()
        {
            // nothing to get
        } // GetGeneralData

        #endregion

        #region "Schedule" tab events / setup & get data

        private void InitScheduleData()
        {
            // Schedule kind
            // Note: this will fire DateTimeChanged and ScheduleKindChanged events,
            // thus allowing us to get the right StartDate and Kind and not having to call more 'setup' methods
            recordingSchedule.SetSchedule(Task.Schedule, IsNewTask);

            // Expiry date
            var schedule = Task.Schedule;
            checkBoxExpiryDate.Checked = schedule.ExpiryDate.HasValue;
            dateTimeExpiryDate.Value = (schedule.ExpiryDate.HasValue) ? schedule.ExpiryDate.Value : CurrentStartDateTime + DefaultExpiryDateTimeSpan;

            // Safety margin
            checkBoxStartMargin.Checked = schedule.SafetyMargin.HasValue;
            numericStartMargin.Value = schedule.SafetyMargin ?? RecordSchedule.DefaultSafetyMargin;
        } // InitScheduleData

        private void GetScheduleData()
        {
            var schedule = recordingSchedule.GetSchedule();
            Task.Schedule = schedule;

            schedule.ExpiryDate = null;
            schedule.SafetyMargin = null;

            if (schedule.Kind == RecordScheduleKind.OneTime) return;

            if ((checkBoxExpiryDate.Checked) && (checkBoxExpiryDate.Enabled))
            {
                var date = dateTimeExpiryDate.Value;
                var expiryDate = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
                schedule.ExpiryDate = expiryDate;
            } // if

            if ((checkBoxStartMargin.Checked) && (checkBoxStartMargin.Enabled) && (numericStartMargin.Value > 0))
            {
                schedule.SafetyMargin = (int)numericStartMargin.Value;
            } // if
        } // GetScheduleData

        private void recordingSchedule_ScheduleKindChanged(object sender, RecordingSchedule.KindChangedEventArgs e)
        {
            CurrentScheduleKind = e.Kind;

            var enabled = (e.Kind != RecordScheduleKind.RightNow);
            UpdateStartMarginStatus(enabled);
            EnableExpiryDate();
            recordingTime.SetScheduleKind(e.Kind);

            ChangeOkButtonText(enabled);
        } // recordingSchedule_ScheduleKindChanged

        private void recordingSchedule_DateTimeChanged(object sender, RecordingSchedule.DateTimeChangedEventArgs e)
        {
            CurrentStartDateTime = e.DateTime;
            recordingTime.SetStartTime(CurrentStartDateTime);
            UpdateTaskName();
        } // schedulePattern_DateTimeChanged

        private void checkBoxExpiryDate_CheckedChanged(object sender, EventArgs e)
        {
            EnableExpiryDate();
        } // checkBoxExpiryDate_CheckedChanged

        private void EnableExpiryDate()
        {
            var expiryDisallowed = (CurrentScheduleKind == RecordScheduleKind.RightNow) ||
                (CurrentScheduleKind == RecordScheduleKind.OneTime);

            checkBoxExpiryDate.Visible = !expiryDisallowed;
            checkBoxExpiryDate.Enabled = !expiryDisallowed;

            dateTimeExpiryDate.Visible = !expiryDisallowed;
            dateTimeExpiryDate.Enabled = checkBoxExpiryDate.Checked;

            EnableSchedulerDeleteTask(expiryDisallowed | (checkBoxExpiryDate.Enabled & checkBoxExpiryDate.Checked));
        } // EnableExpiryDate

        private void checkBoxStartMargin_CheckedChanged(object sender, EventArgs e)
        {
            UpdateStartMarginStatus(true);
        } // checkBoxStartMargin_CheckedChanged

        private void UpdateStartMarginStatus(bool enabled)
        {
            checkBoxStartMargin.Visible = enabled;
            checkBoxStartMargin.Enabled = enabled;

            numericStartMargin.Visible = enabled;
            numericStartMargin.Enabled = checkBoxStartMargin.Checked;

            labelStartMarginSufix.Visible = enabled;
            labelStartMarginSufix.Enabled = checkBoxStartMargin.Checked;
        } // UpdateStartMarginStatus

        private void ChangeOkButtonText(bool schedule)
        {
            buttonOk.ChangeImage(schedule ? Properties.Resources.Action_Ok_16x16 : Properties.Resources.Action_RecordButton_16x16);
            buttonOk.Text = schedule ? Properties.RecordChannel.RecordButtonSchedule : Properties.RecordChannel.RecordButtonRecord;
        } // ChangeOkButtonText

        #endregion

        #region "Duration" tab events / setup & get data

        private void InitDurationData()
        {
            recordingTime.SetDuration(CurrentStartDateTime, Task.Schedule.Kind, Task.Duration);
        } // InitDurationData()

        private void GetDurationData()
        {
            Task.Duration = recordingTime.GetDuration();
        } // GetDurationData

        #endregion

        #region "Save" tab events / setup & get data

        private void InitSaveData()
        {
            // Fill extensions combo
            comboFileExtension.Items.AddRange(GetFilenameExtensions());

            // Name (filename)
            textFilename.SetText(string.IsNullOrEmpty(Task.Action.Filename)? Task.Channel.Name : Task.Action.Filename, false);

            // Extension
            comboFileExtension.Text = string.IsNullOrEmpty(Task.Action.FileExtension) ? comboFileExtension.Items[0] as string : Task.Action.FileExtension;

            // Save locations
            var defaultItem = SetListLocations(AppUiConfiguration.Current.User.Record.SaveLocations);
            SelectSaveLocation(Task.Action.SaveLocationName, Task.Action.SaveLocationPath, defaultItem);
        } // InitSaveData

        private void GetSaveData()
        {
            var locationName = listViewLocations.SelectedItems[0].SubItems[0].Text;
            if (locationName == Properties.RecordChannel.SaveDefaultLocation) locationName = null;
            var locationPath = listViewLocations.SelectedItems[0].SubItems[1].Text;

            Task.Action.Filename = textFilename.Text.Trim();
            Task.Action.FileExtension = comboFileExtension.Text.Trim();
            Task.Action.SaveLocationName = locationName;
            Task.Action.SaveLocationPath = locationPath;
            Task.Action.Recorder = RecordHelper.GetDefaultRecorder();
        } // GetSaveData

        private void textFilename_Validating(object sender, CancelEventArgs e)
        {
            var text = textFilename.Text.Trim();
            e.Cancel = (text.Length == 0);

            if (e.Cancel)
            {
                ControlValidationFailed(Properties.RecordChannel.EmptyFileName, sender as Control);
            } // if
        } // textFilename_Validating

        private void comboFileExtension_Validating(object sender, CancelEventArgs e)
        {
            var text = comboFileExtension.Text.Trim();
            e.Cancel = (text.Length == 0);

            if (e.Cancel)
            {
                ControlValidationFailed(Properties.RecordChannel.EmptyFileExtension, sender as Control);
            } // if
        } // comboFileExtension_Validating

        private void listViewLocations_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentSelectedLocationIndex = (listViewLocations.SelectedIndices.Count > 0) ? listViewLocations.SelectedIndices[0] : -1;

            for (int index = 0; index < listViewLocations.Items.Count; index++)
            {
                var item = listViewLocations.Items[index];
                if (item.Index == CurrentSelectedLocationIndex)
                {
                    item.ImageKey = ListLocationsSelectedImageKey;
                }
                else if (item.ImageKey != ListLocationsDefaultImageKey)
                {
                    item.ImageKey = ListLocationsDefaultImageKey;
                } // if-else
            } // for index
        } // listViewLocations_SelectedIndexChanged

        private void listViewLocations_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = (listViewLocations.SelectedIndices.Count == 0);
            if (e.Cancel)
            {
                ControlValidationFailed(Properties.RecordChannel.NoSaveLocation, sender as Control);
            } // if
        } // listViewLocations_Validating

        private void buttonSelectFolder_Click(object sender, EventArgs e)
        {
            if (listViewLocations.SelectedItems.Count == 0)
            {
                selectFolder.SelectedPath = null;
            }
            else
            {
                selectFolder.SelectedPath = listViewLocations.SelectedItems[0].SubItems[1].Text;
            } // if-else

            if (selectFolder.ShowDialog(this) != System.Windows.Forms.DialogResult.OK) return;

            SelectAppendSaveLocation(selectFolder.SelectedPath);
        } // buttonSelectFolder_Click

        private ListViewItem SetListLocations(IList<RecordSaveLocation> locations)
        {
            ListViewItem defaultItem;

            if (locations == null) throw new ArgumentNullException();

            defaultItem = null;
            listViewLocations.BeginUpdate();
            listViewLocations.Items.Clear();

            CurrentSelectedLocationIndex = (locations.Count == 0) ? -1 : 0;
            for (int index = 0; index < locations.Count; index++)
            {
                var name = locations[index].Name;
                if (string.IsNullOrEmpty(name)) name = Properties.RecordChannel.SaveDefaultLocation;

                var item = listViewLocations.Items.Add(name);
                item.ImageKey = ListLocationsDefaultImageKey;
                item.SubItems.Add(locations[index].Path.Trim());

                if (string.IsNullOrEmpty(locations[index].Name)) defaultItem = item;
            } // for

            int sortColumn = (listViewLocations.CurrentSortColumn < 0) ? 0 : listViewLocations.CurrentSortColumn;
            listViewLocations.Sort(sortColumn, !listViewLocations.CurrentSortIsDescending);
            listViewLocations.EndUpdate();

            return defaultItem;
        } // SetListLocations

        private void SelectSaveLocation(string locationName, string path, ListViewItem defaultItem)
        {
            ListViewItem item;

            if ((locationName == null) && (!string.IsNullOrEmpty(path)))
            {
                locationName = Properties.RecordChannel.SaveDefaultLocation;
            } // if

            var find = from ListViewItem listItem in listViewLocations.Items
                        where string.Compare(listItem.Text, locationName, StringComparison.InvariantCultureIgnoreCase) == 0
                        select listItem;
            item = find.FirstOrDefault();

            if (item == null)
            {
                if (string.IsNullOrEmpty(path))
                {
                    if (listViewLocations.Items.Count > 0)
                    {
                        item = defaultItem;
                    } // if
                }
                else
                {
                    locationName = (locationName != null) ? Properties.RecordChannel.SaveMissingLocation : Properties.RecordChannel.SaveCustomLocation;
                    item = listViewLocations.Items.Add(locationName);
                    item.ImageKey = ListLocationsDefaultImageKey;
                    item.SubItems.Add(Task.Action.SaveLocationPath);
                } // if-else
            } // if-else

            if (item == null)
            {
                CurrentSelectedLocationIndex = -1;
            }
            else
            {
                item.Selected = true;
                CurrentSelectedLocationIndex = item.Index;
            } // if-else
        } // SelectSaveLocation

        private void SelectAppendSaveLocation(string path)
        {
            var find = from ListViewItem listItem in listViewLocations.Items
                       where string.Compare(listItem.SubItems[1].Text, path, StringComparison.InvariantCultureIgnoreCase) == 0
                       select listItem;
            var item = find.FirstOrDefault();

            if (item == null)
            {
                item = listViewLocations.Items.Add(Properties.RecordChannel.SaveCustomLocation);
                item.ImageKey = ListLocationsDefaultImageKey;
                item.SubItems.Add(path);
            } // if

            item.Selected = true;
            CurrentSelectedLocationIndex = item.Index;
        } // SelectAppendSaveLocation

        #endregion

        #region "Description" tab events / setup & get data

        private void InitDescriptionData()
        {
            textTaskDescription.Text = Task.Description.Description;
            checkAppendRecordingDetails.Checked = Task.Description.AddDetails;
        } // InitDescriptionData

        private void GetDescriptionData()
        {
            if (Task.Schedule.Kind == RecordScheduleKind.RightNow)
            {
                CurrentStartDateTime = DateTime.Now;
                UpdateTaskName();
            } // if
            Task.Description.Description = textTaskDescription.Text.Trim();
            Task.Description.AddDetails = checkAppendRecordingDetails.Checked;
        } // GetDescriptionData

        private void textTaskName_TextChanged(object sender, EventArgs e)
        {
            IsTaskNameUserProvided = true;
        } // textTaskName_TextChanged

        private void textTaskName_Validating(object sender, CancelEventArgs e)
        {
            var text = textTaskName.Text.Trim();
            
            e.Cancel = (text.Length == 0);
            if (e.Cancel)
            {
                ControlValidationFailed(Properties.RecordChannel.EmptyTaskName, sender as Control);
                return;
            } // if

            e.Cancel = (text.Length > RecordTaskSerialization.MaxTaskNameLength);
            if (e.Cancel)
            {
                ControlValidationFailed(string.Format(Properties.RecordChannel.TooLongTaskName, RecordTaskSerialization.MaxTaskNameLength), sender as Control);
                return;
            } // if
        } // textTaskName_Validating

        private void UpdateTaskName()
        {
            if (IsTaskNameUserProvided) return;

            var taskName = RecordDescription.CreateTaskName(Task.Channel, CurrentStartDateTime);
            textTaskName.SetText(taskName, false);
        } // UpdateTaskName

        #endregion

        #region "Advanced" tab events / setup & get data

        private void InitAdvancedData()
        {
            // task name
            if (string.IsNullOrEmpty(Task.Description.TaskSchedulerName))
            {
                // When the SetSchedule method of the RecordingSchedule is called, it will fire a DateTime changed event.
                // As we capture this event to adjust the task name, there's no need to call UpdateTaskName() again
                // UpdateTaskName(schedulePattern.Pattern.StartDateTime);
            }
            else
            {
                IsTaskNameUserProvided = true;
                textTaskName.Text = Task.Description.TaskSchedulerName;
            } // if-else
            textTaskName.Enabled = IsNewTask;
            checkAddTaskPrefix.Enabled = IsNewTask;
            checkAddTaskPrefix.Checked = Task.Description.AddPrefix;

            // task folder
            comboSchedulerFolder.DisplayMember = "Name";
            comboSchedulerFolder.ValueMember = "Path";
            var folders = AppUiConfiguration.Current.User.Record.TaskSchedulerFolders;
            if (folders != null)
            {
                comboSchedulerFolder.Items.AddRange(folders);
            } // if
            comboSchedulerFolder.Items.Add(new RecordTaskSchedulerFolder(Properties.RecordChannel.TaskSchedulerRootFolder, ""));
            comboSchedulerFolder.SelectedIndex = 0;
            comboSchedulerFolder.Enabled = IsNewTask;

            checkSchedulerASAP.Checked = Task.AdvancedSettings.AsSoonAsPossible;

            checkSchedulerRetry.Checked = Task.AdvancedSettings.FailureRetry.Enabled && (Task.AdvancedSettings.FailureRetry.MaxRetries > 0);
            timeSpanSchedulerRetry.Value = Task.AdvancedSettings.FailureRetry.RestartInterval;
            numericSchedulerMaxRetries.Value = Task.AdvancedSettings.FailureRetry.MaxRetries;

            checkSchedulerDeleteTask.Checked = Task.AdvancedSettings.DeleteAfter.Enabled;
            timeSpanSchedulerDeleteTaskAfter.Value = Task.AdvancedSettings.DeleteAfter.Time;

            switch (Task.AdvancedSettings.MultipleInstances)
            {
                case RecordMultipleInstances.RecordBoth: comboSchedulerAlreadyRunning.SelectedIndex = 0; break; // Run in paralel (record both)
                case RecordMultipleInstances.DoNotRecord: comboSchedulerAlreadyRunning.SelectedIndex = 1; break; // Do not record (continue previous recording)
                case RecordMultipleInstances.Queue: comboSchedulerAlreadyRunning.SelectedIndex = 2; break; // Wait for previous recording to end and start recording
                case RecordMultipleInstances.StopPrevious: comboSchedulerAlreadyRunning.SelectedIndex = 3; break; // Stop previous recording and proceed
                default:
                    comboSchedulerAlreadyRunning.SelectedIndex = -1; break;
            } // switch
        } // InitAdvancedData

        private void GetAdvancedData()
        {
            Task.Description.Name = textTaskName.Text.Trim();
            Task.Description.AddPrefix = checkAddTaskPrefix.Checked;

            // Task scheduler folder
            if (comboSchedulerFolder.SelectedIndex < 0)
            {
                Task.AdvancedSettings.TaskSchedulerFolder = null;
            }
            else
            {
                Task.AdvancedSettings.TaskSchedulerFolder = (comboSchedulerFolder.SelectedItem as RecordTaskSchedulerFolder).Path;
            } // if-else

            // ASAP
            Task.AdvancedSettings.AsSoonAsPossible = checkSchedulerASAP.Checked & checkSchedulerASAP.Enabled;

            // Retry
            Task.AdvancedSettings.FailureRetry.Enabled = checkSchedulerRetry.Checked & checkSchedulerRetry.Enabled;
            Task.AdvancedSettings.FailureRetry.RestartInterval = timeSpanSchedulerRetry.Value;
            Task.AdvancedSettings.FailureRetry.MaxRetries = (int)numericSchedulerMaxRetries.Value;

            // Delete after
            Task.AdvancedSettings.DeleteAfter.Enabled = checkSchedulerDeleteTask.Checked && checkSchedulerDeleteTask.Enabled;
            Task.AdvancedSettings.DeleteAfter.Time = timeSpanSchedulerDeleteTaskAfter.Value;

            // Max execution time
            // TODO: no UI yet!
            // Remember: 
            //Task.AdvancedSettings.ExecutionTimeLimit.Enabled = checkSchedulerExecutionLimit.Checked;
            //Task.AdvancedSettings.ExecutionTimeLimit.Time = timeSpanSchedulerExecutionLimit.Value;
            Task.AdvancedSettings.ExecutionTimeLimit.Enabled = true; // (Task.Schedule.Kind != RecordScheduleKind.RightNow); // use default time limit

            // Wake up computer
            // TODO: no UI yet!
            Task.AdvancedSettings.WakeComputer = true;

            // Multiple instances
            switch (comboSchedulerAlreadyRunning.SelectedIndex)
            {
                default:
                case 0: Task.AdvancedSettings.MultipleInstances = RecordMultipleInstances.RecordBoth; break; // Run in paralel (record both)
                case 1: Task.AdvancedSettings.MultipleInstances = RecordMultipleInstances.DoNotRecord; break; // Do not record (continue previous recording)
                case 2: Task.AdvancedSettings.MultipleInstances = RecordMultipleInstances.Queue; break; // Wait for previous recording to end and start recording
                case 3: Task.AdvancedSettings.MultipleInstances = RecordMultipleInstances.StopPrevious; break; // Stop previous recording and proceed
            } // switch
        } // GetAdvancedData

        private void checkSchedulerRetry_CheckedChanged(object sender, EventArgs e)
        {
            bool enabled = checkSchedulerRetry.Checked;

            checkSchedulerRetry.Enabled = enabled;
            timeSpanSchedulerRetry.Enabled = enabled;
            labelSchedulerMaxRetries.Enabled = enabled;
            numericSchedulerMaxRetries.Enabled = enabled;
        } // checkSchedulerRetry_CheckedChanged

        private void checkSchedulerDeleteTask_CheckedChanged(object sender, EventArgs e)
        {
            EnableSchedulerDeleteTask(true);
        }

        private void EnableSchedulerDeleteTask(bool enable)
        {
            checkSchedulerDeleteTask.Enabled = enable;
            timeSpanSchedulerDeleteTaskAfter.Enabled = checkSchedulerDeleteTask.Checked & enable;
        } // EnableSchedulerDeleteTask

        #endregion

        #region Private methods

        private void ControlValidationFailed(string text, Control control)
        {
            MessageBox.Show(this, text, this.Name, MessageBoxButtons.OK, MessageBoxIcon.Warning);

            var tabPage = control.Parent as TabPage;
            if (tabPage != null)
            {
                tabProperties.SelectedTab = tabPage;
            } // if
            control.Focus();
        }  // ControlValidationFailed

        #endregion
    } // class DialogRecordChannel
} // namespace
