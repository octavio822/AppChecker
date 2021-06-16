# AppChecker
<br/>Verifica que los procesos se esten ejecutando de manera correcta.
<br/>Version de Framework:4.5 (se puede cambiar).
<br/>Se debe instalar con powershell usando el comando new-service -Description <ServiceName> -Binarypathname <service path> 
<br/>el siguiente comando crea el eventlog :
<pre/>PS C:\> new-eventlog

cmdlet New-EventLog at command pipeline position 1
Supply values for the following parameters:
LogName: App Checker
Source[0]: <ROUTE>\AppChecker.exe
Source[1]: 
</pre>

<br/>Verify Process working in machine.
<br/>Framework 4.5 (Can change)
<br/>Must be installed with powershell using  new-service -Description <ServiceName> -Binarypathname <service path> 
<br/>The next command create the Log
<pre/>PS C:\> new-eventlog

cmdlet New-EventLog at command pipeline position 1
Supply values for the following parameters:
LogName: App Checker
Source[0]: <ROUTE>\AppChecker.exe
Source[1]: 
</pre>
