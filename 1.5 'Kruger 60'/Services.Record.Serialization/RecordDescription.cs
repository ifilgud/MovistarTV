﻿// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace IpTviewr.Services.Record.Serialization
{
    [Serializable]
    [XmlType(Namespace = RecordTask.XmlNamespace)]
    public class RecordDescription
    {
        public static string CreateTaskName(RecordChannel channel, DateTime startDateTime)
        {
            return string.Format(Properties.Texts.RecordTaskNameSuggestedNameFormat, channel.Name, startDateTime, startDateTime);
        } // CreateTaskName

        /// <summary>
        /// The user-provided name for the record task.
        /// </summary>
        public string Name
        {
            get;
            set;
        } // Name

        /// <summary>
        /// The name of the task in Task Scheduler. Filled by Scheduler
        /// </summary>
        public string TaskSchedulerName
        {
            get;
            set;
        } // TaskSchedulerName

        /// <summary>
        /// User-provided description
        /// </summary>
        public string Description
        {
            get;
            set;
        } // Description

        /// <summary>
        /// The details of the task in TaskScheduler. Filled by Scheduler
        /// </summary>
        public string Details
        {
            get;
            set;
        } // Details

        /// <summary>
        /// The task name for Task Scheduler will have an standard prefix (IPTViewr) on it's name
        /// </summary>
        public bool AddPrefix
        {
            get;
            set;
        } // AddPrefix

        /// <summary>
        /// When filling the Details propery, Scheduler will verbalize the most important aspects of the task
        /// </summary>
        public bool AddDetails
        {
            get;
            set;
        } // AddDetails

        public static RecordDescription CreateWithDefaultValues()
        {
            return new RecordDescription()
            {
                Name = "",
                Description = "",
                AddPrefix = false,
                AddDetails = true,
            };
        } // CreateWithDefaultValues
    } // class RecordDescription
} // namespace
