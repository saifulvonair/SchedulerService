# SchedulerService
A windows service that runs in background and transfer data from Background to UI, also send command from UI to server layer. Please check the details how to install the 
service from : https://www.c-sharpcorner.com/UploadFile/naresh.avari/develop-and-install-a-windows-service-in-C-Sharp/

# Precondition
The Service must be installed first to see the communication. Also if port 8085 is used then need to change it from (CManagerClientServer.getInstance().openServer(8085))

# LibClientServer
This library is used to setup cient server layer between UI and service element.

# SchedulerService
The service layer code that also creates the Server in ServiceTaskScheduler.cs class. Here is the code that actually creates the server:

```C#
if (CManagerClientServer.getInstance().openServer(8085))
mModelRemoteObject = CManagerClientServer.getInstance().createRemotingObj();
..
mModelRemoteObject.setHost(delegate (object obj) // Set the Server as Host
```

# ClientObject
The UI which connect the server, after successfull connection the Stop button will be Enabled. Then the message will be displyed in the textbox.
User can then send command to service from Button Stop to stop the timer thhat is running in Service.

# Install and Uninstall

Go to the install Exe folder:

C:\Windows\Microsoft.NET\Framework64\v4.0.30319

InstallUtil.exe "E:\SELECTED_FOR_GITHUB\SchedulerService\SchedulerService\SchedulerService.exe"

InstallUtil.exe /u "E:\SELECTED_FOR_GITHUB\SchedulerService\SchedulerService\SchedulerService.exe"
