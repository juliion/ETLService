using System;
using System.Linq;
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

        public List<string> GetFilesFromInputFolder()
        {
            var filesArr = Directory.GetFiles(_inputPath);
            var filesList = new List<string>(filesArr);
            return filesList.Where((fname => FileHasTxtExtension(fname) || FileHasCvsExtension(fname))).ToList();
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

        private bool FileHasTxtExtension(string filename) => Path.GetExtension(filename) == ".txt";
        private bool FileHasCvsExtension(string filename) => Path.GetExtension(filename) == ".csv";
        public FileReaderCreator GetFileReaderCreator(string filename)
        {
            FileReaderCreator readerCreator = null;
            if (FileHasTxtExtension(filename))
                readerCreator = new TxtReaderCreator();
            else if (FileHasCvsExtension(filename))
                readerCreator = new CsvReaderCreator();
            return readerCreator;
        }
    }
}
