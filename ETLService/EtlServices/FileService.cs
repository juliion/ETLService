using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Text;
using ETLService.Extractors.Readers.Сreators;
using ETLService.Metalog;
using System.Text.Json;

namespace ETLService.EtlServices
{
    public class FileService
    {
        private readonly string _outputPath;
        private readonly string _inputPath;
        private readonly string _donePath;
        private string _currentSubfolderOutput;
        private string _currentSubfolderDone;
        private int _currentFileNumber;
        public FileService(string outputPath, string inputPath, string donePath)
        {
            _outputPath = outputPath;
            _inputPath = inputPath;
            _donePath = donePath;
            _currentFileNumber = 0;
        }

        public List<string> GetFilesFromInputFolder()
        {
            var filesArr = Directory.GetFiles(_inputPath);
            var filesList = new List<string>(filesArr);
            return filesList.Where((fname => FileHasTxtExtension(fname) || FileHasCvsExtension(fname))).ToList();
        }
        public void CreateSubfolderForOutput()
        {
            _currentSubfolderOutput = Path.Combine(_outputPath, DateTime.Today.ToShortDateString());
            Directory.CreateDirectory(_currentSubfolderOutput);
        }
        public void CreateSubfolderForDone()
        {
            _currentSubfolderDone = Path.Combine(_donePath, DateTime.Today.ToShortDateString());
            Directory.CreateDirectory(_currentSubfolderDone);
        }
        public string CreateFileInCurrSubfolder()
        {
            _currentFileNumber++;
            string filePath = Path.Combine(_currentSubfolderOutput, $"output{_currentFileNumber}.json");
            using var newFile = File.Create(filePath);
            return filePath;
        }

        public void MoveDoneFile(string doneFile)
        {
            string currfilePath = Path.Combine(_inputPath, doneFile);
            string donefilePath = Path.Combine(_donePath, _currentSubfolderDone, $"done{_currentFileNumber}.txt");
            File.Move(currfilePath, donefilePath);
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
        public void SaveMetaLog(MetaLog metalog)
        {
            string jsonString = JsonSerializer.Serialize(metalog);
            string metalogFile = Path.Combine(_currentSubfolderOutput, $"metalog.json");
            File.WriteAllText(metalogFile, jsonString);
        }
    }
}
