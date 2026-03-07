@echo off
setlocal enabledelayedexpansion

:: 1. Get the script's directory and the terminal's current directory
set "SOURCE_DIR=%~dp0"
set "TARGET_DIR=%CD%"

:: Remove the trailing backslash from SOURCE_DIR for an accurate comparison
set "COMPARE_SOURCE=%SOURCE_DIR:~0,-1%"

:: 2. Check if we are already in the same directory to prevent errors
if /I "%COMPARE_SOURCE%"=="%TARGET_DIR%" (
    echo The script's directory and the terminal's current directory are the same.
    echo Nothing to copy^^!
    exit /b
)


:: 3. Iterate through everything in the script's directory
set count=0
for %%F in ("%SOURCE_DIR%*") do (
    
    :: Skip the script itself so it doesn't copy itself over
    if /I not "%%~nxF"=="%~nx0" (
        
        :: Copy the file and hide the default output, printing our own instead
        copy "%%F" "%TARGET_DIR%\" >nul
        if !errorlevel! equ 0 (
            set /a count+=1
        )
    )
)

endlocal