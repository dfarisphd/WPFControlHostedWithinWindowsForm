
Thank you all. We created SortByFolderFileAppender, which inherit from RollingFileAppender

Example of final result: somewhere\Logs\20100305\Client-104615.0

namespace CustomLogging
{
  public class SortByFolderFileAppender : log4net.Appender.RollingFileAppender
  {
    protected override void OpenFile(string fileName, bool append)
    {
      //Inject folder [yyyyMMdd] before the file name
      string baseDirectory = Path.GetDirectoryName(fileName);
      string fileNameOnly = Path.GetFileName(fileName);
      string newDirectory = Path.Combine(baseDirectory, DateTime.Now.ToString("yyyyMMdd"));
      string newFileName = Path.Combine(newDirectory, fileNameOnly);

      base.OpenFile(newFileName, append);
    }
  }
}

<appender name="SortByFolderFileAppender" type="CustomLogging.SortByFolderFileAppender">
  <file type="log4net.Util.PatternString" value="Logs\Client"/>
  <appendToFile value="true"/>
  <rollingStyle value="Composite"/>
  <datePattern value="-HHmmss"/>
  <maxSizeRollBackups value="40"/>
  <maximumFileSize value="1MB"/>
  <countDirection value="1"/>
  <encoding value="utf-8"/>
  <staticLogFileName value="false"/>
  <layout type="log4net.Layout.PatternLayout">
    <conversionPattern value="%date{HH:mm:ss.fff}|%-5level|%message%newline"/>
  </layout>
</appender>


I recently came across this need when attempting to clean up log logs based on a maxAgeInDays configuration value passed into my service... As many have before me, I became exposed to the NTFS 'feature' Tunneling, which makes using FileInfo.CreationDate problematic (though I have since worked around this as well)...

Since I had a pattern to go off of, I decided to just roll my own clean up method... My logger is configured programmatically, so I merely call the following after my logger setup has completed...

    //.........................
    //Log Config Stuff Above...

    log4net.Config.BasicConfigurator.Configure(fileAppender);
    if(logConfig.DaysToKeep > 0)
       CleanupLogs(logConfig.LogFilePath, logConfig.DaysToKeep);
}

static void CleanupLogs(string logPath, int maxAgeInDays)
{
    if (File.Exists(logPath))
    {
        var datePattern = "yyyy.MM.dd";
        List<string> logPatternsToKeep = new List<string>();
        for (var i = 0; i <= maxAgeInDays; i++)
        {
            logPatternsToKeep.Add(DateTime.Now.AddDays(-i).ToString(datePattern));
        }

        FileInfo fi = new FileInfo(logPath);

        var logFiles = fi.Directory.GetFiles(fi.Name + "*")
            .Where(x => logPatternsToKeep.All(y => !x.Name.Contains(y) && x.Name != fi.Name));

        foreach (var log in logFiles)
        {
            if (File.Exists(log.FullName)) File.Delete(log.FullName);
        }
    }
}


