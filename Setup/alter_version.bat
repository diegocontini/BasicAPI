mantenha a versao atual caso digitado at

setlocal enabledelayedexpansion

echo.
echo Definindo versão da api

:: Caminho do arquivo .csproj
set "filePath=%~dp0..\BasicApi\BasicApi.csproj"

::Ler a linha que contém a versão
for /f "tokens=*" %%a in ('findstr "<Version>" "%filePath%"') do (
    set "versionLine=%%a"
)

:: Extrair a versão da linha
for /f "tokens=2 delims=<>" %%a in ("%versionLine%") do (
    set "currentVersion=%%a"
)

echo Versão atual: %currentVersion%

:: Separar a versão em partes
for /f "tokens=1,2,3,4 delims=." %%a in ("%currentVersion%") do (
    set "major=%%a"
    set "minor=%%b"
    set "build=%%c"
    set "revision=%%d"
)

:: Perguntar ao usuário o tipo de versão
set /p "versionType=Digite o tipo de versão (a)nual, (m)ensal, (r)elease, (b)uild, (at)ual:"

:: Modificar a versão com base na resposta do usuário
if /i "%versionType%"=="a" (
    set /a major+=1
    set minor=1
    set build=0
    set revision=0
) else if /i "%versionType%"=="b" (
    set /a revision+=1
) else if /i "%versionType%"=="m" (
	for /f %%i in ('powershell ^(get-date^).Month') do set month=%%i
	setlocal enabledelayedexpansion
	
    set minor=!month!
    set build=0
    set revision=0
) else if /i "%versionType%"=="r" (
    set /a build+=1
    set revision=0
) else if /i "%versionType%"=="at" (
    echo Mantendo versao atual
    goto :eof
)  else (
    echo Tipo de versão inválido.
    goto :eof
)

:: Nova versão
set "newVersion=%major%.%minor%.%build%.%revision%"
echo Definindo nova versão: %newVersion%

:: Defina os textos a serem substituídos e os novos textos
set "findstrAssemblyVersion=<AssemblyVersion>[0-9.]+</AssemblyVersion>"
set "newstrAssemblyVersion=<AssemblyVersion>%newVersion%</AssemblyVersion>"

set "findstrFileVersion=<FileVersion>[0-9.]+</FileVersion>"
set "newstrFileVersion=<FileVersion>%newVersion%</FileVersion>"

set "findstrVersion=<Version>[0-9.]+</Version>"
set "newstrVersion=<Version>%newVersion%</Version>"

:: Utilizar Powershell para substituir o texto no arquivo
powershell -Command "(Get-Content '%filePath%') -replace ('%findstrAssemblyVersion%'), ('%newstrAssemblyVersion%') -replace ('%findstrFileVersion%'), ('%newstrFileVersion%') -replace ('%findstrVersion%'), ('%newstrVersion%') | Set-Content '%filePath%'"

endlocal