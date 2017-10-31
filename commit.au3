;ShellExecute(@ScriptDir & "\autocommit.bat")

RunWait(@ComSpec & " /c  " & "for /f ""tokens=2*"" %%i in ('svn status %1 ^| find ""?""') do svn add ""%%i"" ", @TempDir, @SW_HIDE)
RunWait(@ComSpec & ' /c  ' & "for /f ""tokens=2*"" %%i in ('svn status %1 ^| find ""!""') do svn delete ""%%i"" ", @TempDir, @SW_HIDE)
RunWait(@ComSpec & ' /c  ' & "svn commit -m ""Automatic commit"" %1 ", @TempDir, @SW_HIDE)