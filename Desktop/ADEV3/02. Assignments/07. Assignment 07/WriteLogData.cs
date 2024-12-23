/// <summary>
/// Writes the contents of the log data to a file /// </summary>
/// <returns>the contents of the log data</returns> 
public String WriteLogData()
{
	StreamWriter writer = new StreamWriter (logFileName, true); 	writer.WriteLine(logData);
	writer.Close();

	string capturedLog = logData; 
	logData = string.Empty; 
	logFileName = string.Empty;

	return capturedLog;
}