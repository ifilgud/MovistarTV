﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpTviewr.Services.EpgDiscovery
{
    public enum EpgMemoryStorageMethod
    {
        Replace = 0,
        Merge
    } // EpgMemoryStorageMethod

    public sealed class EpgMemoryDatastore: EpgDatastore
    {
        private ConcurrentDictionary<string, EpgService> Data;

        public EpgMemoryDatastore()
        {
            Data = new ConcurrentDictionary<string, EpgService>();
        } // constructor

        public EpgMemoryStorageMethod StorageMethod
        {
            get;
            set;
        } // StorageMethod

        public override ICollection<string> GetServicesRefs()
        {
            return Data.Keys;
        } // GetServicesRefs

        public override IEpgLinkedList GetPrograms(string serviceIdRef, DateTime? localTime, int nodesBefore, int nodesAfter)
        {
            EpgService epgService;

            if (!Data.TryGetValue(serviceIdRef, out epgService)) return null;

            return GetLinkedList(epgService, localTime);
        } // GetPrograms

        public override IDictionary<string, IEpgLinkedList> GetAllPrograms(DateTime? localTime, int nodesBefore, int nodesAfter)
        {
            throw new NotImplementedException();
        } // GetAllPrograms

        protected override void AddEpgService(EpgService epgService)
        {
            Console.WriteLine("Store.Add: {0} with {1} programs", epgService.ServiceIdReference, epgService.Programs?.Count);

            switch (StorageMethod)
            {
                case EpgMemoryStorageMethod.Replace:
                    Data[epgService.ServiceIdReference] = epgService;
                    break;

                case EpgMemoryStorageMethod.Merge:
                    Data.AddOrUpdate(epgService.ServiceIdReference, epgService, (k,v) => Merge(v, epgService));
                    break;
            } // switch
        } // AddEpgService

        private EpgService Merge(EpgService currentEpgService, EpgService newEpgService)
        {
            // TODO
            throw new NotImplementedException();
        } // Merge


        private IEpgLinkedList GetLinkedList(EpgService service, DateTime? localTime)
        {
            LinkedListNode<EpgProgram> node;
            EpgProgram program;
            EpgProgram phantomProgram;

            if ((service.Programs == null) || (service.Programs.First == null)) return null;

            if (localTime == null) localTime = DateTime.Now;
            var utcTime = localTime.Value.ToUniversalTime();
            var truncatedUtcTime = Common.TruncateToMinutes(utcTime);

            program = service.Programs.First.Value;
            if (utcTime < program.UtcStartTime)
            {
                phantomProgram = new EpgProgram()
                {
                    Title = Properties.Texts.EpgBlankTitle,
                    IsBlank = true,
                    UtcStartTime = truncatedUtcTime,
                    Duration = program.UtcStartTime - truncatedUtcTime - new TimeSpan(0, 15, 0)
                };

                return new EpgLinkedListWrapper(service.ServiceIdReference, service.Programs, phantomProgram);
            } // if

            program = service.Programs.Last.Value;
            if (utcTime >= program.UtcEndTime)
            {
                phantomProgram = new EpgProgram()
                {
                    Title = Properties.Texts.EpgBlankTitle,
                    IsBlank = true,
                    UtcStartTime = program.UtcEndTime,
                    Duration = (truncatedUtcTime - program.UtcEndTime) + new TimeSpan(0, 15, 0)
                };

                return new EpgLinkedListWrapper(service.ServiceIdReference, service.Programs, phantomProgram, false);
            } // if

            node = service.Programs.First;
            while (program != null)
            {
                if (program.IsCurrent(utcTime))
                {
                    break;
                } // if
                node = node.Next;
            } // while

            return new EpgLinkedListWrapper(service.ServiceIdReference, service.Programs, node);
        } // GetLinkedList
    } // class EpgMemoryDatastore
} // namespace
