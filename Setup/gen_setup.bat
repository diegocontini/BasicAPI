echo.
echo Gerando instalador, aguarde...

:: Defina o caminho para o executável do compilador Inno Setup (iscc.exe)
set "INNO_COMPILER=C:\Program Files (x86)\Inno Setup 6\ISCC.exe"

:: Defina o caminho completo para o arquivo de script Inno Setup (.iss)
set INNO_SCRIPT=setup_omniapi.iss

:: Nome do arquivo temporário para redirecionar a saída do Inno Setup
set TEMP_LOG_FILE=%TEMP%\inno_setup_log.txt

:: Executar a compilação e redirecionar a saída para o arquivo temporário
"%INNO_COMPILER%" "%INNO_SCRIPT%" > "%TEMP_LOG_FILE%" 2>&1

:: Verificar o código de retorno do comando anterior (%errorlevel%)
if %errorlevel% equ 0 (
    echo Compilação bem-sucedida.
) else (
    echo Erro durante a compilação.
    echo Detalhes do erro podem ser encontrados em: %TEMP_LOG_FILE%
)

:: Opcional: Excluir o arquivo de log temporário após verificar o erro
:: del "%TEMP_LOG_FILE%" /q
