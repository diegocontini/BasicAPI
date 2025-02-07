@Echo Off
 @For /F "tokens=1,2,3,4 delims=/ " %%A in ('Date /t') do @(
 Set DayW=%%A
 Set Day=%%B
 Set Month=%%C
 Set Year=%%D
 Set All=%%A_%%B_%%C_%%D
 )
 @For /F "tokens=1,2,3 delims=:,. " %%A in ('echo %time%') do @(
 Set Hour=%%A
 Set Min=%%B
 Set Sec=%%C
 Set Allm=%%A.%%B.%%C
 )
 @For /F "tokens=3 delims=: " %%A in ('time /t ') do @(
 Set AMPM=%%A
 )
set datestr=%All%%Allm%%AMPM%
echo datestr is %datestr%

set BACKUP_FILE=E:\BACKUP\WMSBACKUP_%datestr%.backup
echo backup file name is %BACKUP_FILE%
SET PGPASSWORD=suasenha
echo on
cd "\Program Files\PostgreSQL\10\bin\"
pg_dump.exe -h localhost -U postgres -p 5432 wms > %BACKUP_FILE% 

pause