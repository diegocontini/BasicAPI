using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace BasicAPI.Features.Configs;

public class ConfigService
{

    string Set = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "set " : "export ";

    public ConfigService()
    {

    }

    public async Task PostgreSqlRestore(
               string inputFile,
               string host,
               string port,
               string database,
               string user,
               string password)
    {
        string dumpCommand = $"{Set}PGPASSWORD={password}\n" +
                             $"psql -h {host} -p {port} -U {user} -d {database} -c \"select pg_terminate_backend(pid) from pg_stat_activity where datname = '{database}'\"\n" +
                             $"dropdb -h " + host + " -p " + port + " -U " + user + $" {database}\n" +
                             $"createdb -h " + host + " -p " + port + " -U " + user + $" {database}\n" +
                             "pg_restore -h " + host + " -p " + port + " -d " + database + " -U " + user + "";

        //psql command disconnect database
        //dropdb and createdb  remove database and create.
        //pg_restore restore database with file create with pg_dump command
        dumpCommand = $"{dumpCommand} {inputFile}";

        await Execute(dumpCommand);
    }
    private Task Execute(string command)
    {
        return Task.Run(() =>
        {

            string batFilePath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}." + (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "bat" : "sh"));
            try
            {

                string batchContent = "";
                batchContent += $"{command}";

                File.WriteAllText(batFilePath, batchContent, Encoding.ASCII);

                ProcessStartInfo info = new ProcessStartInfo(batFilePath)
                {
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory,
                    RedirectStandardError = true
                };

                Process proc = Process.Start(info);
                proc.ErrorDataReceived += (object sender, DataReceivedEventArgs e) =>
       Console.WriteLine("error>>" + e.Data);
                proc.BeginErrorReadLine();



                proc.WaitForExit();
                var exit = proc.ExitCode;

                proc.Close();
            }
            catch (Exception e)
            {
                
                Console.WriteLine(e.Message);
                throw;

            }
            finally
            {
                if (File.Exists(batFilePath)) File.Delete(batFilePath);
            }
        });
    }
    
    public async Task PostgreSqlDump(
            string outFile,
            string host,
            string port,
            string database,
            string user,
            string password)
    {
        string command =
             $"{Set}PGPASSWORD={password}\n" +
             $"pg_dump" + " -Fc" + " -h " + host + " -p " + port + " -d " + database + " -U " + user + "";

        string batchContent = "" + command + "  > " + "\"" + outFile + "\"" + "\n";
        if (File.Exists(outFile)) File.Delete(outFile);

        await Execute(batchContent);
    }
}
