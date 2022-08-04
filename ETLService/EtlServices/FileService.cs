using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using ETLService.Extractors.Readers.Сreators;

namespace ETLService.EtlServices
{
    public class FileService
    {
        private readonly string _outputPath;
        private readonly string _inputPath;
        private string _currentSubfolder;
        private int _currentFileNumber;
        public FileService(string outputPath, string inputPath)
        {
            _outputPath = outputPath;
            _inputPath = inputPath;
            _currentFileNumber = 0;
        }

        public string[] GetFilesFromInputFolder()
        {
            return Directory.GetFiles(_inputPath);
        }
        public void CreateSubfolder()
        {
            _currentSubfolder = Path.Combine(_outputPath, DateTime.Today.ToShortDateString());
            Directory.CreateDirectory(_currentSubfolder);
        }
        public string CreateFileInCurrSubfolder()
        {
            _currentFileNumber++;
            string filePath = Path.Combine(_currentSubfolder, $"output{_currentFileNumber}.json");
            using var newFile = File.Create(filePath);
            return filePath;
        }

        public FileReaderCreator GetFileReaderCreator(string fname)
        {
            string extension = Path.GetExtension(fname);
            FileReaderCreator readerCreator = null;
            if (extension == ".txt")
                readerCreator = new TxtReaderCreator();
            else if (extension == ".csv")
                readerCreator = new CsvReaderCreator();
            return readerCreator;
        }
    }
}
