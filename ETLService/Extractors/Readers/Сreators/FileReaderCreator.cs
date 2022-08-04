using System;
using System.Collections.Generic;
using System.Text;

namespace ETLService.Extractors.Readers.Сreators
{
    public abstract class FileReaderCreator
    {
        public abstract FileReader CreateReader(string filename);
    }
}
