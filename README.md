# notify-me
A .net core console app that will notify the receiver on execute

Setup
---
1) Run CMD to build EXE: [`dotnet build -r win10-x64`](https://docs.microsoft.com/en-us/dotnet/core/rid-catalog#using-rids "Microsoft Docs")

2) Copy contents of build folder to C drive named *NotifyMe* 
   
   - eg. *win10-x64 folder* in *\bin\Debug\netcoreapp2.1*

3) Create file in directory of built exe: *notifyme.cfg*

4) Inside of notifyme.cfg enter each item below on seperate lines:
   
   - `to=myemail@gmail.com` 
   - `from=myemail@gmail.com` 
   - `password=myemailpassword`
   - `subject=NotifyMe Has Started!` *optional*
   - `message=NotifyMe has started!` *optional*

5) *NotifyMe.exe* can be run to send notification based on info in *notifyme.cfg*
  
Notify on PC start
---
This will setup the app to as service

1) Make sure all steps in setup are complete

5) Download NSSM: [`nssm download`](http://nssm.cc/commands " Non-Sucking Service Manager")

   - Any service manager be used, nssm is just easy to use
   
6) Place nssm.exe in same directory as NotifyMe.exe (eg. *C:\NotifyMe*)

7) Open cmd at C:\NotifyMe and run `nssm install NotifyMe "C:\NotifyMe\NotifyMe.exe"`

   - Process can be stopped with command `nssm remove <servicename>`
 
Enable Non Google Apps 
---
If there is an *non secure error* on execute, try enabling non google apps:
[`Enable Non Google Apps`](https://myaccount.google.com/lesssecureapps "Google Account")
