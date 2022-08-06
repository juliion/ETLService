using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using ETLService.Extractors.Readers;
using ETLService.Extractors.Readers.Сreators;
using ETLService.Transformers;
using ETLService.Loaders.Writers;
using ETLService.Models;
using ETLService.Metalog;

namespace ETLService.EtlServices
{
    public class EtlService
    {
        private FileService _fileService;
        private MetaLog _metaLog;
        object locker = new object();
        public EtlService(FileService fileService)
        {
            _fileService = fileService;
            _metaLog = new MetaLog();
        }

        public void Run()
        {
            _fileService.CreateSubfolderForOutput();
            _fileService.CreateSubfolderForDone();
            List<string> inputFiles = _fileService.GetFilesFromInputFolder();
            inputFiles.AsParallel().ForAll((fname) => ProcessFile(_fileService.GetFileReaderCreator(fname), fname));
            _fileService.SaveMetaLog(_metaLog);
        }

        private void ProcessFile(FileReaderCreator readerCreator, string fname)
        {
            lock(locker)
            {
                FileReader reader = readerCreator.CreateReader(fname, ref _metaLog);
                TransactionTransformer transformer = new TransactionTransformer();
                FileWriter writer = new JSONWriter(_fileService.CreateFileInCurrSubfolder());
                List<Transaction> transactions = reader.Extract();
                List<TransactionsByCity> transformedTransactions = transformer.Transform(transactions);
                writer.Load(transformedTransactions);
                _fileService.MoveDoneFile(fname);
                _metaLog.ParsedFiles++;
            }
        }
    }
}
