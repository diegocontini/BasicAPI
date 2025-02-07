@echo off
echo Gerando publicação da API, aguarde...

:: Caminho do arquivo .csproj
set "filePath=%~dp0..\BasicApi\BasicApi.csproj"

:: Nome do arquivo temporário para redirecionar a saída do Inno Setup
set TEMP_LOG_FILE=%TEMP%\publish_log.txt
set PUBLISH_COMMAND=dotnet publish -f net8.0 -r win-x64 --self-contained true -o "C:\BasicApi" "%filePath%"

:: Executar a compilação e redirecionar a saída para o arquivo temporário
call %PUBLISH_COMMAND% > "%TEMP_LOG_FILE%" 2>&1

:: Verificar o código de retorno do comando anterior (%errorlevel%)
if %errorlevel% equ 0 (
    echo Publicação bem-sucedida!
    echo O projeto foi publicado com sucesso. Você pode encontrar os arquivos em C:\BasicApi.
) else (
    echo Erro durante a publicação.
    echo Detalhes do erro podem ser encontrados em: %TEMP_LOG_FILE%
	
	echo Pressione qualquer tecla para sair...
	pause >nul
	exit
	
)

