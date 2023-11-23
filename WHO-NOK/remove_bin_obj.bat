@echo off
for /d /r %%i in (bin obj) do (
    if exist "%%i" (
        rmdir /s /q "%%i"
    )
)