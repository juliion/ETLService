using System;
using System.Collections.Generic;
using System.Text;
using ETLService.Metalog;

namespace ETLService.Extractors.Readers.Сreators
{
    public abstract class FileReaderCreator
    {
        public abstract FileReader CreateReader(string filename, ref MetaLog metaLog);
    }
}
