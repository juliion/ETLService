using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using ETLService.Extractors.Readers;
using ETLService.Transformers;
using ETLService.Loaders.Writers;
using ETLService.Models;
using ETLService.Converters;

namespace ETLService.EtlServices
{
    public class EtlService
    {
        private FileService _fileService;
        object locker = new object();
        public EtlService(FileService fileService)
        {
            _fileService = fileService;
        }

        public void Run()
        {
            _fileService.CreateSubfolder();
            string[] inputFiles = _fileService.GetFilesFromInputFolder();
            inputFiles.AsParallel().ForAll((fname) => ProcessFile(fname));
        }

        private void ProcessFile(string filename)
        {
            lock(locker)
            {
                FileReader reader = new TxtReader(filename, new TransactionConverter());
                TransactionTransformer transformer = new TransactionTransformer();
                FileWriter writer = new JSONWriter(_fileService.CreateFileInCurrSubfolder());
                List<Transaction> transactions = reader.Extract();
                List<TransactionsByCity> transformedTransactions = transformer.Transform(transactions);
                writer.Load(transformedTransactions);
            }
        }
    }
}
