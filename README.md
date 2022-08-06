# ETLService
Console application ETL service.
Basic ETL service that allows you to process files with payment transactions that different users save in the input folder on disk (the path must be specified in the config). The file can be in either TXT or CSV format. The file must contain the following data: <first_name: string>, <last_name: string>, <address: string>, <payment: decimal>, <date: date>, <account_number: long>, <service: string>.

---
**How to use**

1) You must create a configuration file "config.json" which will contain the paths to 3 folders: input, output, done files.
```
{
  "inputfolder": "C:\\{your input folder}",
  "outputfolder": "C:\\{your output folder}",
  "donefolder": "C:\\{your done folder}"
}
```
2) Run the program.All files that were in the input folder will be processed. You can add new files that will also be processed immediately.
